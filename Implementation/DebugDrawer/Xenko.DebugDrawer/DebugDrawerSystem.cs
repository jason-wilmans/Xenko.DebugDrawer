using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;
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

        public void Add<T>(T geometry) where T : IShape
        {
            if (Equals(geometry, default(T)))
                throw new ArgumentException(nameof(geometry));

            var geometries = EnsureEntities(geometry.Color);
            geometries.Add(geometry);
        }

        private ShapeCollection EnsureEntities(Color color)
        {
            if (_rootEntity == null)
            {
                _rootEntity = new Entity
                {
                    Name = "DebugRoot"
                };
                _sceneSystem = _game.Services.GetService<SceneSystem>();
                _sceneSystem.SceneInstance.RootScene.Entities.Add(_rootEntity);
            }

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
            var debugMaterial = Materials.CreateDebugMaterial(color, true, GraphicsDevice);
            model.Materials.Add(debugMaterial);
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