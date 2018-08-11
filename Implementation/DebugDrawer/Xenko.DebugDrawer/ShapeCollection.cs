using System;
using System.Collections.Generic;
using System.Linq;
using Xenko.Core.Mathematics;
using Xenko.Engine;
using Xenko.Graphics;
using Xenko.Rendering;
using Xenko.DebugDrawer.Shapes;
using Buffer = Xenko.Graphics.Buffer;

namespace Xenko.DebugDrawer
{
    internal class ShapeCollection : IShapePropetyChangedHandler
    {
        
        private readonly GraphicsDevice _graphicsDevice;
        private readonly GraphicsContext _graphicsContext;
        private readonly ISet<AShape> _shapes;
        private readonly VertexPositionColorTexture[] _vertexArray;
        private readonly int[] _indexArray;
        private Buffer _vertexBuffer;
        private Buffer _indexBuffer;
        private readonly VertexPositionColorTexture[] _vertices = new VertexPositionColorTexture[1024];
        private readonly int[] _indices = new int[1024];

        public Entity Entity { get; set; }

        public bool IsModified { get; set; }
        public Color Color { get; private set; }

        public event Action<AShape> ColorChanged;

        public ShapeCollection(Color color, GraphicsDevice graphicsDevice, GraphicsContext graphicsContext)
        {
            Color = color;
            _graphicsDevice = graphicsDevice;
            _graphicsContext = graphicsContext;
            _shapes = new HashSet<AShape>();

            _vertexArray = new VertexPositionColorTexture[1024];
            _indexArray = new int[1024];
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
                            DrawCount = _vertexArray.Length
                        }
                    }
                }, Materials = { Materials.CreateDebugMaterial(color, true, graphicsDevice) }
            };

            Entity = new Entity
            {
                Name = color.ToString(),
                Components = { new ModelComponent(model) }
            };
        }

        public void Add(AShape shape)
        {
            if(shape == null) throw new ArgumentNullException(nameof(shape));

            IsModified = true;

            shape.ChangeHandler = this;

            lock (_shapes)
            {
                _shapes.Add(shape);
            }
        }
        
        public bool Remove(AShape shape)
        {
            if (shape == null) throw new ArgumentNullException(nameof(shape));

            IsModified = true;

            lock (_shapes)
            {
                return _shapes.Remove(shape);
            }
        }

        public void UpdateMesh()
        {
            if(!IsModified) return;

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

            _vertexArray.CopyTo(_vertices, 0);
            _indexArray.CopyTo(_indices, 0);

            vertices.CopyTo(_vertices, 0);
            indices.CopyTo(_indices, 0);

            _vertexBuffer.SetData(_graphicsContext.CommandList, _vertices.ToArray());
            _indexBuffer.SetData(_graphicsContext.CommandList, _indices.ToArray());

            IsModified = false;
        }

        private int OptionalInsert(Vector3 point, IList<VertexPositionColorTexture> verticeList)
        {
            var vertex = new VertexPositionColorTexture(point, Color, Vector2.Zero);
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

        ~ShapeCollection()
        {
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
        }

        public void OnPropertyChanged(AShape shape)
        {
            if (shape.Color != Color)
            {
                Remove(shape);
                ColorChanged?.Invoke(shape);
            }
            else
            {
                IsModified = true;
            }
        }
    }
}