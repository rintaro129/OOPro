namespace Game;

public class Bullet : BaseEntity
{
    public event EventHandler OnCreated;
    public event EventHandler OnMoved;
    public event EventHandler OnDied;
    public override bool CanMove() => true;

    public override bool IsSolid() => false;

    public void Move()
    {
        // Move
    }

    public int Direction { get; set; } = 0;
}