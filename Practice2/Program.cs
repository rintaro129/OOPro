
class Field(int SizeX, int SizeY)
{
    public int SizeX { get; set; } = SizeX;
    public int SizeY { get; set; } = SizeY;
    public List<Entity> Entities = new List<Entity>();

    public void Start() {
        Console.Clear();
        Render();
    }

    public void Render()
    {
        foreach(Entity entity in Entities) 
        {
            entity.Render();
        }
    }
}

class Level1 : Field
{
    public Level1(int SizeX, int SizeY) : base(SizeX, SizeY)
    {
        Entities.Add(new Player(3,3));
        Entities.Add(new Rock(2,3));
        Entities.Add(new Rock(4,3));
        Entities.Add(new Rock(3,4));
        Entities.Add(new Rock(3,2));
    }
}
class Entity(int X, int Y)
{
    public int X { get; set; } = X;
    public int Y { get; set; } = Y;
    public int SizeX { get; set; }
    public int SizeY { get; set; }

    protected string sprite = " ";

    public void Render()
    {
        int x = X, y = Y;
        Console.SetCursorPosition(x, y);
        foreach(char c in sprite) 
        {
            if(c == '\n') Console.SetCursorPosition(++x, y);   
            else Console.Write(c);
        }
    }
}

class Player : Entity
{
    public Player(int X, int Y) : base(X, Y)
    {
        sprite = "@";
        SizeX = 1;
        SizeY = 1;
    }
}
class Rock : Entity
{
    public Rock(int X, int Y) : base(X, Y)
    {
        sprite = "O";
        SizeX = 1;
        SizeY = 1;
    }
}

class Practice2
{
    static void Main(string[] args) {
        Level1 level1 = new(10, 10);
        level1.Start();
        Console.SetCursorPosition(0, level1.SizeY+1);
    }
}
