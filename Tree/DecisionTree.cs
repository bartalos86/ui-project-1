

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
            hashes.Clear();

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

                    }
                   

                }
                    
            }

           
   
            return null;
        }

        public CarDecisionNode TrySolveDFS(Map map, int depth = 1)
        {
            var startingCar = map.CarList[0];
            var node = new CarDecisionNode(1, 0, startingCar, map);
            var totalNodeCount = 1;
            node.Depth = 1;
            hashes.Clear();

            if (IsFinished(map))
            {
                Console.WriteLine($"Found at depth: {node.Depth}, Total node count: {totalNodeCount} ");
                map.PrintMap();
                return node;
            }

            CheckContains(map);

            while (node != null)
            {
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
                }

                if (!wasOperationPreformed)
                {
                    node = node.Parent;
                }


            }

            return null;
        }

        private bool IsFinished(Map map)
        {
            var redCar = map.CarList.Find(car => car.Id == 1);
            var exitPosition = new Position(map.Width - 2, redCar.Position.Y);



            return redCar.Position.Equals(exitPosition);
        }
    }
}
