namespace BodegaMovil.WebAPI
{
    public class CadenaConexion
    {
        string _cnn;
        public CadenaConexion(string cnn)
        {
            _cnn = cnn;
        }
        public CadenaConexion()
        {
            
        }
        public string Cadena { get; set; }
    }
}
