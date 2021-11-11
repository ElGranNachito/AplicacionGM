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

        public ViewModelDatosPersonajesRol(ViewModelCrearRol _contenedor)
			:base(_contenedor)
        {
            mRol = SistemaPrincipal.ModeloRolActual;

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
		            await new ViewModelCreacionEdicionPersonaje(async vm =>
		            {
			            if (vm.Resultado.EsAceptarOFinalizar())
			            {
				            var nuevoPersonaje = vm.CrearControlador();

				            if (vm.EstaEditando)
				            {
					            var resultado = await nuevoPersonaje.modelo.CrearCopiaProfundaEnSubtipoAsync(vm.ModeloSiendoEditado.GetType(), vm.ModeloSiendoEditado);

					            await resultado.modelosCreadosEliminados.GuardarYEliminarModelosAsync();
				            }
				            else
				            {
					            await SistemaPrincipal.GuardarDatosAsync();
				            }

				            ViewModelListaPersonajes.Items.Add(new ViewModelPersonajeItem(nuevoPersonaje));
			            }

                        contenedorPasos.DispararPropertyChanged(nameof(contenedorPasos.PuedeFinalizar));
		            }).Inicializar());
            }, true, "Personajes");

            ViewModelListaPersonajes.Items.AddRange(mRol.Personajes.Select(p => new ViewModelPersonajeItem(p)));

            PropertyChanged += (sender, e) =>
            {
	            if (e.PropertyName != nameof(contenedorPasos.PuedeFinalizar))
		            contenedorPasos.DispararPropertyChanged(nameof(contenedorPasos.PuedeFinalizar));
            };
        }

        #endregion

		#region Metodos

		public override bool PuedeAvanzar() => true;

		#endregion
    }
}