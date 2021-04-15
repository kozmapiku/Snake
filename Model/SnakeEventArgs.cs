using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Model
{
    public class SnakeEventArgs : EventArgs
    {
        private Int32 _collectedEggs;
        private Boolean _isOver;
        public Int32 GameEggsCount { get { return _collectedEggs; } }
        public Boolean IsOver { get { return _isOver; } }
        public SnakeEventArgs(Boolean isOver, Int32 collectedEggs)
        {
            _collectedEggs = collectedEggs;
            _isOver = isOver;
        }
    }
}
