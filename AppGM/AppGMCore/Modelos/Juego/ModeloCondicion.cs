namespace AppGM.Core
{
    /// <summary>
    /// Clase que representa una condicion.
    /// En si este modelo no contiene nada sobre la condicion en si, solo nos permite acceder a su
    /// archivo XML a traves del <see cref="Nombre"/> y <see cref="ModeloBase.Id"/>
    /// </summary>
    public class ModeloCondicion : ModeloBase
    {
        public ControladorCondicion controladorCondicion;

        public string Nombre;
    }
}