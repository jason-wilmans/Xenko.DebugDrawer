using System;
using System.Collections.Generic;
using System.IO;
using SiliconStudio.Xenko.Games;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Graphics.GeometricPrimitives;
using SiliconStudio.Xenko.Rendering;
using SiliconStudio.Xenko.Shaders;
using SiliconStudio.Xenko.Shaders.Compiler;

namespace DebugDrawing
{
    public class DebugDrawer : GameSystemBase
    {
        private readonly IGame _game;
        private GeometricPrimitive _geometricPrimitive;
        private EffectInstance _instance;
        private bool _isInitialized;

        public DebugDrawer(IGame game) : base(game.Services)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));

            _game = game;
            Enabled = true;
            Visible = true;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!_isInitialized)
                _isInitialized = TryInitialize();

            if (!_isInitialized) return;

            _geometricPrimitive.Draw(_game.GraphicsContext, _instance);
        }

        private bool TryInitialize()
        {
            if (_game.GraphicsDevice == null) return false;

            var effectSystem = _game.Services.GetService<EffectSystem>();
            Effect effect = effectSystem.LoadEffect("Shaders/DebugEffect.xkfx").WaitForResult();

            _geometricPrimitive = GeometricPrimitive.Sphere.New(_game.GraphicsDevice, 1);
            
            _instance = new EffectInstance(effect);
            return true;
        }
    }
}