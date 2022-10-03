
namespace blazniva_krizovatka
{
    [Serializable]
    public class Car
    {
        public Position Position { get; set; }
        public Orientation Orientation { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }

        public int Id { get; set; }

        private static int ID = 1;

        public Car()
        {
            Id = ID++;
        }

        public Car(Position position, Orientation orientation, string color, int id) : this()
        {
            Position = position;
            Orientation = orientation;
            Color = color;
            Id = id;
        }

        public void ForceMove(int direction)
        {
            if(this.Orientation == Orientation.HORIZONTAL)
            {
                if (direction == 1)
                {
                    this.Position.X++;
                }
                else if(direction == -1)
                {
                    this.Position.X--;

                }
            }
            else
            {
                if (direction == 1)
                {
                    this.Position.Y++;
                }
                else if (direction == -1)
                {
                    this.Position.Y--;

                }
            }
          
        }

    }
}
