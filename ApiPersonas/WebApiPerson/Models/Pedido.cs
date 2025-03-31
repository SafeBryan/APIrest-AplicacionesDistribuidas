namespace WebApiPerson.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public required string Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
