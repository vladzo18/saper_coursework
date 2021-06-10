using System;
using System.Windows.Controls;

namespace Saper_Coursework
{
    [Serializable]
    class GameCell : Button
    {
        public bool isMine;
        public bool isOpened;
        public bool isFlaged;
        public int minesAmountAround = 0;
        public int xCoordinate,
                   yCoordinate;
    }
}
