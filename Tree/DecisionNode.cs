
namespace blazniva_krizovatka.Tree
{
    public class DecisionNode
    {

        public DecisionNode(Car car, Map map)
        {
            Car = car;
            //Direction = direction;
            //Step = step;
            MapConfiguration = map;
            Orientation = car.Orientation;
        }
        public int Direction { get; set; }
        public int Step { get; set; }
        public Car Car { get; set; }

        public Map MapConfiguration { get; set; }

        public Orientation Orientation { get; set; }

        public DecisionNode PositiveDirection { get; set; }
        public DecisionNode NegativeDirection { get; set; }

        /* public DecisionNode Left { get; set; }
         public DecisionNode Right { get; set; }
         public DecisionNode Up { get; set; }
         public DecisionNode Down { get; set; }*/

    }
}
