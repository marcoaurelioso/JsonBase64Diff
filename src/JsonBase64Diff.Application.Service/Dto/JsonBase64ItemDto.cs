using JsonBase64Diff.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace JsonBase64Diff.Application.Service.Dto
{
    public class JsonBase64ItemDto
    {
        [Required]
        public string Id { get; set; }

        public EJsonBase64Position Position { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
