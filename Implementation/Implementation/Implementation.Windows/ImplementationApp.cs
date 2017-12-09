using DebugDrawing;
using SiliconStudio.Xenko.Engine;

namespace Implementation
{
    class ImplementationApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Services.AddService(new DebugDrawer(game));
                game.Run();
            }
        }
    }
}
