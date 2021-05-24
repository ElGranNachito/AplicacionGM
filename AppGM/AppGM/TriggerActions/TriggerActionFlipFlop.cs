namespace AppGM
{
	/// <summary>
	/// Clase derivada de <see cref="TriggerActionBase"/> que alterna entre dos valores de una propiedad
	/// </summary>
	class TriggerActionFlipFlop : TriggerActionBase
	{
		protected override void Invoke(object obj)
		{
			//Guardamos el valor actual de la propiedad
			var valorActualPropiedad = Propiedad.GetValue(Destino);

			//Cambiamos el valor de la propiedad
			Propiedad.SetValue(Destino, Valor);

			//Guardamos en la propiedad Valor el valor anterior
			Valor = valorActualPropiedad;
		}
	}
}
