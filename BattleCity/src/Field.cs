namespace BattleCity;

public class Field
{
    public event EventHandler? EntityCreated;
    public event EventHandler? EntityDeleted;
    public event EventHandler? LevelStarting;
    public int FieldSizeX { get; set; }
    public int FieldSizeY { get; set; }
    public BaseEntity?[,] Map { get; set; }
    public Player Player { get; set; }
    public List<BaseEntity> Entities { get; } = [];
    public string Status { get; set; }
    private List<BaseEntity> EntitiesToDelete { get; } = [];
    private List<BaseEntity> EntitiesToAdd { get; } = [];

    public void Start(string option)
    {
        LevelStarting?.Invoke(this, EventArgs.Empty);
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
                                Player = new Player(this, col, row);
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
                        }
                    }

                    row++;
                }
            }

            Entities.AddRange(EntitiesToAdd);
            EntitiesToAdd.Clear();
            Status = "Playing";
        }
    }

    public void ProcessEntities(int tick)
    {
        foreach (BaseEntity entity in Entities)
        {
            if (tick % entity.SpeedTicks == 0) entity.ProcessTurn();
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

        if (enemiesAreDefeated) Status = "Enemies are defeated!";
    }

    public void SubscribeToEntity(BaseEntity entity)
    {
        entity.Created += HandleEntityCreated;
        entity.Moved += HandleEntityMoved;
        entity.Died += HandleEntityDied;
    }

    private void HandleEntityCreated(object? sender, EventArgs e)
    {
        if (sender is BaseEntity entity)
        {
            Map[entity.X, entity.Y] = entity;
            if (entity.CanMove()) EntitiesToAdd.Add(entity);
            EntityCreated?.Invoke(this, new VisualEntityEventArgs(entity));
        }
        else throw new AggregateException();
    }

    private void HandleEntityDied(object? sender, EventArgs e)
    {
        if (sender is BaseEntity entity)
        {
            Map[entity.X, entity.Y] = null;
            if (entity.CanMove()) EntitiesToDelete.Add(entity);
            if (entity is Player) Status = "Player Died :(";
            EntityDeleted?.Invoke(this, new VisualEntityEventArgs(entity));
        }
        else throw new AggregateException();
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
        else throw new AggregateException();
    }
}