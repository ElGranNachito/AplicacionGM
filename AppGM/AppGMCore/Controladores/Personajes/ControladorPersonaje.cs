using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using CoolLogs;

namespace AppGM.Core
{
    #region Delegados
    
    /// <summary>
    /// Representa un metodo que lidia con eventos de toma de daño de un personaje
    /// </summary>
    /// <param name="cantidad">Cantidad de daño sufrido</param>
    /// <param name="eTipo">Tipo de daño sufrido</param>
    /// <param name="objetivo">Receptor del daño</param>
    /// <param name="instigador">Inflictor del daño</param>
    public delegate void dSufrirDaño(
	    ref int cantidad,
    ETipoDeDaño eTipo,
    IDañable objetivo,
    object instigador);

    /// <summary>
    /// Representa un metodo que lidia con eventos de modificacion de inventario
    /// </summary>
    /// <param name="item">Objeto que fue añadido al inventario</param>
    /// <param name="objetivo"><see cref="ControladorPersonaje"/> cuyo inventario fue modificado</param>
    public delegate void dModificarInventario(
        ControladorBase item,
        ControladorPersonaje objetivo);

    /// <summary>
    /// Representa un metodo que lidia con eventos de modificacion de aliados
    /// </summary>
    /// <param name="aliados">Aliados actuales</param>
    /// <param name="aliadosModificados">Aliados que fueron modificados</param>
    /// <param name="objetivo"><see cref="ControladorPersonaje"/> cuyos aliados cambiaron</param>
    public delegate void dModificarAliados(
        ControladorBase[] aliados,
        ControladorBase[] aliadosModificados,
        ControladorPersonaje objetivo);

    /// <summary>
    /// Representa un metodo que lidia con eventos de recibir un efecto
    /// </summary>
    /// <param name="efectoRecibido"><see cref="ControladorEfecto"/> recibido</param>
    /// <param name="objetivo"><see cref="ControladorPersonaje"/> que recibio el efecto</param>
    /// <param name="instigador"><see cref="ControladorPersonaje"/> que aplico el efecto</param>
    public delegate void dModificacionEfectos(
        ControladorEfectoSiendoAplicado efecto,
        ControladorPersonaje objetivo,
        ControladorPersonaje instigador);

    /// <summary>
    /// Representa un metodo que lidia con eventos de uso de habilidad
    /// </summary>
    /// <param name="habilidad"><see cref="ControladorHabilidad"/> utilizado</param>
    /// <param name="usuario"><see cref="ControladorPersonaje"/> que utilizo la habilidad</param>
    /// <param name="objetivos"><see cref="ControladorPersonaje"/> que reciben los efectos de la habilidad</param>
    public delegate void dUtilizarHabilidad(
        ControladorHabilidad habilidad,
        ControladorPersonaje usuario,
        List<ControladorPersonaje> objetivos);

    /// <summary>
    /// Representa un metodo que lidia con eventos de movimiento
    /// </summary>
    /// <param name="posicionAntigua">Posicion anterior</param>
    /// <param name="posicionNueva">Posicion actual</param>
    public delegate void dMoverse(
        Vector2 posicionAntigua,
        Vector2 posicionNueva);

    /// <summary>
    /// Representa un metodo que lidia con el evento de morir
    /// </summary>
    /// <param name="objetivo"><see cref="ControladorPersonaje"/> que muere</param>
    public delegate void dMorir(ControladorPersonaje objetivo);

    /// <summary>
    /// Representa un metodo que lidia con el evento de realizar una accion generica
    /// </summary>
    /// <param name="descripcion">Descripccion de la accion</param>
    public delegate void dRealizarAccion(string descripcion);

    /// <summary>
    /// Representa un metodo que lidia con el evento de entrar o salir de un combate
    /// </summary>
    /// <param name="personaje"><see cref="ControladorPersonaje"/> que salio o entro de combate</param>
    /// <param name="combate"><see cref="ControladorAdministradorDeCombate"/> del que salio</param>
    public delegate void dEntrarSalirCombate(
        ControladorPersonaje personaje,
        ControladorAdministradorDeCombate combate);

    /// <summary>
    /// Representa un metodo que lidia con el evento de curarse
    /// </summary>
    /// <param name="valor">Cantidad de puntos de vida que se curo</param>
    /// <param name="personaje"><see cref="ControladorPersonaje"/> que recibe la curacion</param>
    /// <param name="fuenteP"><see cref="ControladorPersonaje"/> que realizo la curacion</param>
    /// <param name="fuenteI"><see cref="ControladorItem"/> que realizo la curacion</param>
    public delegate void dCurarse(
        ref int valor, 
        ControladorPersonaje personaje,
        ControladorPersonaje fuenteP,
        ControladorItem fuenteI);
    
    #endregion

    /// <summary>
    /// Controlador de un personaje
    /// </summary>
    public class ControladorPersonaje : Controlador<ModeloPersonaje>, IDañable, IInfligidorDaño
    {
        #region Propiedades

        //-----------------------------PROPIEDADES (CONTROLADORES)-------------------------------

        /// <summary>
        /// Efectos que actualmente se estan aplicando a este personaje
        /// </summary>
        public List<ControladorEfectoSiendoAplicado> Efectos { get; set; } = new List<ControladorEfectoSiendoAplicado>();

        /// <summary>
        /// Items que el personaje tiene en su inventario
        /// </summary>
        public List<ControladorItem> Inventario { get; set; } = new List<ControladorItem>();

        /// <summary>
        /// Alianzas a las que pertenece el personaje
        /// </summary>
        public List<ControladorAlianza> Alianzas { get; set; } = new List<ControladorAlianza>();

        /// <summary>
        /// Habilidades del personaje
        /// </summary>
        public List<ControladorHabilidad> Skills { get; set; } = new List<ControladorHabilidad>();

        /// <summary>
        /// Perks del personaje
        /// </summary>
        public List<ControladorHabilidadGenerico<ModeloPerk>> Perks { get; set; } = new List<ControladorHabilidadGenerico<ModeloPerk>>();

        /// <summary>
        /// Magias del personaje
        /// </summary>
        public List<ControladorHabilidadGenerico<ModeloMagia>> Magias { get; set; } = new List<ControladorHabilidadGenerico<ModeloMagia>>();

        /// <summary>
        /// Noble Phantasms del personaje
        /// </summary>
        public List<ControladorHabilidadGenerico<ModeloNoblePhantasm>> NomblePhantasms { get; set; } = new List<ControladorHabilidadGenerico<ModeloNoblePhantasm>>();

        /// <summary>
        /// Invocaciones que ha generado el personaje
        /// </summary>
        public List<ControladorInvocacion> ControladorInvocaciones { get; set; }

        //-----------------------------PROPIEDADES (VARIAS)-------------------------------

        /// <summary>
        /// Devuelve verdadero si el personaje esta vivo
        /// </summary>
        public bool EstaVivo => modelo.Hp > 0;

        /// <summary>
        /// Muestra datos basicos del personaje
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Nombre={modelo.Nombre} Id={modelo.Id}";

        public int Hp
        {
	        get => modelo.Hp;
	        set => modelo.Hp = value;
        }

        public int MaxHp
        {
	        get => modelo.MaxHp;
        }

        public int Str
        {
	        get => modelo.Str;
	        set => modelo.Str = value;
        }

        public int Agi
        {
	        get => modelo.Agi;
	        set => modelo.Agi = value;
        }

        public int End
        {
	        get => modelo.End;
	        set => modelo.End = value;
        }

        public int Int
        {
	        get => modelo.Int;
	        set => modelo.Int = value;
        }

        public int Lck
        {
	        get => modelo.Lck;
	        set => modelo.Lck = value;
        }

        public int Chr
        {
	        get
	        {
		        if (modelo is ModeloMaster m)
			        return m.Chr;

                SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener la stat CHR de {this}, pero no es un master", ESeveridad.Error);

                return -1;
	        }
	        set
	        {
		        if (modelo is ModeloMaster m)
		        {
			        m.Chr = value;

                    return;
		        }

		        SistemaPrincipal.LoggerGlobal.Log($"Se intento establecer la stat CHR de {this}, pero no es un master", ESeveridad.Error);
	        }
        }

        public ERango Np
        {
	        get
	        {
		        if (modelo is ModeloServant s)
			        return s.RangoNP;

		        SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener la stat NP de {this}, pero no es un servant", ESeveridad.Error);

		        return ERango.NINGUNO;
	        }
	        set
	        {
		        if (modelo is ModeloServant s)
		        {
			        s.RangoNP = value;

			        return;
		        }

		        SistemaPrincipal.LoggerGlobal.Log($"Se intento establecer la stat NP de {this}, pero no es un servant", ESeveridad.Error);
	        }
        }

        public int VentajaStr
        {
	        get => modelo.VentajaStr;
	        set => modelo.VentajaStr = value;
        }

        public int VentajaAgi
        {
	        get => modelo.VentajaAgi;
	        set => modelo.VentajaAgi = value;
        }

        public int VentajaEnd
        {
	        get => modelo.VentajaEnd;
	        set => modelo.VentajaEnd = value;
        }

        public int VentajaInt
        {
	        get => modelo.VentajaAgi;
	        set => modelo.VentajaAgi = value;
        }

        public int VentajaLck
        {
	        get => modelo.VentajaLck;
	        set => modelo.VentajaLck = value;
        }

        public int VentajaChr
        {
	        get
	        {
		        if (modelo is ModeloMaster m)
			        return m.VentajaChr;

		        SistemaPrincipal.LoggerGlobal.Log($"Se intento obtener la ventaja en CHR de {this}, pero no es un master", ESeveridad.Error);

		        return 0;
	        }
	        set
	        {
		        if (modelo is ModeloMaster m)
		        {
			        m.VentajaChr = value;

                    return;
		        }

		        SistemaPrincipal.LoggerGlobal.Log($"Se intento establecer la ventaja en CHR de {this}, pero no es un master", ESeveridad.Error);
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se dispara cuando el personaje sufre daño
        /// </summary>
        public event dSufrirDaño          OnSufrirDaño        = delegate { };

        /// <summary>
        /// Evento que se dispara cuando un item es añadido al inventario del personaje
        /// </summary>
        public event dModificarInventario OnAñadirItem        = delegate { };

        /// <summary>
        /// Evento que se dispara cuando un items es quitado del inventario del personaje
        /// </summary>
        public event dModificarInventario OnQuitarItem        = delegate { };

        /// <summary>
        /// Evento que se dispara cuando se añade uno o mas aliados
        /// </summary>
        public event dModificarAliados    OnAñadirAliado      = delegate { };

        /// <summary>
        /// Evento que se dispara cuando se quita uno a mas aliados
        /// </summary>
        public event dModificarAliados    OnQuitarAliado      = delegate { };

        /// <summary>
        /// Evento que se dispara cuando el personaje recibe un efecto
        /// </summary>
        public event dModificacionEfectos OnRecibirEfecto     = delegate { };

        /// <summary>
        /// Evento que se dispara cuando un evento se quita del personaje
        /// </summary>
        public event dModificacionEfectos OnQuitarEfecto = delegate { };

        /// <summary>
        /// Evento que se dispara cuando el personaje utiliza una habilidad
        /// </summary>
        public event dUtilizarHabilidad   OnUtilizarHabilidad = delegate { };

        /// <summary>
        /// Evento que se dispara cuando el personaje se mueve
        /// </summary>
        public event dMoverse             OnMoverse           = delegate { };

        /// <summary>
        /// Evento que se dispara cuando el personaje muere
        /// </summary>
        public event dMorir               OnMorir             = delegate { };

        /// <summary>
        /// Evento que se dispara cuando el personaje realiza una accion
        /// </summary>
        public event dRealizarAccion      OnRealizarAccion    = delegate { };

        /// <summary>
        /// Evento que se dispara cuando el personaje entra en combate
        /// </summary>
        public event dEntrarSalirCombate  OnEntrarEnCombate   = delegate { };

        /// <summary>
        /// Evento que se dispara cuando el personaje sale de combate
        /// </summary>
        public event dEntrarSalirCombate  OnSalirDeCombate    = delegate { };

        /// <summary>
        /// Evento que se dispara cuando el personaje recibe una curacion
        /// </summary>
        public event dCurarse             OnRecibirCuracion   = delegate { };

        public event IInfligidorDaño.dInfligirDaño OnInfligirDaño = delegate{};

        public event IDañable.dDañado OnDañado;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloPersonaje">Modelo del personaje que representa este controlador</param>
        public ControladorPersonaje(ModeloPersonaje _modeloPersonaje)
            : base(_modeloPersonaje)
        {
	        for (int i = 0; i < modelo.Alianzas.Count; ++i)
            {
                Alianzas.Add(SistemaPrincipal.ObtenerControlador<ControladorAlianza, ModeloAlianza>(modelo.Alianzas[i], true));
            }

            foreach (var habilidad in modelo.Habilidades)
            {
	            switch (habilidad.TipoDeHabilidad)
	            {
		            case ETipoHabilidad.Skill:
		            {
			            var controladorSkill = SistemaPrincipal.ObtenerControlador<ControladorHabilidad, ModeloHabilidad>(habilidad, true);

			            Skills.Add(controladorSkill);

                        break;
		            }

		            case ETipoHabilidad.Hechizo:
		            {
			            var controladorHechizo = SistemaPrincipal.ObtenerControlador<ControladorHabilidadGenerico<ModeloMagia>, ModeloHabilidad>(habilidad, true);

			            Magias.Add(controladorHechizo);

                        break;
		            }

		            case ETipoHabilidad.Perk:
		            {
			            var controladorPerk = SistemaPrincipal.ObtenerControlador<ControladorHabilidadGenerico<ModeloPerk>, ModeloHabilidad>(habilidad, true) as ControladorHabilidadGenerico<ModeloPerk>;

			            Perks.Add(controladorPerk);

			            break;
		            }

		            case ETipoHabilidad.NoblePhantasm:
		            {
			            var controladorNp = SistemaPrincipal.ObtenerControlador<ControladorHabilidadGenerico<ModeloNoblePhantasm>, ModeloHabilidad>(habilidad, true) as ControladorHabilidadGenerico<ModeloNoblePhantasm>;

			            NomblePhantasms.Add(controladorNp);

			            break;
		            }
	            }
            }

            foreach (var item in modelo.Inventario)
            {
				Inventario.Add(SistemaPrincipal.ObtenerControlador<ControladorItem, ModeloItem>(item, true));	            
            }

            CargarVariablesYTiradas();
        }

        #endregion

        #region Metodos

        protected void ModificarVida(int cantidad)
        {
            if ((modelo.Hp = Math.Clamp(modelo.Hp + cantidad, int.MinValue, modelo.MaxHp)) <= 0)
                OnMorir(this);
        }

        /// <summary>
        /// Cura al personaje
        /// </summary>
        /// <param name="cantidad">Valor de la curacion</param>
        /// <param name="fuenteP">Personaje que realizo la curacion</param>
        /// <param name="fuenteI">Item que realizo la curacion</param>
        public void Curar(int cantidad, ControladorPersonaje fuenteP, ControladorItem fuenteI)
        {
            //Llamamos al evento antes de realizar la curacion por si algunos de los metodos subscritos modifica el valor de curacion
            OnRecibirCuracion(ref cantidad, this, fuenteP, fuenteI);

            ModificarVida(cantidad);
        }

        
        public void Dañar(ModeloArgumentosDaño argsDaño, SortedList<int, SubobjetivoDaño> subObjetivos = null)
        {
	        throw new NotImplementedException();
        }

        public void InfligirDaño(IDañable objetivo, ModeloArgumentosDaño argsDaño, SortedList<int, IDañable> subObjetivos = null)
        {
	        throw new NotImplementedException();
        }

        /// <summary>
        /// Aplica un efecto al personaje
        /// </summary>
        /// <param name="efecto"><see cref="ControladorEfectoSiendoAplicado"/> que aplicar al personaje</param>
        public void AñadirEfecto(ControladorEfectoSiendoAplicado efecto)
        {
            Efectos.Add(efecto);

            //TODO:Añadir el efecto a lista de efectos aplicandose cuando el ModeloPersonaje este actualizado

            OnRecibirEfecto(efecto, this, efecto.instigador);
        }

        /// <summary>
        /// Quita un efecto del personaje
        /// </summary>
        /// <param name="efecto"><see cref="ControladorEfectoSiendoAplicado"/> que quitar</param>
        public void QuitarEfecto(ControladorEfectoSiendoAplicado efecto)
        {
            //Si el efecto se quito de la lista...
	        if (Efectos.Remove(efecto))
	        {
                //Disparamos el evento OnQuitarEfecto
		        OnQuitarEfecto(efecto, this, efecto.instigador);
	        }
        }

        /// <summary>
        /// Metodo que lidia con las operacion necesarias a realizar cada vez que avanza un turno
        /// </summary>
        public virtual void AvanzarTurno()
        {
            for (int i = 0; i < Efectos.Count; ++i)
                Efectos[i].AplicarEfecto();

            //TODO: Aplicar los efectos
        }
        
        /// <summary>
        /// Metodo que realiza las operaciones necesarias para que un personaje entre en un combate
        /// </summary>
        /// <param name="combate"><see cref="ControladorAdministradorDeCombate"/> al que se entra</param>
        public virtual void EntrarEnCombate(ControladorAdministradorDeCombate combate)
        {
            modelo.EstaEnCombate = true;

            OnEntrarEnCombate(this, combate);
        }

        /// <summary>
        /// Metodo que realiza las operaciones necesarias para que un personaje salga de un combate
        /// </summary>
        /// <param name="combate"></param>
        public virtual void SalirDeCombate(ControladorAdministradorDeCombate combate)
        {
            modelo.EstaEnCombate = false;

            OnSalirDeCombate(this, combate);
        }
        
        /// <summary>
        /// Modifica el valor de una stat.
        /// </summary>
        /// <param name="stat">Nombre exacto de la propiedad que contiene el valor de la stat. (Usar nameof)</param>
        /// <param name="valor">Valor que asignarle a la stat</param>
        public void ModificarStat(string stat, object valor)
        {
	        var propiedad = typeof(ModeloPersonaje).GetProperty(stat, BindingFlags.Public | BindingFlags.Instance);

            if(propiedad != null)
				propiedad.SetValue(modelo, valor);
        }

        /// <summary>
        /// Añade un item al inventario del personaje
        /// </summary>
        /// <param name="item">Item que añadir</param>
        public void AñadirItem(ControladorBase item)
        {
            OnAñadirItem(item, this);
        }

        /// <summary>
        /// Quita un item del inventario del personaje
        /// </summary>
        /// <param name="item">Item que quitar</param>
        public void QuitarItem(ControladorBase item)
        {
            OnQuitarItem(item, this);
        }

        public List<ControladorHabilidad> ObtenerHabilidades()
        {
	        return Skills.Concat(Perks).Concat(Magias).Concat(NomblePhantasms).ToList();
        }

        /// <summary>
        /// Obtiene el valor de una <paramref name="stat"/> de este personaje
        /// </summary>
        /// <param name="stat">Stat cuyo valor obtener</param>
        /// <returns>Valor de la <paramref name="stat"/></returns>
        public int ObtenerValorStat(EStat stat)
        {
	        switch (stat)
	        {
		        case EStat.STR:
			        return Str;

		        case EStat.AGI:
			        return Agi;

		        case EStat.END:
			        return End;

		        case EStat.INT:
			        return Int;

		        case EStat.LCK:
			        return Lck;

		        case EStat.CHR:
			        return Chr;

                case EStat.NP:
	                return (int)Np;

		        default:
		        {
			        SistemaPrincipal.LoggerGlobal.Log($"valor de {nameof(stat)}({stat}) no soportado", ESeveridad.Error);

			        return 0;
		        }
            }
        }

        /// <summary>
        /// Obtiene la ventaja que tiene este personaje en una <paramref name="stat"/>
        /// </summary>
        /// <param name="stat">Stat cuya ventaja obtener</param>
        /// <returns>Ventaja que tiene el personaje en la <paramref name="stat"/></returns>
        public int ObtenerVentajaStat(EStat stat)
        {
	        switch (stat)
	        {
		        case EStat.STR:
			        return VentajaStr;

		        case EStat.AGI:
			        return VentajaAgi;

		        case EStat.END:
			        return VentajaEnd;

		        case EStat.INT:
			        return VentajaInt;

		        case EStat.LCK:
			        return VentajaLck;

		        case EStat.CHR:
			        return VentajaChr;

		        default:
		        {
			        SistemaPrincipal.LoggerGlobal.Log($"valor de {nameof(stat)}({stat}) no soportado", ESeveridad.Error);

			        return 0;
		        }
	        }
        }

        /// <summary>
        /// Obtiene el modificador que tiene este personaje en una <paramref name="stat"/>
        /// </summary>
        /// <param name="stat">Stat cuyo modificador obtener</param>
        /// <returns>Modificador de la <paramref name="stat"/></returns>
        public int ObtenerModificadorStat(EStat stat)
        {
	        return Helpers.Juego.ObtenerModificadorStat(ObtenerValorStat(stat) + ObtenerVentajaStat(stat));
        }

        /// <summary>
        /// Establece el valor de una <paramref name="stat"/>
        /// </summary>
        /// <param name="stat">Stat cuyo valor establecer</param>
        /// <param name="valor">Valor que darle a la stat</param>
        public void EstablecerValorStat(EStat stat, int valor)
        {
	        switch (stat)
	        {
                case EStat.STR:
	                Str = valor;
	                break;

                case EStat.AGI:
	                Agi = valor;
                    break;

                case EStat.END:
	                End = valor;
	                break;

                case EStat.INT:
	                Int = valor;
                    break;

                case EStat.LCK:
	                Lck = valor;
	                break;

                case EStat.CHR:
	                Chr = valor;
                    break;

                case EStat.NP:
	                Np = valor.ARango();
                    break;

                default:

                    SistemaPrincipal.LoggerGlobal.Log($"valor de {nameof(stat)}({stat}) no soportado", ESeveridad.Error);

	                break;
	        }
        }

        /// <summary>
        /// Establece el valor de una <see cref="stat"/>
        /// </summary>
        /// <param name="stat">Stat cuyo valor establecer</param>
        /// <param name="valor">Rango que asignarle a la stat</param>
        public void EstablecerValorStat(EStat stat, ERango valor)
        {
            EstablecerValorStat(stat, (int)valor);
        }

        /// <summary>
        /// Establece la ventaja que tiene este personaje en una <paramref name="stat"/>
        /// </summary>
        /// <param name="stat">Stat cuya ventaja establecer</param>
        /// <param name="valor">Ventaja que asignarle a la <paramref name="stat"/></param>
        public void EstablecerValorVentajaStat(EStat stat, int valor)
        {
	        switch (stat)
	        {
		        case EStat.STR:
			        VentajaStr = valor;
			        break;

		        case EStat.AGI:
			        VentajaAgi = valor;
			        break;

		        case EStat.END:
			        VentajaEnd = valor;
			        break;

		        case EStat.INT:
			        VentajaInt = valor;
			        break;

		        case EStat.LCK:
			        VentajaLck = valor;
			        break;

                case EStat.CHR:
	                VentajaChr = valor;
                    break;

		        default:

			        SistemaPrincipal.LoggerGlobal.Log($"valor de {nameof(stat)}({stat}) no soportado");

			        break;
	        }
        }

        public ControladorAmbiente ObtenerAmbienteGlobal()
        {
            return SistemaPrincipal.ObtenerControlador<ControladorAmbiente, ModeloAmbiente>(SistemaPrincipal.ModeloRolActual.AmbienteGlobal);
        }

        public ControladorAmbiente ObtenerAmbienteCombateActual()
        {
            var participante = (from participacion in modelo.ParticipacionEnCombates
                where participacion.CombateActual.EstaActivo select participacion).FirstOrDefault();

            return SistemaPrincipal.ObtenerControlador<ControladorAmbiente, ModeloAmbiente>(participante.CombateActual.AmbienteDelCombate);
        }

        public override bool Equals(string cadena)
        {
	        return Regex.IsMatch(modelo.Nombre, $".*{cadena}.*");
        }

        public override ViewModelItemListaBase CrearViewModelItem()
        {
	        return new ViewModelPersonajeItem(this);
        }

        #endregion
    }

    /// <summary>
    /// Una clase para asegurarnos de que los <see cref="ModeloPersonaje"/> sean de cierto tipo, asi podemos evitar errores en los casteos
    /// </summary>
    /// <typeparam name="TipoPersonaje">Tipo del modelo</typeparam>
    public class ControladorPersongajeG<TipoPersonaje> : ControladorPersonaje
        where TipoPersonaje : ModeloPersonaje, new()
    {
        public ControladorPersongajeG(TipoPersonaje _personaje) : base(_personaje) {}
        public static Type ObtenerTipo() => typeof(TipoPersonaje);
    }
}