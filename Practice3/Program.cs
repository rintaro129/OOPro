abstract class BaseEntity
{
    public int X { get; set; }
    public int Y { get; set; }
    public abstract bool CanMove();

    public abstract bool IsSolid();
}

abstract class Tank : BaseEntity
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

class Player : Tank
{
    public Player(int x, int y)
    {
        X = x;
        Y = y;
        HealthPoints = 1;
    }
}

class EnemyLvl1 : Tank
{
    public EnemyLvl1(int x, int y)
    {
        X = x;
        Y = y;
        HealthPoints = 1;
    }
}

class EnemyLvl2 : Tank
{
    public EnemyLvl2(int x, int y)
    {
        X = x;
        Y = y;
        HealthPoints = 2;
    }
}

class Prize : BaseEntity
{
    public override bool CanMove() => false;

    public override bool IsSolid() => false;
}

class Bullet : BaseEntity
{
    public override bool CanMove() => true;

    public override bool IsSolid() => false;
    public void Move()
    {
        // Move
    }
    public int Direction { get; set; } = 0;
}

class Wall : BaseEntity
{
    public override bool CanMove() => false;

    public override bool IsSolid() => true;
}

class Practice3
{
    static void Main()
    {
        
    }
}