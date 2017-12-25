using System.Threading.Tasks;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using Xenko.DebugDrawer;

namespace DebugDrawer
{
    public class SimpleShapesExample : StartupScript
    {
        private DebugDrawerSystem _debugDrawer;

        public override void Start()
        {
            base.Start();

            _debugDrawer = DebugDrawerSystem.Instance;
            var line1 = new Line(new Vector3(0, 0, 0), new Vector3(1, 2, 3), Color.Chartreuse);
            _debugDrawer.Add(line1);
            var line2 = new Line(new Vector3(1, 2, 3), new Vector3(0, 2, 0), Color.Red);
            _debugDrawer.Add(line2);
        }
    }
}