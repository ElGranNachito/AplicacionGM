using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
    /// <summary>
    /// Clase estatica que agrupa todas las mAnimaciones de la aplicacion y se encarga de reproducirlas
    /// </summary>
    public static class ControladorDeAnimaciones
    {
        /// <summary>
        /// Lista con todas las mAnimaciones
        /// </summary>
        private static List<Animacion> mAnimaciones = new List<Animacion>();

        /// <summary>
        /// Thread encargada de actualizar las animaciones
        /// </summary>
        private static Thread mThreadAnimaciones = null;

        /// <summary>
        /// Lock para sincronizar el acceso a <see cref="mAnimaciones"/>
        /// </summary>
        private static object mLock = new object();

        /// <summary>
        /// Inicializa el controlador
        /// </summary>
        public static void Inicializar()
        {
            SistemaPrincipal.LoggerGlobal.Log("Inicializando controlador de animaciones", ESeveridad.Info);

            mThreadAnimaciones = new Thread(() =>
            {
                bool lockObtenido = false;

                while (true)
                {
                    try
                    {
                        Monitor.TryEnter(mLock, Int32.MaxValue, ref lockObtenido);

                        if (lockObtenido)
                        {
                            for (int i = 0; i < mAnimaciones.Count; ++i)
                                mAnimaciones[i].Tick();
                        }
                    }
                    finally
                    {
                        if (lockObtenido)
                        {
                            Monitor.Pulse(mLock);
                            Monitor.Exit(mLock); 
                            lockObtenido = false;
                        }
                    }
                }

            });

            //Le cambiamos el nombre al hilo para poder identificarlo
            mThreadAnimaciones.Name = "AppGM - Animaciones";

            //Lo hacemos un hilo de fondo para que no nos de problemas si cerramos la app mientras aun esta corriendo
            mThreadAnimaciones.IsBackground = true;

            //Iniciamos el hilo
            mThreadAnimaciones.Start();

            SistemaPrincipal.LoggerGlobal.Log("Controlador de animaciones inicializado", ESeveridad.Info);
        }

        /// <summary>
        /// Añade una animacion a la lista de animaciones
        /// </summary>
        /// <param name="animacion">Animacion a añadir</param>
        /// <returns></returns>
        public static async Task AñadirAnimacionAsincronicamente(Animacion animacion)
        {
            await Task.Run(() =>
            {
                bool lockObtenido = false;

                try
                {
                    Monitor.TryEnter(mLock, Int32.MaxValue, ref lockObtenido);

                    if (lockObtenido)
                        mAnimaciones.Add(animacion);
                }
                finally
                {
	                if (lockObtenido)
	                {
                        Monitor.Pulse(mLock);
		                Monitor.Exit(mLock);
	                }
                }
            });
        }

        /// <summary>
        /// Quita una animacion de la lista de animaciones
        /// </summary>
        /// <param name="animacion">Animacion a quitar</param>
        /// <returns></returns>
        public static async Task QuitarAnimacionAsincronicamente(Animacion animacion)
        {
            await Task.Run(() =>
            {
                bool lockObtenido = false;

                try
                {
                    Monitor.TryEnter(mLock, Int32.MaxValue, ref lockObtenido);

                    if (lockObtenido)
                        mAnimaciones.Remove(animacion);
                }
                finally
                {
	                if (lockObtenido)
	                {
                        Monitor.Pulse(mLock);
		                Monitor.Exit(mLock);
	                }
                }
            });
        }
    }
}