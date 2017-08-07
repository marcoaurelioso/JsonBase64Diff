using JsonBase64Diff.Application.Service.Dto;
using JsonBase64Diff.Application.Service.Interfaces;
using JsonBase64Diff.Domain.Entities;
using JsonBase64Diff.Domain.Enums;
using JsonBase64Diff.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsonBase64Diff.Application.Service
{
    public class JsonBase64DiffService : IJsonBase64DiffService
    {
        private readonly IJsonBase64Repository _repository;

        public JsonBase64DiffService(IJsonBase64Repository jsonBase64Repository)
        {
            _repository = jsonBase64Repository;
        }

        public async Task<JsonDiffDto> GetComparison(string id)
        {
            JsonBase64ItemDto itemLeft = await Select(id, EJsonBase64Position.Left);
            JsonBase64ItemDto itemRight = await Select(id, EJsonBase64Position.Right);

            JsonDiffDto result = new JsonDiffDto();
            result.Id = id;

            if (itemLeft == null || itemRight == null)
            {
                result.Message = "Data not found. Send both sides again.";
                return result;
            }

            if (itemLeft.Data.Length != itemRight.Data.Length)
            {
                result.Message = "The data is not the same size";
                return result;
            }

            result.Message = "The data is the same";

            byte[] leftArray = Convert.FromBase64String(itemLeft.Data);
            byte[] right = Convert.FromBase64String(itemRight.Data);
            List<int> offsetList = new List<int>();

            for (int i = 0; i < leftArray.Length; i++)
            {
                if (leftArray[i] != right[i])
                {
                    offsetList.Add(i);
                }
            }

            result.Length = offsetList.Count;

            return result;
        }

        public async Task<bool> Save(JsonBase64ItemDto entity)
        {
            string position = entity.Position == EJsonBase64Position.Left ? "L" : "R";
            JsonBase64Item item = new JsonBase64Item() { Id = entity.Id, Data = entity.Data, Position = position };

            try
            {
                if (await _repository.AddOrUpdate(item))
                    _repository.Save();

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<JsonBase64ItemDto> Select(string id, EJsonBase64Position position)
        {
            string _position = position == EJsonBase64Position.Left ? "L" : "R";
            return ConvertToDto(await _repository.Select(id, _position));
        }

        private JsonBase64ItemDto ConvertToDto(JsonBase64Item item)
        {
            if (item == null)
                return null;
            else
            {
                JsonBase64ItemDto itemDto = new JsonBase64ItemDto() {
                    Id = item.Id,
                    Data = item.Data,
                    Position = (item.Position == "L" ? EJsonBase64Position.Left : EJsonBase64Position.Right)
                };

                return itemDto;
            }
        }

        private JsonBase64Item ConvertFromDto(JsonBase64ItemDto itemDto)
        {
            if (itemDto == null)
                return null;
            else
            {
                JsonBase64Item item = new JsonBase64Item()
                {
                    Id = itemDto.Id,
                    Data = itemDto.Data, 
                    Position = (itemDto.Position == EJsonBase64Position.Left ? "L" : "R")
                };

                return item;
            }
        }
    }
}
