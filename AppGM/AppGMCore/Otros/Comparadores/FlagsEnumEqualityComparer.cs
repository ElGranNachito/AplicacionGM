using System;
using System.Collections.Generic;

namespace AppGM.Core
{	
	/// <summary>
	/// Comparador de igual para enums con el atributo flags
	/// </summary>
	/// <typeparam name="TEnum">Tipo del enum</typeparam>
	public class FlagsEnumEqualityComparer<TEnum> : EqualityComparer<TEnum>

		where TEnum: struct, Enum
	{
		public override bool Equals(TEnum x, TEnum y)
		{
			return x.HasFlag(y) || y.HasFlag(x);
		}

		public override int GetHashCode(TEnum obj)
		{
			return obj.GetHashCode();
		}
	}
}
