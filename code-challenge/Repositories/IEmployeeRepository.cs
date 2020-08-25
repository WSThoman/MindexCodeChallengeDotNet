using System.Threading.Tasks;

using challenge.Models;

namespace challenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(string id);
 
        Employee Add(Employee employee);
        
        Employee Remove(Employee employee);
        
        Task SaveAsync();
    }
}