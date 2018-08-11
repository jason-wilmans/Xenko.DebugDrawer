using System;
using System.Threading.Tasks;
using Xenko.Core.Mathematics;
using Xenko.DebugDrawer;
using Xenko.DebugDrawer.Shapes;
using Xenko.Engine;

namespace DebugDrawer
{
    public class ColorChangeExample : AsyncScript
    {
        private Box _box1;
        private Box _box2;

        public override async Task Execute()
        {
            var debug = DebugDrawerSystem.Instance;

            _box1 = new Box(new Vector3(-.5f, 0, 0), new Vector3(0.3f), Color.White);
            debug.Add(_box1);

            _box2 = new Box(new Vector3(.5f, 0, 0), new Vector3(0.3f), Color.Orange);
            debug.Add(_box2);

            while (Game.IsRunning)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));

                ToggleColor(_box1);
                ToggleColor(_box2);
                
            }
        }

        private void ToggleColor(AShape shape)
        {
            shape.Color = shape.Color == Color.Orange ? Color.White : Color.Orange;
        }
    }
}