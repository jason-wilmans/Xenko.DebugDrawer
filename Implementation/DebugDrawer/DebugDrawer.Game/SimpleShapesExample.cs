using System;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using Xenko.DebugDrawer;
using Xenko.DebugDrawer.Shapes;

namespace DebugDrawer
{
    public class SimpleShapesExample : SyncScript
    {
        private DebugDrawerSystem _debugDrawer;
        private Line _line2;
        private Line _line1;

        public override void Start()
        {
            base.Start();

            _debugDrawer = DebugDrawerSystem.Instance;
            _line1 = new Line(new Vector3(0, 1, 0), new Vector3(0, 1, 1), Color.Chartreuse);
            _debugDrawer.Add(_line1);
            _line2 = new Line(new Vector3(1, 1, 0), new Vector3(1, 1, 1), Color.Red);
            _debugDrawer.Add(_line2);
        }

        public override void Update()
        {
            var delta = Vector3.UnitY + new Vector3(0, 0.5f, 0) * (float)Math.Sin(Game.PlayTime.TotalTime.TotalSeconds / 5);
            _line1.End = delta;
            _line2.Start = delta;
        }
    }
}