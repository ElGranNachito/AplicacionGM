using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppGM.Core
{
    public static class ControladorDeAnimaciones
    {
        private static List<Animacion> animaciones = new List<Animacion>();
        private static Thread threadAnimaciones = null;
        private static object mLock = new object();

        public static void Inicializar()
        {
            threadAnimaciones = new Thread(() =>
            {
                bool lockObtenido = false;

                while (true)
                {
                    try
                    {
                        Monitor.TryEnter(mLock, Int32.MaxValue, ref lockObtenido);

                        if (lockObtenido)
                        {
                            for (int i = 0; i < animaciones.Count; ++i)
                                animaciones[i].Tick();
                        }
                    }
                    finally
                    {
                        if (lockObtenido)
                        {
                            Monitor.Exit(mLock); 
                            lockObtenido = false;
                        }
                    }
                }

            });

            threadAnimaciones.Name = "AppGM - Animaciones";
            threadAnimaciones.IsBackground = true;
            threadAnimaciones.Start();
        }

        public static async Task AñadirAnimacion(Animacion animacion)
        {
            await Task.Run(() =>
            {
                bool lockObtenido = false;

                try
                {
                    Monitor.TryEnter(mLock, Int32.MaxValue, ref lockObtenido);

                    if (lockObtenido)
                        animaciones.Add(animacion);
                }
                finally
                {
                    if(lockObtenido)
                        Monitor.Exit(mLock);
                }
            });
            animaciones.Add(animacion);
        }

        public static async Task QuitarAnimacion(Animacion animacion)
        {
            await Task.Run(() =>
            {
                bool lockObtenido = false;

                try
                {
                    Monitor.TryEnter(mLock, Int32.MaxValue, ref lockObtenido);

                    if (lockObtenido)
                        animaciones.Remove(animacion);
                }
                finally
                {
                    if (lockObtenido)
                        Monitor.Exit(mLock);
                }
            });
            animaciones.Add(animacion);
        }
    }
}