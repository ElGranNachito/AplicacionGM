namespace AppGM.Core
{
    public class ControladorRol : ControladorBase<ModeloRol>
    {
        public DatosRol datosRol;

        #region Eventos

        public delegate void dAvanzarDia(ref ushort nuevoDia);

        public event dAvanzarDia OnAvanzarDia = delegate{};

        #endregion

        #region Constructores
        public ControladorRol(ModeloRol _modelo)
        {
            modelo = _modelo;

            datosRol = new DatosRol(modelo);
        } 

        #endregion

        #region Funciones

        public void AvanzarDia()
        {
            ushort nuevoDia = ++modelo.Dia;

            OnAvanzarDia(ref nuevoDia);

            modelo.Dia = nuevoDia;
        }

        #endregion
    }
}
