using System;
using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Core.Serialization.Contents;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Games;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering;
using Buffer = SiliconStudio.Xenko.Graphics.Buffer;

namespace DebugDrawer
{
    public class DebugDrawerSystem : GameSystemBase
    {
        private readonly IGame _game;
        private bool _initialized;

        public DebugDrawerSystem(IGame game) : base(game.Services)
        {
            if(game == null) throw new ArgumentNullException(nameof(game));

            _game = game;
            Enabled = true;
            Visible = true;
        }

        public override void Initialize()
        {
            base.Initialize();


        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!_initialized)
            {
                _initialized = TryInitialize();
            }
        }

        private bool TryInitialize()
        {
            if (_game.GraphicsDevice == null) return false;

            Random random = new Random();
            float radius = 2;
            int vertexCount = 50;

            var textureCoordinate = new Vector2(.5f, .5f);
            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[50];
            for (int i = 0; i < vertexCount; i++)
            {
                var position = new Vector3((float) (random.NextDouble() * radius),
                    (float) (random.NextDouble() * radius),
                    (float) (random.NextDouble() * radius));
                
                vertices[i] = new VertexPositionColorTexture(position, Color.YellowGreen, textureCoordinate);
            }

            var vBuffer = Buffer.Vertex.New(_game.GraphicsDevice, vertices);

            var meshDraw = new MeshDraw
            {
                PrimitiveType = PrimitiveType.LineStrip,
                VertexBuffers = new[]
                    {new VertexBufferBinding(vBuffer, VertexPositionColorTexture.Layout, vertexCount)},
                DrawCount = vertexCount
            };

            var mesh = new Mesh { Draw = meshDraw };

            var model = new Model { mesh };
            var contentManager = _game.Services.GetService<IContentManager>();
            var debugMaterial = contentManager.Load<Material>("DebugMaterial");
            model.Materials.Add(debugMaterial);
            var modelComponent = new ModelComponent(model);

            var entity = new Entity { modelComponent };

            SceneSystem sceneSystem = _game.Services.GetService<SceneSystem>();
            sceneSystem.SceneInstance.RootScene.Entities.Add(entity);

            return true;
        }
    }
}