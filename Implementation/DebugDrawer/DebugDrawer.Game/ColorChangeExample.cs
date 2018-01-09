﻿using System;
using System.Threading.Tasks;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using Xenko.DebugDrawer;
using Xenko.DebugDrawer.Shapes;

namespace DebugDrawer
{
    public class ColorChangeExample : AsyncScript
    {
        private Box _box1;
        private Box _box2;

        public override async Task Execute()
        {
            var debug = DebugDrawerSystem.Instance;

            _box1 = new Box(new Vector3(-.5f, 0, 0), new Vector3(0.3f), Color.CadetBlue);
            debug.Add(_box1);

            _box2 = new Box(new Vector3(.5f, 0, 0), new Vector3(0.3f), Color.CadetBlue);
            debug.Add(_box2);

            while (Game.IsRunning)
            {
                await Script.NextFrame();

                //_box2.Color = _box2.Color == Color.Orange ? Color.White : Color.Orange;
            }
        }
    }
}