using System;
using SiliconStudio.Core;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Games;

namespace DebugDrawing
{
    public class DebugDrawer : GameSystemBase
    {
        public DebugDrawer(IGame game) : base(game.Services)
        {
            Visible = true;
            Enabled = true;
        }

        public override void Initialize()
        {
            base.Initialize();

            Console.WriteLine("DebugDrawer ready for lift off");
        }
    }
}