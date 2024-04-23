namespace Game;

class Game
{
    private readonly int tickSeconds = 100;
    private Field Field { get; set; }
    
    public void Start()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Field = new Field();
        Visuals visuals = new Visuals(Field);
        Field.Start();
        int i = 0;
        while (Field.Status == "Playing")
        {
            if (i % 1 == 0)
            {
                Field.ProcessBullets();
            }
            if (i % 2 == 0)
            {
                Field.Player.ProcessTurn();
                Field.ProcessTanks();
            }
            

            i++;
            Thread.Sleep(tickSeconds);
        }
        Console.SetCursorPosition(0,Field.FieldSizeY);
        Console.WriteLine(Field.Status);
    }
}