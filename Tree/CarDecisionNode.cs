
namespace blazniva_krizovatka.Tree
{
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
        public Car Car { get; set; }

        public Map MapConfiguration { get; set; }

        public List<CarDecisionNode> Children { get; set; }
        public CarDecisionNode Parent { get; set; }

        public void AddChildren(CarDecisionNode node)
        {
            node.Depth = Depth + 1;
            node.Parent = this;
            Children.Add(node);
        }

    }
}
