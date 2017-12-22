using SiliconStudio.Core;
using SiliconStudio.Xenko.Games;

namespace Xenko.DebugDrawer
{
    public class DebugDrawerSystem : GameSystemBase
    {
        //private readonly IGame _game;
        //private bool _initialized;

        public DebugDrawerSystem(ServiceRegistry services) : base(services)
        {
            //if (services == null) throw new ArgumentNullException(nameof(services));

            //_game = services;
            //Enabled = true;
            //Visible = true;
        }

        //public override void Draw(GameTime gameTime)
        //{
        //base.Draw(gameTime);

        //if (!_initialized)
        //    _initialized = TryInitialize();
        //}

        //private bool TryInitialize()
        //{
        //    if (_game.GraphicsDevice == null) return false;

        //    var random = new Random();
        //    float radius = 2;
        //    var edgeCount = 500;
        //    var i2 = edgeCount * 2;

        //    var vertices = new VertexPositionColorTexture[50];
        //    for (var i = 0; i < vertices.Length; i++)
        //    {
        //        var position = new Vector3((float) (random.NextDouble() * radius),
        //            (float) (random.NextDouble() * radius),
        //            (float) (random.NextDouble() * radius));
        //        var textureCoordinate = new Vector2((float) random.NextDouble(), (float) random.NextDouble());
        //        vertices[i] = new VertexPositionColorTexture(position, Color.Cyan, textureCoordinate);
        //    }

        //    var indices = new int[i2];
        //    for (var i = 0; i < i2; i++)
        //        indices[i] = random.Next(vertices.Length);

        //    var vertexBuffer = Buffer.Vertex.New(_game.GraphicsDevice, vertices);
        //    var indexBuffer = Buffer.Index.New(_game.GraphicsDevice, indices);
        //    var meshDraw = new MeshDraw
        //    {
        //        PrimitiveType = PrimitiveType.LineStrip,
        //        VertexBuffers = new[]
        //        {
        //            new VertexBufferBinding(vertexBuffer, VertexPositionColorTexture.Layout, edgeCount)
        //        },
        //        IndexBuffer = new IndexBufferBinding(indexBuffer, true, indices.Length),
        //        DrawCount = edgeCount
        //    };

        //    var mesh = new Mesh {Draw = meshDraw};
        //    var model = new Model {mesh};
        //    var contentManager = _game.Services.GetService<IContentManager>();
        //    var debugMaterial = contentManager.Load<Material>("DebugMaterial");
        //    model.Materials.Add(debugMaterial);
        //    var modelComponent = new ModelComponent(model);

        //    var entity = new Entity {modelComponent};

        //    var sceneSystem = _game.Services.GetService<SceneSystem>();
        //    sceneSystem.SceneInstance.RootScene.Entities.Add(entity);

        //    return true;
        //}
    }
}