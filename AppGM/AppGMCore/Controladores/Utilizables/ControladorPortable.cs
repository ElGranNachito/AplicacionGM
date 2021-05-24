using System.Collections.Generic;

namespace AppGM.Core
{
    public class ControladorPortable : ControladorUtilizable
    {
        #region Controladores

        public List<ControladorSlot> ControladorSlots { get; set; }

        public IControladorModificadorDeStatBase ControladorDesventajasDeEquiparlo { get; set; }
        public IControladorModificadorDeStatBase ControladorVentajasDeEquiparlo { get; set; }

        // Portable ofensivo
        public List<ControladorTiradaDaño> ControladorTiradaDeDaño { get; set; }

        public ControladorEfecto ControladorEfectoQueInflige { get; set; }

        #endregion

        #region Constructor

        public ControladorPortable(ModeloPortable _modeloPortable)
			:base(_modeloPortable) {}

        #endregion

        #region Funciones

        public virtual void Equipar(ControladorPersonaje objetivo)
        {
            //TODO equipar item al objetivo.
        }

        public virtual void Desequipar(ControladorPersonaje objetivo)
        {
            //TODO desequipar item al objetivo.
        }

        #endregion
    }

    public class ControladorDefensivo: ControladorPortable
    {
        #region Constructor

        public ControladorDefensivo(ModeloDefensivo _modeloDefensivo)
			:base(_modeloDefensivo) {}

        #endregion

        #region Funciones

        public virtual void RecibirDaño(int daño, out int dañoTrasReduccion, ETipoDeDaño tipoDeDaño)
        {
            //TODO: Reducir el tipo de daño pasado y quitar duravilidad al item portable dependiendo del tipo de daño que sea.
            dañoTrasReduccion = 0;
        }

        #endregion
    }

    public class ControladorDefensivoAbsoluto : ControladorDefensivo
    {
        #region Constructor

        public ControladorDefensivoAbsoluto(ModeloDefensivoAbsoluto _modeloDefensivoAbsoluto)
			:base(_modeloDefensivoAbsoluto) {}

        #endregion

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