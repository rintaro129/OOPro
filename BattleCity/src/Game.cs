namespace Game;

class Game
{
    private readonly int tickSeconds = 25;
    Field Field { get; }

    public Game()
    {
        Field = new Field();
    }

    public void Start()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Field = new Field()
        
        while (true)
        {
            UpdateGameState();

            // // Render game
            // RenderGame();

            int status = HandleKeyPress();

            if (status == 0)
                return;

            Console.Clear();
            Field.DrawScene();
            Thread.Sleep(tickSeconds);
        }
    }

    private int HandleKeyPress()
    {
        int status = 1;
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    Field.Player.MovePlayer(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    Field.Player.MovePlayer(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    Field.Player.MovePlayer(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    Field.Player.MovePlayer(1, 0);
                    break;
                case ConsoleKey.Escape:
                    status = 0;
                    break;
            }
        }

        return status;
    }
}