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
                    Nombre = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mapas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreMapa = table.Column<string>(type: "TEXT", nullable: true),
                    EFormatoImagen = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloAlianza",
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
                    table.PrimaryKey("PK_ModeloAlianza", x => x.Id);
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
                name: "ModeloCargasHabilidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CargasMaximas = table.Column<int>(type: "INTEGER", nullable: false),
                    CargasActuales = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloCargasHabilidad", x => x.Id);
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
                    TurnosRestantes = table.Column<ushort>(type: "INTEGER", nullable: false),
                    EstaSiendoAplicado = table.Column<bool>(type: "INTEGER", nullable: false),
                    TurnosDeDuracion = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEfecto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloHabilidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostoDeOdOPrana = table.Column<ushort>(type: "INTEGER", nullable: false),
                    CostoDeMana = table.Column<ushort>(type: "INTEGER", nullable: false),
                    TurnosDeDuracion = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    TipoDeHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Rango = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Nivel = table.Column<byte>(type: "INTEGER", nullable: true),
                    EsParticular = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloHabilidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloLimitador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LimiteDeUsos = table.Column<int>(type: "INTEGER", nullable: false),
                    UsosRestantes = table.Column<int>(type: "INTEGER", nullable: false),
                    DiasDeEnfriamiento = table.Column<int>(type: "INTEGER", nullable: false),
                    DiasRestantes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloLimitador", x => x.Id);
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
                name: "ModeloTiradaBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Dados = table.Column<ushort>(type: "INTEGER", nullable: true),
                    Caras = table.Column<ushort>(type: "INTEGER", nullable: true),
                    TipoDeDaño = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloTiradaBase", x => x.Id);
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
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TiradaIniciativa = table.Column<int>(type: "INTEGER", nullable: false),
                    EsSuTurno = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hour = table.Column<int>(type: "INTEGER", nullable: false),
                    Dia = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Registros = table.Column<string>(type: "TEXT", nullable: true),
                    FechaUltimaSesion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.Id);
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
                        name: "FK_CombateMapas_Mapas_IdMapa",
                        column: x => x.IdMapa,
                        principalTable: "Mapas",
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
                name: "TIHabilidadCargasHabilidad",
                columns: table => new
                {
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCargasHabilidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIHabilidadCargasHabilidad", x => new { x.IdHabilidad, x.IdCargasHabilidad });
                    table.ForeignKey(
                        name: "FK_TIHabilidadCargasHabilidad_ModeloCargasHabilidad_IdCargasHabilidad",
                        column: x => x.IdCargasHabilidad,
                        principalTable: "ModeloCargasHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIHabilidadCargasHabilidad_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
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
                name: "TIHabilidadLimitador",
                columns: table => new
                {
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    IdLimitador = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIHabilidadLimitador", x => new { x.IdHabilidad, x.IdLimitador });
                    table.ForeignKey(
                        name: "FK_TIHabilidadLimitador_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIHabilidadLimitador_ModeloLimitador_IdLimitador",
                        column: x => x.IdLimitador,
                        principalTable: "ModeloLimitador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIEfectoModificadorDeStatBase",
                columns: table => new
                {
                    IdEfecto = table.Column<int>(type: "INTEGER", nullable: false),
                    IdModificadorDeStat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIEfectoModificadorDeStatBase", x => new { x.IdEfecto, x.IdModificadorDeStat });
                    table.ForeignKey(
                        name: "FK_TIEfectoModificadorDeStatBase_ModeloEfecto_IdEfecto",
                        column: x => x.IdEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIEfectoModificadorDeStatBase_ModeloModificadorDeStatBase_IdModificadorDeStat",
                        column: x => x.IdModificadorDeStat,
                        principalTable: "ModeloModificadorDeStatBase",
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
                        name: "FK_TIHabilidadTiradaBase_ModeloTiradaBase_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "ModeloTiradaBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIHabilidadTiradaDeDaño",
                columns: table => new
                {
                    IdHabilidad = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTirada = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIHabilidadTiradaDeDaño", x => new { x.IdHabilidad, x.IdTirada });
                    table.ForeignKey(
                        name: "FK_TIHabilidadTiradaDeDaño_ModeloHabilidad_IdHabilidad",
                        column: x => x.IdHabilidad,
                        principalTable: "ModeloHabilidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIHabilidadTiradaDeDaño_ModeloTiradaBase_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "ModeloTiradaBase",
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
                        name: "FK_TIModificadorDeStatBaseTiradaBase_ModeloTiradaBase_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "ModeloTiradaBase",
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
                        name: "FK_TIArmasDistanciaTiradaDeDaño_ModeloTiradaBase_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "ModeloTiradaBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIArmasDistanciaTiradaDeDaño_ModeloUtilizable_IdArmasDistancia",
                        column: x => x.IdArmasDistancia,
                        principalTable: "ModeloUtilizable",
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
                        name: "FK_TIArmasDistanciaTiradaVariable_ModeloTiradaBase_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "ModeloTiradaBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIArmasDistanciaTiradaVariable_ModeloUtilizable_IdArmasDistancia",
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
                        name: "FK_TIOfensivoTiradaDeDaño_ModeloTiradaBase_IdTiradaDeDaño",
                        column: x => x.IdTiradaDeDaño,
                        principalTable: "ModeloTiradaBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIOfensivoTiradaDeDaño_ModeloUtilizable_IdOfensivo",
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
                        name: "FK_TIUtilizableTiradaBase_ModeloTiradaBase_IdTirada",
                        column: x => x.IdTirada,
                        principalTable: "ModeloTiradaBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIUtilizableTiradaBase_ModeloUtilizable_IdUtilizable",
                        column: x => x.IdUtilizable,
                        principalTable: "ModeloUtilizable",
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
                        name: "FK_CombateParticipantes_Participantes_IdParticipante",
                        column: x => x.IdParticipante,
                        principalTable: "Participantes",
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
                        name: "FK_ParticipanteAccion_Participantes_IdParticipante",
                        column: x => x.IdParticipante,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatesRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdCombate = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloAdministradorDeCombateId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloRolId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloRolId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatesRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatesRol_Combates_IdCombate",
                        column: x => x.IdCombate,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatesRol_Combates_ModeloAdministradorDeCombateId",
                        column: x => x.ModeloAdministradorDeCombateId,
                        principalTable: "Combates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatesRol_Rols_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatesRol_Rols_ModeloRolId1",
                        column: x => x.ModeloRolId1,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapasRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdMapa = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloMapaId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloRolId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloRolId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapasRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapasRol_Mapas_IdMapa",
                        column: x => x.IdMapa,
                        principalTable: "Mapas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapasRol_Mapas_ModeloMapaId",
                        column: x => x.ModeloMapaId,
                        principalTable: "Mapas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MapasRol_Rols_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapasRol_Rols_ModeloRolId1",
                        column: x => x.ModeloRolId1,
                        principalTable: "Rols",
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
                        name: "FK_MapasUnidadesMapa_Mapas_IdMapa",
                        column: x => x.IdMapa,
                        principalTable: "Mapas",
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
                    MaxHp = table.Column<int>(type: "INTEGER", nullable: false),
                    Hp = table.Column<int>(type: "INTEGER", nullable: false),
                    Str = table.Column<ushort>(type: "INTEGER", nullable: false),
                    End = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Agi = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Int = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Lck = table.Column<ushort>(type: "INTEGER", nullable: false),
                    VentajaStr = table.Column<ushort>(type: "INTEGER", nullable: false),
                    VentajaEnd = table.Column<ushort>(type: "INTEGER", nullable: false),
                    VentajaAgi = table.Column<ushort>(type: "INTEGER", nullable: false),
                    VentajaInt = table.Column<ushort>(type: "INTEGER", nullable: false),
                    VentajaLck = table.Column<ushort>(type: "INTEGER", nullable: false),
                    PesoMaximoCargable = table.Column<decimal>(type: "TEXT", nullable: false),
                    PesoCargado = table.Column<decimal>(type: "TEXT", nullable: false),
                    PathImagen = table.Column<string>(type: "TEXT", nullable: true),
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
                        name: "FK_ParticipantePersonaje_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantePersonaje_Participantes_IdParticipante",
                        column: x => x.IdParticipante,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonajeAlianzas",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAlianza = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeAlianzas", x => new { x.IdPersonaje, x.IdAlianza });
                    table.ForeignKey(
                        name: "FK_PersonajeAlianzas_ModeloAlianza_IdAlianza",
                        column: x => x.IdAlianza,
                        principalTable: "ModeloAlianza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeAlianzas_ModeloPersonaje_IdPersonaje",
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
                name: "PersonajeEfectos",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    IdEfecto = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajeEfectos", x => new { x.IdPersonaje, x.IdEfecto });
                    table.ForeignKey(
                        name: "FK_PersonajeEfectos_ModeloEfecto_IdEfecto",
                        column: x => x.IdEfecto,
                        principalTable: "ModeloEfecto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajeEfectos_ModeloPersonaje_IdPersonaje",
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPersonaje = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloPersonajeId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModeloRolId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloRolId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    IdRol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajesRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonajesRol_ModeloPersonaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajesRol_ModeloPersonaje_ModeloPersonajeId",
                        column: x => x.ModeloPersonajeId,
                        principalTable: "ModeloPersonaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonajesRol_Rols_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajesRol_Rols_ModeloRolId1",
                        column: x => x.ModeloRolId1,
                        principalTable: "Rols",
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
                name: "IX_CombatesRol_IdRol",
                table: "CombatesRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_CombatesRol_ModeloAdministradorDeCombateId",
                table: "CombatesRol",
                column: "ModeloAdministradorDeCombateId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatesRol_ModeloRolId1",
                table: "CombatesRol",
                column: "ModeloRolId1");

            migrationBuilder.CreateIndex(
                name: "IX_MapasRol_IdMapa",
                table: "MapasRol",
                column: "IdMapa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MapasRol_IdRol",
                table: "MapasRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_MapasRol_ModeloMapaId",
                table: "MapasRol",
                column: "ModeloMapaId");

            migrationBuilder.CreateIndex(
                name: "IX_MapasRol_ModeloRolId1",
                table: "MapasRol",
                column: "ModeloRolId1");

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
                name: "IX_PersonajeAlianzas_IdAlianza",
                table: "PersonajeAlianzas",
                column: "IdAlianza");

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
                name: "IX_PersonajeEfectos_IdEfecto",
                table: "PersonajeEfectos",
                column: "IdEfecto");

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
                column: "IdHabilidad");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajesRol_IdPersonaje",
                table: "PersonajesRol",
                column: "IdPersonaje",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonajesRol_IdRol",
                table: "PersonajesRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajesRol_ModeloPersonajeId",
                table: "PersonajesRol",
                column: "ModeloPersonajeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajesRol_ModeloRolId1",
                table: "PersonajesRol",
                column: "ModeloRolId1");

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
                name: "IX_TIEfectoModificadorDeStatBase_IdModificadorDeStat",
                table: "TIEfectoModificadorDeStatBase",
                column: "IdModificadorDeStat");

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadCargasHabilidad_IdCargasHabilidad",
                table: "TIHabilidadCargasHabilidad",
                column: "IdCargasHabilidad");

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadCargasHabilidad_IdHabilidad",
                table: "TIHabilidadCargasHabilidad",
                column: "IdHabilidad",
                unique: true);

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
                name: "IX_TIHabilidadLimitador_IdHabilidad",
                table: "TIHabilidadLimitador",
                column: "IdHabilidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadLimitador_IdLimitador",
                table: "TIHabilidadLimitador",
                column: "IdLimitador");

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadTiradaBase_IdTirada",
                table: "TIHabilidadTiradaBase",
                column: "IdTirada");

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadTiradaDeDaño_IdHabilidad",
                table: "TIHabilidadTiradaDeDaño",
                column: "IdHabilidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIHabilidadTiradaDeDaño_IdTirada",
                table: "TIHabilidadTiradaDeDaño",
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
                name: "IX_TISlotItem_IdItem",
                table: "TISlotItem",
                column: "IdItem");

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
                name: "PersonajeAlianzas");

            migrationBuilder.DropTable(
                name: "PersonajeArmasDistancias");

            migrationBuilder.DropTable(
                name: "PersonajeContratos");

            migrationBuilder.DropTable(
                name: "PersonajeDefensivos");

            migrationBuilder.DropTable(
                name: "PersonajeEfectos");

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
                name: "TIAlianzaContrato");

            migrationBuilder.DropTable(
                name: "TIArmasDistanciaEfecto");

            migrationBuilder.DropTable(
                name: "TIArmasDistanciaTiradaDeDaño");

            migrationBuilder.DropTable(
                name: "TIArmasDistanciaTiradaVariable");

            migrationBuilder.DropTable(
                name: "TIEfectoModificadorDeStatBase");

            migrationBuilder.DropTable(
                name: "TIHabilidadCargasHabilidad");

            migrationBuilder.DropTable(
                name: "TIHabilidadEfecto");

            migrationBuilder.DropTable(
                name: "TIHabilidadInvocacion");

            migrationBuilder.DropTable(
                name: "TIHabilidadItem");

            migrationBuilder.DropTable(
                name: "TIHabilidadLimitador");

            migrationBuilder.DropTable(
                name: "TIHabilidadTiradaBase");

            migrationBuilder.DropTable(
                name: "TIHabilidadTiradaDeDaño");

            migrationBuilder.DropTable(
                name: "TIInvocacionDatosInvocacion");

            migrationBuilder.DropTable(
                name: "TIInvocacionEfecto");

            migrationBuilder.DropTable(
                name: "TIInvocacionPersonaje");

            migrationBuilder.DropTable(
                name: "TIModificadorDeStatBaseTiradaBase");

            migrationBuilder.DropTable(
                name: "TIOfensivoEfecto");

            migrationBuilder.DropTable(
                name: "TIOfensivoTiradaDeDaño");

            migrationBuilder.DropTable(
                name: "TIPersonajeJugableCaracteristicas");

            migrationBuilder.DropTable(
                name: "TIPersonajeJugableInvocacion");

            migrationBuilder.DropTable(
                name: "TIPortableModificadorDeStatBase");

            migrationBuilder.DropTable(
                name: "TIPortableSlots");

            migrationBuilder.DropTable(
                name: "TISlotItem");

            migrationBuilder.DropTable(
                name: "TIUtilizableEfecto");

            migrationBuilder.DropTable(
                name: "TIUtilizableModificadorDeStatBase");

            migrationBuilder.DropTable(
                name: "TIUtilizableTiradaBase");

            migrationBuilder.DropTable(
                name: "UnidadesMapaVectores2");

            migrationBuilder.DropTable(
                name: "Combates");

            migrationBuilder.DropTable(
                name: "Mapas");

            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Rols");

            migrationBuilder.DropTable(
                name: "ModeloAlianza");

            migrationBuilder.DropTable(
                name: "ModeloContrato");

            migrationBuilder.DropTable(
                name: "ModeloCargasHabilidad");

            migrationBuilder.DropTable(
                name: "ModeloLimitador");

            migrationBuilder.DropTable(
                name: "ModeloHabilidad");

            migrationBuilder.DropTable(
                name: "ModeloDatosInvocacionBase");

            migrationBuilder.DropTable(
                name: "ModeloCaracteristicas");

            migrationBuilder.DropTable(
                name: "ModeloPersonaje");

            migrationBuilder.DropTable(
                name: "ModeloSlot");

            migrationBuilder.DropTable(
                name: "ModeloEfecto");

            migrationBuilder.DropTable(
                name: "ModeloModificadorDeStatBase");

            migrationBuilder.DropTable(
                name: "ModeloTiradaBase");

            migrationBuilder.DropTable(
                name: "ModeloUtilizable");

            migrationBuilder.DropTable(
                name: "UnidadesMapa");

            migrationBuilder.DropTable(
                name: "Vectores2");
        }
    }
}
