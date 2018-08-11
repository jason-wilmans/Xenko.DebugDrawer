using System;
using System.Collections.Generic;
using Xenko.Core.Mathematics;

namespace Xenko.DebugDrawer.Shapes
{
    public abstract class AShape
    {
        private IShapePropetyChangedHandler _changeHandler;
        private Color _color;

        internal IShapePropetyChangedHandler ChangeHandler
        {
            get { return _changeHandler; }
            set { _changeHandler = value; }
        }

        public virtual Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyPropertyChanged();
            }
        }

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