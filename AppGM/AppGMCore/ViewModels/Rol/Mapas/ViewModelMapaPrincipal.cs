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
                            X = 500,
                            Y = 500
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
                            X = 600,
                            Y = 525
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
                            X = 300,
                            Y = 300
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
                            X = 500,
                            Y = 170
                        }
                    }
                })),

                new ViewModelIngresoPosicion(this, new ControladorUnidadMapa(new ModeloUnidadMapaInvocacionTrampa
                {
                    EClaseServant = EClaseServant.Caster,
                    ETipoUnidad = ETipoUnidad.Invocacion,
                    Nombre = "Trampa caster",
                    Posicion = new TIUnidadMapaVector2
                    {
                        Posicion = new ModeloVector2
                        {
                            X = 100,
                            Y = 500
                        }
                    },
                    Cantidad = 6
                })),

                new ViewModelIngresoPosicion(this, new ControladorUnidadMapa(new ModeloUnidadMapaInvocacionTrampa
                {
                    EClaseServant = EClaseServant.Berserker,
                    ETipoUnidad = ETipoUnidad.Trampa,
                    Nombre = "Trampa berserker",
                    Posicion = new TIUnidadMapaVector2
                    {
                        Posicion = new ModeloVector2
                        {
                            X = 50,
                            Y = 75
                        }
                    },
                    Cantidad = 20
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
