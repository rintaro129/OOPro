namespace BattleCity;

public class EnemyLvl1(Field field, int x, int y) : Tank(field, x, y)
{
    public override void ProcessTurn()
    {
        if (HealthPointsCurrent <= 0) return;
        Random random = new Random();
        int randomNumber = random.Next(5);
        Move((Direction)randomNumber);
        Shoot();
    }
}