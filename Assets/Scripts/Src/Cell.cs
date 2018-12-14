using System;

namespace MazeBankCSHARP_Classes
{
    public class Cell
    {
        public bool Top { get; set; }
        public bool Right { get; set; }
        public bool Bottom { get; set; }
        public bool Left { get; set; }
        public bool IsVisited { get; set; }
        public bool IsInPath { get; set; }
        public Coordinate Coord { get; set;  }

        public Cell(int X, int Y)
        {
            Top = true;
            Right = true;
            Bottom = true;
            Left = true;
            IsVisited = false;
            IsInPath = false;
            Coord = new Coordinate(X, Y);
        }

        public void ResetIsVisited()
        {
            IsVisited = false;
        }

        public override string ToString()
        {
            return ReturWallLetter(Top, "T") + ReturWallLetter(Right, "R") +
                   ReturWallLetter(Bottom, "B") + ReturWallLetter(Left, "L");
        }
        
        private string ReturWallLetter(bool wallDirection, string letter)
        {
            if (wallDirection)
            {
                return letter;
            }

            return "_";
        }
    }
}