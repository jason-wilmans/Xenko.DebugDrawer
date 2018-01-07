using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Games;
using SiliconStudio.Xenko.Rendering;
using Xenko.DebugDrawer.Shapes;

namespace Xenko.DebugDrawer
{
    public class DebugDrawerSystem : GameSystemBase, IShapePropetyChangedHandler
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

            shape.ChangeHandler = this;

            var geometries = EnsureEntities(shape.Color);
            geometries.Add(shape);
        }

        public override bool BeginDraw()
        {
            //bool hasDrawn = false;
            foreach (var shapeCollection in _shapeCollections.Values)
            {
                //if (shapeCollection.IsModified)
                //{
                //    hasDrawn |= shapeCollection.IsModified;
                shapeCollection.UpdateMesh();
            //}
            }

            return true;

            //return hasDrawn && base.BeginDraw();
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

            if (!_shapeCollections.ContainsKey(color))
            {
                var shapeCollection = new ShapeCollection(color, GraphicsDevice, _game.GraphicsContext);
                _shapeCollections[color] = shapeCollection;
                _rootEntity.AddChild(shapeCollection.Entity);
            }

            return _shapeCollections[color];
        }

        public void OnPropertyChanged(AShape shape)
        {
            //EnsureEntities(shape.Color);

            //var collection = _shapeCollections[shape.Color];
            //collection.IsModified = true;
            //if (!collection.Contains(shape))
            //{
            //    foreach (var otherCollection in _shapeCollections)
            //    {
            //        if (otherCollection.Value.Remove(shape))
            //        {
            //            otherCollection.Value.IsModified = true;
            //        }
            //    }

            //    collection.Add(shape);
            //}
        }
    }
}