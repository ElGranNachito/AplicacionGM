using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGM.Core
{
	public class ControladorItem : Controlador<ModeloItem>
    {
	    #region Eventos

	    public delegate void dUtilizarHabilidad(ControladorHabilidad habilidad, ControladorPersonaje usuario, ControladorPersonaje[] objetivos);

	    public event dUtilizarHabilidad OnUtilizarHabilidad = delegate { };

	    #endregion

        #region Propiedades

        public List<ControladorSlot> Slots { get; set; } = new List<ControladorSlot>();

        #endregion

        #region Constructores

        public ControladorItem(ModeloItem _modeloItem)
	        : base(_modeloItem)
        {
            CargarVariablesYTiradas();
        }

        #endregion

        #region Metodos

        public virtual void Utilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos, object parametroExtra, object segundoParametroExtra)
        {
            
        }

        public virtual void Utilizar(ControladorPersonaje usuario, object parametroExtra, object segundoParametroExtra)
        {
           
        }

        public virtual bool PuedeUtilizar(ControladorPersonaje usuario, ControladorPersonaje[] objetivos)
        {
	        return false;
        }

        public override async Task Recargar()
        {
	        await base.Recargar();

	        foreach (var slot in modelo.Slots)
	        {
		        
	        }
        }

        #endregion
    }
}
