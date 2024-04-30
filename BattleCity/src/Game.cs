namespace BattleCity;

class Game
{
    private readonly int tickSeconds = 100;

    public void Start()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Field field = new Field();
        Visuals visuals = new Visuals(field);
        field.Start("../../../res/Level1.lvl");
        int i = 0;
        while (field.Status == "Playing")
        {
            field.ProcessEntities(i);
            i++;
            Thread.Sleep(tickSeconds);
        }

        Console.SetCursorPosition(0, field.FieldSizeY);
        Console.WriteLine(field.Status);
    }
}