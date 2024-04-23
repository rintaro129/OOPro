namespace Game;

public class Wall : BaseEntity
{
    public event EventHandler OnCreated;
    public event EventHandler OnDied;
    public Wall()
    {
        Sprite = '#';
    }

    public override bool CanMove() => false;

    public override bool IsSolid() => true;
}