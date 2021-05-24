using System.ComponentModel.DataAnnotations;

namespace AppGM.Core
{
    public class ModeloVector2 : ModeloBase
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Vector2 : Controlador<ModeloVector2>
    {
        #region Constructores

        public Vector2(double _x, double _y)
        {
            modelo = new ModeloVector2
            {
                X = _x,
                Y = _y
            };
        }
        public Vector2(ModeloVector2 _modelo)
            :base(_modelo){}

        #endregion

        #region Funciones

        public override void Eliminar()
        {
            SistemaPrincipal.EliminarModelo(modelo);
        } 

        #endregion

        #region Propiedades

        public double X
        {
            get => modelo.X;
            set => modelo.X = value;
        }

        public double Y
        {
            get => modelo.Y;
            set => modelo.Y = value;
        } 

        #endregion
    }
}
