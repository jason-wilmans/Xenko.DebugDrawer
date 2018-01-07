using System;
using System.Collections.Generic;
using System.Linq;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering;
using Xenko.DebugDrawer.Shapes;
using Buffer = SiliconStudio.Xenko.Graphics.Buffer;

namespace Xenko.DebugDrawer
{
    internal class ShapeCollection
    {
        private readonly Color _color;
        
        private readonly GraphicsDevice _graphicsDevice;
        private readonly GraphicsContext _graphicsContext;
        private readonly ISet<AShape> _shapes;
        private Material _material;
        private readonly VertexPositionColorTexture[] _vertexArray;
        private readonly int[] _indexArray;
        private Buffer _vertexBuffer;
        private Buffer _indexBuffer;

        public Entity Entity { get; set; }

        public ShapeCollection(Color color, GraphicsDevice graphicsDevice, GraphicsContext graphicsContext)
        {
            _color = color;
            _graphicsDevice = graphicsDevice;
            _graphicsContext = graphicsContext;
            _shapes = new HashSet<AShape>();

            _vertexArray = new VertexPositionColorTexture[65535];
            _indexArray = new int[65535];
            InitEntity(color, graphicsDevice);
        }

        private void InitEntity(Color color, GraphicsDevice graphicsDevice)
        {
            _vertexBuffer = Buffer.Vertex.New(_graphicsDevice, _vertexArray, GraphicsResourceUsage.Dynamic);
            _indexBuffer = Buffer.Index.New(_graphicsDevice, _indexArray, GraphicsResourceUsage.Dynamic);

            var model = new Model
            {
                Meshes = {
                    new Mesh {
                        Draw = new MeshDraw
                        {
                            PrimitiveType = PrimitiveType.LineList,
                            VertexBuffers = new[] {
                                new VertexBufferBinding(_vertexBuffer,
                                    VertexPositionColorTexture.Layout,
                                    _vertexArray.Length * VertexPositionColorTexture.Layout.CalculateSize())
                            },
                            IndexBuffer = new IndexBufferBinding(_indexBuffer, true, _indexArray.Length * sizeof(int)),
                            DrawCount = 1
                        }
                    }
                }
            };
            _material = Materials.CreateDebugMaterial(color, true, graphicsDevice);
            model.Materials.Add(_material);

            Entity = new Entity
            {
                Name = color.ToString(),
                Components = { new ModelComponent(model) }
            };
        }

        public void Add(AShape shape)
        {
            if(shape == null) throw new ArgumentNullException(nameof(shape));

            lock (_shapes)
            {
                _shapes.Add(shape);
            }
        }
        
        public bool Remove(AShape shape)
        {
            if (shape == null) throw new ArgumentNullException(nameof(shape));

            lock (_shapes)
            {
                return _shapes.Remove(shape);
            }
        }

        public void UpdateMesh()
        {
            List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
            var indices = new LinkedList<int>();
            lock (_shapes)
            {
                foreach (var line in _shapes.SelectMany(shape => shape.Lines))
                {
                    int startIndex = OptionalInsert(line.Start, vertices);
                    int endIndex = OptionalInsert(line.End, vertices);
                    indices.AddLast(startIndex);
                    indices.AddLast(endIndex);
                }
            }

            _vertexBuffer.SetData(_graphicsContext.CommandList, vertices.ToArray());
            _indexBuffer.SetData(_graphicsContext.CommandList, indices.ToArray());
        }

        private int OptionalInsert(Vector3 point, IList<VertexPositionColorTexture> verticeList)
        {
            var vertex = new VertexPositionColorTexture(point, _color, Vector2.Zero);
            int index = verticeList.IndexOf(vertex);
            if (index >= 0)
            {
                return index;
            }

            index = verticeList.Count;
            verticeList.Insert(index, vertex);
            return index;
        }

        public bool Contains(AShape aShape)
        {
            lock (_shapes)
            {
                return _shapes.Contains(aShape);
            }
        }
    }
}