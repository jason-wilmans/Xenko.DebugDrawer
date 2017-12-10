using System;
using SiliconStudio.Xenko.Games;

namespace DebugDrawing
{
    public class DebugDrawer : GameSystemBase
    {
        public DebugDrawer(IGame game)
            :base(game.Services)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            Console.WriteLine("Get this!");
        }
    }
}