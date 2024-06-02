namespace BattleCity;

class Program
{
    [STAThread]
    static void Main()
    {
        BaseIO IO = null;
        do
        {
            Console.Clear();
            Console.WriteLine(@"What version of the game you want to play?
    c - console
    w - WinForms
    q - quit");
            string input = Console.ReadLine();
            switch (input)
            {
                case "c":
                    IO = new ConsoleIO();
                    break;
                case "w":
                    IO = new WinFormsIO();
                    break;
                case "q":
                    return;
            }
        } while (IO is null);

        Game game = new Game(IO);
        IO.Game = game;
        game.Start();
    }
}