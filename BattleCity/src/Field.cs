namespace Game;

public class Field
{
    public event EventHandler OnEntityCreated;
    public event EventHandler OnEntityDeleted;
    public int FieldSizeX { get; }
    public int FieldSizeY { get; }
    public BaseEntity[,] Map { get; set; }
    public Player Player { get; set; }
    public List<Tank> Tanks { get; }
    public List<Bullet> Bullets { get; }
    public string Status { get; set; }

    public Field()
    {
        Tanks = new List<Tank>();
        Bullets = new List<Bullet>();
        FieldSizeX = 35;
        FieldSizeY = 16;
        Map = new BaseEntity[FieldSizeX, FieldSizeY];
    }
    public void Start()
    {
        string filePath = "../../../res/Level1.lvl";
        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

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
                        // Check if the character is P, W, or T
                        case 'P':
                            Player = new Player(this, col, row);
                            break;
                        case 'W':
                            new Wall(this, col, row);
                            break;
                        case 'T':
                            Tanks.Add(new Tank(this, col, row));
                            break;
                    }
                }
                row++;
            }
        }
        Status = "Playing";
    }

    public void ProcessBullets()
    {
        foreach (Bullet bullet in Bullets)
        {
            bullet.ProcessTurn();
        }

        foreach (Bullet bullet in BulletsToDelete)
        {
            Bullets.Remove(bullet);
        }
        BulletsToDelete.Clear();
    }

    public void ProcessTanks()
    {
        foreach (Tank tank in Tanks)
        {
            tank.ProcessTurn();
        }

        if (Tanks.Count == 0)
        {
            Status = "Tanks are defeated!";
        }
    }
    
    public void SubscribeToTank(Tank tank)
    {
        tank.OnCreated += Tank_OnCreated;
        tank.OnMoved += Tank_OnMoved;
        tank.OnDied += Tank_OnDied;
    }
    public void SubscribeToBullet(Bullet bullet)
    {
        bullet.OnCreated += Bullet_OnCreated;
        bullet.OnMoved += Bullet_OnMoved;
        bullet.OnDied += Bullet_OnDied;
    }
    public void SubscribeToWall(Wall wall)
    {
        wall.OnCreated += Wall_OnCreated;
        wall.OnDied += Wall_OnDied;
    }

    public void SubscribeToPlayer(Player player)
    {
        player.OnCreated += Player_OnCreated;
        player.OnMoved += Player_OnMoved;
        player.OnDied += Player_OnDied;
    }

    private void Player_OnCreated(object sender, EventArgs e)
    {
        if(sender is Player entity)
        {
            MapCreateEntity(entity);
        } else throw new ArgumentException();
    }
    private void Player_OnMoved(object sender, EventArgs e)
    {
        if(sender is Player entity)
        {
            MapMoveEntity(entity);
        } else throw new ArgumentException();
    }
    private void Player_OnDied(object sender, EventArgs e)
    {
        if(sender is Player entity)
        {
            MapDeleteEntity(entity);
            Status = "Player Died :(";
        } else throw new ArgumentException();
    }
    private void Tank_OnCreated(object sender, EventArgs e)
    {
        if(sender is Tank entity)
        {
            MapCreateEntity(entity);
        } else throw new ArgumentException();
    }
    private void Tank_OnMoved(object sender, EventArgs e)
    {
        if(sender is Tank entity) {
            MapMoveEntity(entity);
        } else throw new ArgumentException();
    }
    private void Tank_OnDied(object sender, EventArgs e)
    {
        if(sender is Tank entity) {
            MapDeleteEntity(entity);
            Tanks.Remove(entity);
        } else throw new ArgumentException();
    }
    private void Bullet_OnCreated(object sender, EventArgs e)
    {
        if(sender is Bullet entity) {
            MapCreateEntity(entity);
            Bullets.Add(entity);
        } else throw new ArgumentException();
    }
    private void Bullet_OnMoved(object sender, EventArgs e)
    {
        if(sender is Bullet entity)
        {
            MapMoveEntity(entity);
        } else throw new ArgumentException();
    }
    private void Bullet_OnDied(object sender, EventArgs e)
    {
        if(sender is Bullet entity) {
            MapDeleteEntity(entity);
            BulletsToDelete.Add(entity);
        } else throw new ArgumentException();
    }
    private void Wall_OnCreated(object sender, EventArgs e)
    {
        if(sender is Wall entity) {
            MapCreateEntity(entity);
        } else throw new ArgumentException();
    }
    private void Wall_OnDied(object sender, EventArgs e)
    {
        if(sender is Wall entity)
        {
            MapDeleteEntity(entity);
        } else throw new ArgumentException();
    }

    private void MapCreateEntity(BaseEntity entity)
    {
        Map[entity.X, entity.Y] = entity;
        OnEntityCreated?.Invoke(this, new VisualEntityEventArgs(entity));
    }
    private void MapDeleteEntity(BaseEntity entity)
    {
        Map[entity.X, entity.Y] = null;
        OnEntityDeleted?.Invoke(this, new VisualEntityEventArgs(entity));
    }

    private void MapMoveEntity(BaseEntity entity)
    {
        Map[entity.X, entity.Y] = entity;
        OnEntityCreated?.Invoke(this, new VisualEntityEventArgs(entity));
        int x = entity.X;
        int y = entity.Y;

        switch (entity.Direction)
        {
            case Direction.Down:
                y--;
                break;
            case Direction.Up:
                y++;
                break;
            case Direction.Left:
                x++;
                break;
            case Direction.Right:
                x--;
                break;  
        }
        Map[x, y] = null;
        OnEntityDeleted?.Invoke(this, new VisualEntityEventArgs(entity.GetSprite(), entity.GetSpriteColor(), x, y));
    }
    
    private List<Bullet> BulletsToDelete { get; } = [];
}