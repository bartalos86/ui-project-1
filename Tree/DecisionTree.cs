using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace blazniva_krizovatka.Tree
{
    public class DecisionTree
    {
        DecisionNode root;
        private List<int> hashes = new List<int>();

        public bool CheckContains(CarDecisionNode node)
        {
            return CheckContains(node.MapConfiguration);
        }

        public bool CheckContains(DecisionNode node)
        {
            return CheckContains(node.MapConfiguration);
        }

        public bool CheckContains(Map map)
        {
            if (hashes.Contains(map.GetHash()))
                return true;

            hashes.Add(map.GetHash());
            return false;
        }

        public CarDecisionNode TrySolveBFS(Map map, int depth = 1)
        {
            var startingCar = map.CarList[0];
            var node = new CarDecisionNode(1, 0, startingCar, map);
            var queue = new Queue<CarDecisionNode>();
            var totalNodeCount = 1;
            queue.Enqueue(node);
            node.Depth = 1;
           
           
                CheckContains(map);

            while(queue.Count > 0)
            {
                node = queue.Dequeue();

                foreach (var car in node.MapConfiguration.CarList)
                {
                    if (node.MapConfiguration.CanMoveCar(car, 1))
                    {
                        var movedMap = node.MapConfiguration.MoveCar(car, 1);
                        var movedCar = movedMap.CarList.Find(lcar => lcar.Id == car.Id);
                        var newNode = new CarDecisionNode(1, 1, movedCar, movedMap);
                        newNode.Depth = depth;

                        if (!CheckContains(movedMap))
                        {
                            node.AddChildren(newNode);
                            queue.Enqueue(newNode);
                            totalNodeCount++;
                        }

                        if (IsFinished(movedMap))
                        {
                            Console.WriteLine($"Found at depth: {newNode.Depth}, Total node count: {totalNodeCount} ");

                            movedMap.PrintMap();
                            return newNode;

                        }

                    }


                    if (node.MapConfiguration.CanMoveCar(car, -1))
                    {
                        var movedMap = node.MapConfiguration.MoveCar(car, -1);
                        var movedCar = movedMap.CarList.Find(lcar => lcar.Id == car.Id);

                        var newNode = new CarDecisionNode(-1, 1, movedCar, movedMap);
                        newNode.Depth = depth;

                        if (!CheckContains(movedMap))
                        {
                            node.AddChildren(newNode);
                            queue.Enqueue(newNode);
                            totalNodeCount++;
                        }

                        if (IsFinished(movedMap))
                        {
                            Console.WriteLine($"Found at depth: {newNode.Depth}, Total node count: {totalNodeCount} ");
                            movedMap.PrintMap();
                            return newNode;

                        }


                        // node.MapConfiguration.MoveCar(car, -1);
                    }
                   

                }
                    
            }

           
      

            /* for(int i = 0; i < queue.Count; i++)
            {
                var nextNode = queue.Dequeue();
                TrySolveBFS(nextNode.MapConfiguration, nextNode, depth + 1, queue);
            }*/
            /*foreach (var child in node.Children)
            {
                TrySolveBFS(child.MapConfiguration, child, depth + 1) ;
            }*/


            return null;
        }

        public CarDecisionNode TrySolveDFS(Map map, int depth = 1)
        {
            var startingCar = map.CarList[0];
            var node = new CarDecisionNode(1, 0, startingCar, map);
            var stack = new Stack<CarDecisionNode>();
            var totalNodeCount = 1;
            stack.Push(node);
            node.Depth = 1;

         
                CheckContains(map);

           // node = stack.Pop();

            while (stack.Count > 0)
            {
                // node.MapConfiguration.PrintMap();
                // Console.WriteLine(node.MapConfiguration.GetHash());
                bool wasOperationPreformed = false;
                foreach (var car in node.MapConfiguration.CarList)
                {
                    if (node.MapConfiguration.CanMoveCar(car, 1))
                    {
                        var movedMap = node.MapConfiguration.MoveCar(car, 1);
                        var movedCar = movedMap.CarList.Find(lcar => lcar.Id == car.Id);
                        var newNode = new CarDecisionNode(1, 1, movedCar, movedMap);
                        newNode.Depth = depth;
                        bool contains = CheckContains(movedMap);
                        
                        if (!contains)
                        {
                            node.AddChildren(newNode);
                            stack.Push(newNode);
                            node = newNode;
                            totalNodeCount++;
                            wasOperationPreformed = true;
                        }

                        if (IsFinished(movedMap))
                        {
                            Console.WriteLine($"Found at depth: {newNode.Depth}, Total node count: {totalNodeCount} ");

                            movedMap.PrintMap();
                            return newNode;

                        }

                        if (!contains)
                            break;

                    }
                   


                    if (node.MapConfiguration.CanMoveCar(car, -1))
                    {
                        var movedMap = node.MapConfiguration.MoveCar(car, -1);
                        var movedCar = movedMap.CarList.Find(lcar => lcar.Id == car.Id);

                        var newNode = new CarDecisionNode(-1, 1, movedCar, movedMap);
                        newNode.Depth = depth;

                        bool contains = CheckContains(movedMap);
                        if (!contains)
                        {
                            node.AddChildren(newNode);
                            stack.Push(newNode);
                            node = newNode;
                            totalNodeCount++;
                            wasOperationPreformed = true;

                        }

                        if (IsFinished(movedMap))
                        {
                            Console.WriteLine($"Found at depth: {newNode.Depth}, Total node count: {totalNodeCount} ");
                            movedMap.PrintMap();
                            return newNode;

                        }

                        if (!contains)
                            break;


                        // node.MapConfiguration.MoveCar(car, -1);
                    }



                }

                if(!wasOperationPreformed)
                node = node.Parent;


            }




            /* for(int i = 0; i < queue.Count; i++)
            {
                var nextNode = queue.Dequeue();
                TrySolveBFS(nextNode.MapConfiguration, nextNode, depth + 1, queue);
            }*/
            /*foreach (var child in node.Children)
            {
                TrySolveBFS(child.MapConfiguration, child, depth + 1) ;
            }*/


            return null;
        }

        public void TrySolve(Map mapToSolve)
        {
            foreach (Car car in mapToSolve.CarList)
            {
                var subtree = CreateDFSSubtreeForCar(car, mapToSolve).Item1;

                while (subtree.NegativeDirection != null)
                {
                    Console.WriteLine("----------------");

                    subtree.MapConfiguration.PrintMap();
                    Console.WriteLine("################");
                    foreach (Car secondCar in subtree.MapConfiguration.CarList)
                    {
                        if (car.Id == secondCar.Id)
                            continue;
                        // var subsub = CreateDFSSubtreeForCar(car, null, subtree);
                        TrySolve(subtree.MapConfiguration);

                    }
                    subtree = subtree.PositiveDirection;
                }

            }
        }

        public (DecisionNode, bool) CreateDFSSubtreeForCar(Car car, Map map, DecisionNode node = null, List<int> subHashes = null)
        {
            subHashes ??= new List<int>();

            if (node != null)
                map = node.MapConfiguration;

            node ??= new DecisionNode(car, map);



            while (map.CanMoveCar(car, -1) && !subHashes.Contains(map.GetHash()))
            {
                map.MoveCar(car, -1);
                if (IsFinished(map))
                {
                    map.PrintMap();
                    return (node, true);

                }
                node.NegativeDirection = new DecisionNode(car, map);
                var tempNode = node;
                node = node.NegativeDirection;
                node.PositiveDirection = tempNode;//parent
            }


            while (map.CanMoveCar(car, 1))
            {
                map.MoveCar(car, 1);

                if (IsFinished(map))
                {
                    map.PrintMap();
                    return (node, true);

                }

                if (node.PositiveDirection != null)
                    node = node.PositiveDirection;
                else
                {
                    node.PositiveDirection = new DecisionNode(car, map);
                    var tempNode = node;
                    node = node.PositiveDirection;
                    node.NegativeDirection = tempNode;
                }


            }
            var rootNode = node;
            /*  while (rootNode.NegativeDirection != null)
                  rootNode = rootNode.NegativeDirection;*/

            return (rootNode, false);
        }

        public bool SolveMapDFS(Map mapToSolve, DecisionNode node = null)
        {
            foreach (Car car in mapToSolve.CarList)
            {
                if (root == null)
                {
                    root = new DecisionNode(car, mapToSolve);
                }

                if (mapToSolve.CanMoveCar(car, 1))
                {

                    if (node == null)
                    {
                        node = root;
                    }
                    bool contains = CheckContains(node);

                    if (!contains)
                        mapToSolve.MoveCar(car, 1);

                    if (car.Orientation == Orientation.HORIZONTAL)
                    {
                        node.PositiveDirection ??= new DecisionNode(car, mapToSolve);
                    }
                    else
                    {
                        node.PositiveDirection ??= new DecisionNode(car, mapToSolve);

                    }



                    Console.WriteLine("----------------------");
                    mapToSolve.PrintMap();
                    Console.WriteLine("#####################");

                    if (!contains)
                    {
                        SolveMapDFS(mapToSolve, node.PositiveDirection);
                    }
                    else
                    {
                        if (node.PositiveDirection.PositiveDirection != null)
                            SolveMapDFS(mapToSolve, node.PositiveDirection.PositiveDirection);
                    }



                }
                //else if(!CheckContains(node))
                //{

                //}


                if (IsFinished(mapToSolve))
                {
                    mapToSolve.PrintMap();
                    return true;

                }


            }

            foreach (var car in mapToSolve.CarList)
            {
                if (mapToSolve.CanMoveCar(car, -1))
                {

                    if (node == null)
                    {
                        node = root;
                    }

                    bool contains = mapToSolve.MoveCar(car, -1) != null;

                    if (!contains)
                        mapToSolve.MoveCar(car, -1);

                    if (car.Orientation == Orientation.HORIZONTAL)
                    {
                        node.NegativeDirection ??= new DecisionNode(car, mapToSolve);
                    }
                    else
                    {
                        node.NegativeDirection ??= new DecisionNode(car, mapToSolve);

                    }


                    Console.WriteLine("----------------------");
                    mapToSolve.PrintMap();
                    Console.WriteLine("#####################");

                    if (!contains)
                    {
                        SolveMapDFS(mapToSolve, node.NegativeDirection);
                    }
                    else
                    {
                        if (node.NegativeDirection.NegativeDirection != null)
                            SolveMapDFS(mapToSolve, node.NegativeDirection.NegativeDirection);

                    }
                }

                if (IsFinished(mapToSolve))
                {
                    mapToSolve.PrintMap();
                    return true;

                }
            }

            return false;
        }

        private bool IsFinished(Map map)
        {
            var redCar = map.CarList.Find(car => car.Id == 1);
            var exitPosition = new Position(map.Width - 2, redCar.Position.Y);



            return redCar.Position.Equals(exitPosition);
        }
    }
}
