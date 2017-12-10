using DebugDrawing;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Rendering.Compositing;

namespace Implementation
{
    class ImplementationApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.GameSystems.Add(new DebugDrawer(game));
                game.Run();
            }
        }
    }
}
