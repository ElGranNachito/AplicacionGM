﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace AppGM.Core
{
    public class DatosRol
    {
        #region Miembros

        private RolContext mDBRol;

        #endregion

        #region Propiedades
        public List<ControladorPersonaje> Servants { get; set; }
        public List<ControladorPersonaje> Masters { get; set; }
        public List<ControladorInvocacion> Invocaciones { get; set; }
        public List<ControladorUtilizable> Items { get; set; }
        public List<ControladorPortable> Portables { get; set; }
        public List<ControladorPortable> PortableOfensivo { get; set; }
        public List<ControladorDefensivo> Defensivos { get; set; }
        public List<ControladorDefensivoAbsoluto> DefensivosAbsolutos { get; set; }
        public List<ControladorConsumible> Consumibles { get; set; }
        public List<ControladorArmaDistancia> ArmasDistancia { get; set; }
        public List<ControladorSlot> Slots { get; set; }
        public List<ControladorHabilidad> Perks { get; set; }
        public List<ControladorHabilidad> Skills { get; set; }
        public List<ControladorHabilidad> NoblePhantasms { get; set; }
        public List<ControladorMagia> Magias { get; set; }
        public List<ControladorEfecto> Efectos { get; set; }
        public List<ControladorCondicion> Condiciones { get; set; }
        public List<ControladorAdministradorDeCombate> CombatesActivos { get; set; }
        public List<ControladorLimitador> Limitadores { get; set; }
        public List<ControladorCargasHabilidad> CargasHabilidades { get; set; }

        //El primer mapa siempre es el principal
        public List<ControladorMapa> Mapas { get; set; } 
        #endregion

        /// <summary>
        /// Crea la clase y abre conexion con la base de datos
        /// </summary>
        /// <param name="_modeloRol"></param>
        public DatosRol(ModeloRol _modeloRol)
        {
            mDBRol = new RolContext(_modeloRol.Nombre);
        }
        public async Task CargarDatos()
        {
            await Task.Run(() =>
            {
                if (!mDBRol.Masters.Any())
                {
                    mDBRol.Add(new ModeloAdministradorDeCombate
                    {
                        IdAdministradorDeCombate = 0,
                        IndicePersonajeTurnoActual = 0,
                        Nombre = "SuperCombateFeroz",
                        TurnoActual = 0,
                        Participantes = new List<TIAdministradorDeCombateParticipante>
                        {
                            new TIAdministradorDeCombateParticipante
                            {
                                AdministradorDeCombate = null,
                                IdAdministradorDeCombate = 0,
                                IdParticipante = 0,
                                Participante = null
                            }
                        }
                    });
                }
                else
                {
                    mDBRol.Add(new ModeloMaster
                    {
                        Nombre = "Juanito"
                    });
                }

                //TODO: Cargar datos
            });
        }

        /// <summary>
        /// Funcion que cierra la conexion con la base de datos. Es necesario llamarla al terminar de utilizar la base de datos
        /// </summary>
        public void CerrarConexion()
        {
            mDBRol.Dispose();
        }
    }
}