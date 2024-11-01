using System.Data;
namespace CRUDOperations.Models
{
    public interface IDbConnectionProvider
    {
        IDbConnection CreateConnection();
    }
}
