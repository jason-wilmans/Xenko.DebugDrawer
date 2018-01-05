using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;

namespace Xenko.DebugDrawer.Shapes
{
    public class Box : AShape
    {
        public Vector3 Position
        {
            get => _transform.Position;
            set
            {
                _transform.Position = value;
                CalculateLines();
            }
        }

        public Vector3 Scale
        {
            get => _transform.Scale;
            set
            {
                _transform.Scale = value;
                CalculateLines();
            }
        }

        private readonly Line[] _lines;
        private readonly TransformComponent _transform;

        public override IEnumerable<Line> Lines => _lines;

        public Box(Color color) : this(Vector3.Zero, Vector3.One, color)
        {
        }

        public Box(Vector3 position, Vector3 scale, Color color) : base(color)
        {
            _transform = new TransformComponent();
            _lines = new Line[12];

            Position = position;
            Scale = scale;

            CalculateLines();
        }

        private void CalculateLines()
        {
            float xHalf = Scale.X / 2;
            float yHalf = Scale.Y / 2;
            float zHalf = Scale.Z / 2;

            // bottom
            _lines[0] = new Line(
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y - yHalf, _transform.Position.Z - zHalf),
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y - yHalf, _transform.Position.Z + zHalf), Color);
            _lines[1] = new Line(
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y - yHalf, _transform.Position.Z + zHalf),
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y - yHalf, _transform.Position.Z + zHalf), Color);
            _lines[2] = new Line(
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y - yHalf, _transform.Position.Z + zHalf),
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y - yHalf, _transform.Position.Z - zHalf), Color);
            _lines[3] = new Line(
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y - yHalf, _transform.Position.Z - zHalf),
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y - yHalf, _transform.Position.Z - zHalf), Color);

            // top
            _lines[4] = new Line(
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y + yHalf, _transform.Position.Z - zHalf),
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y + yHalf, _transform.Position.Z + zHalf), Color);
            _lines[5] = new Line(
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y + yHalf, _transform.Position.Z + zHalf),
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y + yHalf, _transform.Position.Z + zHalf), Color);
            _lines[6] = new Line(
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y + yHalf, _transform.Position.Z + zHalf),
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y + yHalf, _transform.Position.Z - zHalf), Color);
            _lines[7] = new Line(
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y + yHalf, _transform.Position.Z - zHalf),
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y + yHalf, _transform.Position.Z - zHalf), Color);

            // vertical
            _lines[8] = new Line(
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y - yHalf, _transform.Position.Z - zHalf),
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y + yHalf, _transform.Position.Z - zHalf), Color);
            _lines[9] = new Line(
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y - yHalf, _transform.Position.Z + zHalf),
                new Vector3(_transform.Position.X - xHalf, _transform.Position.Y + yHalf, _transform.Position.Z + zHalf), Color);
            _lines[10] = new Line(
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y - yHalf, _transform.Position.Z - zHalf),
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y + yHalf, _transform.Position.Z - zHalf), Color);
            _lines[11] = new Line(
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y - yHalf, _transform.Position.Z + zHalf),
                new Vector3(_transform.Position.X + xHalf, _transform.Position.Y + yHalf, _transform.Position.Z + zHalf), Color);

            NotifyPropertyChanged();
        }
    }
}