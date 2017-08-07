using JsonBase64Diff.Application.Service.Dto;
using JsonBase64Diff.Domain.Enums;
using System.Threading.Tasks;

namespace JsonBase64Diff.Application.Service.Interfaces
{
    public interface IJsonBase64DiffService
    {
        Task<JsonBase64ItemDto> Select(string id, EJsonBase64Position position);
        Task<bool> Save(JsonBase64ItemDto entity);
        Task<JsonDiffDto> GetComparison(string id);
    }
}
