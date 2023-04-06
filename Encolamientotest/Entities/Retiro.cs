namespace Encolamientotest.Entities
{
    public class RetiroDTO
    {
      
        public string[] Retiros { get; set; }
        public string Transportista { get; set; }
        public string Auto { get; set; }
        public Sucursal SucursalDestino { get; set; }
        public Sucursal SucursalPlanificacion { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
    }
    public class Retiro: RetiroDTO
    {
        public string Id { get; set; }

        
    }
    public class Sucursal
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
    }
    public class DistanceResult
    {
        public int Distance { get; set; }
        public string RetiroCode { get; set; }
    }
}
