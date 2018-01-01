using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering;
using SiliconStudio.Xenko.Rendering.Materials;
using SiliconStudio.Xenko.Rendering.Materials.ComputeColors;

namespace Xenko.DebugDrawer
{
    internal static class Materials
    {
        public static Material CreateDebugMaterial(Color color, bool emissive, GraphicsDevice device)
        {
            var descriptor = new MaterialDescriptor();
            var computeColor = new ComputeColor(color);
            if (emissive)
            {
                descriptor.Attributes.Emissive = new MaterialEmissiveMapFeature(computeColor);
            }
            else
            {
                descriptor.Attributes.Diffuse = new MaterialDiffuseMapFeature(computeColor);
                descriptor.Attributes.DiffuseModel = new MaterialDiffuseLambertModelFeature();
            }

            return Material.New(device, descriptor);
        }
    }
}