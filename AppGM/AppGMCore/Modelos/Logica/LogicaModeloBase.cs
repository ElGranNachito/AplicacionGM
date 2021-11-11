using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Clase que contiene la logica del <see cref="ModeloBase"/>
	/// </summary>
	public abstract partial class ModeloBase
	{
		#region Eventos

		/// <summary>
		/// Evento disparado cuando se crea un controlador para este modelo
		/// </summary>
		public event Action<ModeloBase, ControladorBase> OnControladorCreado = delegate { };

		/// <summary>
		/// Evento que se dispara cuando se realizar una copia profunda hacia este modelo
		/// </summary>
		public event Action<ModeloBase, ModeloBase> OnRecibioCopiaProfunda = delegate { }; 

		#endregion

		#region Metodos

		/// <summary>
		/// Dispara el evento <see cref="OnControladorCreado"/>
		/// </summary>
		/// <param name="controlador"><see cref="ControladorBase"/> de este modelo</param>
		public void DispararControladorCreado(ControladorBase controlador) => OnControladorCreado(this, controlador);

		/// <summary>
		/// Dispara el evento <see cref="OnRecibioCopiaProfunda"/>
		/// </summary>
		/// <param name="fuenteCopia">Modelo que se copio</param>
		public void DispararRecibioCopiaProfunda(ModeloBase fuenteCopia) => OnRecibioCopiaProfunda(this, fuenteCopia);

		/// <summary>
		/// Añade un handler a <see cref="OnControladorCreado"/> que se desubscribe despues de un uso
		/// </summary>
		/// <param name="handler">Handler que añadir</param>
		public void AñadirHandlerSoloUsoControladorCreado(Action<ModeloBase, ControladorBase> handler)
		{
			Action<ModeloBase, ControladorBase> wrapper = null;

			wrapper = (modelo, controlador) =>
			{
				handler(modelo, controlador);

				OnControladorCreado -= wrapper;
			};

			OnControladorCreado += wrapper;
		}

		#region Copiado profundo

		/// <summary>
		/// Crea una copia superficial de un <see cref="ModeloBase"/> de manera sincronica
		/// </summary>
		/// <param name="modeloAlQueCopiarLosDatos">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <returns>En caso de que <paramref name="modeloAlQueCopiarLosDatos"/> sea null, devuelve un nuevo modelo con los datos copiados.
		/// En caso de que no lo sea devuelve una referencia al modelo existente al que se copiaron los datos</returns>
		public (ModeloBase resultado, ContenedorModelosCreadosEliminadosAlCopiar modelosCreadosEliminados) CrearCopiaProfundaEnSubtipo(

			Type tipoAlQueCopiarLosDatos,
			ModeloBase modeloAlQueCopiarLosDatos = null,
			ModeloBase modeloQueCopiar = null,
			Dictionary<ModeloBase, ModeloBase> referenciasQueReemplazar = null)
		{
			modeloQueCopiar ??= this;

			//No aseguramos que en caso de que se este copiando a un modelo existente este sea igual al tipo al que se copiaran los datos
			if (modeloAlQueCopiarLosDatos != null && !tipoAlQueCopiarLosDatos.IsAssignableFrom(modeloAlQueCopiarLosDatos.GetType()))
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"no se puede asignar a {tipoAlQueCopiarLosDatos.Name} desde {modeloAlQueCopiarLosDatos.GetType().Name} ");

				return (null, null);
			}

			var metodoGenerico = typeof(ModeloBase).GetMethod(nameof(CrearCopiaProfundaEnSubtipo_Interno), BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(tipoAlQueCopiarLosDatos);

			List<ModeloBase> outModelosEliminados = new List<ModeloBase>();
			List<ModeloBase> outModelosAñadidos = new List<ModeloBase>();

			return ((ModeloBase)metodoGenerico.Invoke(this, new object[] { modeloAlQueCopiarLosDatos, modeloQueCopiar, outModelosEliminados, outModelosAñadidos, referenciasQueReemplazar, null }), new ContenedorModelosCreadosEliminadosAlCopiar(outModelosAñadidos, outModelosEliminados));
		}

		/// <summary>
		/// Crea una copia superficial de un <see cref="ModeloBase"/> de manera sincronica
		/// </summary>
		/// <typeparam name="TResultado">Tipo del modelo al que se copiaran los datos. Debe ser un tipo asignable al tipo del <paramref name="modeloQueCopiar"/></typeparam>
		/// <typeparam name="TModeloOrigen">Tipo del modelo del que se copiaran los datos</typeparam>
		/// <param name="modeloAlQueCopiarLosDatos">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <returns>En caso de que <paramref name="modeloAlQueCopiarLosDatos"/> sea null, devuelve un nuevo modelo con los datos copiados.
		/// En caso de que no lo sea devuelve una referencia al modelo existente al que se copiaron los datos</returns>
		public (TResultado resultado, ContenedorModelosCreadosEliminadosAlCopiar modelosCreadosEliminados) CrearCopiaProfundaEnSubtipo<TResultado, TModeloOrigen>(

			TResultado modeloAlQueCopiarLosDatos = null,
			TModeloOrigen modeloQueCopiar = null,
			Dictionary<ModeloBase, ModeloBase> referenciasQueReemplazar = null)

			where TModeloOrigen : ModeloBase, new()
			where TResultado : ModeloBase, new()
		{
			List<ModeloBase> outModelosEliminados = new List<ModeloBase>();
			List<ModeloBase> outModelosAñadidos = new List<ModeloBase>();

			return (CrearCopiaProfundaEnSubtipo_Interno<TResultado>(modeloAlQueCopiarLosDatos, modeloQueCopiar, outModelosEliminados, outModelosAñadidos, referenciasQueReemplazar, null), new ContenedorModelosCreadosEliminadosAlCopiar(outModelosAñadidos, outModelosEliminados));
		}

		/// <summary>
		/// Crea una copia superficial de un <see cref="ModeloBase"/> de manera asincronica
		/// </summary>
		/// <param name="modeloAlQueCopiarLosDatos">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <returns>En caso de que <paramref name="modeloAlQueCopiarLosDatos"/> sea null, devuelve un nuevo modelo con los datos copiados.
		/// En caso de que no lo sea devuelve una referencia al modelo existente al que se copiaron los datos</returns>
		public async Task<(ModeloBase resultado, ContenedorModelosCreadosEliminadosAlCopiar modelosCreadosEliminados)> CrearCopiaProfundaEnSubtipoAsync(
			Type tipoAlQueCopiarLosDatos,
			ModeloBase modeloAlQueCopiarLosDatos = null,
			ModeloBase modeloQueCopiar = null,
			Dictionary<ModeloBase, ModeloBase> referenciasQueReemplazar = null)
		{
			return await Task.Run(() => CrearCopiaProfundaEnSubtipo(tipoAlQueCopiarLosDatos, modeloAlQueCopiarLosDatos, modeloQueCopiar, referenciasQueReemplazar));
		}

		/// <summary>
		/// Crea una copia superficial de un <see cref="ModeloBase"/> de manera asincronica
		/// </summary>
		/// <typeparam name="TResultado">Tipo del modelo al que se copiaran los datos. Debe ser un tipo asignable al tipo del <paramref name="modeloQueCopiar"/></typeparam>
		/// <typeparam name="TModeloOrigen">Tipo del modelo del que se copiaran los datos</typeparam>
		/// <param name="modeloAlQueCopiarLosDatos">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <returns>En caso de que <paramref name="modeloAlQueCopiarLosDatos"/> sea null, devuelve un nuevo modelo con los datos copiados.
		/// En caso de que no lo sea devuelve una referencia al modelo existente al que se copiaron los datos</returns>
		public async Task<(TResultado resultado, ContenedorModelosCreadosEliminadosAlCopiar modelosCreadosEliminados)> CrearCopiaProfundaEnSubtipoAsync<TResultado, TModeloOrigen>(
			TResultado modeloAlQueCopiarLosDatos = null,
			TModeloOrigen modeloQueCopiar = null,
			Dictionary<ModeloBase, ModeloBase> referenciasQueReemplazar = null)

			where TModeloOrigen : ModeloBase, new()
			where TResultado : ModeloBase, new()
		{
			return await Task.Run(() =>
			{
				List<ModeloBase> outModelosEliminados = new List<ModeloBase>();
				List<ModeloBase> outModelosAñadidos = new List<ModeloBase>();

				return (CrearCopiaProfundaEnSubtipo_Interno<TResultado>(modeloAlQueCopiarLosDatos, modeloQueCopiar, outModelosEliminados, outModelosAñadidos, referenciasQueReemplazar, null), new ContenedorModelosCreadosEliminadosAlCopiar(outModelosAñadidos, outModelosEliminados));
			});
		}

		/// <summary>
		/// Implementacion interna de la copia profunda
		/// </summary>
		/// <typeparam name="TResultado">Tipo del modelo al que se copiaran los datos. Debe ser un tipo asignable al tipo del <paramref name="modeloQueCopiar"/></typeparam>
		/// <param name="modeloDestino">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <param name="outModelosEliminados">Modelos</param>
		/// <param name="outModelosAñadidos"></param>
		/// <param name="modelosYaCopiados"></param>
		/// <returns></returns>
		private TResultado CrearCopiaProfundaEnSubtipo_Interno<TResultado>(
			TResultado modeloDestino,
			ModeloBase modeloQueCopiar,
			List<ModeloBase> outModelosEliminados,
			List<ModeloBase> outModelosAñadidos,
			Dictionary<ModeloBase, ModeloBase> referenciasQueReemplazar,
			Dictionary<ModeloBase, ModeloBase> modelosYaCopiados)

			where TResultado : ModeloBase, new()
		{
			bool copiandoAUnModeloExistente = modeloDestino != null;

			//Creamos una nueva instancia del modelo resultado
			modeloDestino ??= new TResultado();

			modeloQueCopiar ??= this;

			modelosYaCopiados ??= new Dictionary<ModeloBase, ModeloBase>();

			referenciasQueReemplazar ??= new Dictionary<ModeloBase, ModeloBase>();

			//Si ya existe una copia del modelo que se pretende copiar entonces la retornamos
			if (modelosYaCopiados.ContainsKey(modeloQueCopiar))
			{
				SistemaPrincipal.LoggerGlobal.Log($"Intentado copiar un modelo del que ya existe una copia", ESeveridad.Error);

				return (TResultado)modelosYaCopiados[modeloQueCopiar];
			}

			//Añadimos este modelo a la lista de modelos ya copiados
			modelosYaCopiados.Add(modeloQueCopiar, modeloDestino);

			var tipoModeloOrigen = modeloQueCopiar.GetType();

			var modeloDelQueObtenerLosMiembros = modeloDestino.GetType().IsSubclassOf(tipoModeloOrigen)
				? tipoModeloOrigen
				: modeloDestino.GetType();

			//Obtenemos las propiedades a las que podemos escribir
			var propiedades = tipoModeloOrigen.GetProperties().Where(p => p.CanWrite).ToList();

			//Obtenemos los campos
			var campos = tipoModeloOrigen.GetFields().ToList();

			if (outModelosEliminados != null && tipoModeloOrigen.IsSubclassOf(typeof(TResultado)))
			{
				var propiedadesModeloOrigen = tipoModeloOrigen.GetProperties().Where(p => !propiedades.Contains(p) && typeof(ModeloBase).IsAssignableFrom(p.PropertyType)).ToList();

				foreach (var propiedad in propiedadesModeloOrigen)
				{
					if (typeof(IList).IsAssignableFrom(propiedad.PropertyType))
					{
						var valorPropiedad = propiedad.ObtenerValorComoLista<ModeloBase>(modeloQueCopiar);

						foreach (var modelo in valorPropiedad)
						{
							if (!outModelosEliminados.Contains(modelo))
								outModelosEliminados.Add(modelo);
						}
					}
					else
					{
						var valorPropiedad = propiedad.GetValue(modeloQueCopiar) as ModeloBase;

						if (!outModelosEliminados.Contains(valorPropiedad))
							outModelosEliminados.Add(valorPropiedad);
					}
				}
			}

			var metodoClonar = typeof(ModeloBase).GetMethod(nameof(CrearCopiaProfundaEnSubtipo_Interno), BindingFlags.Instance | BindingFlags.NonPublic);

			//Por cada propiedad...
			foreach (var propiedadActual in propiedades)
			{
				if(propiedadActual.TieneAtributo<NoCopiar>())
					continue;

				bool propiedadDebeCopiarseSuperficialmente = propiedadActual.TieneAtributo<CopiarSuperficialmente>();

				//Intentamos establecer el valor de la propiedad. Lo envolvemos en un try 
				//por si no podemos escribir a la propiedad
				try
				{
					//Si la propiedad actual es una lista...
					if (typeof(IList).IsAssignableFrom(propiedadActual.PropertyType))
					{
						//Si es una lista de modelos...
						if (propiedadActual.EsListaDe<ModeloBase>(modeloQueCopiar, out var listaModeloOrigen))
						{
							//Si estamos copiando a un modelo existente...
							if (copiandoAUnModeloExistente)
							{
								//Creamos listas con los modelos existente en el destino y el modelo origen
								var modelosRestantesDestino = (Activator.CreateInstance(propiedadActual.PropertyType, propiedadActual.GetValue(modeloDestino)) as IList).Cast<ModeloBase>().ToList();
								var modelosRestantesModeloActual = (Activator.CreateInstance(propiedadActual.PropertyType, propiedadActual.GetValue(modeloQueCopiar)) as IList).Cast<ModeloBase>().ToList();
								var listaModeloDestino = propiedadActual.GetValue(modeloDestino) as IList;
								var listaModeloDestinoCasteada = listaModeloDestino.Cast<ModeloBase>().ToList();

								//Por cada elemento en la lista del modelo de origen...
								for (int i = 0; i < listaModeloOrigen.Count; ++i)
								{
									ModeloBase elementoActualListaOrigen = listaModeloOrigen[i];

									var equivalenteEnDestino = elementoActualListaOrigen.Id == 0 ? null : listaModeloDestinoCasteada.FirstOrDefault(m => m.Id == elementoActualListaOrigen.Id);

									//Si no hemos copiado este modelo aun, lo copiamos a su par en el destino
									if (!modelosYaCopiados.ContainsKey(elementoActualListaOrigen))
									{
										object modeloQueAñadir = elementoActualListaOrigen;

										if(!propiedadDebeCopiarseSuperficialmente)
										{
											modeloQueAñadir = metodoClonar.MakeGenericMethod(elementoActualListaOrigen.GetType()).Invoke(elementoActualListaOrigen, new object[] { equivalenteEnDestino, null, outModelosEliminados, outModelosAñadidos, referenciasQueReemplazar, modelosYaCopiados });
										}

										if (equivalenteEnDestino == null)
										{
											listaModeloDestinoCasteada.Add((ModeloBase)modeloQueAñadir);
										}
									}
									//Si lo hace entonces asignamos el modelo correspondiente del destino a la copia
									else
									{
										if (equivalenteEnDestino != null)
										{
											listaModeloDestinoCasteada.Remove(equivalenteEnDestino);
										}

										listaModeloDestinoCasteada.Add(modelosYaCopiados[elementoActualListaOrigen]);
									}
								}

								listaModeloDestino.Clear();

								foreach (var modelo in listaModeloDestinoCasteada)
									listaModeloDestino.Add(modelo);

								var modelosEliminadosDestino = listaModeloDestinoCasteada.FindAll(m => modelosRestantesModeloActual.All(ma => ma.Id != m.Id));

								foreach (var modelo in modelosEliminadosDestino)
									listaModeloDestino.Remove(modelo);

								//Los modelos restantes, es decir los que no se encontraban en ambas listas, son modelos que fueron eliminados, si 
								//se encontraban en el destino, o añadidos, si se encontraban en el modelo de origen
								outModelosAñadidos?.AddRange(listaModeloDestinoCasteada.FindAll(m => modelosRestantesDestino.All(ma => ma.Id != m.Id)));
								outModelosEliminados?.AddRange(modelosEliminadosDestino);
							}
							//Si no estamos copiando a un modelo existente...
							else
							{
								//Obtenemos el valor de la propiedad
								var nuevaLista = propiedadActual.GetValue(modeloDestino) as IList;

								//Por cada elemento en la lista del modelo origen...
								for (int i = 0; i < listaModeloOrigen.Count; ++i)
								{
									//Si el set de referencias que reemplazar contiene un valor para el modelo actual añadimos la referencia a la lista
									if (referenciasQueReemplazar.TryGetValue(listaModeloOrigen[i], out var referencia))
									{
										nuevaLista.Add(referencia);
									}
									//Si ya existe una copia del modelo que queremos copiar entonces añadimos a la lista del destino el modelo ya copiado
									else if (modelosYaCopiados.ContainsKey(listaModeloOrigen[i]))
									{
										nuevaLista.Add(modelosYaCopiados[listaModeloOrigen[i]]);
									}
									//Si no lo hace creamos una copia del elemento actual en un modelo vacio y lo añadimos a la lista
									else
									{
										object modeloQueAñadir = listaModeloOrigen[i];

										if(!propiedadDebeCopiarseSuperficialmente)
										{
											var metodoGenerico = metodoClonar.MakeGenericMethod(listaModeloOrigen[i].GetType());

											modeloQueAñadir = metodoGenerico.Invoke(listaModeloOrigen[i], new object[] { null, null, outModelosEliminados, outModelosAñadidos, referenciasQueReemplazar, modelosYaCopiados }) as ModeloBase;
										}

										nuevaLista.Add(modeloQueAñadir);
									}
								}
							}
						}
					}
					//Si la propiedad actual es un modelo
					else if (typeof(ModeloBase).IsAssignableFrom(propiedadActual.PropertyType))
					{
						var valorPropiedadOrigen = propiedadActual.GetValue(modeloQueCopiar) as ModeloBase;
						var valorPropiedadDestino = propiedadActual.GetValue(modeloDestino) as ModeloBase;

						//Si la propiedad de origen es null entonces continuamos a la siguiente iteracion
						if (valorPropiedadOrigen == null)
						{
							if (valorPropiedadDestino != null)
							{
								outModelosEliminados?.Add(valorPropiedadDestino);

								propiedadActual.SetValue(modeloDestino, null);
							}

							continue;
						}

						//Si el set de referencias que reemplazar contiene un valor para el modelo de origen colocamos su
						if (!copiandoAUnModeloExistente && referenciasQueReemplazar.TryGetValue(valorPropiedadOrigen, out var referencia))
						{
							propiedadActual.SetValue(modeloDestino, referencia);
						}
						//Si ya se ha copiado este modelo entonces simplemente asignamos el valor de la propiedad al modelo copiado
						else if (modelosYaCopiados.ContainsKey(valorPropiedadOrigen))
						{
							propiedadActual.SetValue(modeloDestino, modelosYaCopiados[valorPropiedadOrigen]);
						}
						//Si no...
						else
						{
							if (propiedadDebeCopiarseSuperficialmente)
							{
								if (copiandoAUnModeloExistente)
								{
									if (valorPropiedadDestino == null)
									{
										outModelosAñadidos?.Add(modelosYaCopiados[valorPropiedadOrigen]);
									}
								}
								//Sino simplemente creamos una copia del modelo actual.
								else
								{
									propiedadActual.SetValue(modeloDestino, valorPropiedadOrigen);
								}
							}
							else
							{
								var metodoClonarGenerico = metodoClonar.MakeGenericMethod(propiedadActual.PropertyType);

								//Si estamos copiando a un modelo existente entonces copiamos el valor de la propiedad de origen al modelo existente en la propiedad de destino
								if (copiandoAUnModeloExistente)
								{
									metodoClonarGenerico.Invoke(valorPropiedadOrigen, new object[] { propiedadActual.GetValue(modeloDestino), null, outModelosEliminados, outModelosAñadidos, referenciasQueReemplazar, modelosYaCopiados });

									if (valorPropiedadDestino == null)
									{
										outModelosAñadidos?.Add(modelosYaCopiados[valorPropiedadOrigen]);
									}
								}
								//Sino simplemente creamos una copia del modelo actual.
								else
								{
									var copiaModelo = metodoClonarGenerico.Invoke(valorPropiedadOrigen, new object[] { null, null, outModelosEliminados, outModelosAñadidos, referenciasQueReemplazar, modelosYaCopiados });

									propiedadActual.SetValue(modeloDestino, copiaModelo);
								}
							}
						}
					}
					//Si no lo es simplemente copiamos el valor
					else
					{
						propiedadActual.SetValue(modeloDestino, propiedadActual.GetValue(modeloQueCopiar));
					}
				}
				catch (Exception ex)
				{
					SistemaPrincipal.LoggerGlobal.Log($"Ocurrio un error durante la copia. Propiedad: {propiedadActual}{Environment.NewLine}Excepcion: {ex.Message}", ESeveridad.Error);
				}
			}

			//Por cada campo...
			foreach (var campoActual in campos)
			{
				//Establecemos el valor del campo
				campoActual.SetValue(modeloDestino, campoActual.GetValue(modeloQueCopiar));
			}

			if(copiandoAUnModeloExistente)
				modeloDestino.DispararRecibioCopiaProfunda(modeloQueCopiar);

			return modeloDestino;
		}
		#endregion 

		#endregion
	}
}
