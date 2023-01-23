public class Tarjeta 
{
    public int id { get; set; }
    public string informacion { get; set; }
    public string titulo { get; set; }
    public int sistema { get; set; }
    public baseDatos baseDatos { get; set; }

    public Tarjeta ()
    {
        informacion = string.Empty;
        titulo = string.Empty;
        sistema = 0;
        baseDatos = baseDatos.Facope;
    }
}


public enum baseDatos : ushort
{
    Facope = 1 ,
    Core = 2
}