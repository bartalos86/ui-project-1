
namespace blazniva_krizovatka
{
    //Car class contains car specific information such as its position, orientation, color, size and id
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

    }
}
