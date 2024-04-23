namespace Game;

public class Prize : BaseEntity
{
    public override bool CanMove() => false;

    public override bool IsSolid() => false;
}