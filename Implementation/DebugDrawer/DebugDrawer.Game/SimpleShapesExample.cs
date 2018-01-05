using System;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using Xenko.DebugDrawer;
using Xenko.DebugDrawer.Shapes;

namespace DebugDrawer
{
    public class SimpleShapesExample : SyncScript
    {
        private DebugDrawerSystem _debug;
        private Line _line2;
        private Line _line1;
        private Box _box;

        public override void Start()
        {
            base.Start();

            _debug = DebugDrawerSystem.Instance;
            _line1 = new Line(new Vector3(0, 1, 0), new Vector3(0, 1, 1), Color.Yellow);
            _debug.Add(_line1);
            _line2 = new Line(new Vector3(1, 1, 0), new Vector3(1, 1, 1), Color.Yellow);
            _debug.Add(_line2);
            _box = new Box(new Vector3(1.5f, .5f, 0), Vector3.One, Color.SlateBlue);
            _debug.Add(_box);
        }

        public override void Update()
        {
            float sin = (float)Math.Sin(Game.PlayTime.TotalTime.TotalSeconds);
            var delta = Vector3.UnitY + new Vector3(0, 0.5f, 0) * sin / 5;
            _line1.End = delta;
            _line2.Start = delta;
            _box.Scale = Vector3.One * .25f + Vector3.One * sin * .125f;
        }
    }
}