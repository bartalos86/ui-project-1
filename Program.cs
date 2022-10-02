using blazniva_krizovatka;
using blazniva_krizovatka.Tree;
using System.Drawing;

namespace BlaznivaKrizovatka{

    public class Program{

        public static void Main(String[] args){        
            Map createdMap = CreateMap();
            createdMap.PrintMap();

            string map = "((cervene 2 3 2 h)(oranzove 2 3 4 v)(zlte 2 3 5 v))";
            Map customMap = CreateMap(map);

            DecisionTree tree = new DecisionTree();

            //var solutionTree = tree.TrySolveDFS(createdMap);
            var solutionTree = tree.TrySolveBFS(customMap);

            var stepList = new List<string>();
            string prevStep = "";
            int stepCount = 1;
            while(solutionTree.Parent != null)
            {
                var direction = solutionTree.Car.Orientation == Orientation.HORIZONTAL ? (solutionTree.Direction == 1 ? "RIGHT" : "LEFT") : (solutionTree.Direction == 1 ? "DOWN" : "UP");
                var step = $"{direction} - {solutionTree.Car.Color}";
               
                if(prevStep == step)
                {
                    stepCount++;
                }else if(stepCount > 1)
                {
                    //stepList.Add($"{prevStep} - {stepCount}");
                    //stepList.Add($"{direction} - {solutionTree.Car.Color} - {1}");
                    stepCount = 1;
                }
                else
                {
                    //stepList.Add($"{direction} - {solutionTree.Car.Color} - {1}");
                    stepCount = 1;
                }
                stepList.Add($"{direction} - {solutionTree.Car.Color} - {1}");

                solutionTree = solutionTree.Parent;
                prevStep = step;
            }

            foreach(var line in stepList)
                Console.WriteLine(line);

        }

        public static Map CreateMap(string mapString = null)
        {
             mapString ??= "((cervene 2 3 2 h)(oranzove 2 1 1 h)(zlte 3 2 1 v)(fialove 2 5 1 v)(zelene 3 2 4 v)(svetlomodre 3 6 3 h)(sive 2 5 5 h)(tmavomodre 3 1 6 v))";
            Map newMap = new Map(5, 4);
            string[] parts = mapString.Split("(");
            int id = 0;
            foreach(var part in parts)
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

        public static bool Solve()
        {
            return true;
        }

    }
}
