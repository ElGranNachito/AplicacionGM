namespace AppGM.Core
{
    public class ViewModelVector2 : BaseViewModel
    {
        public Vector2 Posicion { get; set; }

        public ViewModelVector2(Vector2 v)
        {
            Posicion = v;
        }
        public ViewModelVector2(double _x = 0, double _y = 0)
        {
            Posicion = new Vector2(_x, _y);
        }

        public double X
        {
            get => Posicion.X;
            set => Posicion.X = value;
        }

        public double Y
        {
            get => Posicion.Y;
            set => Posicion.Y = value;
        }
    }
}
