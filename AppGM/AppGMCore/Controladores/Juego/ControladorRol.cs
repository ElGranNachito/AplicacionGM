namespace AppGM.Core
{
    public class ControladorRol : ControladorBase<ModeloRol>
    {
        public DatosRol datos;

        #region Eventos

        public delegate void dAvanzarDia(ref ushort nuevoDia);

        public event dAvanzarDia OnAvanzarDia = delegate{};

        #endregion

        #region Constructores
        public ControladorRol(ModeloRol _modelo)
        {
            modelo = _modelo;


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
