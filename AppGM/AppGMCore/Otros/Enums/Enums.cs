using System;

namespace AppGM.Core
{
    public enum EPagina
    {
        PaginaPrincipal = 1,
        PaginaPrincipalRol = 2
    }

    public enum EMenuRol
    {
        NINGUNO = 0,
        SeleccionTipoFichas = 1,
        VistaFichas = 2,
        Mapas = 3,
        Registro = 4,
        Tirada = 5,
        AdministrarCombates = 6,
        Combate = 7
    }

    [Flags]
    public enum ETipoPersonaje
    {
        Master = 1,
        Servant = 2,
        Invocacion = 4,
        NPC = 8
    }

    [Flags]
    public enum EClaseServant
    {
        NINGUNO = 0,
        Saber = 1, 
        Lancer = 2, 
        Archer = 4, 
        Caster = 8, 
        Rider = 16, 
        Assassin = 32, 
        Berserker = 64, 
        Ruler = 128 
    }

    [Flags]
    public enum EArquetipo
    {
        NINGUNO = 0, 
        Inocente = 1,
        Amigo = 2,
        Heroe = 4,
        Cuidador = 8,
        Explorador = 16,
        Rebelde = 32,
        Amante = 64,
        Creador = 128,
        Bufon = 256,
        Sabio = 512,
        Mago = 1024,
        Gobernante = 2048,
        Sombra = 4096,
        Persona = 8192,
        UnoMismo = 16384,
    }

    [Flags]
    public enum EManoDominante
    {
        NINGUNA = 0, Izquierda = 1, Derecha = 2, Ambidiestro = Izquierda | Derecha
    }

    [Flags]
    public enum ESexo
    {
        NINGUNO = 0, Femenino = 1, Masculino = 2
    }


    [Flags]
    public enum EStat
    {
        NINGUNA = 0,
        HP = 1,
        STR = 2,
        END = 4,
        AGI = 8,
        INT = 16,
        LCK = 32,
        CHR = 64,
        NP = 128
    }

    public enum ERango
    {
        NINGUNO = 0,
        F = 14,
        E = 15,
        D = 16,
        C = 17,
        B = 18,
        A = 19,
        AMas = 20,
        AMasMas = 21,
        Ex = 22
    }

    [Flags]
    public enum EBienestar
    {
        NINGUNO = 0,
        MuyBueno = 1,
        Bueno = 2,
        Neutro = 3,
        Malo = 4,
        MuyMalo = 5,
        Deplorable = 6
    }

    [Flags]
    public enum EUsoDeHabilidad
    {
        NINGUNA = 0, 
        Personal = 1, 
        Soporte = 2, 
        Defensa = 4,
        Ataque = 8
    }

    [Flags]
    public enum ETipoHabilidad
    {
        NINGUNO = 0, 
        Perk = 1, 
        Skill = 2, 
        Magia = 4, 
        NoblePhantasm = 8
    }

    [Flags]
    public enum ETipoValorTemporal
    {
        NINGUNO = 0,
        Usos = 1,
        Cargas = 2,
        Dias = 4,
        Turnos = 8
    }

    [Flags]
    public enum ETipoClaseConElValorTemporal
    {
        NINGUNO = 0,
        Utilizable = 1,
        Habilidad = 2,
        Efecto = 4
    }

    [Flags]
    public enum ETipoDeDaño
    {
        NINGUNO = 0,
        Contundente = 1,
        Cortante = 2,
        Penetrante = 4,
        Explosivo = 8,
        Magico = 16,
        Proyectil = 32
    }

    [Flags]
    public enum EParteDelCuerpo
    {
        NINGUNA = 0,
        Torso = 1,
        Hombros = 2,
        Muslos = 4,
        Espalda = 8,
        Corazón = 16,
        Manos = 32,
        Muñeca = 64,
        Pies = 128,
        Piernas = 256,
        Cuello = 512,
        Entrepierna = 1024,
        Cabeza = 2048,
        Canilla = 4096
    }

    public enum EFormatoImagen
    {
        Png = 1,
        Jpg = 2
    }
}
