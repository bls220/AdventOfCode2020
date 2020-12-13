using System;
using System.Collections.Generic;
using System.Linq;

namespace day9
{
    class Program
    {

        static void Main(string[] args)
        {
            var inputs = System.IO.File.ReadAllLines("input.txt")
            .Select(line => long.Parse(line))
            .ToArray();

            const int preambleLength = 25;
            var workingQueue = new Queue<long>(inputs.Take(preambleLength));
            long invalidNumber = -1;
            foreach (var input in inputs.Skip(preambleLength))
            {
                var results = workingQueue.SelectMany(q1 =>
                    workingQueue.Where(q2 => q1 != q2)
                    .Select(q2 => q1 + q2)
                );
                if (!results.Contains(input))
                {
                    invalidNumber = input;
                    break;
                }
                // shift working queue
                workingQueue.Enqueue(input);
                workingQueue.Dequeue();
            }
            Console.WriteLine($"Invalid Number = {invalidNumber}");

            // Scan for contingous set of number that sum to invalidNumber
            bool done = false;
            for (int startIndex = 0; startIndex < inputs.Length && !done; startIndex++)
            {
                workingQueue.Clear();
                foreach (var input in inputs.Skip(startIndex))
                {
                    var sum = workingQueue.Aggregate(0L, (a, b) => a + b);
                    if (sum == invalidNumber)
                    {
                        done = true;
                        break;
                    }
                    if (sum > invalidNumber)
                    {
                        break;
                    }
                    workingQueue.Enqueue(input);
                }
            }
            var sortedQueue = workingQueue.ToList();
            sortedQueue.Sort();
            var first = sortedQueue.First();
            var last = sortedQueue.Last();
            Console.WriteLine($"{first} + {last} = {first + last}");
        }
    }
}
