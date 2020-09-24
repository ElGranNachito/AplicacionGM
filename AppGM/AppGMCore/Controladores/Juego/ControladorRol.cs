namespace AppGM.Core
{
    class ControladorRol : ControladorBase<ModeloRol>
    {
        #region Eventos

        public delegate void dAvanzarDia(ref ushort nuevoDia);

        public event dAvanzarDia OnAvanzarDia = delegate{};

        #endregion

        #region Funciones

        public void AvanzarDia()
        {
            ushort nuevoDia = ++modelo.DiaEnRol;

            OnAvanzarDia(ref nuevoDia);

            modelo.DiaEnRol = nuevoDia;
        }

        #endregion
    }
}
