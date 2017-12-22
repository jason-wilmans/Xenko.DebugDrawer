using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Core.Serialization.Contents;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Games;
using SiliconStudio.Xenko.Rendering;

namespace Xenko.DebugDrawer
{
    public class DebugDrawerSystem : GameSystemBase
    {
        public static DebugDrawerSystem Instance;

        private readonly IGame _game;
        private readonly IDictionary<Color, ShapeCollection> _geometries;
        private Material _debugMaterial;
        private bool _initialized;
        private Entity _rootEntity;
        private SceneSystem _sceneSystem;

        public DebugDrawerSystem(IGame game) : base(game.Services)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));

            _geometries = new ConcurrentDictionary<Color, ShapeCollection>();
            _game = game;
            Enabled = true;
            Visible = true;
            Instance = this;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!_initialized)
                _initialized = TryInitialize();
        }

        private bool TryInitialize()
        {
            if (_game.GraphicsDevice == null) return false;

            _rootEntity = new Entity
            {
                Name = "DebugRoot"
            };
            var contentManager = _game.Services.GetService<IContentManager>();
            _debugMaterial = contentManager.Load<Material>("DebugMaterial");
            _sceneSystem = _game.Services.GetService<SceneSystem>();
            _sceneSystem.SceneInstance.RootScene.Entities.Add(_rootEntity);

            return true;
        }

        public void Add<T>(T geometry) where T : IShape
        {
            if (Equals(geometry, default(T)))
                throw new ArgumentException(nameof(geometry));

            var geometries = EnsureInfrastructure(geometry.Color);
            geometries.Add(geometry);
        }

        private ShapeCollection EnsureInfrastructure(Color color)
        {
            if (!_geometries.ContainsKey(color))
            {
                var model = CreateEntity(color);
                _geometries[color] = new ShapeCollection(color, GraphicsDevice, model);
            }

            return _geometries[color];
        }

        private Model CreateEntity(Color color)
        {
            var model = new Model();
            model.Materials.Add(_debugMaterial);
            var modelComponent = new ModelComponent(model);
            var colorEntity = new Entity
            {
                Name = color.ToString(),
                Components = {modelComponent}
            };
            _rootEntity.AddChild(colorEntity);
            return model;
        }
    }
}