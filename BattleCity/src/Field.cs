namespace Game;
public class Field
{
    public int FieldSizeX { get; }
    public int FieldSizeY { get; }
    public BaseEntity[,] Map {get; set;}
    public Player Player {get;}
    public Field()
    {
        Player = new Player {X = 0, Y = 0, ParentField = this};
        FieldSizeX = Console.WindowWidth;
        FieldSizeY = Console.WindowHeight;
        Map = new BaseEntity[FieldSizeX, FieldSizeY];
        Map[0,0] = Player;
        Map[1,0] = new Wall() {X = 1, Y = 0, ParentField = this};
    }
    public void DrawScene()
    {
        for(int i = 0; i < FieldSizeX; i++) {
            for(int j = 0; j < FieldSizeY; j++) {
                if(Map[i, j] != null) {
                    Console.SetCursorPosition(Map[i, j].X, Map[i, j].Y);
                    Console.Write(Map[i, j].Sprite);
                }
            }
        }
        
    }
}
abstract public class BaseEntity
{
    public Field ParentField { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public char Sprite{get; set;}
    public abstract bool CanMove();

    public abstract bool IsSolid();
}

abstract public class Tank : BaseEntity
{
    public override bool CanMove() => true;
    public override bool IsSolid() => true;
    public void Move()
    {
        // Move
    }
    protected int HealthPoints;
    public int Direction { get; set; } = 0;
}

public class Player : Tank
{
    public Player() {
        Sprite = 'P';
    } 
    public void MovePlayer(int deltaX, int deltaY)
    {
        int oldX = X, oldY = Y;
        if (X + deltaX >= 0 && X + deltaX < ParentField.FieldSizeX &&
            Y + deltaY >= 0 && Y + deltaY < ParentField.FieldSizeY &&
            (ParentField.Map[X + deltaX, Y + deltaY] == null || 
            !ParentField.Map[X + deltaX, Y + deltaY].IsSolid())
            )
        {
            ParentField.Map[oldX, oldY] = null;
            X += deltaX;
            Y += deltaY;
            ParentField.Map[X, Y] = this;
        }
    }
}

public class EnemyLvl1 : Tank
{
    public EnemyLvl1(int x, int y)
    {
        X = x;
        Y = y;
        HealthPoints = 1;
    }
}

public class EnemyLvl2 : Tank
{
    public EnemyLvl2(int x, int y)
    {
        X = x;
        Y = y;
        HealthPoints = 2;
    }
}

public class Prize : BaseEntity
{
    public override bool CanMove() => false;

    public override bool IsSolid() => false;
}

public class Bullet : BaseEntity
{
    public override bool CanMove() => true;

    public override bool IsSolid() => false;
    public void Move()
    {
        // Move
    }
    public int Direction { get; set; } = 0;
}

public class Wall : BaseEntity
{
    public Wall() {
        Sprite = '#';
    }
    public override bool CanMove() => false;

    public override bool IsSolid() => true;
}

