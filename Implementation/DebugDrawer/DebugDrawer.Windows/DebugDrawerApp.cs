using SiliconStudio.Xenko.Engine;
using Xenko.DebugDrawer;

namespace DebugDrawer
{
    class DebugDrawerApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.GameSystems.Add(new DebugDrawerSystem(game));
                game.Run();
            }
        }
    }
}
