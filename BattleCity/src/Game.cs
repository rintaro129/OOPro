namespace BattleCity;

class Game
{
    private const int TickSeconds = 25;

    public void Start()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Field field = new Field();
        field.Start("../../../res/Level1.lvl");
        field.Play(TickSeconds);
        Console.SetCursorPosition(0, field.FieldSizeY);
        Console.WriteLine(field.Status);
    }
}