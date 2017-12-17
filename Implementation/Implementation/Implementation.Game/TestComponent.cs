using DebugDrawing;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Graphics.GeometricPrimitives;
using SiliconStudio.Xenko.Rendering;

namespace Implementation
{
    public class TestComponent : StartupScript
    {
        private GeometricPrimitive _geometricPrimitive;
        private EffectInstance _instance;

        public override void Start()
        {
            base.Start();
            
            Effect effect = EffectSystem.LoadEffect("Shaders/DebugEffect.xkfx").WaitForResult();

            _geometricPrimitive = GeometricPrimitive.Sphere.New(GraphicsDevice, 1);

            _instance = new EffectInstance(effect);
        }
    }
}