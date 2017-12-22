using System.Threading.Tasks;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using Xenko.DebugDrawer;

namespace DebugDrawer
{
    public class SimpleShapesExample : AsyncScript
    {
        private DebugDrawerSystem _debugDrawer;

        public override async Task Execute()
        {
            await Task.Delay(1000);
            _debugDrawer = DebugDrawerSystem.Instance;
            var line1 = new Line(new Vector3(0, 0, 0), new Vector3(1, 2, 3), Color.Cyan);
            _debugDrawer.Add(line1);
            var line2 = new Line(new Vector3(1, 2, 3), new Vector3(0, 2, 0), Color.Cyan);
            _debugDrawer.Add(line2);
        }
    }
}