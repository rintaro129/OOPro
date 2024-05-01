using System.Text.RegularExpressions;

namespace BattleCity;

public class Field
{
    public Field()
    {
        ConsoleIo.ConnectVisuals(this);
    }

    public event EventHandler? EntityCreated;
    public event EventHandler? EntityDeleted;
    public event EventHandler? LevelStarting;
    public event EventHandler? LevelStarted;
    public string Name { get; set; }
    public int FieldSizeX { get; set; }
    public int FieldSizeY { get; set; }
    public BaseEntity?[,] Map { get; set; }
    public Player Player { get; set; }
    public List<BaseEntity> Entities { get; } = [];
    public string Status { get; set; }
    public int EntitiesToSpawnCount { get; set; }
    public int FreezeLeftForTicks { get; set; }
    public Tank FreezeExceptionTank { get; set; }
    private const int SpawnTicks = 400;
    private List<BaseEntity> EntitiesToDelete { get; } = [];
    private List<BaseEntity> EntitiesToAdd { get; } = [];

    public void Start(string option, int score = 0)
    {
        LevelStarting?.Invoke(this, EventArgs.Empty);
        Name = Path.GetFileNameWithoutExtension(option);
        if (option == "RANDOM")
        {
            throw new NotImplementedException();
        }
        else
        {
            string filePath = option;
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            FieldSizeX = 35;
            FieldSizeY = 16;
            Map = new BaseEntity[FieldSizeX, FieldSizeY];
            Entities.Clear();
            // Read the contents of the file character by character
            using (StreamReader reader = new StreamReader(filePath))
            {
                int row = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int col = 0; col < line.Length; col++)
                    {
                        char c = line[col];
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
                            case 'n':
                                string subString = line.Substring(col + 1, line.Length - col - 1);
                                if (int.TryParse(subString, out int result))
                                {
                                    EntitiesToSpawnCount = result;
                                    break;
                                }

                                Console.WriteLine("Unable to parse substring to int.");
                                throw new Exception();
                        }
                    }

                    row++;
                }
            }

            Entities.AddRange(EntitiesToAdd);
            EntitiesToAdd.Clear();
            Status = "Playing";
            LevelStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Play(int speedMs)
    {
        int i = 0;
        while (Status == "Playing")
        {
            ProcessEntities(i);
            i++;
            FreezeLeftForTicks= int.Max(0, FreezeLeftForTicks-1);
            Thread.Sleep(speedMs);
        }
    }

    public void ProcessEntities(int tick)
    {
        foreach (BaseEntity entity in Entities)
        {
            if (FreezeLeftForTicks > 0 && entity is Tank tank && tank != FreezeExceptionTank) continue;
            if (tick % entity.SpeedTicks == 0) entity.ProcessTurn();
        }

        if (tick % SpawnTicks == 0)
        {
            List<Tuple<int, int>> freeTiles = [];
            for (int i = 0; i < FieldSizeX; i++)
            {
                for (int j = 0; j < FieldSizeY; j++)
                {
                    if (Map[i, j] == null) freeTiles.Add(new Tuple<int, int>(i, j));
                }
            }

            if (freeTiles.Count != 0)
            {
                Random random = new Random();
                int randomNumber = random.Next(freeTiles.Count);
                EntitiesToSpawnCount--;
                Map[freeTiles[randomNumber].Item1, freeTiles[randomNumber].Item2] = new Spawn(this,
                    freeTiles[randomNumber].Item1, freeTiles[randomNumber].Item2);
            }
        }

        foreach (BaseEntity entity in EntitiesToDelete)
        {
            Entities.Remove(entity);
        }

        Entities.AddRange(EntitiesToAdd);
        EntitiesToAdd.Clear();
        EntitiesToDelete.Clear();
        bool enemiesAreDefeated = true;
        foreach (BaseEntity entity in Entities)
        {
            if (entity is Tank && entity is not BattleCity.Player)
            {
                enemiesAreDefeated = false;
                break;
            }
        }

        if (enemiesAreDefeated && EntitiesToSpawnCount == 0) Status = "Enemies are defeated!";
    }

    public void SubscribeToEntity(BaseEntity entity)
    {
        entity.Created += HandleEntityCreated;
        entity.Moved += HandleEntityMoved;
        entity.Updated += HandleEntityUpdated;
        entity.Died += HandleEntityDied;
        if(entity is Player player) ConsoleIo.SubscribeToPlayer(player);
        if(entity is Spawn spawn) ConsoleIo.SubscribeToSpawn(spawn);
    }

    private void HandleEntityCreated(object? sender, EventArgs e)
    {
        if (sender is BaseEntity entity)
        {
            Map[entity.X, entity.Y] = entity;
            if (entity.CanProcessTurn()) EntitiesToAdd.Add(entity);
            EntityCreated?.Invoke(this, new VisualEntityEventArgs(entity));
        }
        else throw new ArgumentException();
    }

    private void HandleEntityDied(object? sender, EventArgs e)
    {
        if (sender is BaseEntity entity)
        {
            Map[entity.X, entity.Y] = null;
            if (entity.CanProcessTurn()) EntitiesToDelete.Add(entity);
            if (entity is Player) Status = "Player Died :(";
            EntityDeleted?.Invoke(this, new VisualEntityEventArgs(entity));
            if (entity is Tank or Obstacle) Map[entity.X, entity.Y] = new Explosion(this, entity.X, entity.Y);
        }
        else throw new ArgumentException();
    }

    private void HandleEntityMoved(object? sender, EventArgs e)
    {
        if (sender is BaseEntity entity)
        {
            Map[entity.X, entity.Y] = entity;
            EntityCreated?.Invoke(this, new VisualEntityEventArgs(entity));
            int xInvertDifference, yInvertDifference;
            (xInvertDifference, yInvertDifference) = DirectionUtils.ToInts(DirectionUtils.Invert(entity.Direction));
            int x = entity.X + xInvertDifference;
            int y = entity.Y + yInvertDifference;
            Map[x, y] = null;
            EntityDeleted?.Invoke(this, new VisualEntityEventArgs(x, y));
        }
        else throw new ArgumentException();
    }

    private void HandleEntityUpdated(object? sender, EventArgs e)
    {
        if (sender is BaseEntity entity)
        {
            EntityCreated?.Invoke(this, new VisualEntityEventArgs(entity));
        }
        else throw new ArgumentException();
    }
}