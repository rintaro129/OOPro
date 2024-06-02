namespace BattleCity_WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            while (true)
            {
                Console.WriteLine(@"What version of the game you want to play?
    c - console
    w - WinForms
    q - quit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "c":
                        Console.WriteLine("Welcome to the Console version!!!");
                        return;
                    case "w":
                        ApplicationConfiguration.Initialize();
                        Application.Run(new Form1());
                        return;
                    case "q":
                        return;
                }
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            
        }
    }
}