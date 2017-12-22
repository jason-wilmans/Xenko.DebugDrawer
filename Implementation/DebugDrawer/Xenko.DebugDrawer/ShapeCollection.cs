using System.Collections.Generic;
using System.Linq;
using SiliconStudio.Core.Extensions;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering;

namespace Xenko.DebugDrawer
{
    internal class ShapeCollection
    {
        private static readonly Vector2 Zero = Vector2.Zero;
        private readonly Color _color;

        private readonly object _geometryKey = new object();
        private readonly GraphicsDevice _graphicsDevice;
        private readonly Model _model;
        private readonly ISet<IShape> _shapes;

        public ShapeCollection(Color color, GraphicsDevice graphicsDevice, Model model)
        {
            _color = color;
            _graphicsDevice = graphicsDevice;
            _model = model;
            _shapes = new HashSet<IShape>();
        }

        public void Add(IShape geometry)
        {
            lock (_geometryKey)
            {
                _shapes.Add(geometry);
                UpdateMesh();
            }
        }

        public void UpdateMesh()
        {
            VertexPositionColorTexture[] vertices;
            int[] indices;

            lock (_shapes)
            {
                vertices = _shapes
                    .SelectMany(shape => shape.Vertices)
                    .Select(vertex => new VertexPositionColorTexture(vertex, _color, Zero))
                    .ToArray();

                var indicesList = new LinkedList<int>();
                foreach (var line in _shapes.SelectMany(shape => shape.Lines))
                {
                    var startIndex = vertices.IndexOf(vertex => vertex.Position == line.Start);
                    var endIndex = vertices.IndexOf(vertex => vertex.Position == line.End);

                    indicesList.AddLast(startIndex);
                    indicesList.AddLast(endIndex);
                }
                indices = indicesList.ToArray();
            }

            var vertexBuffer = Buffer.Vertex.New(_graphicsDevice, vertices);
            var indexBuffer = Buffer.Index.New(_graphicsDevice, indices);
            var meshDraw = new MeshDraw
            {
                PrimitiveType = PrimitiveType.LineStrip,
                VertexBuffers = new[]
                {
                    new VertexBufferBinding(vertexBuffer, VertexPositionColorTexture.Layout, vertices.Length)
                },
                IndexBuffer = new IndexBufferBinding(indexBuffer, true, indices.Length),
                DrawCount = indices.Length
            };

            var mesh = new Mesh {Draw = meshDraw};
            _model.Meshes = new List<Mesh> {mesh};
        }
    }
}