using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloVector2
    {
        [Key]
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Vector2 : ControladorBase<ModeloVector2>
    {
        public double X => modelo.X;
        public double Y => modelo.Y;
        public Vector2(double _x, double _y)
        {
            modelo = new ModeloVector2
            {
                X = _x,
                Y = _y
            };
        }
        public Vector2(ModeloVector2 _modelo)
        {
            modelo = _modelo;
        }
    }
}
