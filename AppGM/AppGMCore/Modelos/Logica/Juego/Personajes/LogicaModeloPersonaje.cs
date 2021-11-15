namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica del <see cref="ModeloPersonaje"/>
	/// </summary>
	public partial class ModeloPersonaje
	{
		public override ModeloPersonaje ObtenerPersonajeContenedor() => this;

		public int ObtenerValorStat(EStat stat)
		{
			switch (stat)
			{
				case EStat.HP:
					return Hp;
				case EStat.STR:
					return Str;
				case EStat.END:
					return End;
				case EStat.AGI:
					return Agi;
				case EStat.INT:
					return Int;
				case EStat.LCK:
					return Lck;
				case EStat.CHR:
				{
					if (this is ModeloMaster m)
						return m.Chr;

					return 0;
				}
				case EStat.NP:
				{
					if (this is ModeloServant s)
						return s.RangoNP.AValorNumerico();

					return 0;
				}
			}

			return 0;
		}
	}
}
