
namespace blazniva_krizovatka.Tree
{
    //Nodes which iis used to build the solution trees and its also responsible for saving the data required for calculating the steps
    public class CarDecisionNode
    {

        public CarDecisionNode(int direction, int step, Car car, Map mapConfiguration)
        {
            Direction = direction;
            Step = step;
            Car = car;
            MapConfiguration = mapConfiguration;
            Children = new List<CarDecisionNode>();
        }

        public int Direction { get; set; }
        public int Step { get; set; }
        public int Depth { get; set; }
        //The car that was moved
        public Car Car { get; set; }

        public Map MapConfiguration { get; set; }

        public List<CarDecisionNode> Children { get; private set; }
        public CarDecisionNode Parent { get; private set; }

        //Add a child node, sets automatically up the parent
        public void AddChildren(CarDecisionNode node)
        {
            node.Depth = Depth + 1;
            node.Parent = this;
            Children.Add(node);
        }

    }
}
