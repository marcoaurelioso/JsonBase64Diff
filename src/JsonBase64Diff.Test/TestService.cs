using JsonBase64Diff.Application.Service;
using JsonBase64Diff.Application.Service.Dto;
using JsonBase64Diff.Application.Service.Interfaces;
using JsonBase64Diff.Domain.Entities;
using JsonBase64Diff.Domain.Interfaces;
using JsonBase64Diff.Infrastructure;
using JsonBase64Diff.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JsonBase64Diff.Test
{
    public class TestService
    {
        public TestService()
        {
        }

        [Fact(DisplayName = "Check if return the same as expected")]
        public void ShouldBeTheSame()
        {
            JsonBase64Context context = GetContextWithData();
            IJsonBase64Repository _repository = new JsonBase64Repository(context);
            IJsonBase64DiffService _service = new JsonBase64DiffService(_repository);

            Task<JsonDiffDto> returnDTO = _service.GetComparison("1");

            Assert.True(returnDTO.Result.Message == "The data is the same");
        }

        private JsonBase64Context GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<JsonBase64Context>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new JsonBase64Context(options);

            var jsonBase64ItemLEFT = new JsonBase64Item { Id = "1", Position = "L", Data = "YXNkZmFzZGZhc2RmYXNkZmFzZGY=" };
            var jsonBase64ItemRIGHT = new JsonBase64Item { Id = "1", Position = "R", Data = "YXNkZmFzZGZhc2RmYXNkZmFzZGY=" };

            context.JsonBase64Items.Add(jsonBase64ItemLEFT);
            context.JsonBase64Items.Add(jsonBase64ItemRIGHT);
            
            context.SaveChanges();

            return context;
        }
    }
}
