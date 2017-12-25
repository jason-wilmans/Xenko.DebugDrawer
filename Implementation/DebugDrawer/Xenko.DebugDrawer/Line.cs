using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;

namespace Xenko.DebugDrawer
{
    public struct Line : IShape
    {
        public Line(Vector3 start, Vector3 end, Color color)
        {
            Start = start;
            End = end;
            Color = color;
            Lines = new Line[1];
            ((Line[])Lines)[0] = this;
        }

        public Vector3 Start { get; set; }
        public Vector3 End { get; set; }
        public Color Color { get; set; }

        public IEnumerable<Line> Lines { get; }
    }
}