namespace BattleCity;

class Game
{
    private const int tickSeconds = 25;

    public void Start()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Field field = new Field();
        Visuals.ConnectVisuals(field);
        field.Start("../../../res/Level1.lvl");
        field.Play(tickSeconds);
        Console.SetCursorPosition(0, field.FieldSizeY);
        Console.WriteLine(field.Status);
    }
}