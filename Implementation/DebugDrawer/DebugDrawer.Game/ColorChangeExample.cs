using System;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using Xenko.DebugDrawer;
using Xenko.DebugDrawer.Shapes;

namespace DebugDrawer
{
    public class ColorChangeExample : SyncScript
    {
        private Box _box1;
        private Box _box2;

        public override void Start()
        {
            base.Start();

            var debug = DebugDrawerSystem.Instance;

            _box1 = new Box(new Vector3(-.5f, 0, 0), new Vector3(0.3f), Color.Azure);
            debug.Add(_box1);

            _box2 = new Box(new Vector3(.5f, 0, 0), new Vector3(0.3f), Color.Azure);
            debug.Add(_box2);
        }

        public override void Update()
        {
            var sin = (float) Math.Sin(Game.PlayTime.TotalTime.TotalSeconds);
            var cos = (float) Math.Cos(Game.PlayTime.TotalTime.TotalSeconds);

            if (Game.PlayTime.ElapsedTime.TotalSeconds > 5) _box1.Color = Color.Orange;
        }
    }
}