using SiliconStudio.Xenko.Engine;

namespace DebugDrawer
{
    class DebugDrawerApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.GameSystems.Add(new DebugDrawerSystem(game.Services));
                game.Run();
            }
        }
    }
}
