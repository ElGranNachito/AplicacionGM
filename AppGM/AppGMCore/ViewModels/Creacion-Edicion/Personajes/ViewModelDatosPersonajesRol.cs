using System.Linq;

namespace AppGM.Core
{
    public class ViewModelDatosPersonajesRol : ViewModelPaso<ViewModelCrearRol>
    {
        #region Miembros

        private readonly ModeloRol mRol;

        #endregion

        #region Propiedades

        /// <summary>
        /// Nombre del personaje que esta buscando el usuario
        /// </summary>
        public string NombrePersonajeBuscado { get; set; }

        public ViewModelListaItems<ViewModelPersonajeItem> ViewModelListaPersonajes { get; set; }

        public ViewModelMultiselectComboBox<ETipoPersonaje> ViewModelMultiselectComboBoxFiltroTiposPersonajes { get; set; }

        public ViewModelVistaFuentesDeDaño ViewModelVistaFuentesDeDaño { get; set; }

        #endregion

        #region Constructor

        public ViewModelDatosPersonajesRol(ModeloRol _rol, ViewModelCrearRol vmCrearRol)
        {
            mRol = _rol;

            mRol.FuentesDeDaño.Add(new ModeloFuenteDeDaño
            {
                NombreFuente = "Pistolita",
                TiposDeDaño = ETipoDeDaño.Proyectil | ETipoDeDaño.Cortante
            });

            ViewModelVistaFuentesDeDaño = new ViewModelVistaFuentesDeDaño(mRol);

            ViewModelMultiselectComboBoxFiltroTiposPersonajes = new ViewModelMultiselectComboBox<ETipoPersonaje>(
	            EnumHelpers.TiposDePersonajesDisponibles.Select(t => new ViewModelMultiselectComboBoxItem<ETipoPersonaje>(t, t.ToString(), ViewModelMultiselectComboBoxFiltroTiposPersonajes, true)).ToList());

            ViewModelListaPersonajes = new ViewModelListaItems<ViewModelPersonajeItem>(async () =>
            {
	            SistemaPrincipal.MostrarViewModelCreacionEdicion<ViewModelCreacionEdicionPersonaje, ModeloPersonaje, ControladorPersonaje>(
		            await new ViewModelCreacionEdicionPersonaje(vm =>
		            {
			            if (vm.Resultado.EsAceptarOFinalizar())
			            {
				            var modeloNuevoPersonaje = vm.CrearModelo();

				            ViewModelListaPersonajes.Items.Add(new ViewModelPersonajeItem(modeloNuevoPersonaje));
			            }
		            }).Inicializar());
            }, true, "Personajes");

            ViewModelListaPersonajes.Items.AddRange(mRol.Personajes.Select(p => new ViewModelPersonajeItem(p)));
        }

        #endregion
    }
}