using SiliconStudio.Xenko.Engine;

namespace Implementation
{
    class ImplementationApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
