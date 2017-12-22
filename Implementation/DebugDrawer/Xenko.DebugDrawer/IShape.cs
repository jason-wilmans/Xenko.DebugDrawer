using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;

namespace Xenko.DebugDrawer
{
    public interface IShape
    {
        Color Color { get; set; }
        IEnumerable<Vector3> Vertices { get; }
        IEnumerable<Line> Lines { get; }
    }
}