using WebApiPerson.Models;

public interface IPedidoService
{
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task<Pedido?> GetByIdAsync(int id);
    Task<Pedido> CreateAsync(Pedido pedido);
    Task<bool> UpdateAsync(int id, Pedido pedido);
    Task<bool> DeleteAsync(int id);
}
