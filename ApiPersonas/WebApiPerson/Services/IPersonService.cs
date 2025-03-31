
using WebApiPerson.Models;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task<Person> CreateAsync(Person person);
    Task<bool> UpdateAsync(int id, Person person);
    Task<bool> DeleteAsync(int id);
}

