using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGM.Core
{
	public partial class ModeloArgumentosDaño
	{
		[NotMapped] 
		public bool FuenteEsMagia => NivelMagia == ENivelMagia.NINGUNO;
	}
}
