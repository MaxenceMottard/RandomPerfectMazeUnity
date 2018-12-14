using System;
using System.Collections.Generic;

namespace MazeBankCSHARP_Classes
{
    public class Maze
    {
        public Cell[,] Table { get; set; }
        public int Size { get; set; }
        private int NumberCell;
        private int NumberVisitedCell;
        public Cell StartCell { get; set; }
        public Cell EndCell { get; set; }

        public Maze(int size)
        {
            Table = new Cell[size, size];
            Size = size;
            NumberCell = Size * Size;
            NumberVisitedCell = 0;


            CreateTableCell();

            GenerateMaze();

            foreach (Cell cell in Table)
            {
                cell.ResetIsVisited();
            }

            SetStartEndCells();
            GeneratePath();
        }

        private void CreateTableCell()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Table[x, y] = new Cell(x, y);
                }
            }
        }

        private void GenerateMaze()
        {
            Stack<Cell> visitedStack = new Stack<Cell>();
            Random rnd = new Random();
            Cell target = Table[0, 0];

            while (NumberVisitedCell < NumberCell)
            {
                List<Cell> adjacentCells = GetAdjacentCells(target);
                if (!target.IsVisited)
                {
                    NumberVisitedCell++;
                }

                target.IsVisited = true;

                if (adjacentCells.Count == 0)
                {
                    target = visitedStack.Pop();
                }
                else
                {
                    visitedStack.Push(target);

                    int nextCellIndex = rnd.Next(0, adjacentCells.Count);
                    Cell nextCell = adjacentCells[nextCellIndex];

                    DeleteCommonWall(target, nextCell);
                    target = nextCell;
                }
            }
        }

        private void SetStartEndCells()
        {
            Random rnd = new Random();
            int xStart = rnd.Next(0, Size - 1);
            int yStart = rnd.Next(0, Size - 1);
            StartCell = Table[xStart, yStart];
            StartCell.IsInPath = true;

            int xEnd;
            int yEnd;
            do
            {
                xEnd = rnd.Next(0, Size);
            } while (xStart - xEnd > -3 && xStart - xEnd < 3);

            do
            {
                yEnd = rnd.Next(0, Size);
            } while (yStart - yEnd > -3 && yStart - yEnd < 3);

            EndCell = Table[xEnd, yEnd];
            EndCell.IsInPath = true;
        }

        private void GeneratePath()
        {
            Stack<Cell> visitedStack = new Stack<Cell>();
            Random rnd = new Random();
            Cell target = StartCell;

            while (target != EndCell)
            {
                List<Cell> adjacentCells = GetAdjacentOpenCells(target);
                target.IsVisited = true;

                if (adjacentCells.Count == 0)
                {
                    target.IsInPath = false;
                    target = visitedStack.Pop();
                }
                else
                {
                    target.IsInPath = true;
                    visitedStack.Push(target);

                    int nextCellIndex = rnd.Next(0, adjacentCells.Count);
                    target = adjacentCells[nextCellIndex];
                }
            }
        }

        private List<Cell> GetAdjacentCells(Cell cell)
        {
            List<Cell> cellTable = new List<Cell>();

            int cellX = cell.Coord.X;
            int cellY = cell.Coord.Y;

            Cell TopCell;
            Cell RightCell;
            Cell BottomCell;
            Cell LeftCell;

            if (cellY > 0)
            {
                TopCell = Table[cellX, cellY - 1];
                if (!TopCell.IsVisited)
                {
                    cellTable.Add(TopCell);
                }
            }

            if (cellX < Size - 1)
            {
                RightCell = Table[cellX + 1, cellY];
                if (!RightCell.IsVisited)
                {
                    cellTable.Add(RightCell);
                }
            }

            if (cellY < Size - 1)
            {
                BottomCell = Table[cellX, cellY + 1];
                if (!BottomCell.IsVisited)
                {
                    cellTable.Add(BottomCell);
                }
            }

            if (cellX > 0)
            {
                LeftCell = Table[cellX - 1, cellY];
                if (!LeftCell.IsVisited)
                {
                    cellTable.Add(LeftCell);
                }
            }

            return cellTable;
        }

        private List<Cell> GetAdjacentOpenCells(Cell cell)
        {
            List<Cell> cellTable = new List<Cell>();

            int cellX = cell.Coord.X;
            int cellY = cell.Coord.Y;

            Cell TopCell;
            Cell RightCell;
            Cell BottomCell;
            Cell LeftCell;

            if (!cell.Top)
            {
                TopCell = Table[cellX, cellY - 1];
                if (!TopCell.IsVisited)
                {
                    cellTable.Add(TopCell);
                }
            }

            if (!cell.Right)
            {
                RightCell = Table[cellX + 1, cellY];
                if (!RightCell.IsVisited)
                {
                    cellTable.Add(RightCell);
                }
            }

            if (!cell.Bottom)
            {
                BottomCell = Table[cellX, cellY + 1];
                if (!BottomCell.IsVisited)
                {
                    cellTable.Add(BottomCell);
                }
            }

            if (!cell.Left)
            {
                LeftCell = Table[cellX - 1, cellY];
                if (!LeftCell.IsVisited)
                {
                    cellTable.Add(LeftCell);
                }
            }

            return cellTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstCell"></param>
        /// <param name="secondCell"></param>
        private void DeleteCommonWall(Cell firstCell, Cell secondCell)
        {
            if (firstCell.Coord.X == secondCell.Coord.X - 1)
            {
                //    CELL AT RIGHT
                firstCell.Right = false;
                secondCell.Left = false;
            }
            else if (firstCell.Coord.X == secondCell.Coord.X + 1)
            {
                //    CELL AT LEFT
                firstCell.Left = false;
                secondCell.Right = false;
            }
            else if (firstCell.Coord.Y == secondCell.Coord.Y - 1)
            {
                //    CELL AT BOTTOM
                firstCell.Bottom = false;
                secondCell.Top = false;
            }
            else
            {
                //    CELL AT TOP
                firstCell.Top = false;
                secondCell.Bottom = false;
            }
        }

        public override string ToString()
        {
            String result = "";
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    result += Table[x, y].ToString();
                }

                result += "\n";
            }

            return result;
        }
    }
}