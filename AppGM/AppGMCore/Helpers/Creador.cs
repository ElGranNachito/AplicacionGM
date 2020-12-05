namespace AppGM.Core
{
    public static class Creador
    {
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