using System;
using System.Reflection;

namespace AppGM.Core
{
	/// <summary>
	/// Item de un <see cref="ViewModelVentanaAutocompletadoMiembros"/>
	/// </summary>
	public sealed class ViewModelItemAutocompletadoMiembro : ViewModelItemAutocompletado<MemberInfo>
	{
		public ViewModelItemAutocompletadoMiembro(MemberInfo _infoMiembro)
			: base(_infoMiembro)
		{
			ActualizarRepresentacionTextual();
		}

		protected override void ActualizarRepresentacionTextual()
		{
			if (valorItem.GetCustomAttribute(typeof(AccesibleEnGuraScratch)) is AccesibleEnGuraScratch att)
			{
				RepresentacionTextual = att.nombreQueMostrar;

				return;
			}

			RepresentacionTextual = valorItem.Name;
			//TODO:Añadir valor extra
		}

		public override bool Comparar(string cadena, bool comparacionExacta = false)
		{
			return true;
		}
	}

	public class ViewModelItemAutocompletadoString : ViewModelItemAutocompletado<string>
	{
		public ViewModelItemAutocompletadoString(string _cadena)
			: base(_cadena)
		{
			ActualizarRepresentacionTextual();
		}

		protected override void ActualizarRepresentacionTextual()
		{
			RepresentacionTextual = valorItem;
		}

		public override bool Comparar(string cadena, bool comparacionExacta = false)
		{
			if (!comparacionExacta)
				return cadena.Length <= valorItem.Length && valorItem.StartsWith(cadena);
			
			return cadena.Equals(valorItem);
		}
	}

	public sealed class ViewModelItemAutocompletadoVariable : ViewModelItemAutocompletadoString
	{
		public BloqueVariable bloque;

		public ViewModelItemAutocompletadoVariable(string _cadena, BloqueVariable _bloque)
			:base(_cadena)
		{
			bloque = _bloque;
		}
	}

	public sealed class ViewModelItemAutocompletadoTipo : ViewModelItemAutocompletadoString
	{
		public Type tipo;

		public ViewModelItemAutocompletadoTipo(Type _tipo)
			:base(_tipo.Name)
		{
			tipo = _tipo;
		}
	}
}