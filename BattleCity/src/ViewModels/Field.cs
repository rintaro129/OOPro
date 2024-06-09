using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

namespace BattleCity;

public class Field
{
    public Field(BaseIO IO)
    {
        this.IO = IO;
        IO.ConnectIO(this);
    }

    public event EventHandler? EntityCreated;
    public event EventHandler? EntityDeleted;
    public event EventHandler? EntityMoved;
    public event EventHandler? LevelStarting;
    public event EventHandler? LevelStarted;
    public event EventHandler? SizeSet;
    public string Name { get; set; }
    public BaseIO IO { get; }
    public int FieldSizeX { get; set; }
    public int FieldSizeY { get; set; }
    public BaseEntity?[,] Map { get; set; }
    public Player Player { get; set; }
    public List<BaseEntity> MovableEntities { get; } = [];
    public string Status { get; set; }
    public int EntitiesToSpawnCount { get; set; }
    public int FreezeLeftForTicks { get; set; }
    public Tank FreezeExceptionTank { get; set; }
    private const int SpawnTicks = 400;
    private List<BaseEntity> entitiesToDelete { get; } = [];
    private List<BaseEntity> entitiesToAdd { get; } = [];
    private const int minWidth = 20;
    private const int minHeight = 10;
    private const int maxWidth = 49;
    private const int maxHeight = 30;

    private (int, int) getLevelSize(string filePath)
    {
        int maxRowLength = 0;
        int maxColumnLength = 0;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Calculate row length
                int rowLength = line.Length;
                if (rowLength > maxRowLength)
                {
                    maxRowLength = rowLength;
                }

                maxColumnLength++;
            }
        }

        return (maxRowLength, maxColumnLength - 1); // the spawn specifier in the end
    }

    private void populateMap(string filePath, int score)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            int row = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                for (int col = 0; col < line.Length; col++)
                {
                    char c = line[col];
                    if (c == 'n')
                    {
                        string subString = line.Substring(col + 1, line.Length - col - 1);
                        if (int.TryParse(subString, out int result))
                        {
                            EntitiesToSpawnCount = result;
                            continue;
                        }
                        Console.WriteLine("Unable to parse substring to int.");
                        throw new Exception();
                    }
                    ParseCharacter(c, col, row, score);
                }
                row++;
            }
        }

        MovableEntities.AddRange(entitiesToAdd);
        entitiesToAdd.Clear();
    }
    private void ParseCharacter(char c, int col, int row, int score)
    {
        switch (c)
        {
            // Check if the character is P, W, or 1
            case 'P':
                Player = new Player(this, col, row, score);
                Map[col, row] = Player;
                break;
            case 'W':
                Map[col, row] = new BrickWall(this, col, row);
                break;
            case '1':
                Map[col, row] = new EnemyLvl1(this, col, row);
                break;
            case '2':
                Map[col, row] = new EnemyLvl2(this, col, row);
                break;
            case '3':
                Map[col, row] = new EnemyLvl3(this, col, row);
                break;
            case 'S':
                Map[col, row] = new SteelWall(this, col, row);
                break;
            case 'B':
                Map[col, row] = new Bomb(this, col, row);
                break;
            case 's':
                Map[col, row] = new PrizeSpeed(this, col, row);
                break;
            case 'h':
                Map[col, row] = new PrizeHealth(this, col, row);
                break;
            case 'f':
                Map[col, row] = new PrizeFreeze(this, col, row);
                break;
        }
        return;
    }

    public void Start(string filepath, int score = 0)
    {
        LevelStarting?.Invoke(this, EventArgs.Empty);
        Name = Path.GetFileNameWithoutExtension(filepath);
        string filePath = filepath;
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        (FieldSizeX, FieldSizeY) = getLevelSize(filePath);
        Map = new BaseEntity[FieldSizeX, FieldSizeY];
        MovableEntities.Clear();
        SizeSet?.Invoke(this, EventArgs.Empty);
        populateMap(filePath, score);

        Status = "Playing";
        LevelStarted?.Invoke(this, EventArgs.Empty);
    }

    public void StartRandom()
    {
        LevelStarting?.Invoke(this, EventArgs.Empty);
        Name = "Random Mode";
        Random random = new Random();
        FieldSizeY = minHeight + random.Next(maxHeight - minHeight);
        FieldSizeX = minWidth + random.Next(maxWidth - minWidth);
        SizeSet?.Invoke(this, EventArgs.Empty);
        Map = new BaseEntity[FieldSizeX, FieldSizeY];
        MovableEntities.Clear();
        char[,] level = CreateBeatableLevel();
        for (int i = 0; i < level.GetLength(0); i++)
        {
            for (int j = 0; j < level.GetLength(1); j++)
            {
                ParseCharacter(level[i, j], i, j, 0);
            }
        }
        EntitiesToSpawnCount = random.Next(6);
        MovableEntities.AddRange(entitiesToAdd);
        entitiesToAdd.Clear();
        Status = "Playing";
        LevelStarted?.Invoke(this, EventArgs.Empty);
    }
    private char[,] CreateBeatableLevel()
    {
        Random random = new Random();
        bool beatable = false;
        char[,] level = new char[FieldSizeX, FieldSizeY];
        int z = 0;
        while (!beatable)
        {
            z++;
            Name = $"Random. Try #{z}";
            level = new char[FieldSizeX, FieldSizeY];
            char[] entities = ['W', '1', '2', '3', 'S', 'B', 's', 'h', 'f', ' '];
            int[] odds = [100, 2, 2, 2, 100, 10, 1, 1, 1, 125];
            int sum = odds.Sum();
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    int randomNumber = random.Next(sum);
                    for(int k = 0; k < entities.Length; k++)
                    {
                        if(randomNumber < odds[k])
                        {
                            level[i,j] = entities[k];
                            break;
                        }
                        randomNumber -= odds[k];
                    }
                }
            }
            int x = random.Next(FieldSizeX);
            int y = random.Next(FieldSizeY);
            level[x, y] = 'P';
            beatable = IsBeatable(level, x, y);
        }
        return level;
    }
    private bool IsBeatable(char[,] level, int x, int y)
    {
        bool res = true;
        bool[,] used = new bool[FieldSizeX, FieldSizeY];
        used[x, y] = true;
        void dfs(int x, int y)
        {
            int[][] Directions = [[1, 0], [-1, 0], [0, 1], [0, -1]];
            foreach (int[] direction in Directions)
            {
                int ux = x + direction[0];
                int uy = y + direction[1];
                if (!(ux >= 0 && ux < FieldSizeX && uy >= 0 && uy < FieldSizeY)
                    || level[ux, uy] == 'S'
                    || used[ux, uy]) continue;
                used[ux, uy] = true;
                dfs(ux, uy);
            }
        }
        dfs(x, y);
        for (int i = 0; i < level.GetLength(0); i++)
        {
            for (int j = 0; j < level.GetLength(1); j++)
            {
                if (!used[i, j] && level[i, j] != 'S')
                    res = false;
            }
        }
        return res;
    }

    public void Play(int speedMs)
    {
        int i = 0;
        while (Status == "Playing")
        {
            ProcessEntities(i);
            i++;
            FreezeLeftForTicks = int.Max(0, FreezeLeftForTicks - 1);
            Thread.Sleep(speedMs);
        }
    }
    private void IterateMovableEntities(int tick)
    {
        foreach (BaseEntity entity in MovableEntities)
        {
            if (FreezeLeftForTicks > 0 && entity is Tank tank && tank != FreezeExceptionTank)
                continue;
            if (tick % entity.SpeedTicks == 0)
                entity.ProcessTurn();
        }
    }
    private void CheckForSpawn(int tick)
    {
        if (tick % SpawnTicks != 0 || EntitiesToSpawnCount <= 0)
            return;

        Tuple<int, int> freeTile = GetFreeTile();

        if (freeTile == Tuple.Create(-1, -1))
            return; // When no free tiles

        Map[freeTile.Item1, freeTile.Item2] = new Spawn(this, freeTile.Item1, freeTile.Item2);
    }
    private Tuple<int, int> GetFreeTile()
    {
        List<Tuple<int, int>> freeTiles = [];
        for (int i = 0; i < FieldSizeX; i++)
        {
            for (int j = 0; j < FieldSizeY; j++)
            {
                if (Map[i, j] == null)
                    freeTiles.Add(new Tuple<int, int>(i, j));
            }
        }

        if (freeTiles.Count == 0)
            return Tuple.Create(-1, -1); // When no free tiles

        Random random = new Random();
        int randomNumber = random.Next(freeTiles.Count);
        return freeTiles[randomNumber];
    }
    private void DeleteAccumulatedMovableEntities()
    {
        foreach (BaseEntity entity in entitiesToDelete)
        {
            MovableEntities.Remove(entity);
        }
        entitiesToDelete.Clear();
    }
    private void AddAccumulatedMovableEntities()
    {
        MovableEntities.AddRange(entitiesToAdd);
        entitiesToAdd.Clear();
    }
    private void CheckEnemiesDefeated()
    {
        bool enemiesAreDefeated = true;
        foreach (BaseEntity entity in MovableEntities)
        {
            if (entity is not Tank || entity is BattleCity.Player)
            {
                continue;
            }
            enemiesAreDefeated = false;
            break;
        }

        if (enemiesAreDefeated && EntitiesToSpawnCount == 0)
            Status = "Enemies are defeated!";
    }
    public void ProcessEntities(int tick)
    {
        IterateMovableEntities(tick);

        CheckForSpawn(tick);

        DeleteAccumulatedMovableEntities();

        AddAccumulatedMovableEntities();

        CheckEnemiesDefeated();
    }

    public void SubscribeToEntity(BaseEntity entity)
    {
        entity.Created += HandleEntityCreated;
        entity.Moved += HandleEntityMoved;
        entity.Updated += HandleEntityUpdated;
        entity.Died += HandleEntityDied;
        if (entity is Player player)
            IO.SubscribeToPlayer(player);
        if (entity is Spawn spawn)
            IO.SubscribeToSpawn(spawn);
    }

    private void HandleEntityCreated(object? sender, EventArgs e)
    {
        if (sender is not BaseEntity entity)
            throw new ArgumentException();
        Map[entity.X, entity.Y] = entity;
        if (entity.CanProcessTurn())
            entitiesToAdd.Add(entity);
        EntityCreated?.Invoke(this, new IOEventArgs(entity));
    }

    private void HandleEntityDied(object? sender, EventArgs e)
    {
        if (sender is not BaseEntity entity)
            throw new ArgumentException();
        Map[entity.X, entity.Y] = null;
        if (entity.CanProcessTurn())
            entitiesToDelete.Add(entity);
        if (entity is Player)
            Status = "Player Died :(";
        EntityDeleted?.Invoke(this, new IOEventArgs(entity));
        if ((entity is Tank or Obstacle) && (entity is not Prize))
            Map[entity.X, entity.Y] = new Explosion(this, entity.X, entity.Y);
        if (entity is Spawn)
            EntitiesToSpawnCount = Int32.Max(0, EntitiesToSpawnCount - 1);
    }

    private void HandleEntityMoved(object? sender, EventArgs e)
    {
        if (sender is not BaseEntity entity)
            throw new ArgumentException();
        Map[entity.X, entity.Y] = entity;
        int xInvertDifference, yInvertDifference;
        (xInvertDifference, yInvertDifference) = DirectionUtils.ToInts(DirectionUtils.Invert(entity.Direction));
        int x = entity.X + xInvertDifference;
        int y = entity.Y + yInvertDifference;
        Map[x, y] = null;
        if (IO is WinFormsIO winFormsIO)
        {
            EntityMoved?.Invoke(this, new IOEventArgs(entity));
            return;
        }
        EntityCreated?.Invoke(this, new IOEventArgs(entity));
        EntityDeleted?.Invoke(this, new IOEventArgs(x, y));
    }

    private void HandleEntityUpdated(object? sender, EventArgs e)
    {
        if (sender is not BaseEntity entity)
            throw new ArgumentException();
        EntityCreated?.Invoke(this, new IOEventArgs(entity));
    }
}