
namespace blazniva_krizovatka
{
    //Setp class, used to summarized same steps after each other
    public class Step
    {
        public string Car { get; set; }
        public string Direction { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Direction}({Car} {Count})";
        }
    }
}
