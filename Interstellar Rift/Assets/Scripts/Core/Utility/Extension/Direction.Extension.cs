using System;
using Core.Object.Module;

namespace Core.Utility.Extension
{
    /// <summary>
    /// Provides extension methods for computing derived <see cref="Direction"/> values, such as the opposite direction or a direction rotated between two reference directions.
    /// </summary>
    public static class Direction_Extension
    {
        public static Direction Opposite(this Direction direction)
        {
            return direction switch
            {
                Direction.Up    => Direction.Down,
                Direction.Left  => Direction.Right,
                Direction.Right => Direction.Left,
                Direction.Down  => Direction.Up,
                _               => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Direction Rotate(this Direction direction, Direction from, Direction to)
        {
            var steps = Rotate(from, to);
            if (steps == 0)
            {
                return direction;
            }

            var index = ToIndex(direction);

            return FromIndex((index + steps) % 4);
        }

        private static int Rotate(Direction from, Direction to)
        {
            return (ToIndex(to) - ToIndex(from) + 4) % 4;
        }

        private static int ToIndex(Direction direction)
        {
            return direction switch
            {
                Direction.Up    => 0,
                Direction.Left  => 1,
                Direction.Down  => 2,
                Direction.Right => 3,
                _               => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        private static Direction FromIndex(int index)
        {
            return index switch
            {
                0 => Direction.Up,
                1 => Direction.Left,
                2 => Direction.Down,
                3 => Direction.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
        }
    }
}
