using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Threading.Tasks;

using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador para una funcion encargada de lidiar con eventos
	/// </summary>
	public class ControladorFuncion_HandlerEvento : ControladorFuncion<Action<ControladorFuncionBase, object[]>>
	{
		/// <summary>
		/// Delegado que se subscribira a los eventos e internamente transforma los argumentos a una lista de
		/// <see cref="object"/> para luego pasarlos a <see cref="EjecutarFuncion"/>
		/// </summary>
		private Delegate HandlerEvento;

		/// <summary>
		/// Diccionario que relaciona un <see cref="ControladorBase"/> con una <see cref="List{T}"/> de <see cref="EventInfo"/>
		/// contenidos por dicho controlador a los que se encuentra subscripto el <see cref="HandlerEvento"/>
		/// </summary>
		private readonly Dictionary<ControladorBase, List<EventInfo>> mEventosALosQueEstaSubscripto = new Dictionary<ControladorBase, List<EventInfo>>();

		/// <summary>
		/// Modelo de tipo <see cref="ModeloFuncion_HandlerEvento"/> para facilitar el acceso a varios miembros
		/// </summary>
		public readonly ModeloFuncion_HandlerEvento modeloGenerico;

		/// <summary>
		/// Tipo del handler que requieren los eventos a los que se puede subscribir el <see cref="HandlerEvento"/>
		/// </summary>
		public Type TipoHandler { get; init; }

		public ControladorFuncion_HandlerEvento(ModeloFuncion _modelo) : base(_modelo)
		{
			modeloGenerico = (ModeloFuncion_HandlerEvento) modelo;

			TipoHandler = modeloGenerico.TipoHandler;
		}

		/// <summary>
		/// Wrapper para llamar a la funcion subyacente de manera segura
		/// </summary>
		/// <param name="parametrosExtra">Parametros extra que toma la funcion</param>
		/// <returns>Tupla con dos <see cref="bool"/>, el primero indica si se pudo ejecutar la funcion en su totalidad y el segundo contiene su resultado</returns>
		private void EjecutarFuncion(params object[] parametrosExtra)
		{
			try
			{
				Funcion(this, parametrosExtra);
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.Log($"Error al intentar ejecutar funcion {this}.{Environment.NewLine}{ex.Message}", ESeveridad.Error);
			}
		}

		/// <summary>
		/// Vincula este handler a un <paramref name="evento"/> en un <paramref name="controlador"/>
		/// </summary>
		/// <param name="controlador"><see cref="ControladorBase"/> que contiene el <paramref name="evento"/></param>
		/// <param name="evento"><see cref="EventInfo"/> al que subscribirse</param>
		public async Task VincularAAsync(ControladorBase controlador, EventInfo evento)
		{
			//Nos aseguramos de que los tipos de los handlers coincidan
			if(evento.EventHandlerType != TipoHandler)
				SistemaPrincipal.LoggerGlobal.LogCrash($"Tipo de handler de {nameof(evento)}({evento.EventHandlerType}) no es del mismo tipo que {nameof(TipoHandler)}({TipoHandler})");

			try
			{
				//Si aun no se ha creado el handler, lo creamos
				if (HandlerEvento == null)
				{
					HandlerEvento = await Task.Run(() =>
					{
						//Obtenemos los parametros del metodo invoke del handler
						var parametrosMetodoInvoke = evento.EventHandlerType.GetMethod("Invoke").GetParameters();

						var expresionesHandler = new List<Expression>(2);
						var parametrosHandler  = new List<ParameterExpression>(parametrosMetodoInvoke.Length);

						//Creamos una nueva parameter expression por cada parametro requerido por el metodo invoke
						foreach (var parametro in parametrosMetodoInvoke)
						{
							parametrosHandler.Add(Expression.Parameter(parametro.ParameterType, parametro.Name));
						}

						//---Cuerpo de la funcion---
						
						
						//Cremos un arreglo con los parametros
						var expresionArregloParametros = Expression.NewArrayInit(typeof(object),
							parametrosHandler.Select(p => Expression.Convert(p, typeof(object))));

						expresionesHandler.Add(expresionArregloParametros);

						//Ejecutamos el metodo 'EjecutarFuncion' pasando como argumento la lista que creamos anteriormente
						var metodoEjecutarFuncion =
							typeof(ControladorFuncion_HandlerEvento).GetMethod(nameof(EjecutarFuncion),
								BindingFlags.NonPublic | BindingFlags.Instance);

						expresionesHandler.Add(Expression.Call(Expression.Constant(this), metodoEjecutarFuncion,
							expresionArregloParametros));

						//---Fin del cuerpo de la funcion---

						//Creamos un expresion lambda y la compilamos
						return Expression.Lambda(Expression.Block(expresionesHandler), parametrosHandler).Compile();
					});
					
				}

				//Añadimos el handler a la lista de invocacion del evento
				evento.AddEventHandler(controlador, HandlerEvento);

				if (mEventosALosQueEstaSubscripto.TryGetValue(controlador, out var eventos))
				{
					eventos.Add(evento);
				}
				else
				{
					mEventosALosQueEstaSubscripto.Add(controlador, new List<EventInfo>{evento});
				}
			}
			catch (Exception ex)
			{
				SistemaPrincipal.LoggerGlobal.LogCrash($"Error al vincular {this} con {evento} perteneciente a {controlador}{Environment.NewLine}{ex.Message}");
			}
		}

		/// <summary>
		/// Desvincula este handler de un <paramref name="evento"/>
		/// </summary>
		/// <param name="controlador"><see cref="ControladorBase"/> que contiene el <paramref name="evento"/></param>
		/// <param name="evento"><see cref="EventInfo"/> del que desvincularse</param>
		/// <returns><see cref="bool"/> indicando si se pudo desvincular con exito</returns>
		public bool DesvincularDe(ControladorBase controlador, EventInfo evento)
		{
			if (EstaVinculadoA(controlador, evento))
			{
				evento.RemoveEventHandler(controlador, HandlerEvento);

				return true;
			}

			return false;
		}

		/// <summary>
		/// Indica si el handler esta vinculado a un <paramref name="evento"/> en determinado <paramref name="controlador"/>
		/// </summary>
		/// <param name="controlador"><see cref="ControladorBase"/> que contiene el <paramref name="evento"/></param>
		/// <param name="evento"><see cref="EventInfo"/> del que desvincularse</param>
		/// <returns><see cref="bool"/> indicando si esta vinculado al <paramref name="evento"/></returns>
		public bool EstaVinculadoA(ControladorBase controlador, EventInfo evento)
		{
			return mEventosALosQueEstaSubscripto.TryGetValue(controlador, out var eventos) && eventos.Contains(evento);
		}

		public override ViewModelCreacionDeFuncionBase CrearVMParaEditar(Action<ViewModelCreacionDeFuncionBase> accionSalir)
		{
			return new ViewModelCreacionDeFuncionHandlerEvento(accionSalir, TipoHandler, this);
		}

		public override string ToString()
		{
			return $"Handler - {modeloGenerico.NombreFuncion} - Subscripto a eventos en {mEventosALosQueEstaSubscripto.Count} controladores";
		}
	}
}