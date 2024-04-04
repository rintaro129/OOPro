namespace Game;
class Game
{
    Field Field {get;}

    public Game() 
    {
        Field = new Field();
    }
    public void Start()
    {
        Console.CursorVisible = false;
        Console.Clear();
        
        Field.DrawScene();

        while (true)
        {
            // // Update game state
            // UpdateGameState();

            // // Render game
            // RenderGame();

            int status = HandleKeyPress();

            if(status == 0) 
                return;

            Console.Clear();
            Field.DrawScene();
            Thread.Sleep(50);
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
