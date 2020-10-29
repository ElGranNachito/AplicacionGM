using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
    public class ViewModelMapaPrincipal : ViewModelMapa
    {
        public ViewModelIngresoPosicion PosicionIglesia { get; set; }

        public ViewModelMapaPrincipal(ControladorMapa _controlador) : base(_controlador)
        {
            
        }
        public ViewModelMapaPrincipal()
        {
            PathImagen = "../../../Media/Imagenes/Mapas/Seoul.png";

            Posiciones = new List<ViewModelIngresoPosicion>
            {
                new ViewModelIngresoPosicion(this, new ControladorUnidadMapa(new ModeloUnidadMapaMasterServant
                {
                    EClaseServant = EClaseServant.Saber,
                    ETipoUnidad = ETipoUnidad.Servant,
                    Nombre = "Saber",
                    Posicion = new TIUnidadMapaVector2
                    {
                        Posicion = new ModeloVector2
                        {
                            X = 200,
                            Y = 150
                        }
                    }
                })),

                new ViewModelIngresoPosicion(this, new ControladorUnidadMapa(new ModeloUnidadMapaMasterServant
                {
                    EClaseServant = EClaseServant.Assassin,
                    ETipoUnidad = ETipoUnidad.Servant,
                    Nombre = "Assassin",
                    Posicion = new TIUnidadMapaVector2
                    {
                        Posicion = new ModeloVector2
                        {
                            X = 200,
                            Y = 150
                        }
                    }
                })),

                new ViewModelIngresoPosicion(this, new ControladorUnidadMapa(new ModeloUnidadMapaMasterServant
                {
                    EClaseServant = EClaseServant.Lancer,
                    ETipoUnidad = ETipoUnidad.Servant,
                    Nombre = "Lancer",
                    Posicion = new TIUnidadMapaVector2
                    {
                        Posicion = new ModeloVector2
                        {
                            X = 200,
                            Y = 150
                        }
                    }
                })),

                new ViewModelIngresoPosicion(this, new ControladorUnidadMapa(new ModeloUnidadMapaMasterServant
                {
                    EClaseServant = EClaseServant.Lancer,
                    ETipoUnidad = ETipoUnidad.Master,
                    Nombre = "Master Lancer",
                    Posicion = new TIUnidadMapaVector2
                    {
                        Posicion = new ModeloVector2
                        {
                            X = 200,
                            Y = 150
                        }
                    }
                }))
            };
            return;

            if (mControladorMapa.modelo.PosicionesElementos.Count != 0)
            {
                PosicionIglesia = new ViewModelIngresoPosicion(
                    this,
                    new Vector2(mControladorMapa.modelo.PosicionesElementos.First().Posicion.X, mControladorMapa.modelo.PosicionesElementos.First().Posicion.Y));
            }
        }
    }
}
