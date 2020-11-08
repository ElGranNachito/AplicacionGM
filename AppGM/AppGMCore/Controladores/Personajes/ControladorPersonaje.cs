using System;
using System.Collections.Generic;

namespace AppGM.Core
{
    #region Delegados
    
    public delegate void dSufrirDaño(
    ref int cantidad,
    ETipoDeDaño eTipo,
    IDañable objetivo,
    object instigador);

    public delegate void dModificarInventario(
        ControladorBase item,
        ControladorPersonaje objetivo);

    public delegate void dModificarAliados(
        ControladorBase[] aliados,
        ControladorPersonaje objetivo);

    public delegate void dRecibirEfecto(
        ControladorEfecto efectoRecibido,
        ControladorPersonaje objetivo,
        ControladorPersonaje[] instigadores);

    public delegate void dUtilizarHabilidad(
        ETipoHabilidad eTipo,
        string NombreHabilidad,
        ControladorPersonaje[] Objetivos);

    public delegate void dMoverse(
        ModeloVector2 posicionAntigua,
        ref ModeloVector2 posicionNueva);

    public delegate void dMorir(ControladorPersonaje objetivo);

    public delegate void dRealizarAccion(string descripcion);

    public delegate void dEntrarSalirCombate(
        object personaje,
        ControladorAdministradorDeCombate combate);

    public delegate void dCurarse(
        ref int valor, 
        ControladorPersonaje personaje,
        ControladorPersonaje fuenteP,
        ControladorUtilizable fuenteI);
    
    #endregion

    public interface IDañable
    {
        void SufrirDaño(int cantidad, ETipoDeDaño ETipo, ControladorPersonaje instigador);
    }

    public class ControladorPersonaje : Controlador<ModeloPersonaje>, IDañable
    {
        #region Controladores
   
        public List<ControladorEfecto> Efectos { get; set; }
        public List<ControladorUtilizable> Inventario { get; set; }
        public List<ControladorDefensivo> Armadura { get; set; }
        public List<ControladorPersonaje> Aliados { get; set; }
        public List<ControladorHabilidadG<ModeloPerk>> Perks { get; set; }
        public List<ControladorHabilidad> Skills { get; set; }
        public List<ControladorMagia> Magias { get; set; }
        public List<ControladorModificadorDeStat> ModificadoresDeDefensa { get; set; }
        public List<ControladorInvocacion> ControladorInvocaciones { get; set; }
        public List<ControladorHabilidadG<ModeloNoblePhantasm>> ControladorNoblePhantasms { get; set; }

        #endregion

        #region Eventos

        public event dSufrirDaño          OnSufrirDaño        = delegate { };
        public event dModificarInventario OnAñadirItem        = delegate { };
        public event dModificarInventario OnQuitarItem        = delegate { };
        public event dModificarAliados    OnAñadirAliado      = delegate { };
        public event dModificarAliados    OnQuitarAliado      = delegate { };
        public event dRecibirEfecto       OnRecibirEfecto     = delegate { };
        public event dUtilizarHabilidad   OnUtilizarHabilidad = delegate { };
        public event dMoverse             OnMoverse           = delegate { };
        public event dMorir               OnMorir             = delegate { };
        public event dRealizarAccion      OnRealizarAccion    = delegate { };
        public event dEntrarSalirCombate  OnEntrarEnCombate   = delegate { };
        public event dEntrarSalirCombate  OnSalirDeCombate    = delegate { };
        public event dCurarse             OnRecibirCuracion   = delegate { };

        #endregion

        #region Constructores
        public ControladorPersonaje(ModeloPersonaje _modeloPersonaje)
        {
            modelo = _modeloPersonaje;
        }

        #endregion

        #region Funciones

        protected void ModificarVida(int cantidad)
        {
            if ((modelo.Hp = SuperDll.SuperUtilidades.Math.Clamp(modelo.Hp + cantidad, int.MinValue, modelo.MaxHp)) <= 0)
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

        public virtual void AvanzarTurno()
        {
            for (int i = 0; i < Efectos.Count; ++i)
                Efectos[i].AplicarEfecto(this);

            //TODO: Aplicar los efectos
        }
        
        public virtual void EntrarEnCombate(ControladorAdministradorDeCombate combate)
        {
            modelo.EstaEnCombate = true;

            OnEntrarEnCombate(this, combate);
        }
        
        public virtual void SalirDeCombate(ControladorAdministradorDeCombate combate)
        {
            modelo.EstaEnCombate = false;

            OnSalirDeCombate(this, combate);
        }
        
        public void ModificarStat(string stat, object valor)
        {
        }

        public void AñadirItem(ControladorBase item)
        {
            OnAñadirItem(item, this);
        }

        public void QuitarItem(ControladorBase item)
        {
            OnQuitarItem(item, this);
        }
        
        public bool EstaVido => modelo.Hp > 0;

        public override string ToString()
        {
            return string.Format($"Nombre={modelo.Nombre} Id={modelo.Id}");
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
