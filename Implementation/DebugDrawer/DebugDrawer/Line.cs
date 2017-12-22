using SiliconStudio.Core.Mathematics;

namespace DebugDrawer
{
    public class Line
    {
        public Vector3 Start { get; set; }
        public Vector3 End { get; set; }
        public Color Color { get; set; }

        public Line() : this(Vector3.Zero, Vector3.One, Color.YellowGreen)
        {
        }

        public Line(Vector3 start, Vector3 end, Color color)
        {
            Start = start;
            End = end;
            Color = color;
        }
    }
}