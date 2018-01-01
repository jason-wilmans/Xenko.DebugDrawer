using System;
using System.Collections.Generic;
using SiliconStudio.Core.Mathematics;

namespace Xenko.DebugDrawer.Shapes
{
    public abstract class AShape
    {
        private IShapePropetyChangedHandler _changeHandler;

        internal IShapePropetyChangedHandler ChangeHandler
        {
            get { return _changeHandler; }
            set { _changeHandler = value; }
        }

        public virtual Color Color { get; set; }

        public abstract IEnumerable<Line> Lines { get; }

        protected AShape(Color color)
        {
            Color = color;
        }

        protected void NotifyPropertyChanged()
        {
            if (ChangeHandler != null)
            {
                ChangeHandler.OnPropertyChanged(this);
            }
        }
    }
}