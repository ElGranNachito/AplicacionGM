namespace AppGMCore
{
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

    public enum EAlineamiento
    {
        NINGUNO = 0, 
        LawfulGood = 1,
        LawfulNeutral = 2,
        LawfulEvil = 4,
        NeutralGood = 8,
        Neutral = 16,
        NeutralEvil = 32,
        ChaoticGood = 64,
        ChaoticNeutral = 128,
        ChaoticEvil = 256
    }

    public enum EManoDominante
    {
        NINGUNA = 0, Izquierda = 1, Derecha = 2, Ambidiestro = 4
    }

    public enum ESexo
    {
        NINGUNO = 0, Femenino = 1, Masculino = 2
    }

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
        F = 0,
        E = 1,
        D = 2,
        C = 4,
        B = 8,
        A = 16,
        AMas = 32,
        AMasMas = 64,
        Ex = 128
    }

    public enum ETipoDeHabilidad
    {
        NINGUNA = 0, Personal = 1, Soporte = 2, Defensa = 4, Ataque = 8
    }

    public enum ETipoHabilidad
    {
        NINGUNO = 0, Perk = 1, Skill = 2, Magia = 4, NoblePhantasm = 8
    }

    public enum EEstado
    {
        NINGUNO = 0,
        MuyBueno = 1,
        Bueno = 2,
        Utilizable = 4,
        Malo = 8,
        Irreparable = 16,
    }

    public enum ETipoValorTemporal
    {
        NINGUNO = 0,
        Usos = 1,
        Cargas = 2,
        Dias = 4,
        Turnos = 8
    }

    public enum ETipoClaseConElValorTemporal
    {
        NINGUNO = 0,
        Utilizable = 1,
        Habilidad = 2,
        Efecto = 4
    }

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
}
