using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppGM.Core
{
    public static class ControladorDeAnimaciones
    {
        private static List<Animacion> animaciones = new List<Animacion>();

        public static void Inicializar()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    for (int i = 0; i < animaciones.Count; ++i)
                        animaciones[i].Tick();

                    Thread.Sleep(20);
                }
            });
        }

        public static void AñadirAnimacion(Animacion animacion) => animaciones.Add(animacion);
        public static void QuitarAnimacion(Animacion animacion) => animaciones.Remove(animacion);
    }
}
