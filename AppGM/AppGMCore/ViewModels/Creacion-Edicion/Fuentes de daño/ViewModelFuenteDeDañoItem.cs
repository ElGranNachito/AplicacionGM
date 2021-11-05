namespace AppGM.Core
{
	/// <summary>
	/// Representa un <see cref="ModeloFuenteDeDaño"/> en un <see cref="ViewModelListaItems{TItem}"/>
	/// </summary>
	public sealed class ViewModelFuenteDeDañoItem : ViewModelItemListaGenerico<ViewModelFuenteDeDañoItem>
	{
		public readonly ModeloFuenteDeDaño fuenteDeDaño;

		public ViewModelFuenteDeDañoItem(ModeloFuenteDeDaño _fuenteDeDaño) : base(string.Empty)
		{
			fuenteDeDaño = _fuenteDeDaño;

			if(fuenteDeDaño == null)
				SistemaPrincipal.LoggerGlobal.LogCrash($"{nameof(_fuenteDeDaño)} no puede ser null");

			fuenteDeDaño.OnRecibioCopiaProfunda += (receptor, fuente) => ActualizarCaracteristicas(); 

			ActualizarCaracteristicas();
		}

		protected override void ActualizarCaracteristicas()
		{
			CaracteristicasItem = new ViewModelListaDeElementos<ViewModelCaracteristicaItem>
			{
				new ViewModelCaracteristicaItem
				{
					Titulo = string.Intern("Nombre fuente"),
					Valor = $"{fuenteDeDaño.NombreFuente} - {fuenteDeDaño.TiposDeDaño.FlagsActivasEnumToString()}"
				}
			};
		}

		protected override void ActualizarGruposDeBotones()
		{
			
		}
	}
}
