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
        private Box _box2;


        public override void Start()
        {
            base.Start();

            _debug = DebugDrawerSystem.Instance;
            _box = new Box(new Vector3(1.5f, .5f, 0), Vector3.One, Color.Chartreuse);
            _debug.Add(_box);

            _box2 = new Box(new Vector3(-1.5f, .5f, 0), Vector3.One, Color.Red);
            _debug.Add(_box2);

            _line1 = new Line(new Vector3(0, 1, 0), new Vector3(0, 1, 1), Color.Yellow);
            _debug.Add(_line1);

            _line2 = new Line(new Vector3(.25f, 1, 0), new Vector3(0, 1, 1), Color.HotPink);
            _debug.Add(_line2);
        }

        public override void Update()
        {
            float sin = (float)Math.Sin(Game.PlayTime.TotalTime.TotalSeconds);
            float cos = (float)Math.Cos(Game.PlayTime.TotalTime.TotalSeconds * 4);
            var delta = Vector3.UnitX * sin * 0.01f;
            _line1.Start += delta;
            _line1.End += delta;
            _line2.Start += delta;

            _box.Scale = Vector3.One * .25f + Vector3.One * sin * .125f;
            _box2.Scale = Vector3.One * .25f + Vector3.One * cos * .125f;
        }
    }
}