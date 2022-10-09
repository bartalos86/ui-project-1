

namespace blazniva_krizovatka.Tree
{
    public class DecisionTree
    {
        private List<int> hashes = new List<int>();

        public bool CheckContains(CarDecisionNode node)
        {
            return CheckContains(node.MapConfiguration);
        }


        //Checks if the item is in hash list, if not then adds it to the hashes list
        public bool CheckContains(Map map)
        {
            if (hashes.Contains(map.GetHash()))
                return true;

            hashes.Add(map.GetHash());
            return false;
        }

        //Tries to solve the map using BFS algorithm
        public CarDecisionNode TrySolveBFS(Map map)
        {
            int depth = 1;
            var startingCar = map.CarList[0];
            var node = new CarDecisionNode(1, 0, startingCar, map);
            var queue = new Queue<CarDecisionNode>();
            var totalNodeCount = 1;
            queue.Enqueue(node);
            node.Depth = 1;
            hashes.Clear();

            //Checks is the staring configuration is final
            if (IsFinished(map))
            {
                Console.WriteLine($"Found at depth: {node.Depth}, Total node count: {totalNodeCount} ");
                map.PrintMap();
                return node;
            }


            CheckContains(map);

            while(queue.Count > 0)
            {
                node = queue.Dequeue();

                foreach (var car in node.MapConfiguration.CarList)
                {
                    //Checks if the car can move on the current node's map configuration in positive direction
                    if (node.MapConfiguration.CanMoveCar(car, 1))
                    {
                        //Moves the car in positive directon and returns the new map instance
                        var movedMap = node.MapConfiguration.MoveCar(car, 1);
                        var movedCar = movedMap.CarList.Find(lcar => lcar.Id == car.Id);
                        //Creates the new decision node
                        var newNode = new CarDecisionNode(1, 1, movedCar, movedMap);
                        newNode.Depth = depth;

                        //Check if the new map is already checked if not, then adds the node containing the map as children
                        if (!CheckContains(movedMap))
                        {
                            node.AddChildren(newNode);
                            queue.Enqueue(newNode);
                            totalNodeCount++;
                        }
                        //Check if final position of the red car has been reached
                        if (IsFinished(movedMap))
                        {
                            Console.WriteLine($"Found at depth: {newNode.Depth}, Total node count: {totalNodeCount} ");
                            movedMap.PrintMap();
                            return newNode;
                        }

                    }

                    //Checks if the car can move on the current node's map configuration in negative direction
                    if (node.MapConfiguration.CanMoveCar(car, -1))
                    {
                        //Moves the car in negative directon and returns the new map instance
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

                    }
                   

                }
                    
            }
            //If no soludtions has been found
            Console.WriteLine("No solution");
            Console.WriteLine($"Depth: {node.Depth}, Total node count: {totalNodeCount}");
            return null;
        }

        //Tries to solve the problem using DFS method
        public CarDecisionNode TrySolveDFS(Map map, int depth = 1)
        {
            var startingCar = map.CarList[0];
            var node = new CarDecisionNode(1, 0, startingCar, map);
            var totalNodeCount = 1;
            var maxDepth = 1;
            node.Depth = 1;
            hashes.Clear();

            if (IsFinished(map))
            {
                Console.WriteLine($"Found at depth: {node.Depth}, Total node count: {totalNodeCount} ");
                map.PrintMap();
                return node;
            }

            CheckContains(map);

            //While we not back at the root node
            while (node != null)
            {
                bool wasOperationPreformed = false;
                foreach (var car in node.MapConfiguration.CarList)
                {
                    //Try to move the car in positive direction
                    if (node.MapConfiguration.CanMoveCar(car, 1))
                    {
                        //Move the car
                        var movedMap = node.MapConfiguration.MoveCar(car, 1);
                        var movedCar = movedMap.CarList.Find(lcar => lcar.Id == car.Id);
                        var newNode = new CarDecisionNode(1, 1, movedCar, movedMap);
                        newNode.Depth = depth;
                        bool contains = CheckContains(movedMap);
                        
                        if (!contains)
                        {
                            node.AddChildren(newNode);
                            node = newNode;
                            totalNodeCount++;
                            wasOperationPreformed = true;
                            //Set new maximal depth 
                            maxDepth = maxDepth < newNode.Depth ? newNode.Depth : maxDepth;
                        }

                        if (IsFinished(movedMap))
                        {
                            Console.WriteLine($"Found at depth: {newNode.Depth}, Total node count: {totalNodeCount} ");

                            movedMap.PrintMap();
                            return newNode;

                        }
                        //If new node has been added exit the for loop
                        if (!contains)
                            break;

                    }


                    //Try to move the car in negative direction
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
                            node = newNode;
                            totalNodeCount++;
                            wasOperationPreformed = true;
                            maxDepth = maxDepth < newNode.Depth ? newNode.Depth : maxDepth;

                        }

                        if (IsFinished(movedMap))
                        {
                            Console.WriteLine($"Found at depth: {newNode.Depth}, Total node count: {totalNodeCount} ");
                            movedMap.PrintMap();
                            return newNode;

                        }

                        //If new node has been added exit the for loop
                        if (!contains)
                            break;
                    }
                }

                if (!wasOperationPreformed)
                {
                    node = node.Parent;
                }


            }
            Console.WriteLine("No solution");
            Console.WriteLine($"Max depth: {maxDepth},Total node count: {totalNodeCount}");
            return null;
        }

        //Checks if the red car, with id == 1 is in the reight position
        private bool IsFinished(Map map)
        {
            var redCar = map.CarList.Find(car => car.Id == 1);
            var exitPosition = new Position(map.Width - 2, redCar.Position.Y);

            return redCar.Position.Equals(exitPosition);
        }
    }
}
