using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppGM.Core
{
    public class ViewModelMapaPrincipal : ViewModelMapa
    {
        public ViewModelIngresoPosicion PosicionIglesia { get; set; }

        public ViewModelMapaPrincipal(ControladorMapa _controlador) : base(_controlador){}
        public ViewModelMapaPrincipal()
        {
            PathImagen = "../../../Media/Imagenes/Mapas/Seoul.png";

            Posiciones = new ObservableCollection<ViewModelIngresoPosicion>
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
                    EClaseServant = EClaseServant.Saber,
                    ETipoUnidad = ETipoUnidad.Master,
                    Nombre = "Master Saber",
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
                    EClaseServant = EClaseServant.Berserker,
                    ETipoUnidad = ETipoUnidad.Servant,
                    Nombre = "Berserker",
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
                    EClaseServant = EClaseServant.Berserker,
                    ETipoUnidad = ETipoUnidad.Master,
                    Nombre = "Master Berserker",
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
                    EClaseServant = EClaseServant.Archer,
                    ETipoUnidad = ETipoUnidad.Master,
                    Nombre = "Master Archer",
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
                    EClaseServant = EClaseServant.Archer,
                    ETipoUnidad = ETipoUnidad.Servant,
                    Nombre = "Archer",
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
                    EClaseServant = EClaseServant.Rider,
                    ETipoUnidad = ETipoUnidad.Servant,
                    Nombre = "Rider",
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
                    EClaseServant = EClaseServant.Rider,
                    ETipoUnidad = ETipoUnidad.Master,
                    Nombre = "Master Rider",
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
                    EClaseServant = EClaseServant.Caster,
                    ETipoUnidad = ETipoUnidad.Master,
                    Nombre = "Master Caster",
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
                    EClaseServant = EClaseServant.Caster,
                    ETipoUnidad = ETipoUnidad.Servant,
                    Nombre = "Caster",
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

            if (controladorMapa.modelo.PosicionesElementos.Count != 0)
            {
                PosicionIglesia = new ViewModelIngresoPosicion(
                    this,
                    new Vector2(controladorMapa.modelo.PosicionesElementos.First().Posicion.X, controladorMapa.modelo.PosicionesElementos.First().Posicion.Y));
            }
        }
    }
}
