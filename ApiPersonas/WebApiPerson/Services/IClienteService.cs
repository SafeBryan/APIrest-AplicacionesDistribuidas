using WebApiPerson.Models;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<bool> UpdateAsync(int id, Cliente cliente);
    Task<bool> DeleteAsync(int id);
}
