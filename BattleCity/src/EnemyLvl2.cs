namespace BattleCity;

public class EnemyLvl2(Field field, int x, int y) : Tank(field, x, y)
{
    public override int HealthPointsCurrent { get; set; } = 2;
    public override int HealthPointsMax { get; set; } = 2;

    public override void ProcessTurn()
    {
        if (HealthPointsCurrent <= 0) return;
        Random random = new Random();
        Direction moveDirection = (Direction)random.Next(5);
        Move(moveDirection);
        Shoot();
    }
}