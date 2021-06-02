using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AppGM.Core.Controladores.Efectos;

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
    /// <param name="fuenteI"><see cref="ControladorUtilizable"/> que realizo la curacion</param>
    public delegate void dCurarse(
        ref int valor, 
        ControladorPersonaje personaje,
        ControladorPersonaje fuenteP,
        ControladorUtilizable fuenteI);
    
    #endregion

    /// <summary>
    /// Representa una entidad que puede ser dañada
    /// </summary>
    public interface IDañable
    {
        void SufrirDaño(int cantidad, ETipoDeDaño ETipo, ControladorPersonaje instigador);
    }

    /// <summary>
    /// Controlador de un personaje
    /// </summary>
    public class ControladorPersonaje : Controlador<ModeloPersonaje>, IDañable
    {
        #region Propiedades

        //-----------------------------PROPIEDADES (CONTROLADORES)-------------------------------
   
        /// <summary>
        /// Efectos que actualmente se estan aplicando a este personaje
        /// </summary>
        public List<ControladorEfectoSiendoAplicado> Efectos { get; set; }

        /// <summary>
        /// Items que el personaje tiene en su inventario
        /// </summary>
        public List<ControladorUtilizable> Inventario { get; set; }

        /// <summary>
        /// Items defensivos que el personaje tiene equipados
        /// </summary>
        public List<ControladorDefensivo> Armadura { get; set; }

        /// <summary>
        /// Aliados del personaje
        /// </summary>
        public List<ControladorPersonaje> Aliados { get; set; }

        /// <summary>
        /// Perks del personaje
        /// </summary>
        public List<ControladorHabilidadG<ModeloPerk>> Perks { get; set; }

        /// <summary>
        /// Habilidades del personaje
        /// </summary>
        public List<ControladorHabilidad> Skills { get; set; }

        /// <summary>
        /// Magias del personaje
        /// </summary>
        public List<ControladorMagia> Magias { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //TODO: Revisar bien que era esto
        public List<ControladorModificadorDeStat> ModificadoresDeDefensa { get; set; }

        /// <summary>
        /// Invocaciones que ha generado el personaje
        /// </summary>
        public List<ControladorInvocacion> ControladorInvocaciones { get; set; }

        /// <summary>
        /// Noble Phantasms del personaje
        /// </summary>
        public List<ControladorHabilidadG<ModeloNoblePhantasm>> ControladorNoblePhantasms { get; set; }

        //-----------------------------PROPIEDADES (VARIAS)-------------------------------

        /// <summary>
        /// Devuelve verdadero si el personaje esta vivo
        /// </summary>
        public bool EstaVivo => modelo.Hp > 0;

        /// <summary>
        /// Muestra datos basicos del personaje
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format($"Nombre={modelo.Nombre} Id={modelo.Id}");

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

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloPersonaje">Modelo del personaje que representa este controlador</param>
        public ControladorPersonaje(ModeloPersonaje _modeloPersonaje)
            :base(_modeloPersonaje){}

        #endregion

        #region Funciones

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
        public void Curar(int cantidad, ControladorPersonaje fuenteP, ControladorUtilizable fuenteI)
        {
            //Llamamos al evento antes de realizar la curacion por si algunos de los metodos subscritos modifica el valor de curacion
            OnRecibirCuracion(ref cantidad, this, fuenteP, fuenteI);

            ModificarVida(cantidad);
        }

        /// <summary>
        /// Realiza daño al personaje
        /// </summary>
        /// <param name="cantidad">Cantidad de daño</param>
        /// <param name="eTipo">Tipo del daño realizado</param>
        /// <param name="instigador">Controlador del personaje que realiza el daño</param>
        public void SufrirDaño(int cantidad, ETipoDeDaño eTipo, ControladorPersonaje instigador)
        {
            //Llamamos el evento de sufrir daño antes por si algunos de los metodos subscritos modifica el daño realizado
            OnSufrirDaño(ref cantidad, eTipo, this, instigador);

            ModificarVida(-cantidad);
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
                Efectos[i].AplicarEfecto(this);

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

        /// <summary>
        /// Obtiene el path a la imagen del personaje
        /// </summary>
        /// <param name="nivelDelDirectorioActual">Nivel del directorio en el que se encuentra el control con respecto a la carpeta Media</param>
        /// <returns>Ruta absoluta de la imagen del personaje</returns>
        public string ObtenerPathAImagen(byte nivelDelDirectorioActual)
        {
            StringBuilder sb = new StringBuilder();

            for(; nivelDelDirectorioActual > 0; --nivelDelDirectorioActual)
                sb.Append("../");

            sb.Append(modelo.PathImagen + Enum.GetName(typeof(EFormatoImagen), modelo.FormatoImagen));

            return sb.ToString();
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