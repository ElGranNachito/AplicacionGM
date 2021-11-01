namespace AppGM.Core
{
    /// <summary>
    /// Indica cual es el clima general en el rol.
    /// </summary>
    public enum EClima
    {
        Soleado = 0,
        Nublado = 1,
        Tormenta = 2,
        Nieve = 3,
        Neblina = 4,
        Lluvia = 5,
        Granizo = 6,
    }

    /// <summary>
    /// Indica el tipo de viento general en el rol.
    /// </summary>
    public enum EViento
    {
        Brisa = 0,
        Rafagas = 1,
        Viento = 2
    }

    /// <summary>
    /// Indica cual es el estado de temperatura general en el rol.
    /// </summary>
    public enum ETemperatura
    {
        Calor = 0,
        Frio = 1,
        Templado = 2,
    }

    /// <summary>
    /// Indica cual es el estado de humedad general en el rol.
    /// </summary>
    public enum EHumedad
    {
        Humedad = 0,
        MuchaHumedad = 1,
        Seco = 2,
    }
}
