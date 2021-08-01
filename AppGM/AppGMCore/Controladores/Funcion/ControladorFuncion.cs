using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using CoolLogs;

namespace AppGM.Core
{
	public abstract class ControladorFuncionBase : Controlador<ModeloFuncion>
	{
		private Dictionary<int, ControladorVariableFuncionBase> mVariablesPersistenes;

		public List<BloqueBase> Bloques { get; private set; } = new List<BloqueBase>();

		public string NombreCompletoFuncion => string.Intern($"{modelo.NombreFuncion}_{modelo.Id}.xml");

		public string NombreFuncion
		{
			get => modelo.NombreFuncion;
			set
			{
				if (value == NombreFuncion || value.IsNullOrWhiteSpace())
					return;

				//Cambiamos el nombre del archivo
				File.Move(
					Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones, NombreCompletoFuncion),
					Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones, string.Intern($"{value}_{modelo.Id}.xml")));

				modelo.NombreFuncion = value;
			}
		}

		public static ControladorFuncionBase CrearControladorCorrespondiente(ModeloFuncion modelo, ETipoFuncion tipoFuncion)
		{
			switch (tipoFuncion)
			{
				case ETipoFuncion.Habilidad:
					return new ControladorFuncion_Habilidad(modelo);
				case ETipoFuncion.Efecto:
					return new ControladorFuncion_Efecto(modelo);
				case ETipoFuncion.Predicado:
					return new ControladorFuncion_Predicado(modelo);
				default:
					return null;
			}
		}

		public ControladorFuncionBase(ModeloFuncion _modelo)
			: base(_modelo)
		{
			modelo.VariablesPersistentes ??= new List<TIFuncionVariable>();

			mVariablesPersistenes = new Dictionary<int, ControladorVariableFuncionBase>(modelo.VariablesPersistentes.Select(var =>
			{
				Type tipoVariable = null;

				try
				{
					tipoVariable = Type.GetType(var.Variable.TipoVariable);
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.Log(
						@$"Error al intentar obtener tipo de variable. IDFuncion: {var.Funcion.Id} - IDVariable: {var.Variable.Id} - TipoVariable: {var.Variable.TipoVariable}
															{Environment.NewLine} Excepcion: {ex.Message}.");

					return new KeyValuePair<int, ControladorVariableFuncionBase>();
				}

				if (tipoVariable == typeof(int))
					return new KeyValuePair<int, ControladorVariableFuncionBase>(var.Variable.IDVariable, new ControladorVariableFuncion_Int(var.Variable));
				else if (tipoVariable == typeof(float))
					return new KeyValuePair<int, ControladorVariableFuncionBase>(var.Variable.IDVariable, new ControladorVariableFuncion_Float(var.Variable));
				else if (tipoVariable == typeof(string))
					return new KeyValuePair<int, ControladorVariableFuncionBase>(var.Variable.IDVariable, new ControladorVariableFuncion_String(var.Variable));

				SistemaPrincipal.LoggerGlobal.Log($"{tipoVariable} no soportado!", ESeveridad.Error);

				return new KeyValuePair<int, ControladorVariableFuncionBase>();
			}));
		}

		public void ActualizarBloques(List<BloqueBase> nuevosBloques)
		{
			Bloques.Clear();

			Bloques.AddRange(nuevosBloques);

			Bloques.TrimExcess();

			List<int> idsVariablesPersistentesExistentes = 
				modelo.VariablesPersistentes.Select(var => var.IDVariable).ToList();

			foreach (var bloque in nuevosBloques)
			{
				if (bloque is BloqueVariable var)
				{
					if (!idsVariablesPersistentesExistentes.Remove(var.IDBloque))
					{
						ModeloVariableFuncionBase modeloVariable = null;

						if (var.tipo == typeof(int))
							modeloVariable = new ModeloVariableFuncion_Int{IDVariable = var.IDBloque, NombreVariable = var.nombre};
						else if (var.tipo == typeof(float))
							modeloVariable = new ModeloVariableFuncion_Float {IDVariable = var.IDBloque, NombreVariable = var.nombre };
						else if (var.tipo == typeof(string))
							modeloVariable = new ModeloVariableFuncion_String {IDVariable = var.IDBloque, NombreVariable = var.nombre };
						else
						{
							SistemaPrincipal.LoggerGlobal.Log($"No se pudo crear modelo para variable persistente ({var})", ESeveridad.Error);

							continue;
						}

						var tiFuncionVariable = new TIFuncionVariable {Funcion = modelo, Variable = modeloVariable};

						modelo.VariablesPersistentes.Add(tiFuncionVariable);
					}
				}
			}

			//Quitamos las relaciones del modelo con variables que ya no se encuentran en la funcion
			modelo.VariablesPersistentes.RemoveAll(var =>
			{
				for (int i = 0; i < idsVariablesPersistentesExistentes.Count; ++i)
				{
					if (var.Variable.IDVariable == idsVariablesPersistentesExistentes[i])
					{
						idsVariablesPersistentesExistentes.RemoveAt(i);

						return true;
					}
				}

				return false;
			});

			GuardarXML();
		}

		public void GuardarXML()
		{
			XmlWriterSettings config = new XmlWriterSettings {Encoding = Encoding.UTF8, Indent = true, NewLineOnAttributes = true};

			using XmlWriter writer = XmlWriter.Create(File.Open(Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioFunciones, NombreCompletoFuncion), FileMode.Create), config);
			
			writer.WriteStartDocument();
			writer.WriteStartElement("Cuerpo");

			foreach (var bloque in Bloques)
				bloque.ConvertirHaciaXML(writer);

			writer.WriteEndElement();
			writer.WriteEndDocument();
		}

		[IndexerName("VariablesPersistentes")]
		public object this[int idVariable]
		{
			get
			{
				if (mVariablesPersistenes.ContainsKey(idVariable))
					return mVariablesPersistenes[idVariable].ObtenerValorVariable();

				SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener una variable con id: {idVariable}, pero no se encuentra en {nameof(mVariablesPersistenes)}", ESeveridad.Error);

				return null;
			}

			set
			{
				//TODO: Implementar
			}
		}
	}

	public abstract class ControladorFuncion<TipoFuncion> : ControladorFuncionBase
	{
		public TipoFuncion funcion;

		public ControladorFuncion(ModeloFuncion _modelo)
			: base(_modelo)
		{
			Compilador compilador = new Compilador(modelo.NombreFuncion, modelo.Id);

			funcion = compilador.Compilar<TipoFuncion>().Funcion;
		}
	}

	public class ControladorFuncion_Efecto : ControladorFuncion<Func<ControladorEfecto, ControladorPersonaje, List<ControladorPersonaje>, ControladorFuncionBase, bool>>
	{
		public ControladorFuncion_Efecto(ModeloFuncion _modelo)
			: base(_modelo)
		{ }


	}

	public class ControladorFuncion_Habilidad : ControladorFuncion<Action<ControladorPersonaje, List<ControladorPersonaje>, List<object>>>
	{
		public ControladorFuncion_Habilidad(ModeloFuncion _modelo)
			: base(_modelo)
		{}


	}

	public class ControladorFuncion_Predicado : ControladorFuncion<Func<ControladorEfecto, ControladorPersonaje, List<ControladorPersonaje>, ControladorFuncionBase, bool>>
	{
		public ControladorFuncion_Predicado(ModeloFuncion _modelo)
			: base(_modelo)
		{ }


	}
}