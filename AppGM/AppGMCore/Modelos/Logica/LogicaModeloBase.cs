using System;
using System.Collections.Generic;
using System.Collections;
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
		#region Copiado profundo

		/// <summary>
		/// Crea una copia superficial de un <see cref="ModeloBase"/> de manera sincronica
		/// </summary>
		/// <param name="modeloAlQueCopiarLosDatos">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <param name="outModelosEliminados">Modelos que fueron eliminados</param>
		/// <param name="outModelosAñadidos">Modelos que fueron añadidos</param>
		/// <returns>En caso de que <paramref name="modeloAlQueCopiarLosDatos"/> sea null, devuelve un nuevo modelo con los datos copiados.
		/// En caso de que no lo sea devuelve una referencia al modelo existente al que se copiaron los datos</returns>
		public ModeloBase CrearCopiaProfundaEnSubtipo(
			Type tipoAlQueCopiarLosDatos,
			ModeloBase modeloAlQueCopiarLosDatos = null,
			ModeloBase modeloQueCopiar = null,
			List<ModeloBase> outModelosEliminados = null,
			List<ModeloBase> outModelosAñadidos = null)
		{
			modeloQueCopiar ??= this;

			//No aseguramos que en caso de que se este copiando a un modelo existente este sea igual al tipo al que se copiaran los datos
			if (modeloAlQueCopiarLosDatos != null && !tipoAlQueCopiarLosDatos.IsAssignableFrom(modeloAlQueCopiarLosDatos.GetType()))
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"no se puede asignar a {tipoAlQueCopiarLosDatos.Name} desde {modeloAlQueCopiarLosDatos.GetType().Name} ");

				return null;
			}

			var metodoGenerico = typeof(ModeloBase).GetMethod(nameof(CrearCopiaProfundaEnSubtipo_Interno), BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(tipoAlQueCopiarLosDatos);

			return (ModeloBase)metodoGenerico.Invoke(this, new object[] { modeloAlQueCopiarLosDatos, modeloQueCopiar, outModelosEliminados, outModelosAñadidos, null });
		}

		/// <summary>
		/// Crea una copia superficial de un <see cref="ModeloBase"/> de manera sincronica
		/// </summary>
		/// <typeparam name="TResultado">Tipo del modelo al que se copiaran los datos. Debe ser un tipo asignable al tipo del <paramref name="modeloQueCopiar"/></typeparam>
		/// <typeparam name="TModeloOrigen">Tipo del modelo del que se copiaran los datos</typeparam>
		/// <param name="modeloAlQueCopiarLosDatos">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <param name="outModelosEliminados">Modelos que fueron eliminados</param>
		/// <param name="outModelosAñadidos">Modelos que fueron añadidos</param>
		/// <returns>En caso de que <paramref name="modeloAlQueCopiarLosDatos"/> sea null, devuelve un nuevo modelo con los datos copiados.
		/// En caso de que no lo sea devuelve una referencia al modelo existente al que se copiaron los datos</returns>
		public TResultado CrearCopiaProfundaEnSubtipo<TResultado, TModeloOrigen>(
			TResultado modeloAlQueCopiarLosDatos = null,
			TModeloOrigen modeloQueCopiar = null,
			List<ModeloBase> outModelosEliminados = null,
			List<ModeloBase> outModelosAñadidos = null)

			where TModeloOrigen : ModeloBase, new()
			where TResultado : ModeloBase, new()
		{
			return CrearCopiaProfundaEnSubtipo_Interno<TResultado>(modeloAlQueCopiarLosDatos, modeloQueCopiar, outModelosEliminados, outModelosAñadidos, null);
		}

		/// <summary>
		/// Crea una copia superficial de un <see cref="ModeloBase"/> de manera asincronica
		/// </summary>
		/// <param name="modeloAlQueCopiarLosDatos">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <param name="outModelosEliminados">Modelos que fueron eliminados</param>
		/// <param name="outModelosAñadidos">Modelos que fueron añadidos</param>
		/// <returns>En caso de que <paramref name="modeloAlQueCopiarLosDatos"/> sea null, devuelve un nuevo modelo con los datos copiados.
		/// En caso de que no lo sea devuelve una referencia al modelo existente al que se copiaron los datos</returns>
		public async Task<ModeloBase> CrearCopiaProfundaEnSubtipoAsync(
			Type tipoAlQueCopiarLosDatos,
			ModeloBase modeloAlQueCopiarLosDatos = null,
			ModeloBase modeloQueCopiar = null,
			List<ModeloBase> outModelosEliminados = null,
			List<ModeloBase> outModelosAñadidos = null)
		{
			return await Task.Run(() =>
			{
				return CrearCopiaProfundaEnSubtipo(tipoAlQueCopiarLosDatos, modeloAlQueCopiarLosDatos, modeloQueCopiar, outModelosEliminados, outModelosAñadidos);
			});
		}

		/// <summary>
		/// Crea una copia superficial de un <see cref="ModeloBase"/> de manera asincronica
		/// </summary>
		/// <typeparam name="TResultado">Tipo del modelo al que se copiaran los datos. Debe ser un tipo asignable al tipo del <paramref name="modeloQueCopiar"/></typeparam>
		/// <typeparam name="TModeloOrigen">Tipo del modelo del que se copiaran los datos</typeparam>
		/// <param name="modeloAlQueCopiarLosDatos">Modelo al que se copiaran los datos. Si se deja como null se copiaran a una nueva instancia</param>
		/// <param name="modeloQueCopiar">Modelo cuyos datos seran copiados</param>
		/// <param name="outModelosEliminados">Modelos que fueron eliminados</param>
		/// <param name="outModelosAñadidos">Modelos que fueron añadidos</param>
		/// <returns>En caso de que <paramref name="modeloAlQueCopiarLosDatos"/> sea null, devuelve un nuevo modelo con los datos copiados.
		/// En caso de que no lo sea devuelve una referencia al modelo existente al que se copiaron los datos</returns>
		public async Task<TResultado> CrearCopiaProfundaEnSubtipoAsync<TResultado, TModeloOrigen>(
			TResultado modeloAlQueCopiarLosDatos = null,
			TModeloOrigen modeloQueCopiar = null,
			List<ModeloBase> outModelosEliminados = null,
			List<ModeloBase> outModelosAñadidos = null)

			where TModeloOrigen : TResultado, new()
			where TResultado : ModeloBase, new()
		{
			return await Task.Run(() =>
			{
				return CrearCopiaProfundaEnSubtipo_Interno<TResultado>(modeloAlQueCopiarLosDatos, modeloQueCopiar, outModelosEliminados, outModelosAñadidos, null);
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
			Dictionary<ModeloBase, ModeloBase> modelosYaCopiados)

			where TResultado : ModeloBase, new()
		{
			bool copiandoAUnModeloExistente = modeloDestino != null;

			//Creamos una nueva instancia del modelo resultado
			modeloDestino ??= new TResultado();

			modeloQueCopiar ??= this;

			modelosYaCopiados ??= new Dictionary<ModeloBase, ModeloBase>();

			//Si ya existe una copia del modelo que se pretende copiar entonces la retornamos
			if (modelosYaCopiados.ContainsKey(modeloQueCopiar))
			{
				SistemaPrincipal.LoggerGlobal.Log($"Intentado copiar un modelo del que ya existe una copia", ESeveridad.Error);

				return (TResultado)modelosYaCopiados[modeloQueCopiar];
			}

			//Añadimos este modelo a la lista de modelos ya copiados
			modelosYaCopiados.Add(modeloQueCopiar, modeloDestino);

			//Obtenemos las propiedades a las que podemos escribir
			var propiedades = typeof(TResultado).GetProperties().Where(p => p.CanWrite).ToList();

			//Obtenemos los campos
			var campos = typeof(TResultado).GetFields().ToList();

			var tipoModeloOrigen = modeloQueCopiar.GetType();

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

									var equivalenteEnDestino = listaModeloDestinoCasteada.FirstOrDefault(m => m.Id == elementoActualListaOrigen.Id);

									//Si no hemos copiado este modelo aun, lo copiamos a su par en el destino
									if (!modelosYaCopiados.ContainsKey(elementoActualListaOrigen))
									{
										var resultadoCopia = metodoClonar.MakeGenericMethod(elementoActualListaOrigen.GetType()).Invoke(elementoActualListaOrigen, new object[] { equivalenteEnDestino, null, outModelosEliminados, outModelosAñadidos, modelosYaCopiados });

										if (equivalenteEnDestino == null)
										{
											listaModeloDestinoCasteada.Add((ModeloBase)resultadoCopia);
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

								//Los modelos restantes, es decir los que no se encontraban en ambas listas, son modelos que fueron eliminados, si 
								//se encontraban en el destino, o añadidos, si se encontraban en el modelo de origen
								outModelosAñadidos?.AddRange(listaModeloDestinoCasteada.FindAll(m => !modelosRestantesDestino.Any(ma => ma.Id == m.Id)));
								outModelosEliminados?.AddRange(listaModeloDestinoCasteada.FindAll(m => !modelosRestantesModeloActual.Any(ma => ma.Id == m.Id)));
							}
							//Si no estamos copiando a un modelo existente...
							else
							{
								//Obtenemos el valor de la propiedad
								var nuevaLista = propiedadActual.GetValue(modeloDestino) as IList;

								//Por cada elemento en la lista del modelo origen...
								for (int i = 0; i < listaModeloOrigen.Count; ++i)
								{
									//Si ya existe una copia del modelo que queremos copiar entonces añadimos a la lista del destino el modelo ya copiado
									if (modelosYaCopiados.ContainsKey(listaModeloOrigen[i]))
									{
										nuevaLista.Add(modelosYaCopiados[listaModeloOrigen[i]]);
									}
									//Si no lo hace creamos una copia del elemento actual en un modelo vacio y lo añadimos a la lista
									else
									{
										var metodoGenerico = metodoClonar.MakeGenericMethod(listaModeloOrigen[i].GetType());

										var copia = metodoGenerico.Invoke(listaModeloOrigen[i], new object[] { null, null, outModelosEliminados, outModelosAñadidos, modelosYaCopiados }) as ModeloBase;

										nuevaLista.Add(copia);
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

						//Si ya se ha copiado este modelo entonces simplemente asignamos el valor de la propiedad al modelo copiado
						if (modelosYaCopiados.ContainsKey(valorPropiedadOrigen))
						{
							propiedadActual.SetValue(modeloDestino, modelosYaCopiados[valorPropiedadOrigen]);
						}
						//Si no...
						else
						{
							var metodoClonarGenerico = metodoClonar.MakeGenericMethod(propiedadActual.PropertyType);

							//Si estamos copiando a un modelo existente entonces copiamos el valor de la propiedad de origen al modelo existente en la propiedad de destino
							if (copiandoAUnModeloExistente)
							{
								metodoClonarGenerico.Invoke(valorPropiedadOrigen, new object[] { propiedadActual.GetValue(modeloDestino), null, null, null, modelosYaCopiados });
							}
							//Sino simplemente creamos una copia del modelo actual.
							else
							{
								var copiaModelo = metodoClonarGenerico.Invoke(valorPropiedadOrigen, new object[] { null, null, null, null, modelosYaCopiados });

								propiedadActual.SetValue(modeloDestino, copiaModelo);
							}

							if (valorPropiedadDestino == null)
							{
								outModelosAñadidos?.Add(modelosYaCopiados[valorPropiedadOrigen]);
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

			return modeloDestino;
		}
		#endregion
	}
}
