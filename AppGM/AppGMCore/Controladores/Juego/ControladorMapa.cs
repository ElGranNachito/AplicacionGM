namespace AppGM.Core
{
    public class ControladorMapa : ControladorBase<ModeloMapa>
    {
        public ControladorMapa(ModeloMapa _modeloMapa)
        {
            modelo = _modeloMapa;
        }

        public string ObtenerExtension()
        {
            switch (modelo.EFormatoImagen)
            {
                case EFormatoImagen.Png:
                    return ".png";
                case EFormatoImagen.Jpg:
                    return ".jpg";
                default:
                    return string.Empty;
            }
        }
    }
}
