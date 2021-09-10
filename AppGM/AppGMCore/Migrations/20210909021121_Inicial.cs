using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppGM.Core.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Combates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IndicePersonajeTurnoActual = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnoActual = table.Column<uint>(type: "INTEGER", nullable: false),
                    EstaActivo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloAlianza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EIconoAlianza = table.Column<int>(type: "INTEGER", nullable: false),
                    FormatoImagen = table.Column<int>(type: "INTEGER", nullable: false),
                    PathImagenIcono = table.Column<string>(type: "TEXT", nullable: true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    EsVigente = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloAlianza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloAmbiente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CaracteristicasAmbiente = table.Column<int>(type: "INTEGER", nullable: false),
                    CantidadCasillas = table.Column<int>(type: "INTEGER", nullable: false),
                    TemperaturaActual = table.Column<int>(type: "INTEGER", nullable: false),
                    HumedadActual = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloAmbiente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloCaracteristicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Edad = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Estatura = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Peso = table.Column<ushort>(type: "INTEGER", nullable: false),
                    ESexo = table.Column<int>(type: "INTEGER", nullable: false),
                    EArquetipo = table.Column<int>(type: "INTEGER", nullable: false),
                    EManoDominante = table.Column<int>(type: "INTEGER", nullable: false),
                    Fisico = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Nacionalidad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloCaracteristicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    EsVigente = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloContrato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloDatosInvocacionBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloDatosInvocacionBase", x => x.Id);
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
                    ComportamientoAcumulativo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEfecto", x => x.Id);
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
                    ComportamientoAcumulativo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEfectoSiendoAplicado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloEspecialidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEspecialidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloFuncion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreFuncion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloFuncion", x => x.Id);
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
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Nivel = table.Column<byte>(type: "INTEGER", nullable: true),
                    EsParticular = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloHabilidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloMapa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreMapa = table.Column<string>(type: "TEXT", nullable: true),
                    EFormatoImagen = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloMapa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloModificadorDeStatBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ValorRequeridoTirada = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    TiposDeDaño = table.Column<int>(type: "INTEGER", nullable: true),
                    ModificacionPorcentual = table.Column<byte>(type: "INTEGER", nullable: true),
                    ModificacionFija = table.Column<byte>(type: "INTEGER", nullable: true),
                    NombreClase = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    IdObjeto = table.Column<int>(type: "INTEGER", nullable: true),
                    StatsQueAfecta = table.Column<int>(type: "INTEGER", nullable: true),
                    Valor = table.Column<byte>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloModificadorDeStatBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloParticipante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TiradaIniciativa = table.Column<int>(type: "INTEGER", nullable: false),
                    EsSuTurno = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloParticipante", x => x.Id);
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
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Registros = table.Column<string>(type: "TEXT", nullable: true),
                    FechaUltimaSesion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloRol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EspacioTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    EspacioDisponible = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloSlot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloUtilizable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Peso = table.Column<decimal>(type: "TEXT", nullable: false),
                    EStatQueAfecta = table.Column<int>(type: "INTEGER", nullable: false),
                    EStatDeLaQueDepende = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    SlotsQueOcupa = table.Column<decimal>(type: "TEXT", nullable: true),
                    Usos = table.Column<ushort>(type: "INTEGER", nullable: true),
                    UsosRestantes = table.Column<ushort>(type: "INTEGER", nullable: true),
                    TipoDeDañoQueInflige = table.Column<int>(type: "INTEGER", nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloDefensivoAbsoluto_Usos = table.Column<short>(type: "INTEGER", nullable: true),
                    ModeloDefensivoAbsoluto_UsosRestantes = table.Column<short>(type: "INTEGER", nullable: true),
                    DañosQuePuedeInfligir = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloUtilizable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloVariable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreVariable = table.Column<string>(type: "TEXT", nullable: true),
                    DescripcionVariable = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    TipoVariable = table.Column<string>(type: "TEXT", nullable: true),
                    IDVariable = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorVariable = table.Column<float>(type: "REAL", nullable: true),
                    ModeloVariableInt_ValorVariable = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloVariableString_ValorVariable = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloVariable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tirada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    MultiplicadorDeEspecialidad = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoTirada = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    StatDeLaQueDepende = table.Column<int>(type: "INTEGER", nullable: true),
                    Dados = table.Column<ushort>(type: "INTEGER", nullable: true),
                    Caras = table.Column<ushort>(type: "INTEGER", nullable: true),
                    TipoDeDaño = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tirada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesMapa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    ETipoUnidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    EClaseServant = table.Column<int>(type: "INTEGER", nullable: true),
                    Inicial = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: true),
                    EsDeMaster = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMapa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vectores2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    X = table.Column<double>(type: "REAL", nullable: false),
                    Y = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vectores2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TIAdministradorDeCombateAmbiente",
                columns: table => new
                {
                    IdAdministradorDeCombate = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAmbiente = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIAdministradorDeCombateAmbiente", x => new { x.IdAdministradorDeCombate, x.IdAmbiente });
                    table.ForeignKey(
                        name: "FK_TIAdministradorDeCombateAmbiente_Combates_IdAdministradorDeCombate",
                        column: x => x.IdAdministradorDeCombate,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIAdministradorDeCombateAmbiente_ModeloAmbiente_IdAmbiente",
                        column: x => x.IdAmbiente,
                        principalTable: "ModeloAmbiente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIAlianzaContrato",
                columns: table => new
                {
                    IdAlianza = table.Column<int>(type: "INTEGER", nullable: false),
                    IdContrato = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIAlianzaContrato", x => new { x.IdAlianza, x.IdContrato });
                    table.ForeignKey(
                        name: "FK_TIAlianzaContrato_ModeloAlianza_IdAlianza",
                        column: x => x.IdAlianza,
                        principalTable: "ModeloAlianza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIAlianzaContrato_ModeloContrato_IdContrato",
                        column: x => x.IdContrato,
                        principalTable: "ModeloContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIEfectoSiendoAplicadoEfecto",
                columns: table => new
                {
                    IdEfectoSiendoAplicado = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEfecto = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIEfectoSiendoAplicadoEfecto", x => new { x.IdEfectoSiendoAplicado, x.IdEfecto });
                    table.ForeignKey(
                        name: "FK_TIEfectoSiendoAplicadoEfecto_ModeloEfecto_IdEfecto",
                        column: x => x.IdEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIEfectoSiendoAplicadoEfecto_ModeloEfectoSiendoAplicado_IdEfectoSiendoAplicado",
                        column: x => x.IdEfectoSiendoAplicado,
                        principalTable: "ModeloEfectoSiendoAplicado",
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
                    table.PrimaryKey("PK_TIEfectoSiendoAplicadoFuncion", x => new { x.IdEfectoSiendoAplicado, x.IdFuncion });
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
                name: "TIHabilidadEfecto",
                columns: table => new
                {
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEfecto = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIHabilidadEfecto", x => new { x.IdHabilidad, x.IdEfecto });
                    table.ForeignKey(
                        name: "FK_TIHabilidadEfecto_ModeloEfecto_IdEfecto",
                        column: x => x.IdEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIHabilidadEfecto_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombateMapas",
                columns: table => new
                {
                    IdAdministradorDeCombate = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMapa = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombateMapas", x => new { x.IdAdministradorDeCombate, x.IdMapa });
                    table.ForeignKey(
                        name: "FK_CombateMapas_Combates_IdAdministradorDeCombate",
                        column: x => x.IdAdministradorDeCombate,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombateMapas_ModeloMapa_IdMapa",
                        column: x => x.IdMapa,
                        principalTable: "ModeloMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIMapaAmbiente",
                columns: table => new
                {
                    IdMapa = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAmbiente = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIMapaAmbiente", x => new { x.IdAmbiente, x.IdMapa });
                    table.ForeignKey(
                        name: "FK_TIMapaAmbiente_ModeloAmbiente_IdAmbiente",
                        column: x => x.IdAmbiente,
                        principalTable: "ModeloAmbiente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIMapaAmbiente_ModeloMapa_IdMapa",
                        column: x => x.IdMapa,
                        principalTable: "ModeloMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombateParticipantes",
                columns: table => new
                {
                    IdAdministradorDeCombate = table.Column<int>(type: "INTEGER", nullable: false),
                    IdParticipante = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombateParticipantes", x => new { x.IdAdministradorDeCombate, x.IdParticipante });
                    table.ForeignKey(
                        name: "FK_CombateParticipantes_Combates_IdAdministradorDeCombate",
                        column: x => x.IdAdministradorDeCombate,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombateParticipantes_ModeloParticipante_IdParticipante",
                        column: x => x.IdParticipante,
                        principalTable: "ModeloParticipante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipanteAccion",
                columns: table => new
                {
                    IdParticipante = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAccion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipanteAccion", x => new { x.IdParticipante, x.IdAccion });
                    table.ForeignKey(
                        name: "FK_ParticipanteAccion_Acciones_IdAccion",
                        column: x => x.IdAccion,
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipanteAccion_ModeloParticipante_IdParticipante",
                        column: x => x.IdParticipante,
                        principalTable: "ModeloParticipante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatesRol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCombate = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatesRol", x => new { x.IdRol, x.IdCombate });
                    table.ForeignKey(
                        name: "FK_CombatesRol_Combates_IdCombate",
                        column: x => x.IdCombate,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatesRol_ModeloRol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "ModeloRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapasRol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMapa = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapasRol", x => new { x.IdRol, x.IdMapa });
                    table.ForeignKey(
                        name: "FK_MapasRol_ModeloMapa_IdMapa",
                        column: x => x.IdMapa,
                        principalTable: "ModeloMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapasRol_ModeloRol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "ModeloRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIRolAmbiente",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAmbiente = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIRolAmbiente", x => new { x.IdRol, x.IdAmbiente });
                    table.ForeignKey(
                        name: "FK_TIRolAmbiente_ModeloAmbiente_IdAmbiente",
                        column: x => x.IdAmbiente,
                        principalTable: "ModeloAmbiente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIRolAmbiente_ModeloRol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "ModeloRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIArmasDistanciaEfecto",
                columns: table => new
                {
                    IdArmasDistancia = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEfecto = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIArmasDistanciaEfecto", x => new { x.IdArmasDistancia, x.IdEfecto });
                    table.ForeignKey(
                        name: "FK_TIArmasDistanciaEfecto_ModeloEfecto_IdEfecto",
                        column: x => x.IdEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIArmasDistanciaEfecto_ModeloUtilizable_IdArmasDistancia",
                        column: x => x.IdArmasDistancia,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIHabilidadItem",
                columns: table => new
                {
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    IdItem = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIHabilidadItem", x => new { x.IdHabilidad, x.IdItem });
                    table.ForeignKey(
                        name: "FK_TIHabilidadItem_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIHabilidadItem_ModeloUtilizable_IdItem",
                        column: x => x.IdItem,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIOfensivoEfecto",
                columns: table => new
                {
                    IdOfensivo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEfecto = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIOfensivoEfecto", x => new { x.IdOfensivo, x.IdEfecto });
                    table.ForeignKey(
                        name: "FK_TIOfensivoEfecto_ModeloEfecto_IdEfecto",
                        column: x => x.IdEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIOfensivoEfecto_ModeloUtilizable_IdOfensivo",
                        column: x => x.IdOfensivo,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIPortableModificadorDeStatBase",
                columns: table => new
                {
                    IdPortable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdModificadorDeStat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPortableModificadorDeStatBase", x => new { x.IdPortable, x.IdModificadorDeStat });
                    table.ForeignKey(
                        name: "FK_TIPortableModificadorDeStatBase_ModeloModificadorDeStatBase_IdModificadorDeStat",
                        column: x => x.IdModificadorDeStat,
                        principalTable: "ModeloModificadorDeStatBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPortableModificadorDeStatBase_ModeloUtilizable_IdPortable",
                        column: x => x.IdPortable,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIPortableSlots",
                columns: table => new
                {
                    IdPortable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdSlot = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPortableSlots", x => new { x.IdPortable, x.IdSlot });
                    table.ForeignKey(
                        name: "FK_TIPortableSlots_ModeloSlot_IdSlot",
                        column: x => x.IdSlot,
                        principalTable: "ModeloSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPortableSlots_ModeloUtilizable_IdPortable",
                        column: x => x.IdPortable,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TISlotItem",
                columns: table => new
                {
                    IdSlot = table.Column<int>(type: "INTEGER", nullable: false),
                    IdItem = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TISlotItem", x => new { x.IdSlot, x.IdItem });
                    table.ForeignKey(
                        name: "FK_TISlotItem_ModeloSlot_IdSlot",
                        column: x => x.IdSlot,
                        principalTable: "ModeloSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TISlotItem_ModeloUtilizable_IdItem",
                        column: x => x.IdItem,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIUtilizableEfecto",
                columns: table => new
                {
                    IdUtilizable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEfecto = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIUtilizableEfecto", x => new { x.IdUtilizable, x.IdEfecto });
                    table.ForeignKey(
                        name: "FK_TIUtilizableEfecto_ModeloEfecto_IdEfecto",
                        column: x => x.IdEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIUtilizableEfecto_ModeloUtilizable_IdUtilizable",
                        column: x => x.IdUtilizable,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIUtilizableModificadorDeStatBase",
                columns: table => new
                {
                    IdUtilizable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdModificadorStatBase = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIUtilizableModificadorDeStatBase", x => new { x.IdUtilizable, x.IdModificadorStatBase });
                    table.ForeignKey(
                        name: "FK_TIUtilizableModificadorDeStatBase_ModeloModificadorDeStatBase_IdModificadorStatBase",
                        column: x => x.IdModificadorStatBase,
                        principalTable: "ModeloModificadorDeStatBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIUtilizableModificadorDeStatBase_ModeloUtilizable_IdUtilizable",
                        column: x => x.IdUtilizable,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIVariableFuncion",
                columns: table => new
                {
                    IdVariable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdFuncion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIVariableFuncion", x => new { x.IdVariable, x.IdFuncion });
                    table.ForeignKey(
                        name: "FK_TIVariableFuncion_ModeloFuncion_IdFuncion",
                        column: x => x.IdFuncion,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIVariableFuncion_ModeloVariable_IdVariable",
                        column: x => x.IdVariable,
                        principalTable: "ModeloVariable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIVariableHabilidad",
                columns: table => new
                {
                    IdVariable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIVariableHabilidad", x => new { x.IdVariable, x.IdHabilidad });
                    table.ForeignKey(
                        name: "FK_TIVariableHabilidad_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIVariableHabilidad_ModeloVariable_IdVariable",
                        column: x => x.IdVariable,
                        principalTable: "ModeloVariable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIVariableUtilizable",
                columns: table => new
                {
                    IdVariable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUtilizable = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIVariableUtilizable", x => new { x.IdVariable, x.IdUtilizable });
                    table.ForeignKey(
                        name: "FK_TIVariableUtilizable_ModeloUtilizable_IdUtilizable",
                        column: x => x.IdUtilizable,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIVariableUtilizable_ModeloVariable_IdVariable",
                        column: x => x.IdVariable,
                        principalTable: "ModeloVariable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIArmasDistanciaTiradaDeDaño",
                columns: table => new
                {
                    IdArmasDistancia = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIArmasDistanciaTiradaDeDaño", x => new { x.IdArmasDistancia, x.IdTirada });
                    table.ForeignKey(
                        name: "FK_TIArmasDistanciaTiradaDeDaño_ModeloUtilizable_IdArmasDistancia",
                        column: x => x.IdArmasDistancia,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIArmasDistanciaTiradaDeDaño_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIArmasDistanciaTiradaVariable",
                columns: table => new
                {
                    IdArmasDistancia = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIArmasDistanciaTiradaVariable", x => new { x.IdArmasDistancia, x.IdTirada });
                    table.ForeignKey(
                        name: "FK_TIArmasDistanciaTiradaVariable_ModeloUtilizable_IdArmasDistancia",
                        column: x => x.IdArmasDistancia,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIArmasDistanciaTiradaVariable_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIHabilidadTiradaBase",
                columns: table => new
                {
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIHabilidadTiradaBase", x => new { x.IdHabilidad, x.IdTirada });
                    table.ForeignKey(
                        name: "FK_TIHabilidadTiradaBase_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIHabilidadTiradaBase_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIModificadorDeStatBaseTiradaBase",
                columns: table => new
                {
                    IdModificadorDeStatBase = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIModificadorDeStatBaseTiradaBase", x => new { x.IdModificadorDeStatBase, x.IdTirada });
                    table.ForeignKey(
                        name: "FK_TIModificadorDeStatBaseTiradaBase_ModeloModificadorDeStatBase_IdModificadorDeStatBase",
                        column: x => x.IdModificadorDeStatBase,
                        principalTable: "ModeloModificadorDeStatBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIModificadorDeStatBaseTiradaBase_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIOfensivoTiradaDeDaño",
                columns: table => new
                {
                    IdOfensivo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTiradaDeDaño = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIOfensivoTiradaDeDaño", x => new { x.IdOfensivo, x.IdTiradaDeDaño });
                    table.ForeignKey(
                        name: "FK_TIOfensivoTiradaDeDaño_ModeloUtilizable_IdOfensivo",
                        column: x => x.IdOfensivo,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIOfensivoTiradaDeDaño_Tirada_IdTiradaDeDaño",
                        column: x => x.IdTiradaDeDaño,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TITiradaFuncion",
                columns: table => new
                {
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false),
                    IdFuncion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TITiradaFuncion", x => new { x.IdTirada, x.IdFuncion });
                    table.ForeignKey(
                        name: "FK_TITiradaFuncion_ModeloFuncion_IdFuncion",
                        column: x => x.IdFuncion,
                        principalTable: "ModeloFuncion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TITiradaFuncion_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TITiradaHabilidad",
                columns: table => new
                {
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false),
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TITiradaHabilidad", x => new { x.IdTirada, x.IdHabilidad });
                    table.ForeignKey(
                        name: "FK_TITiradaHabilidad_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TITiradaHabilidad_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TITiradaUtilizable",
                columns: table => new
                {
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUtilizable = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TITiradaUtilizable", x => new { x.IdTirada, x.IdUtilizable });
                    table.ForeignKey(
                        name: "FK_TITiradaUtilizable_ModeloUtilizable_IdUtilizable",
                        column: x => x.IdUtilizable,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TITiradaUtilizable_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIUtilizableTiradaBase",
                columns: table => new
                {
                    IdUtilizable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIUtilizableTiradaBase", x => new { x.IdUtilizable, x.IdTirada });
                    table.ForeignKey(
                        name: "FK_TIUtilizableTiradaBase_ModeloUtilizable_IdUtilizable",
                        column: x => x.IdUtilizable,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIUtilizableTiradaBase_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapasUnidadesMapa",
                columns: table => new
                {
                    IdMapa = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUnidadMapa = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapasUnidadesMapa", x => new { x.IdMapa, x.IdUnidadMapa });
                    table.ForeignKey(
                        name: "FK_MapasUnidadesMapa_ModeloMapa_IdMapa",
                        column: x => x.IdMapa,
                        principalTable: "ModeloMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapasUnidadesMapa_UnidadesMapa_IdUnidadMapa",
                        column: x => x.IdUnidadMapa,
                        principalTable: "UnidadesMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeloPersonaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    PathImagenRelativo = table.Column<string>(type: "TEXT", nullable: true),
                    PathImagenAbsoluto = table.Column<string>(type: "TEXT", nullable: true),
                    EstaEnCombate = table.Column<bool>(type: "INTEGER", nullable: false),
                    FormatoImagen = table.Column<int>(type: "INTEGER", nullable: false),
                    PosicionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Clase = table.Column<int>(type: "INTEGER", nullable: false),
                    EsAutomata = table.Column<bool>(type: "INTEGER", nullable: true),
                    TurnosDeDuracion = table.Column<byte>(type: "INTEGER", nullable: true),
                    EClaseServant = table.Column<int>(type: "INTEGER", nullable: true),
                    RangoHechiceria = table.Column<ushort>(type: "INTEGER", nullable: true),
                    EClaseDeSuServant = table.Column<int>(type: "INTEGER", nullable: true),
                    EBienestar = table.Column<int>(type: "INTEGER", nullable: true),
                    Od = table.Column<int>(type: "INTEGER", nullable: true),
                    OdActual = table.Column<int>(type: "INTEGER", nullable: true),
                    Mana = table.Column<int>(type: "INTEGER", nullable: true),
                    ManaActual = table.Column<int>(type: "INTEGER", nullable: true),
                    Chr = table.Column<ushort>(type: "INTEGER", nullable: true),
                    VentajaChr = table.Column<ushort>(type: "INTEGER", nullable: true),
                    CommandSpells = table.Column<ushort>(type: "INTEGER", nullable: true),
                    Lore = table.Column<string>(type: "TEXT", nullable: true),
                    Origen = table.Column<string>(type: "TEXT", nullable: true),
                    Afinidad = table.Column<string>(type: "TEXT", nullable: true),
                    mERangoNP = table.Column<int>(type: "INTEGER", nullable: true),
                    Prana = table.Column<int>(type: "INTEGER", nullable: true),
                    PranaActual = table.Column<int>(type: "INTEGER", nullable: true),
                    Fuente = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloPersonaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloPersonaje_Vectores2_PosicionId",
                        column: x => x.PosicionId,
                        principalTable: "Vectores2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesMapaVectores2",
                columns: table => new
                {
                    IdUnidadMapa = table.Column<int>(type: "INTEGER", nullable: false),
                    IdVector = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMapaVectores2", x => new { x.IdUnidadMapa, x.IdVector });
                    table.ForeignKey(
                        name: "FK_UnidadesMapaVectores2_UnidadesMapa_IdUnidadMapa",
                        column: x => x.IdUnidadMapa,
                        principalTable: "UnidadesMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnidadesMapaVectores2_Vectores2_IdVector",
                        column: x => x.IdVector,
                        principalTable: "Vectores2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantePersonaje",
                columns: table => new
                {
                    IdParticipante = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantePersonaje", x => new { x.IdParticipante, x.IdPersonaje });
                    table.ForeignKey(
                        name: "FK_ParticipantePersonaje_ModeloParticipante_IdParticipante",
                        column: x => x.IdParticipante,
                        principalTable: "ModeloParticipante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantePersonaje_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeArmasDistancias",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdArmaDistancia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeArmasDistancias", x => new { x.IdArmaDistancia, x.IdPersonaje });
                    table.ForeignKey(
                        name: "FK_PersonajeArmasDistancias_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeArmasDistancias_ModeloUtilizable_IdArmaDistancia",
                        column: x => x.IdArmaDistancia,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeContratos",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdContrato = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeContratos", x => new { x.IdPersonaje, x.IdContrato });
                    table.ForeignKey(
                        name: "FK_PersonajeContratos_ModeloContrato_IdContrato",
                        column: x => x.IdContrato,
                        principalTable: "ModeloContrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeContratos_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeDefensivos",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdDefensivo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeDefensivos", x => new { x.IdPersonaje, x.IdDefensivo });
                    table.ForeignKey(
                        name: "FK_PersonajeDefensivos_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeDefensivos_ModeloUtilizable_IdDefensivo",
                        column: x => x.IdDefensivo,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeEfectosAplicandose",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEfectoSiendoAplicado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeEfectosAplicandose", x => new { x.IdPersonaje, x.IdEfectoSiendoAplicado });
                    table.ForeignKey(
                        name: "FK_PersonajeEfectosAplicandose_ModeloEfectoSiendoAplicado_IdEfectoSiendoAplicado",
                        column: x => x.IdEfectoSiendoAplicado,
                        principalTable: "ModeloEfectoSiendoAplicado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeEfectosAplicandose_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeMagias",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMagia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeMagias", x => new { x.IdPersonaje, x.IdMagia });
                    table.ForeignKey(
                        name: "FK_PersonajeMagias_ModeloHabilidad_IdMagia",
                        column: x => x.IdMagia,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeMagias_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeModificadoresDeDefensa",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdModificadorDefensa = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeModificadoresDeDefensa", x => new { x.IdPersonaje, x.IdModificadorDefensa });
                    table.ForeignKey(
                        name: "FK_PersonajeModificadoresDeDefensa_ModeloModificadorDeStatBase_IdModificadorDefensa",
                        column: x => x.IdModificadorDefensa,
                        principalTable: "ModeloModificadorDeStatBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeModificadoresDeDefensa_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajePerks",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPerk = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajePerks", x => new { x.IdPersonaje, x.IdPerk });
                    table.ForeignKey(
                        name: "FK_PersonajePerks_ModeloHabilidad_IdPerk",
                        column: x => x.IdPerk,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajePerks_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeSkills",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeSkills", x => new { x.IdPersonaje, x.IdHabilidad });
                    table.ForeignKey(
                        name: "FK_PersonajeSkills_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeSkills_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajesRol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajesRol", x => new { x.IdRol, x.IdPersonaje });
                    table.ForeignKey(
                        name: "FK_PersonajesRol_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajesRol_ModeloRol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "ModeloRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajesUnidadesMapa",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUnidadMapa = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajesUnidadesMapa", x => new { x.IdPersonaje, x.IdUnidadMapa });
                    table.ForeignKey(
                        name: "FK_PersonajesUnidadesMapa_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajesUnidadesMapa_UnidadesMapa_IdUnidadMapa",
                        column: x => x.IdUnidadMapa,
                        principalTable: "UnidadesMapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeUtilizables",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUtilizable = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeUtilizables", x => new { x.IdPersonaje, x.IdUtilizable });
                    table.ForeignKey(
                        name: "FK_PersonajeUtilizables_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeUtilizables_ModeloUtilizable_IdUtilizable",
                        column: x => x.IdUtilizable,
                        principalTable: "ModeloUtilizable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServantNoblePhantasms",
                columns: table => new
                {
                    IdServant = table.Column<int>(type: "INTEGER", nullable: false),
                    IdNoblePhantasm = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServantNoblePhantasms", x => new { x.IdServant, x.IdNoblePhantasm });
                    table.ForeignKey(
                        name: "FK_ServantNoblePhantasms_ModeloHabilidad_IdNoblePhantasm",
                        column: x => x.IdNoblePhantasm,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServantNoblePhantasms_ModeloPersonaje_IdServant",
                        column: x => x.IdServant,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIEfectoSiendoAplicadoPersonajeInstigador",
                columns: table => new
                {
                    IdEfectoSiendoAplicado = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPersonajeInstigador = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIEfectoSiendoAplicadoPersonajeInstigador", x => new { x.IdEfectoSiendoAplicado, x.IdPersonajeInstigador });
                    table.ForeignKey(
                        name: "FK_TIEfectoSiendoAplicadoPersonajeInstigador_ModeloEfectoSiendoAplicado_IdEfectoSiendoAplicado",
                        column: x => x.IdEfectoSiendoAplicado,
                        principalTable: "ModeloEfectoSiendoAplicado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIEfectoSiendoAplicadoPersonajeInstigador_ModeloPersonaje_IdPersonajeInstigador",
                        column: x => x.IdPersonajeInstigador,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIEfectoSiendoAplicadoPersonajeObjetivo",
                columns: table => new
                {
                    IdEfectoSiendoAplicado = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPersonajeObjetivo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIEfectoSiendoAplicadoPersonajeObjetivo", x => new { x.IdEfectoSiendoAplicado, x.IdPersonajeObjetivo });
                    table.ForeignKey(
                        name: "FK_TIEfectoSiendoAplicadoPersonajeObjetivo_ModeloEfectoSiendoAplicado_IdEfectoSiendoAplicado",
                        column: x => x.IdEfectoSiendoAplicado,
                        principalTable: "ModeloEfectoSiendoAplicado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIEfectoSiendoAplicadoPersonajeObjetivo_ModeloPersonaje_IdPersonajeObjetivo",
                        column: x => x.IdPersonajeObjetivo,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIEspecialidadPersonaje",
                columns: table => new
                {
                    IDEspecialidad = table.Column<int>(type: "INTEGER", nullable: false),
                    IDPersonaje = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIEspecialidadPersonaje", x => new { x.IDEspecialidad, x.IDPersonaje });
                    table.ForeignKey(
                        name: "FK_TIEspecialidadPersonaje_ModeloEspecialidad_IDEspecialidad",
                        column: x => x.IDEspecialidad,
                        principalTable: "ModeloEspecialidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIEspecialidadPersonaje_ModeloPersonaje_IDPersonaje",
                        column: x => x.IDPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIHabilidadInvocacion",
                columns: table => new
                {
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    IdInvocacion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIHabilidadInvocacion", x => new { x.IdHabilidad, x.IdInvocacion });
                    table.ForeignKey(
                        name: "FK_TIHabilidadInvocacion_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIHabilidadInvocacion_ModeloPersonaje_IdInvocacion",
                        column: x => x.IdInvocacion,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIInvocacionDatosInvocacion",
                columns: table => new
                {
                    IdInvocacion = table.Column<int>(type: "INTEGER", nullable: false),
                    IdDatos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIInvocacionDatosInvocacion", x => new { x.IdInvocacion, x.IdDatos });
                    table.ForeignKey(
                        name: "FK_TIInvocacionDatosInvocacion_ModeloDatosInvocacionBase_IdDatos",
                        column: x => x.IdDatos,
                        principalTable: "ModeloDatosInvocacionBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIInvocacionDatosInvocacion_ModeloPersonaje_IdInvocacion",
                        column: x => x.IdInvocacion,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIInvocacionEfecto",
                columns: table => new
                {
                    IdInvocacion = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEfecto = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIInvocacionEfecto", x => new { x.IdInvocacion, x.IdEfecto });
                    table.ForeignKey(
                        name: "FK_TIInvocacionEfecto_ModeloEfecto_IdEfecto",
                        column: x => x.IdEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIInvocacionEfecto_ModeloPersonaje_IdInvocacion",
                        column: x => x.IdInvocacion,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIInvocacionPersonaje",
                columns: table => new
                {
                    IdInvocacion = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIInvocacionPersonaje", x => new { x.IdInvocacion, x.IdPersonaje });
                    table.ForeignKey(
                        name: "FK_TIInvocacionPersonaje_ModeloPersonaje_IdInvocacion",
                        column: x => x.IdInvocacion,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIInvocacionPersonaje_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIPersonajeAlianza",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAlianza = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPersonajeAlianza", x => new { x.IdPersonaje, x.IdAlianza });
                    table.ForeignKey(
                        name: "FK_TIPersonajeAlianza_ModeloAlianza_IdAlianza",
                        column: x => x.IdAlianza,
                        principalTable: "ModeloAlianza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPersonajeAlianza_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIPersonajeJugableCaracteristicas",
                columns: table => new
                {
                    IdPersonajeJugable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCaracteristica = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPersonajeJugableCaracteristicas", x => new { x.IdPersonajeJugable, x.IdCaracteristica });
                    table.ForeignKey(
                        name: "FK_TIPersonajeJugableCaracteristicas_ModeloCaracteristicas_IdCaracteristica",
                        column: x => x.IdCaracteristica,
                        principalTable: "ModeloCaracteristicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPersonajeJugableCaracteristicas_ModeloPersonaje_IdPersonajeJugable",
                        column: x => x.IdPersonajeJugable,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIPersonajeJugableInvocacion",
                columns: table => new
                {
                    IdPersonajeJugable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdInvocacion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPersonajeJugableInvocacion", x => new { x.IdPersonajeJugable, x.IdInvocacion });
                    table.ForeignKey(
                        name: "FK_TIPersonajeJugableInvocacion_ModeloPersonaje_IdInvocacion",
                        column: x => x.IdInvocacion,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPersonajeJugableInvocacion_ModeloPersonaje_IdPersonajeJugable",
                        column: x => x.IdPersonajeJugable,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TITiradaPersonaje",
                columns: table => new
                {
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TITiradaPersonaje", x => new { x.IdTirada, x.IdPersonaje });
                    table.ForeignKey(
                        name: "FK_TITiradaPersonaje_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TITiradaPersonaje_Tirada_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "Tirada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIVariablePersonaje",
                columns: table => new
                {
                    IdVariable = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIVariablePersonaje", x => new { x.IdVariable, x.IdPersonaje });
                    table.ForeignKey(
                        name: "FK_TIVariablePersonaje_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIVariablePersonaje_ModeloVariable_IdVariable",
                        column: x => x.IdVariable,
                        principalTable: "ModeloVariable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombateMapas_IdMapa",
                table: "CombateMapas",
                column: "IdMapa");

            migrationBuilder.CreateIndex(
                name: "IX_CombateParticipantes_IdParticipante",
                table: "CombateParticipantes",
                column: "IdParticipante",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CombatesRol_IdCombate",
                table: "CombatesRol",
                column: "IdCombate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MapasRol_IdMapa",
                table: "MapasRol",
                column: "IdMapa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MapasUnidadesMapa_IdUnidadMapa",
                table: "MapasUnidadesMapa",
                column: "IdUnidadMapa");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloPersonaje_PosicionId",
                table: "ModeloPersonaje",
                column: "PosicionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteAccion_IdAccion",
                table: "ParticipanteAccion",
                column: "IdAccion");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantePersonaje_IdParticipante",
                table: "ParticipantePersonaje",
                column: "IdParticipante",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantePersonaje_IdPersonaje",
                table: "ParticipantePersonaje",
                column: "IdPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajeArmasDistancias_IdPersonaje",
                table: "PersonajeArmasDistancias",
                column: "IdPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajeContratos_IdContrato",
                table: "PersonajeContratos",
                column: "IdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajeDefensivos_IdDefensivo",
                table: "PersonajeDefensivos",
                column: "IdDefensivo");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajeEfectosAplicandose_IdEfectoSiendoAplicado",
                table: "PersonajeEfectosAplicandose",
                column: "IdEfectoSiendoAplicado");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajeMagias_IdMagia",
                table: "PersonajeMagias",
                column: "IdMagia");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajeModificadoresDeDefensa_IdModificadorDefensa",
                table: "PersonajeModificadoresDeDefensa",
                column: "IdModificadorDefensa");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajePerks_IdPerk",
                table: "PersonajePerks",
                column: "IdPerk");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajeSkills_IdHabilidad",
                table: "PersonajeSkills",
                column: "IdHabilidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonajesRol_IdPersonaje",
                table: "PersonajesRol",
                column: "IdPersonaje",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonajesUnidadesMapa_IdUnidadMapa",
                table: "PersonajesUnidadesMapa",
                column: "IdUnidadMapa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonajeUtilizables_IdUtilizable",
                table: "PersonajeUtilizables",
                column: "IdUtilizable");

            migrationBuilder.CreateIndex(
                name: "IX_ServantNoblePhantasms_IdNoblePhantasm",
                table: "ServantNoblePhantasms",
                column: "IdNoblePhantasm");

            migrationBuilder.CreateIndex(
                name: "IX_TIAdministradorDeCombateAmbiente_IdAdministradorDeCombate",
                table: "TIAdministradorDeCombateAmbiente",
                column: "IdAdministradorDeCombate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIAdministradorDeCombateAmbiente_IdAmbiente",
                table: "TIAdministradorDeCombateAmbiente",
                column: "IdAmbiente");

            migrationBuilder.CreateIndex(
                name: "IX_TIAlianzaContrato_IdAlianza",
                table: "TIAlianzaContrato",
                column: "IdAlianza",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIAlianzaContrato_IdContrato",
                table: "TIAlianzaContrato",
                column: "IdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_TIArmasDistanciaEfecto_IdEfecto",
                table: "TIArmasDistanciaEfecto",
                column: "IdEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIArmasDistanciaTiradaDeDaño_IdArmasDistancia",
                table: "TIArmasDistanciaTiradaDeDaño",
                column: "IdArmasDistancia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIArmasDistanciaTiradaDeDaño_IdTirada",
                table: "TIArmasDistanciaTiradaDeDaño",
                column: "IdTirada");

            migrationBuilder.CreateIndex(
                name: "IX_TIArmasDistanciaTiradaVariable_IdArmasDistancia",
                table: "TIArmasDistanciaTiradaVariable",
                column: "IdArmasDistancia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIArmasDistanciaTiradaVariable_IdTirada",
                table: "TIArmasDistanciaTiradaVariable",
                column: "IdTirada");

            migrationBuilder.CreateIndex(
                name: "IX_TIEfectoSiendoAplicadoEfecto_IdEfecto",
                table: "TIEfectoSiendoAplicadoEfecto",
                column: "IdEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIEfectoSiendoAplicadoEfecto_IdEfectoSiendoAplicado",
                table: "TIEfectoSiendoAplicadoEfecto",
                column: "IdEfectoSiendoAplicado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIEfectoSiendoAplicadoFuncion_IdFuncion",
                table: "TIEfectoSiendoAplicadoFuncion",
                column: "IdFuncion");

            migrationBuilder.CreateIndex(
                name: "IX_TIEfectoSiendoAplicadoPersonajeInstigador_IdEfectoSiendoAplicado",
                table: "TIEfectoSiendoAplicadoPersonajeInstigador",
                column: "IdEfectoSiendoAplicado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIEfectoSiendoAplicadoPersonajeInstigador_IdPersonajeInstigador",
                table: "TIEfectoSiendoAplicadoPersonajeInstigador",
                column: "IdPersonajeInstigador");

            migrationBuilder.CreateIndex(
                name: "IX_TIEfectoSiendoAplicadoPersonajeObjetivo_IdEfectoSiendoAplicado",
                table: "TIEfectoSiendoAplicadoPersonajeObjetivo",
                column: "IdEfectoSiendoAplicado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIEfectoSiendoAplicadoPersonajeObjetivo_IdPersonajeObjetivo",
                table: "TIEfectoSiendoAplicadoPersonajeObjetivo",
                column: "IdPersonajeObjetivo");

            migrationBuilder.CreateIndex(
                name: "IX_TIEspecialidadPersonaje_IDEspecialidad",
                table: "TIEspecialidadPersonaje",
                column: "IDEspecialidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIEspecialidadPersonaje_IDPersonaje",
                table: "TIEspecialidadPersonaje",
                column: "IDPersonaje");

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
                name: "IX_TIHabilidadEfecto_IdEfecto",
                table: "TIHabilidadEfecto",
                column: "IdEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadInvocacion_IdInvocacion",
                table: "TIHabilidadInvocacion",
                column: "IdInvocacion");

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadItem_IdItem",
                table: "TIHabilidadItem",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadTiradaBase_IdTirada",
                table: "TIHabilidadTiradaBase",
                column: "IdTirada");

            migrationBuilder.CreateIndex(
                name: "IX_TIInvocacionDatosInvocacion_IdDatos",
                table: "TIInvocacionDatosInvocacion",
                column: "IdDatos");

            migrationBuilder.CreateIndex(
                name: "IX_TIInvocacionDatosInvocacion_IdInvocacion",
                table: "TIInvocacionDatosInvocacion",
                column: "IdInvocacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIInvocacionEfecto_IdEfecto",
                table: "TIInvocacionEfecto",
                column: "IdEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIInvocacionEfecto_IdInvocacion",
                table: "TIInvocacionEfecto",
                column: "IdInvocacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIInvocacionPersonaje_IdInvocacion",
                table: "TIInvocacionPersonaje",
                column: "IdInvocacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIInvocacionPersonaje_IdPersonaje",
                table: "TIInvocacionPersonaje",
                column: "IdPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_TIMapaAmbiente_IdAmbiente",
                table: "TIMapaAmbiente",
                column: "IdAmbiente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIMapaAmbiente_IdMapa",
                table: "TIMapaAmbiente",
                column: "IdMapa");

            migrationBuilder.CreateIndex(
                name: "IX_TIModificadorDeStatBaseTiradaBase_IdModificadorDeStatBase",
                table: "TIModificadorDeStatBaseTiradaBase",
                column: "IdModificadorDeStatBase",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIModificadorDeStatBaseTiradaBase_IdTirada",
                table: "TIModificadorDeStatBaseTiradaBase",
                column: "IdTirada");

            migrationBuilder.CreateIndex(
                name: "IX_TIOfensivoEfecto_IdEfecto",
                table: "TIOfensivoEfecto",
                column: "IdEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIOfensivoEfecto_IdOfensivo",
                table: "TIOfensivoEfecto",
                column: "IdOfensivo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIOfensivoTiradaDeDaño_IdTiradaDeDaño",
                table: "TIOfensivoTiradaDeDaño",
                column: "IdTiradaDeDaño");

            migrationBuilder.CreateIndex(
                name: "IX_TIPersonajeAlianza_IdAlianza",
                table: "TIPersonajeAlianza",
                column: "IdAlianza");

            migrationBuilder.CreateIndex(
                name: "IX_TIPersonajeJugableCaracteristicas_IdCaracteristica",
                table: "TIPersonajeJugableCaracteristicas",
                column: "IdCaracteristica");

            migrationBuilder.CreateIndex(
                name: "IX_TIPersonajeJugableCaracteristicas_IdPersonajeJugable",
                table: "TIPersonajeJugableCaracteristicas",
                column: "IdPersonajeJugable",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIPersonajeJugableInvocacion_IdInvocacion",
                table: "TIPersonajeJugableInvocacion",
                column: "IdInvocacion");

            migrationBuilder.CreateIndex(
                name: "IX_TIPortableModificadorDeStatBase_IdModificadorDeStat",
                table: "TIPortableModificadorDeStatBase",
                column: "IdModificadorDeStat");

            migrationBuilder.CreateIndex(
                name: "IX_TIPortableSlots_IdSlot",
                table: "TIPortableSlots",
                column: "IdSlot");

            migrationBuilder.CreateIndex(
                name: "IX_TIRolAmbiente_IdAmbiente",
                table: "TIRolAmbiente",
                column: "IdAmbiente");

            migrationBuilder.CreateIndex(
                name: "IX_TIRolAmbiente_IdRol",
                table: "TIRolAmbiente",
                column: "IdRol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TISlotItem_IdItem",
                table: "TISlotItem",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_TITiradaFuncion_IdFuncion",
                table: "TITiradaFuncion",
                column: "IdFuncion");

            migrationBuilder.CreateIndex(
                name: "IX_TITiradaFuncion_IdTirada",
                table: "TITiradaFuncion",
                column: "IdTirada",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TITiradaHabilidad_IdHabilidad",
                table: "TITiradaHabilidad",
                column: "IdHabilidad");

            migrationBuilder.CreateIndex(
                name: "IX_TITiradaHabilidad_IdTirada",
                table: "TITiradaHabilidad",
                column: "IdTirada",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TITiradaPersonaje_IdPersonaje",
                table: "TITiradaPersonaje",
                column: "IdPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_TITiradaPersonaje_IdTirada",
                table: "TITiradaPersonaje",
                column: "IdTirada",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TITiradaUtilizable_IdTirada",
                table: "TITiradaUtilizable",
                column: "IdTirada",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TITiradaUtilizable_IdUtilizable",
                table: "TITiradaUtilizable",
                column: "IdUtilizable");

            migrationBuilder.CreateIndex(
                name: "IX_TIUtilizableEfecto_IdEfecto",
                table: "TIUtilizableEfecto",
                column: "IdEfecto");

            migrationBuilder.CreateIndex(
                name: "IX_TIUtilizableModificadorDeStatBase_IdModificadorStatBase",
                table: "TIUtilizableModificadorDeStatBase",
                column: "IdModificadorStatBase");

            migrationBuilder.CreateIndex(
                name: "IX_TIUtilizableModificadorDeStatBase_IdUtilizable",
                table: "TIUtilizableModificadorDeStatBase",
                column: "IdUtilizable",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIUtilizableTiradaBase_IdTirada",
                table: "TIUtilizableTiradaBase",
                column: "IdTirada");

            migrationBuilder.CreateIndex(
                name: "IX_TIUtilizableTiradaBase_IdUtilizable",
                table: "TIUtilizableTiradaBase",
                column: "IdUtilizable",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIVariableFuncion_IdFuncion",
                table: "TIVariableFuncion",
                column: "IdFuncion");

            migrationBuilder.CreateIndex(
                name: "IX_TIVariableFuncion_IdVariable",
                table: "TIVariableFuncion",
                column: "IdVariable",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIVariableHabilidad_IdHabilidad",
                table: "TIVariableHabilidad",
                column: "IdHabilidad");

            migrationBuilder.CreateIndex(
                name: "IX_TIVariableHabilidad_IdVariable",
                table: "TIVariableHabilidad",
                column: "IdVariable",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIVariablePersonaje_IdPersonaje",
                table: "TIVariablePersonaje",
                column: "IdPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_TIVariablePersonaje_IdVariable",
                table: "TIVariablePersonaje",
                column: "IdVariable",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIVariableUtilizable_IdUtilizable",
                table: "TIVariableUtilizable",
                column: "IdUtilizable");

            migrationBuilder.CreateIndex(
                name: "IX_TIVariableUtilizable_IdVariable",
                table: "TIVariableUtilizable",
                column: "IdVariable",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesMapaVectores2_IdUnidadMapa",
                table: "UnidadesMapaVectores2",
                column: "IdUnidadMapa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesMapaVectores2_IdVector",
                table: "UnidadesMapaVectores2",
                column: "IdVector");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombateMapas");

            migrationBuilder.DropTable(
                name: "CombateParticipantes");

            migrationBuilder.DropTable(
                name: "CombatesRol");

            migrationBuilder.DropTable(
                name: "MapasRol");

            migrationBuilder.DropTable(
                name: "MapasUnidadesMapa");

            migrationBuilder.DropTable(
                name: "ParticipanteAccion");

            migrationBuilder.DropTable(
                name: "ParticipantePersonaje");

            migrationBuilder.DropTable(
                name: "PersonajeArmasDistancias");

            migrationBuilder.DropTable(
                name: "PersonajeContratos");

            migrationBuilder.DropTable(
                name: "PersonajeDefensivos");

            migrationBuilder.DropTable(
                name: "PersonajeEfectosAplicandose");

            migrationBuilder.DropTable(
                name: "PersonajeMagias");

            migrationBuilder.DropTable(
                name: "PersonajeModificadoresDeDefensa");

            migrationBuilder.DropTable(
                name: "PersonajePerks");

            migrationBuilder.DropTable(
                name: "PersonajeSkills");

            migrationBuilder.DropTable(
                name: "PersonajesRol");

            migrationBuilder.DropTable(
                name: "PersonajesUnidadesMapa");

            migrationBuilder.DropTable(
                name: "PersonajeUtilizables");

            migrationBuilder.DropTable(
                name: "ServantNoblePhantasms");

            migrationBuilder.DropTable(
                name: "TIAdministradorDeCombateAmbiente");

            migrationBuilder.DropTable(
                name: "TIAlianzaContrato");

            migrationBuilder.DropTable(
                name: "TIArmasDistanciaEfecto");

            migrationBuilder.DropTable(
                name: "TIArmasDistanciaTiradaDeDaño");

            migrationBuilder.DropTable(
                name: "TIArmasDistanciaTiradaVariable");

            migrationBuilder.DropTable(
                name: "TIEfectoSiendoAplicadoEfecto");

            migrationBuilder.DropTable(
                name: "TIEfectoSiendoAplicadoFuncion");

            migrationBuilder.DropTable(
                name: "TIEfectoSiendoAplicadoPersonajeInstigador");

            migrationBuilder.DropTable(
                name: "TIEfectoSiendoAplicadoPersonajeObjetivo");

            migrationBuilder.DropTable(
                name: "TIEspecialidadPersonaje");

            migrationBuilder.DropTable(
                name: "TIFuncionEfecto");

            migrationBuilder.DropTable(
                name: "TIFuncionHabilidad");

            migrationBuilder.DropTable(
                name: "TIFuncionPadreFuncion");

            migrationBuilder.DropTable(
                name: "TIHabilidadEfecto");

            migrationBuilder.DropTable(
                name: "TIHabilidadInvocacion");

            migrationBuilder.DropTable(
                name: "TIHabilidadItem");

            migrationBuilder.DropTable(
                name: "TIHabilidadTiradaBase");

            migrationBuilder.DropTable(
                name: "TIInvocacionDatosInvocacion");

            migrationBuilder.DropTable(
                name: "TIInvocacionEfecto");

            migrationBuilder.DropTable(
                name: "TIInvocacionPersonaje");

            migrationBuilder.DropTable(
                name: "TIMapaAmbiente");

            migrationBuilder.DropTable(
                name: "TIModificadorDeStatBaseTiradaBase");

            migrationBuilder.DropTable(
                name: "TIOfensivoEfecto");

            migrationBuilder.DropTable(
                name: "TIOfensivoTiradaDeDaño");

            migrationBuilder.DropTable(
                name: "TIPersonajeAlianza");

            migrationBuilder.DropTable(
                name: "TIPersonajeJugableCaracteristicas");

            migrationBuilder.DropTable(
                name: "TIPersonajeJugableInvocacion");

            migrationBuilder.DropTable(
                name: "TIPortableModificadorDeStatBase");

            migrationBuilder.DropTable(
                name: "TIPortableSlots");

            migrationBuilder.DropTable(
                name: "TIRolAmbiente");

            migrationBuilder.DropTable(
                name: "TISlotItem");

            migrationBuilder.DropTable(
                name: "TITiradaFuncion");

            migrationBuilder.DropTable(
                name: "TITiradaHabilidad");

            migrationBuilder.DropTable(
                name: "TITiradaPersonaje");

            migrationBuilder.DropTable(
                name: "TITiradaUtilizable");

            migrationBuilder.DropTable(
                name: "TIUtilizableEfecto");

            migrationBuilder.DropTable(
                name: "TIUtilizableModificadorDeStatBase");

            migrationBuilder.DropTable(
                name: "TIUtilizableTiradaBase");

            migrationBuilder.DropTable(
                name: "TIVariableFuncion");

            migrationBuilder.DropTable(
                name: "TIVariableHabilidad");

            migrationBuilder.DropTable(
                name: "TIVariablePersonaje");

            migrationBuilder.DropTable(
                name: "TIVariableUtilizable");

            migrationBuilder.DropTable(
                name: "UnidadesMapaVectores2");

            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropTable(
                name: "ModeloParticipante");

            migrationBuilder.DropTable(
                name: "Combates");

            migrationBuilder.DropTable(
                name: "ModeloContrato");

            migrationBuilder.DropTable(
                name: "ModeloEfectoSiendoAplicado");

            migrationBuilder.DropTable(
                name: "ModeloEspecialidad");

            migrationBuilder.DropTable(
                name: "ModeloDatosInvocacionBase");

            migrationBuilder.DropTable(
                name: "ModeloMapa");

            migrationBuilder.DropTable(
                name: "ModeloAlianza");

            migrationBuilder.DropTable(
                name: "ModeloCaracteristicas");

            migrationBuilder.DropTable(
                name: "ModeloAmbiente");

            migrationBuilder.DropTable(
                name: "ModeloRol");

            migrationBuilder.DropTable(
                name: "ModeloSlot");

            migrationBuilder.DropTable(
                name: "ModeloEfecto");

            migrationBuilder.DropTable(
                name: "ModeloModificadorDeStatBase");

            migrationBuilder.DropTable(
                name: "Tirada");

            migrationBuilder.DropTable(
                name: "ModeloFuncion");

            migrationBuilder.DropTable(
                name: "ModeloHabilidad");

            migrationBuilder.DropTable(
                name: "ModeloPersonaje");

            migrationBuilder.DropTable(
                name: "ModeloUtilizable");

            migrationBuilder.DropTable(
                name: "ModeloVariable");

            migrationBuilder.DropTable(
                name: "UnidadesMapa");

            migrationBuilder.DropTable(
                name: "Vectores2");
        }
    }
}
