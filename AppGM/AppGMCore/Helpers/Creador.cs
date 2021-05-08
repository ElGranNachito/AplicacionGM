namespace AppGM.Core
{
    /// <summary>
    /// Clase encargada de crear <see cref="ModeloPersonaje"/>, <see cref="ModeloHabilidad"/>
    /// </summary>
    public static class Creador
    {
        //TODO: Expandir
        public static ModeloPersonaje CrearPersonaje(ETipoPersonaje tipo)
        {
            switch (tipo)
            {
                case ETipoPersonaje.Master:
                    ModeloMaster nuevoMaster = new ModeloMaster
                    {
                        TipoPersonaje = ETipoPersonaje.Master
                    };

                    return nuevoMaster;

                case ETipoPersonaje.Servant:
                    ModeloServant nuevoServant = new ModeloServant
                    {
                        TipoPersonaje = ETipoPersonaje.Servant
                    };

                    return nuevoServant;

                case ETipoPersonaje.Invocacion:
                    ModeloInvocacion nuevaInvocacion = new ModeloInvocacion
                    {
                        TipoPersonaje = ETipoPersonaje.Invocacion
                    };

                    return  nuevaInvocacion;

                case ETipoPersonaje.NPC:
                    ModeloPersonaje nuevoPersonaje = new ModeloPersonaje
                    {
                        TipoPersonaje = ETipoPersonaje.NPC
                    };

                    return nuevoPersonaje;

                default:
                    return null;

            }
        }
    }
}