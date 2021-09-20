using System;

namespace Max_Duck_Hunter_Reload
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BaseEngine.BaseEngine())
            {
                game.Run();
            }

        }
    }
}
