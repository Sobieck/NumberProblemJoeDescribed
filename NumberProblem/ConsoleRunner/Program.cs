using System;
using NumberProblem;

namespace ConsoleRunner
{
    class Program
    {
        private static NumberCombinationGenerator numberGenerator = new NumberCombinationGenerator();
        private static NumberCombinationGeneratorParallel numberCombinationGeneratorParallel = new NumberCombinationGeneratorParallel();

        static void Main(string[] args)
        {
            while (true)
            {
                Run();
            }
        }

        private static void Run()
        {
            Console.WriteLine("How many do you want to generate?");

            var numberToGenerate = int.Parse(Console.ReadLine());

            Console.WriteLine($"Generating {numberToGenerate}");

            Console.WriteLine("Generate in Parallel(0) or Serially(1)");

            var choice = int.Parse(Console.ReadLine());

            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (choice == 0)
            {
                Console.WriteLine("Generating in Parallel");

                numberCombinationGeneratorParallel.Generate(numberToGenerate);
            }

            if (choice == 1)
            {
                Console.WriteLine("Generating Serially");

                numberGenerator.Generate(numberToGenerate);
            }

            watch.Stop();

            var timeElapsed = watch.Elapsed.TotalSeconds;

            Console.WriteLine($"Seconds Elapsed: {timeElapsed}");
        }
    }
}
