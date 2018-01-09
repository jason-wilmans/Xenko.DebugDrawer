using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Games;
using Xenko.DebugDrawer.Shapes;

namespace Xenko.DebugDrawer
{
    public class DebugDrawerSystem : GameSystemBase
    {
        public static DebugDrawerSystem Instance;

        private readonly IGame _game;
        private readonly IDictionary<Color, ShapeCollection> _shapeCollections;
        private Entity _rootEntity;
        private SceneSystem _sceneSystem;

        public DebugDrawerSystem(IGame game) : base(game.Services)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));

            _shapeCollections = new ConcurrentDictionary<Color, ShapeCollection>();
            _game = game;
            Enabled = true;
            Visible = true;
            Instance = this;
        }

        public void Add<T>(T shape) where T : AShape
        {
            if (Equals(shape, default(T)))
                throw new ArgumentException(nameof(shape));

            var shapeCollection = EnsureEntities(shape.Color);
            shapeCollection.Add(shape);
        }

        public override bool BeginDraw()
        {
            foreach (var shapeCollection in _shapeCollections.Values)
            {
                shapeCollection.UpdateMesh();
            }

            return true;
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

            return EnsureCollection(color);
        }

        private ShapeCollection EnsureCollection(Color color)
        {
            if (_shapeCollections.TryGetValue(color, out var shapeCollection)) return shapeCollection;

            shapeCollection = new ShapeCollection(color, GraphicsDevice, _game.GraphicsContext);
            shapeCollection.ColorChanged += OnColorChanged;
            _shapeCollections[color] = shapeCollection;
            _rootEntity.AddChild(shapeCollection.Entity);

            return shapeCollection;
        }

        private void OnColorChanged(AShape shape)
        {
            if(shape == null) throw new ArgumentNullException(nameof(shape));

            ShapeCollection shapeCollection = EnsureEntities(shape.Color);
            shapeCollection.Add(shape);
        }

        public void Clear()
        {
            foreach (var shapeCollection in _shapeCollections.Values)
            {
                Delete(shapeCollection);
            }
        }

        private void Delete(ShapeCollection shapeCollection)
        {
            _rootEntity.RemoveChild(shapeCollection.Entity);
            shapeCollection.ColorChanged -= OnColorChanged;
            _shapeCollections.Remove(shapeCollection.Color);
        }
    }
}