namespace Game;

public class Field
{
    public event EventHandler OnEntityCreated;
    public event EventHandler OnEntityMoved;
    public event EventHandler OnEntityDied;
    public int FieldSizeX { get; }
    public int FieldSizeY { get; }
    public BaseEntity[,] Map { get; set; }
    public Player Player { get; }
    public List<Tank> Tanks { get; }
    public List<Bullet> Bullets { get; }
    public Field()
    {
        Player = new Player { X = 0, Y = 0, Field = this };
        Tanks = new List<Tank>();
        Bullets = new List<Bullet>();
        FieldSizeX = Console.WindowWidth;
        FieldSizeY = Console.WindowHeight;
        Map = new BaseEntity[FieldSizeX, FieldSizeY];
        Map[0, 0] = Player;
        Map[1, 0] = new Wall() { X = 1, Y = 0, Field = this };
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

    private void Tank_OnCreated(object sender, EventArgs e)
    {
        if(sender is Tank entity)
        {
            MapCreateEntity(entity);
            Tanks.Add(entity);
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
            bulletsToDelete.Add(entity);
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
    }
    private void MapDeleteEntity(BaseEntity entity)
    {
        Map[entity.X, entity.Y] = null;
    }

    private void MapMoveEntity(BaseEntity entity)
    {
        Map[entity.X, entity.Y] = entity;
        switch (entity.Direction)
        {
            case Direction.Down:
                Map[entity.X, entity.Y-1] = null;
                break;
            case Direction.Up:
                Map[entity.X, entity.Y+1] = null;
                break;
            case Direction.Left:
                Map[entity.X+1, entity.Y] = null;
                break;
            case Direction.Right:
                Map[entity.Y-1, entity.X] = null;
                break;  
        }
    }

    private List<Bullet> bulletsToDelete { get; } = [];
}