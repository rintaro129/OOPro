namespace BattleCity;

public class Bomb(Field field, int x, int y) : Obstacle(field, x, y)
{
    public override string GetName() => "Bomb";
    protected override void OnDied(EventArgs e)
    {
        base.OnDied(e);
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                if (X + i < 0 || X + i >= Field.FieldSizeX)
                    continue;
                if (Y + j < 0 || Y + j >= Field.FieldSizeY) 
                    continue;
                if (Field.Map[X + i, Y + j] != null) 
                    Field.Map[X + i, Y + j].TakeDamage();
            }
        }
    }
}