using blazniva_krizovatka;
using blazniva_krizovatka.Tree;
using System.Diagnostics;
using System.Drawing;

namespace BlaznivaKrizovatka
{

    public class Program
    {

        public static void Main(String[] args)
        {


            TestCommonMaps();





            string map = "((cervene 2 3 2 h)(oranzove 2 3 4 v)(zlte 2 3 5 v))";
            Map customMap = CreateMap(map, 5, 4);

          

        }

        public static void TestCommonMaps()
        {
            List<string> maps = new List<string>
            {
                "((cervene 2 3 3 h)(oranzove 3 2 5 v)(zlte 3 1 3 h)(fialove 4 1 2 v)(zelene 5 5 2 h))",
                "((cervene 2 3 5 h)(oranzove 3 5 3 h)(zlte 3 2 4 v))",
                "((cervene 2 3 1 h)(oranzove 3 2 4 v)(zlte 3 1 5 v)(fialove 2 4 5 h)(zelene 3 5 3 h))",
                "((cervene 2 3 2 h)(oranzove 2 1 1 h)(zlte 3 2 1 v)(fialove 2 5 1 v)(zelene 3 2 4 v)(svetlomodre 3 6 3 h)(sive 2 5 5 h)(tmavomodre 3 1 6 v))"
            };


            foreach(var map in maps)
            {
                Map createdMap = CreateMap(map);
                Console.WriteLine("Starting map");
                createdMap.PrintMap();

                TestCustomMap(createdMap);
            }
           
        }

        public static void TestCustomMap(Map createdMap)
        {
            DecisionTree tree = new DecisionTree();
            Console.WriteLine("BFS solution");
            var solutionTreeBFS = MeasurePerformance(() => tree.TrySolveBFS(createdMap));
             PrintSolution(solutionTreeBFS);


            Console.WriteLine("==============");
            Console.WriteLine("DFS solution");
            var solutionTreeDFS = MeasurePerformance(() => tree.TrySolveDFS(createdMap));
            Console.WriteLine("-----------------------------------------------");
        }

        public static T MeasurePerformance<T>(Func<T> methodToTest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = methodToTest.Invoke();
            stopwatch.Stop();
            Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}ms");
            return result;
        }

        public static void PrintSolution(CarDecisionNode solutionTree)
        {
            if (solutionTree == null)
            {
                Console.WriteLine("No solution");
                return;
            }

            Stack<Step> solutionStack = new Stack<Step>();

            while (solutionTree.Parent != null)
            {
                var direction = solutionTree.Car.Orientation == Orientation.HORIZONTAL ? (solutionTree.Direction == 1 ? "VPRAVO" : "VLAVO") : (solutionTree.Direction == 1 ? "DOLE" : "HORE");
                var carColor = solutionTree.Car.Color;

                if (solutionStack.Count == 0)
                {
                    solutionStack.Push(new Step() { Direction = direction, Car = carColor, Count = 1 });
                    solutionTree = solutionTree.Parent;
                    continue;
                }

                if (direction == solutionStack.Peek().Direction && carColor == solutionStack.Peek().Car)
                {
                    solutionStack.Peek().Count++;
                }
                else
                {
                    solutionStack.Push(new Step() { Direction = direction, Car = carColor, Count = 1 });
                }

                solutionTree = solutionTree.Parent;

            }

            foreach (var step in solutionStack)
                Console.WriteLine(step);
        }

        public static Map CreateMap(string mapString = null, int width = 6, int height = 6)
        {
            Map newMap = new Map(width, height);
            string[] parts = mapString.Split("(");
            int id = 0;
            foreach (var part in parts)
            {
                if (string.IsNullOrEmpty(part))
                    continue;

                var cleaned = part.Replace(")", "");
                var data = cleaned.Split(" ");


                var orientation = data[4] == "h" ? Orientation.HORIZONTAL : Orientation.VERTICAL;
                var car = new Car()
                {
                    Position = new Position(int.Parse(data[3]) - 1, int.Parse(data[2]) - 1),
                    Color = data[0],
                    Orientation = orientation,
                    Size = int.Parse(data[1]),
                    Id = ++id
                };

                newMap.AddCar(car);

            }

            return newMap;
        }

    }
}
