using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;

namespace Xenko.DebugDrawer
{
    public class Line : IShape
    {
        private readonly Vector3[] _lines;

        public Line(Vector3 start, Vector3 end, Color color)
        {
            _lines = new[] {start, end};
            Lines = new[] {this};
            Color = color;
        }

        public Vector3 Start => _lines[0];
        public Vector3 End => _lines[1];
        public Color Color { get; set; }
        public IEnumerable<Vector3> Vertices => _lines;

        public IEnumerable<Line> Lines { get; }
    }
}