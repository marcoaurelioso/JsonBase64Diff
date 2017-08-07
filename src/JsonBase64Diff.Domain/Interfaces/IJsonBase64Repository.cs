using JsonBase64Diff.Domain.Entities;
using System.Threading.Tasks;

namespace JsonBase64Diff.Domain.Interfaces
{
    public interface IJsonBase64Repository
    {
        Task<bool> AddOrUpdate(JsonBase64Item entity);

        Task<JsonBase64Item> Select(string id, string position);

        void Save();
    }
}
