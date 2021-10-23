using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppGM.Core.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModeloAlianza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EIconoAlianza = table.Column<int>(type: "INTEGER", nullable: false),
                    FormatoImagen = table.Column<int>(type: "INTEGER", nullable: false),
                    PathImagenIcono = table.Column<string>(type: "varchar(260)", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    EsVigente = table.Column<bool>(type: "INTEGER", nullable: false),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloAlianza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloFuncion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreFuncion = table.Column<string>(type: "varchar(50)", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloFuncion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Temporada = table.Column<int>(type: "INTEGER", nullable: false),
                    CondicionClimatica = table.Column<int>(type: "INTEGER", nullable: false),
                    Hora = table.Column<int>(type: "INTEGER", nullable: false),
                    Dia = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 3500, nullable: true),
                    Registros = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    FechaUltimaSesion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloRol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vectores2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    X = table.Column<double>(type: "REAL", nullable: false),
                    Y = table.Column<double>(type: "REAL", nullable: false),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vectores2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    EsVigente = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdAlianza = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloContrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloContrato_ModeloAlianza_IdAlianza",
                        column: x => x.IdAlianza,
                        principalTable: "ModeloAlianza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIFuncionPadreFuncion",
                columns: table => new
                {
                    IDFuncion = table.Column<int>(type: "INTEGER", nullable: false),
                    IDPadre = table.Column<int>(type: "INTEGER", nullable: false),
                    PadreId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIFuncionPadreFuncion", x => new { x.IDPadre, x.IDFuncion });
                    table.ForeignKey(
                        name: "FK_TIFuncionPadreFuncion_ModeloFuncion_IDFuncion",
                        column: x => x.IDFuncion,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIFuncionPadreFuncion_ModeloFuncion_PadreId",
                        column: x => x.PadreId,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Combates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RolId = table.Column<int>(type: "INTEGER", nullable: true),
                    IndicePersonajeTurnoActual = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnoActual = table.Column<uint>(type: "INTEGER", nullable: false),
                    EstaActivo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combates_ModeloRol_RolId",
                        column: x => x.RolId,
                        principalTable: "ModeloRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloPersonaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RolId = table.Column<int>(type: "INTEGER", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    TipoPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroParty = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxHp = table.Column<int>(type: "INTEGER", nullable: false),
                    Hp = table.Column<int>(type: "INTEGER", nullable: false),
                    Str = table.Column<int>(type: "INTEGER", nullable: false),
                    End = table.Column<int>(type: "INTEGER", nullable: false),
                    Agi = table.Column<int>(type: "INTEGER", nullable: false),
                    Int = table.Column<int>(type: "INTEGER", nullable: false),
                    Lck = table.Column<int>(type: "INTEGER", nullable: false),
                    VentajaStr = table.Column<int>(type: "INTEGER", nullable: false),
                    VentajaEnd = table.Column<int>(type: "INTEGER", nullable: false),
                    VentajaAgi = table.Column<int>(type: "INTEGER", nullable: false),
                    VentajaInt = table.Column<int>(type: "INTEGER", nullable: false),
                    VentajaLck = table.Column<int>(type: "INTEGER", nullable: false),
                    PesoMaximoCargable = table.Column<decimal>(type: "TEXT", nullable: false),
                    PesoCargado = table.Column<decimal>(type: "TEXT", nullable: false),
                    PathImagenRelativo = table.Column<string>(type: "varchar(260)", nullable: true),
                    PathImagenAbsoluto = table.Column<string>(type: "varchar(260)", nullable: true),
                    EstaEnCombate = table.Column<bool>(type: "INTEGER", nullable: false),
                    FormatoImagen = table.Column<int>(type: "INTEGER", nullable: false),
                    PosicionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Clase = table.Column<int>(type: "INTEGER", nullable: false),
                    EsAutomata = table.Column<bool>(type: "INTEGER", nullable: true),
                    InvocadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    TurnosDeDuracion = table.Column<byte>(type: "INTEGER", nullable: true),
                    EClaseServant = table.Column<int>(type: "INTEGER", nullable: true),
                    RangoHechiceria = table.Column<ushort>(type: "INTEGER", nullable: true),
                    NombreReal = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EClaseDeSuServant = table.Column<int>(type: "INTEGER", nullable: true),
                    EBienestar = table.Column<int>(type: "INTEGER", nullable: true),
                    Od = table.Column<int>(type: "INTEGER", nullable: true),
                    OdActual = table.Column<int>(type: "INTEGER", nullable: true),
                    Mana = table.Column<int>(type: "INTEGER", nullable: true),
                    ManaActual = table.Column<int>(type: "INTEGER", nullable: true),
                    Chr = table.Column<int>(type: "INTEGER", nullable: true),
                    VentajaChr = table.Column<ushort>(type: "INTEGER", nullable: true),
                    CommandSpells = table.Column<ushort>(type: "INTEGER", nullable: true),
                    Lore = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: true),
                    Origen = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Afinidad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ERangoNP = table.Column<int>(type: "INTEGER", nullable: true),
                    Prana = table.Column<int>(type: "INTEGER", nullable: true),
                    PranaActual = table.Column<int>(type: "INTEGER", nullable: true),
                    Fuente = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloPersonaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloPersonaje_ModeloPersonaje_InvocadorId",
                        column: x => x.InvocadorId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ModeloPersonaje_ModeloRol_RolId",
                        column: x => x.RolId,
                        principalTable: "ModeloRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloPersonaje_Vectores2_PosicionId",
                        column: x => x.PosicionId,
                        principalTable: "Vectores2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModeloMapa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreMapa = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EFormatoImagen = table.Column<int>(type: "INTEGER", nullable: false),
                    RolId = table.Column<int>(type: "INTEGER", nullable: true),
                    CombateAlQuePerteneceId = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloMapa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloMapa_Combates_CombateAlQuePerteneceId",
                        column: x => x.CombateAlQuePerteneceId,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloMapa_ModeloRol_RolId",
                        column: x => x.RolId,
                        principalTable: "ModeloRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloAlianzaModeloPersonaje",
                columns: table => new
                {
                    AlianzasId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonajesAfectadosId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloAlianzaModeloPersonaje", x => new { x.AlianzasId, x.PersonajesAfectadosId });
                    table.ForeignKey(
                        name: "FK_ModeloAlianzaModeloPersonaje_ModeloAlianza_AlianzasId",
                        column: x => x.AlianzasId,
                        principalTable: "ModeloAlianza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloAlianzaModeloPersonaje_ModeloPersonaje_PersonajesAfectadosId",
                        column: x => x.PersonajesAfectadosId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloCaracteristicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Edad = table.Column<int>(type: "INTEGER", nullable: false),
                    Estatura = table.Column<int>(type: "INTEGER", nullable: false),
                    Peso = table.Column<int>(type: "INTEGER", nullable: false),
                    ESexo = table.Column<int>(type: "INTEGER", nullable: false),
                    EArquetipo = table.Column<int>(type: "INTEGER", nullable: false),
                    EManoDominante = table.Column<int>(type: "INTEGER", nullable: false),
                    Fisico = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Nacionalidad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloPersonajeJugableId = table.Column<int>(type: "INTEGER", nullable: false),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloCaracteristicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloCaracteristicas_ModeloPersonaje_Id",
                        column: x => x.Id,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ModeloCaracteristicas_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloContratoModeloPersonaje",
                columns: table => new
                {
                    ContratosId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonajesAfectadosId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloContratoModeloPersonaje", x => new { x.ContratosId, x.PersonajesAfectadosId });
                    table.ForeignKey(
                        name: "FK_ModeloContratoModeloPersonaje_ModeloContrato_ContratosId",
                        column: x => x.ContratosId,
                        principalTable: "ModeloContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloContratoModeloPersonaje_ModeloPersonaje_PersonajesAfectadosId",
                        column: x => x.PersonajesAfectadosId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloDatosInvocacionBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdInvocacion = table.Column<int>(type: "INTEGER", nullable: false),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloDatosInvocacionBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloDatosInvocacionBase_ModeloPersonaje_IdInvocacion",
                        column: x => x.IdInvocacion,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloEspecialidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    PersonajeContenedorId = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEspecialidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloEspecialidad_ModeloPersonaje_PersonajeContenedorId",
                        column: x => x.PersonajeContenedorId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloHabilidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostoDeOdOPrana = table.Column<int>(type: "INTEGER", nullable: false),
                    CostoDeMana = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnosDeDuracion = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    TipoDeHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Rango = table.Column<int>(type: "INTEGER", nullable: false),
                    IgnoraAmbiente = table.Column<bool>(type: "INTEGER", nullable: false),
                    DueñoId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloPersonajeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Nivel = table.Column<byte>(type: "INTEGER", nullable: true),
                    EsParticular = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModeloPersonajeId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloServantId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloPerk_ModeloPersonajeId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloHabilidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloHabilidad_ModeloPersonaje_DueñoId",
                        column: x => x.DueñoId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloHabilidad_ModeloPersonaje_ModeloPerk_ModeloPersonajeId1",
                        column: x => x.ModeloPerk_ModeloPersonajeId1,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloHabilidad_ModeloPersonaje_ModeloPersonajeId",
                        column: x => x.ModeloPersonajeId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloHabilidad_ModeloPersonaje_ModeloPersonajeId1",
                        column: x => x.ModeloPersonajeId1,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloHabilidad_ModeloPersonaje_ModeloServantId",
                        column: x => x.ModeloServantId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModeloParticipante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TiradaIniciativa = table.Column<int>(type: "INTEGER", nullable: false),
                    EsSuTurno = table.Column<bool>(type: "INTEGER", nullable: false),
                    PersonajeId = table.Column<int>(type: "INTEGER", nullable: true),
                    CombateActualId = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloParticipante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloParticipante_Combates_CombateActualId",
                        column: x => x.CombateActualId,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ModeloParticipante_ModeloPersonaje_PersonajeId",
                        column: x => x.PersonajeId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloAmbiente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CaracteristicasAmbiente = table.Column<int>(type: "INTEGER", nullable: false),
                    CantidadCasillas = table.Column<int>(type: "INTEGER", nullable: false),
                    TemperaturaActual = table.Column<float>(type: "REAL", nullable: false),
                    HumedadActual = table.Column<float>(type: "REAL", nullable: false),
                    IdMapa = table.Column<int>(type: "INTEGER", nullable: false),
                    IdRol = table.Column<int>(type: "INTEGER", nullable: true),
                    IdCombateAlQuePertenece = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloAmbiente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloAmbiente_Combates_IdCombateAlQuePertenece",
                        column: x => x.IdCombateAlQuePertenece,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloAmbiente_ModeloMapa_IdMapa",
                        column: x => x.IdMapa,
                        principalTable: "ModeloMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloAmbiente_ModeloRol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "ModeloRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesMapa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: true),
                    ETipoUnidad = table.Column<int>(type: "INTEGER", nullable: false),
                    PosicionId = table.Column<int>(type: "INTEGER", nullable: true),
                    PersonajeId = table.Column<int>(type: "INTEGER", nullable: true),
                    MapaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    EClaseServant = table.Column<int>(type: "INTEGER", nullable: true),
                    Inicial = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: true),
                    EsDeMaster = table.Column<bool>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMapa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadesMapa_ModeloMapa_MapaId",
                        column: x => x.MapaId,
                        principalTable: "ModeloMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnidadesMapa_ModeloPersonaje_PersonajeId",
                        column: x => x.PersonajeId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnidadesMapa_Vectores2_PosicionId",
                        column: x => x.PosicionId,
                        principalTable: "Vectores2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModeloEfecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TurnosDeDuracion = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    ComportamientoAcumulativo = table.Column<int>(type: "INTEGER", nullable: false),
                    HabilidadDueñaId = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEfecto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloEfecto_ModeloHabilidad_HabilidadDueñaId",
                        column: x => x.HabilidadDueñaId,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIFuncionHabilidad",
                columns: table => new
                {
                    IDFuncion = table.Column<int>(type: "INTEGER", nullable: false),
                    IDHabilidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIFuncionHabilidad", x => new { x.IDFuncion, x.IDHabilidad });
                    table.ForeignKey(
                        name: "FK_TIFuncionHabilidad_ModeloFuncion_IDFuncion",
                        column: x => x.IDFuncion,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIFuncionHabilidad_ModeloHabilidad_IDHabilidad",
                        column: x => x.IDHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    ParticipanteId = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acciones_ModeloParticipante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "ModeloParticipante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloEfectoSiendoAplicado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TurnosRestantes = table.Column<int>(type: "INTEGER", nullable: false),
                    EstaSiendoAplicado = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContadorAcumulaciones = table.Column<int>(type: "INTEGER", nullable: false),
                    ComportamientoAcumulativo = table.Column<int>(type: "INTEGER", nullable: false),
                    EfectoId = table.Column<int>(type: "INTEGER", nullable: true),
                    InstigadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    ObjetivoId = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEfectoSiendoAplicado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloEfectoSiendoAplicado_ModeloEfecto_EfectoId",
                        column: x => x.EfectoId,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloEfectoSiendoAplicado_ModeloPersonaje_InstigadorId",
                        column: x => x.InstigadorId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloEfectoSiendoAplicado_ModeloPersonaje_ObjetivoId",
                        column: x => x.ObjetivoId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIFuncionEfecto",
                columns: table => new
                {
                    IDFuncion = table.Column<int>(type: "INTEGER", nullable: false),
                    IDEfecto = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoFuncion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIFuncionEfecto", x => new { x.IDFuncion, x.IDEfecto });
                    table.ForeignKey(
                        name: "FK_TIFuncionEfecto_ModeloEfecto_IDEfecto",
                        column: x => x.IDEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIFuncionEfecto_ModeloFuncion_IDFuncion",
                        column: x => x.IDFuncion,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIEfectoSiendoAplicadoFuncion",
                columns: table => new
                {
                    IdEfectoSiendoAplicado = table.Column<int>(type: "INTEGER", nullable: false),
                    IdFuncion = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoFuncion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIEfectoSiendoAplicadoFuncion", x => new { x.IdFuncion, x.IdEfectoSiendoAplicado });
                    table.ForeignKey(
                        name: "FK_TIEfectoSiendoAplicadoFuncion_ModeloEfectoSiendoAplicado_IdEfectoSiendoAplicado",
                        column: x => x.IdEfectoSiendoAplicado,
                        principalTable: "ModeloEfectoSiendoAplicado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIEfectoSiendoAplicadoFuncion_ModeloFuncion_IdFuncion",
                        column: x => x.IdFuncion,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloVariable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreVariable = table.Column<string>(type: "varchar(50)", nullable: true),
                    DescripcionVariable = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    TipoVariableString = table.Column<string>(type: "varchar(50)", nullable: true),
                    IDVariable = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonajeContenedorId = table.Column<int>(type: "INTEGER", nullable: true),
                    HabilidadContenedoraId = table.Column<int>(type: "INTEGER", nullable: true),
                    UtilizableContenedorId = table.Column<int>(type: "INTEGER", nullable: true),
                    FuncionContenedoraId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorVariable = table.Column<float>(type: "REAL", nullable: true),
                    ModeloVariableInt_ValorVariable = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloVariableString_ValorVariable = table.Column<string>(type: "TEXT", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloVariable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloVariable_ModeloFuncion_FuncionContenedoraId",
                        column: x => x.FuncionContenedoraId,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloVariable_ModeloHabilidad_HabilidadContenedoraId",
                        column: x => x.HabilidadContenedoraId,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloVariable_ModeloPersonaje_PersonajeContenedorId",
                        column: x => x.PersonajeContenedorId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tirada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    MultiplicadorDeEspecialidad = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoTirada = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonajeContenedorId = table.Column<int>(type: "INTEGER", nullable: true),
                    HabilidadContenedoraId = table.Column<int>(type: "INTEGER", nullable: true),
                    UtilizableContenedorId = table.Column<int>(type: "INTEGER", nullable: true),
                    FuncionContenedoraId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloHabilidadId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    StatDeLaQueDepende = table.Column<int>(type: "INTEGER", nullable: true),
                    Tirada = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    DescripcionVariableExtra = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    TipoDeDaño = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tirada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tirada_ModeloFuncion_FuncionContenedoraId",
                        column: x => x.FuncionContenedoraId,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tirada_ModeloHabilidad_HabilidadContenedoraId",
                        column: x => x.HabilidadContenedoraId,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tirada_ModeloHabilidad_ModeloHabilidadId",
                        column: x => x.ModeloHabilidadId,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tirada_ModeloPersonaje_PersonajeContenedorId",
                        column: x => x.PersonajeContenedorId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloUtilizable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Peso = table.Column<decimal>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    PersonajePortadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    SlotsQueOcupa = table.Column<decimal>(type: "TEXT", nullable: true),
                    ModeloSlotId = table.Column<int>(type: "INTEGER", nullable: true),
                    Usos = table.Column<ushort>(type: "INTEGER", nullable: true),
                    UsosRestantes = table.Column<ushort>(type: "INTEGER", nullable: true),
                    ModeloArmasDistancia_ModeloPersonajeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloPersonajeId = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloUtilizable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloUtilizable_ModeloPersonaje_ModeloArmasDistancia_ModeloPersonajeId",
                        column: x => x.ModeloArmasDistancia_ModeloPersonajeId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloUtilizable_ModeloPersonaje_ModeloPersonajeId",
                        column: x => x.ModeloPersonajeId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloUtilizable_ModeloPersonaje_PersonajePortadorId",
                        column: x => x.PersonajePortadorId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EspacioTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    EspacioDisponible = table.Column<decimal>(type: "TEXT", nullable: false),
                    DueñoId = table.Column<int>(type: "INTEGER", nullable: true),
                    EsValido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloSlot_ModeloUtilizable_DueñoId",
                        column: x => x.DueñoId,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_ParticipanteId",
                table: "Acciones",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Combates_RolId",
                table: "Combates",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloAlianzaModeloPersonaje_PersonajesAfectadosId",
                table: "ModeloAlianzaModeloPersonaje",
                column: "PersonajesAfectadosId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloAmbiente_IdCombateAlQuePertenece",
                table: "ModeloAmbiente",
                column: "IdCombateAlQuePertenece",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloAmbiente_IdMapa",
                table: "ModeloAmbiente",
                column: "IdMapa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloAmbiente_IdRol",
                table: "ModeloAmbiente",
                column: "IdRol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloCaracteristicas_IdPersonaje",
                table: "ModeloCaracteristicas",
                column: "IdPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloContrato_IdAlianza",
                table: "ModeloContrato",
                column: "IdAlianza",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloContratoModeloPersonaje_PersonajesAfectadosId",
                table: "ModeloContratoModeloPersonaje",
                column: "PersonajesAfectadosId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloDatosInvocacionBase_IdInvocacion",
                table: "ModeloDatosInvocacionBase",
                column: "IdInvocacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEfecto_HabilidadDueñaId",
                table: "ModeloEfecto",
                column: "HabilidadDueñaId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEfectoSiendoAplicado_EfectoId",
                table: "ModeloEfectoSiendoAplicado",
                column: "EfectoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEfectoSiendoAplicado_InstigadorId",
                table: "ModeloEfectoSiendoAplicado",
                column: "InstigadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEfectoSiendoAplicado_ObjetivoId",
                table: "ModeloEfectoSiendoAplicado",
                column: "ObjetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEspecialidad_PersonajeContenedorId",
                table: "ModeloEspecialidad",
                column: "PersonajeContenedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloHabilidad_DueñoId",
                table: "ModeloHabilidad",
                column: "DueñoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloHabilidad_ModeloPerk_ModeloPersonajeId1",
                table: "ModeloHabilidad",
                column: "ModeloPerk_ModeloPersonajeId1");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloHabilidad_ModeloPersonajeId",
                table: "ModeloHabilidad",
                column: "ModeloPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloHabilidad_ModeloPersonajeId1",
                table: "ModeloHabilidad",
                column: "ModeloPersonajeId1");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloHabilidad_ModeloServantId",
                table: "ModeloHabilidad",
                column: "ModeloServantId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloMapa_CombateAlQuePerteneceId",
                table: "ModeloMapa",
                column: "CombateAlQuePerteneceId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloMapa_RolId",
                table: "ModeloMapa",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloParticipante_CombateActualId",
                table: "ModeloParticipante",
                column: "CombateActualId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloParticipante_PersonajeId",
                table: "ModeloParticipante",
                column: "PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloPersonaje_InvocadorId",
                table: "ModeloPersonaje",
                column: "InvocadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloPersonaje_PosicionId",
                table: "ModeloPersonaje",
                column: "PosicionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloPersonaje_RolId",
                table: "ModeloPersonaje",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloSlot_DueñoId",
                table: "ModeloSlot",
                column: "DueñoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloUtilizable_ModeloArmasDistancia_ModeloPersonajeId",
                table: "ModeloUtilizable",
                column: "ModeloArmasDistancia_ModeloPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloUtilizable_ModeloPersonajeId",
                table: "ModeloUtilizable",
                column: "ModeloPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloUtilizable_ModeloSlotId",
                table: "ModeloUtilizable",
                column: "ModeloSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloUtilizable_PersonajePortadorId",
                table: "ModeloUtilizable",
                column: "PersonajePortadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloVariable_FuncionContenedoraId",
                table: "ModeloVariable",
                column: "FuncionContenedoraId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloVariable_HabilidadContenedoraId",
                table: "ModeloVariable",
                column: "HabilidadContenedoraId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloVariable_PersonajeContenedorId",
                table: "ModeloVariable",
                column: "PersonajeContenedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloVariable_UtilizableContenedorId",
                table: "ModeloVariable",
                column: "UtilizableContenedorId");

            migrationBuilder.CreateIndex(
                name: "IX_TIEfectoSiendoAplicadoFuncion_IdEfectoSiendoAplicado",
                table: "TIEfectoSiendoAplicadoFuncion",
                column: "IdEfectoSiendoAplicado");

            migrationBuilder.CreateIndex(
                name: "IX_TIFuncionEfecto_IDEfecto",
                table: "TIFuncionEfecto",
                column: "IDEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIFuncionEfecto_IDFuncion",
                table: "TIFuncionEfecto",
                column: "IDFuncion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIFuncionHabilidad_IDFuncion",
                table: "TIFuncionHabilidad",
                column: "IDFuncion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIFuncionHabilidad_IDHabilidad",
                table: "TIFuncionHabilidad",
                column: "IDHabilidad");

            migrationBuilder.CreateIndex(
                name: "IX_TIFuncionPadreFuncion_IDFuncion",
                table: "TIFuncionPadreFuncion",
                column: "IDFuncion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIFuncionPadreFuncion_PadreId",
                table: "TIFuncionPadreFuncion",
                column: "PadreId");

            migrationBuilder.CreateIndex(
                name: "IX_Tirada_FuncionContenedoraId",
                table: "Tirada",
                column: "FuncionContenedoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Tirada_HabilidadContenedoraId",
                table: "Tirada",
                column: "HabilidadContenedoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Tirada_ModeloHabilidadId",
                table: "Tirada",
                column: "ModeloHabilidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Tirada_PersonajeContenedorId",
                table: "Tirada",
                column: "PersonajeContenedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tirada_UtilizableContenedorId",
                table: "Tirada",
                column: "UtilizableContenedorId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesMapa_MapaId",
                table: "UnidadesMapa",
                column: "MapaId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesMapa_PersonajeId",
                table: "UnidadesMapa",
                column: "PersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesMapa_PosicionId",
                table: "UnidadesMapa",
                column: "PosicionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloVariable_ModeloUtilizable_UtilizableContenedorId",
                table: "ModeloVariable",
                column: "UtilizableContenedorId",
                principalTable: "ModeloUtilizable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tirada_ModeloUtilizable_UtilizableContenedorId",
                table: "Tirada",
                column: "UtilizableContenedorId",
                principalTable: "ModeloUtilizable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloUtilizable_ModeloSlot_ModeloSlotId",
                table: "ModeloUtilizable",
                column: "ModeloSlotId",
                principalTable: "ModeloSlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModeloPersonaje_ModeloRol_RolId",
                table: "ModeloPersonaje");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloUtilizable_ModeloPersonaje_ModeloArmasDistancia_ModeloPersonajeId",
                table: "ModeloUtilizable");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloUtilizable_ModeloPersonaje_ModeloPersonajeId",
                table: "ModeloUtilizable");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloUtilizable_ModeloPersonaje_PersonajePortadorId",
                table: "ModeloUtilizable");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloSlot_ModeloUtilizable_DueñoId",
                table: "ModeloSlot");

            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropTable(
                name: "ModeloAlianzaModeloPersonaje");

            migrationBuilder.DropTable(
                name: "ModeloAmbiente");

            migrationBuilder.DropTable(
                name: "ModeloCaracteristicas");

            migrationBuilder.DropTable(
                name: "ModeloContratoModeloPersonaje");

            migrationBuilder.DropTable(
                name: "ModeloDatosInvocacionBase");

            migrationBuilder.DropTable(
                name: "ModeloEspecialidad");

            migrationBuilder.DropTable(
                name: "ModeloVariable");

            migrationBuilder.DropTable(
                name: "TIEfectoSiendoAplicadoFuncion");

            migrationBuilder.DropTable(
                name: "TIFuncionEfecto");

            migrationBuilder.DropTable(
                name: "TIFuncionHabilidad");

            migrationBuilder.DropTable(
                name: "TIFuncionPadreFuncion");

            migrationBuilder.DropTable(
                name: "Tirada");

            migrationBuilder.DropTable(
                name: "UnidadesMapa");

            migrationBuilder.DropTable(
                name: "ModeloParticipante");

            migrationBuilder.DropTable(
                name: "ModeloContrato");

            migrationBuilder.DropTable(
                name: "ModeloEfectoSiendoAplicado");

            migrationBuilder.DropTable(
                name: "ModeloFuncion");

            migrationBuilder.DropTable(
                name: "ModeloMapa");

            migrationBuilder.DropTable(
                name: "ModeloAlianza");

            migrationBuilder.DropTable(
                name: "ModeloEfecto");

            migrationBuilder.DropTable(
                name: "Combates");

            migrationBuilder.DropTable(
                name: "ModeloHabilidad");

            migrationBuilder.DropTable(
                name: "ModeloRol");

            migrationBuilder.DropTable(
                name: "ModeloPersonaje");

            migrationBuilder.DropTable(
                name: "Vectores2");

            migrationBuilder.DropTable(
                name: "ModeloUtilizable");

            migrationBuilder.DropTable(
                name: "ModeloSlot");
        }
    }
}
