using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// View model que representa el control de muestra de clima y horario.
    /// </summary>
    public class ViewModelClimaHorario : ViewModel
    {
        #region Miembros

        // Campos ---


        /// <summary>
        /// Controlador del climaHorario
        /// </summary>
        private ControladorClimaHorario climaHorario;

        /// <summary>
        /// Timer para actualizar la hora a cada minuto que pase.
        /// </summary>
        private Timer timerHora;


        // Propiedades ---

        
        /// <summary>
        /// Comando que se ejecuta al presionar el boton de actualizar clima.
        /// </summary>
        public ICommand ComandoBotonActualizarClima { get; set; }

        /// <summary>
        /// Comando que se ejecuta al presionar el boton de avanzar dia.
        /// </summary>
        public ICommand ComandoBotonAvanzarDia { get; set; }

        /// <summary>
        /// Comando que se ejecuta al presionar el boton de retroceder dia.
        /// </summary>
        public ICommand ComandoBotonRetrocederDia { get; set; }

        /// <summary>
        /// Tipo de clima.
        /// </summary>
        public EClima Clima => climaHorario.modelo.Clima;

        /// <summary>
        /// Clase de viento.
        /// </summary>
        public EViento Viento => climaHorario.modelo.Viento;

        /// <summary>
        /// Estimacion general de la humedad.
        /// </summary>
        public EHumedad Humedad => climaHorario.modelo.Humedad;

        /// <summary>
        /// Estimacion general de la temperatura.
        /// </summary>
        public ETemperatura Temperatura => climaHorario.modelo.Temperatura;

        /// <summary>
        /// Dia de la semana en el rol seleccionado.
        /// </summary>
        public EDiaSemana DiaSemana => climaHorario.modelo.DiaSemana;

        /// <summary>
        /// String con el nombre del dia de la semana.
        /// </summary>
        public string DiaDeLaSemana => EnumHelpers.ToStringDiaSemana(DiaSemana);

        /// <summary>
        /// Hora actual.
        /// </summary>
        public string Hora => DateTimeOffset.Now.ToString("HH:mm");
        
        /// <summary>
        /// Ruta de la imagen del cima correspondiente.
        /// </summary>
        public string PathImagenClima => $"../../../../Media/Imagenes/Clima/{EnumHelpers.ToStringClima(Clima)}.png";

        /// <summary>
        /// Ruta de la imagen del tipo de viento correspondiente.
        /// </summary>
        public string PathImagenViento => $"../../../../Media/Imagenes/Clima/Viento/{EnumHelpers.ToStringViento(Viento)}.png";

        /// <summary>
        /// Ruta de la imagen del valor de humedad correspondiente.
        /// </summary>
        public string PathImagenHumedad => $"../../../../Media/Imagenes/Clima/Humedad/{EnumHelpers.ToStringHumedad(Humedad)}.png";

        /// <summary>
        /// Ruta de la imagen del valor de temperatura correspondiente.
        /// </summary>
        public string PathImagenTemperatura => $"../../../../Media/Imagenes/Clima/Temperatura/{EnumHelpers.ToStringTemperatura(Temperatura)}.png";


        #endregion

        #region Constructor

        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="_climaHorario">Viewmodel del mapa</param>
        public ViewModelClimaHorario(ControladorClimaHorario _climaHorario)
        {
            climaHorario   = _climaHorario;

            ComandoBotonActualizarClima = new Comando(ActualizarCondicionClimatica);
            ComandoBotonAvanzarDia      = new Comando(AvanzarDia);
            ComandoBotonRetrocederDia   = new Comando(RetrocederDia);

            timerHora = new Timer(TimeSpan.TicksPerMinute);
            timerHora.Elapsed += (sender, args) => { DispararPropertyChanged(new PropertyChangedEventArgs(nameof(Hora))); };
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Dispara el evento property changed para las propiedades <see cref="Clima"/>, <see cref="Viento"/>, <see cref="Humedad"/> y <see cref="Temperatura"/>
        /// </summary>
        public void DispararPropertyChangedClima()
        {
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(Clima)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(Viento)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(Humedad)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(Temperatura)));

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PathImagenClima)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PathImagenViento)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PathImagenHumedad)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PathImagenTemperatura)));
        }

        /// <summary>
        /// Actualiza las condiciones climaticas de forma aleatoria.
        /// </summary>
        public void ActualizarCondicionClimatica()
        {
            climaHorario.ActualizarClima();

            DispararPropertyChangedClima();
        }

        /// <summary>
        /// Avanza un dia de la semana.
        /// </summary>
        public void AvanzarDia()
        {
            climaHorario.AvanzarDiaSemana();

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DiaSemana)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DiaDeLaSemana)));
        }

        /// <summary>
        /// Retrocede un dia de la semana.
        /// </summary>
        public void RetrocederDia()
        {
            climaHorario.RetrocederDiaSemana();

            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DiaSemana)));
            DispararPropertyChanged(new PropertyChangedEventArgs(nameof(DiaDeLaSemana)));
        }

        #endregion
    }
}
