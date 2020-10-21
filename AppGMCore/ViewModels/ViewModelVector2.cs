namespace AppGM.Core
{
    public class ViewModelVector2 : BaseViewModel
    {
        public double X { get; set; }
        public double Y { get; set; }

        public ViewModelVector2(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
        }
        public ViewModelVector2(double _x = 0, double _y = 0)
        {
            X = _x;
            Y = _y;
        }
    }
}
