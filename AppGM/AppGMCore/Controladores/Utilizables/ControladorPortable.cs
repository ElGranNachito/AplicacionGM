using Microsoft.Win32.SafeHandles;

namespace AppGMCore
{
    class ControladorPortable<TipoPortable> : ControladorUtilizable<ModeloPortable>
    {
        #region Funciones

        public virtual void Equipar(ControladorPersonaje<ModeloPersonaje> objetivo)
        {
            //TODO equipar item al objetivo.
        }

        public virtual void Desequipar(ControladorPersonaje<ModeloPersonaje> objetivo)
        {
            //TODO desequipar item al objetivo.
        }

        #endregion
    }

    class ControladorDefensivo<TipoDefensivo> : ControladorPortable<ModeloDefensivo>
    {
        #region Funciones

        public virtual void RecibirDaño(int daño, out int dañoTrasReduccion, ETipoDeDaño tipoDeDaño)
        {
            //TODO: Reducir el tipo de daño pasado y quitar duravilidad al item portable dependiendo del tipo de daño que sea.
            dañoTrasReduccion = 0;
        }

        #endregion
    }

    class ControladorDefensivoAbsoluto : ControladorDefensivo<ModeloDefensivoAbsoluto>
    {
        #region Funciones

        public override void RecibirDaño(int daño, out int dañoTrasReduccion, ETipoDeDaño tipoDeDaño)
        {
            //TODO: Determinar el tipo de defensa, si usa HP para restar a esta el daño del golpe hasta que se acabe.
            //TODO: Si la durabilidad es por usos, por cada golpe restar uno  hasta 0.
            //TODO: Si la durabilidad no es por ninguna de las opciones anteriores, tratar al portable como armadura normal.
            
            dañoTrasReduccion = 0;
        }

        #endregion
    }
}