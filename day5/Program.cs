using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace day5
{
    class Program
    {

        private enum Partition
        {
            Lower = 0,
            Upper = 1
        }

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            var passParts = lines.Select(
                line =>
                    new[]
                    {
                        line.Take(7).Select(c => c == 'F' ? Partition.Lower : Partition.Upper),
                        line.TakeLast(3).Select(c => c == 'L' ? Partition.Lower : Partition.Upper)
                    });
            var seatIds = passParts
            .Select(pass => new { Row = BSP(pass[0], 127, 0), Column = BSP(pass[1], 7, 0) })
            .Select(pos => 8 * pos.Row + pos.Column)
            .OrderBy(x => x);

            // answer 1: Console.WriteLine(seatIds.Max());

            int lastId = seatIds.First();
            foreach (var id in seatIds.Skip(1))
            {
                if (id - lastId > 1)
                {
                    Console.WriteLine(lastId + 1);
                    return;
                }
                lastId = id;
            }
        }

        private static int BSP(IEnumerable<Partition> input, int upper, int lower)
        {
            if (upper == lower)
            {
                return upper;
            }

            float midPoint = (upper - lower) / 2f + lower;

            switch (input.First())
            {
                case Partition.Lower:
                    return BSP(input.Skip(1), (int)Math.Floor(midPoint), lower);
                case Partition.Upper:
                    return BSP(input.Skip(1), upper, (int)Math.Ceiling(midPoint));
                default:
                    throw new ApplicationException($"Invalid {nameof(Partition)}");
            }

        }
    }
}
