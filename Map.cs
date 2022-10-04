
using System.Text;

namespace blazniva_krizovatka
{
    [Serializable]
    public class Map
    {
        public string[][] Values { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Car> CarList { get; set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Values = new string[height][];
            CarList = new List<Car>();
            for (int i = 0; i < height; i++)
            {
                Values[i] = new string[width];
            }
        }

        public bool AddCar(Car car)
        {
            bool isPossible = CheckInsert(car.Position, car.Color, car.Size, car.Orientation);

            if (!isPossible)
                return false;

            CarList.Add(car);
            var position = car.Position;

            for (int i = 0; i < car.Size; i++)
            {
                if (car.Orientation == Orientation.HORIZONTAL)
                {
                    Values[position.Y][position.X + i] = car.Id.ToString();
                }
                else
                {
                    Values[position.Y + i][position.X] = car.Id.ToString();

                }
            }

            return true;

        }

        private bool CheckInsert(Position position, string color, int size, Orientation orientation)
        {
            try
            {
                for (int i = 0; i < size; i++)
                {
                    if (orientation == Orientation.HORIZONTAL)
                    {
                        if (!string.IsNullOrEmpty(Values[position.Y][position.X + i]))
                            return false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Values[position.Y + i][position.X]))
                            return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Map MoveCar(Car car, int direction)
        {
            var newMap = this.DeepClone();
            var newCar = newMap.CarList.Find(ncar => ncar.Id == car.Id);

            if (!CanMoveCar(newCar, direction))
            {
                return null;
            }


            if (car.Orientation == Orientation.HORIZONTAL)
            {
                if (direction == 1)
                {
                    newMap.Values[newCar.Position.Y][newCar.Position.X + (newCar.Size)] = car.Id.ToString();
                    newMap.Values[newCar.Position.Y][newCar.Position.X] = null;
                    newCar.Position.X++;

                }

                if (direction == -1)
                {
                    newMap.Values[newCar.Position.Y][newCar.Position.X - 1] = car.Id.ToString();
                    newMap.Values[newCar.Position.Y][newCar.Position.X + (newCar.Size) - 1] = null;
                    newCar.Position.X--;
                }

            }
            else
            {
                if (direction == 1)
                {
                    newMap.Values[newCar.Position.Y + (newCar.Size)][newCar.Position.X] = car.Id.ToString();
                    newMap.Values[newCar.Position.Y][newCar.Position.X] = null;
                    newCar.Position.Y++;

                }

                if (direction == -1)
                {
                    newMap.Values[newCar.Position.Y - 1][newCar.Position.X] = car.Id.ToString();
                    newMap.Values[newCar.Position.Y + (newCar.Size) - 1][newCar.Position.X] = null;
                    newCar.Position.Y--;


                }
            }

            return newMap;
        }

        public bool CanMoveCar(Car car, int direction)
        {
            try
            {
                if (car.Orientation == Orientation.HORIZONTAL)
                {
                    if (direction == 1)
                    {
                        return string.IsNullOrEmpty(Values[car.Position.Y][car.Position.X + (car.Size)]);
                    }

                    if (direction == -1)
                    {
                        return string.IsNullOrEmpty(Values[car.Position.Y][car.Position.X - 1]);
                    }
                }
                else
                {
                    if (direction == 1)
                    {
                        return string.IsNullOrEmpty(Values[car.Position.Y + (car.Size)][car.Position.X]);
                    }

                    if (direction == -1)
                    {
                        return string.IsNullOrEmpty(Values[car.Position.Y - 1][car.Position.X]);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public void PrintMap()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (string.IsNullOrEmpty(Values[i][j]))
                        Console.Write(".");
                    else
                        Console.Write(Values[i][j]);
                }

                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            StringBuilder mapHashString = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (string.IsNullOrEmpty(Values[i][j]))
                        mapHashString.Append(".");
                    else
                        mapHashString.Append(Values[i][j]);
                }

                mapHashString.AppendLine();
            }

            return mapHashString.ToString();
        }

        public int GetHash()
        {
            return this.ToString().GetHashCode();
        }

    }
}
