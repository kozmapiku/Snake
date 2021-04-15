using Snake.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Persistence
{
    public enum TableObject { Wall, Egg, Snake, Empty}
    public class SnakeTable
    {
        private Int32 _tableSize; //tábla mérete
        private TableObject[,] _fieldValues; //tábla

        public TableObject this[Int32 x, Int32 y] { get { return GetValue(x,y); } }
        public Int32 Size { get { return _tableSize; } }

        public TableObject GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y];
        }
        public SnakeTable(Int32 tableSize)
        {
            if(tableSize < 0)
            {
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");
            }

            _tableSize = tableSize;
            _fieldValues = new TableObject[tableSize, tableSize];
        }

        public Boolean IsEmpty(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == TableObject.Empty;
        }
        public Boolean IsEgg(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == TableObject.Egg;
        }
        public Boolean IsWall(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == TableObject.Wall;
        }
        public void SetValue(Int32 x, Int32 y, TableObject value)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if (value < 0 || value > TableObject.Empty)
                throw new ArgumentOutOfRangeException("value", "The value is out of range.");

            _fieldValues[x, y] = value;
        }
        public TableObject GetNextObject(Tuple<Int32,Int32> xy, Direction d)
        {
            Int32 x = xy.Item1;
            Int32 y = xy.Item2;
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if ((Int32)d < 0 || (Int32)d > 3)
                throw new ArgumentOutOfRangeException("d", "Direction is incorrect.");

            try
            {
                switch (d)
                {
                    case Direction.Down:
                        return _fieldValues[x+1, y];
                    case Direction.Left:
                        return _fieldValues[x, y-1];
                    case Direction.Up:
                        return _fieldValues[x-1, y];
                    case Direction.Right:
                        return _fieldValues[x, y+1];
                    default:
                        throw new ArgumentOutOfRangeException("d", "Direction is incorrect.");
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return TableObject.Wall;
            }
        }
        public Tuple<Int32, Int32> GetNextXY(Tuple<Int32, Int32> xy, Direction d)
        {
            Int32 x = xy.Item1;
            Int32 y = xy.Item2;
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if ((Int32)d < 0 || (Int32)d > 3)
                throw new ArgumentOutOfRangeException("d", "Direction is incorrect.");

            switch (d)
            {
                case Direction.Down:
                    return Tuple.Create(x + 1, y);
                case Direction.Left:
                    return Tuple.Create(x, y - 1);
                case Direction.Up:
                    return Tuple.Create(x - 1, y);
                case Direction.Right:
                    return Tuple.Create(x, y + 1);
                default:
                    throw new ArgumentOutOfRangeException("xy", "Something went wrong!");
            }
        }

    }
}
