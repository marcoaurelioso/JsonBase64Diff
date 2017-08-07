using JsonBase64Diff.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using JsonBase64Diff.Domain.Entities;
using JsonBase64Diff.Domain.Enums;
using JsonBase64Diff.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JsonBase64Diff.Infrastructure
{
    public class JsonBase64Repository : IJsonBase64Repository
    {
        private readonly JsonBase64Context _context;

        public JsonBase64Repository(JsonBase64Context jsonBase64Context)
        {
            _context = jsonBase64Context;
        }

        public async Task<bool> AddOrUpdate(JsonBase64Item entity)
        {
            try
            {
                JsonBase64Item foundItem = await Select(entity.Id, entity.Position);

                if (foundItem == null)
                {
                    _context.Add(entity);
                }
                else
                {
                    foundItem.Data = entity.Data;
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<JsonBase64Item> Select(string id, string position)
        {
            return await _context.JsonBase64Items.
                                  Where(n => n.Id == id && n.Position == position).
                                  FirstOrDefaultAsync();
        }
    }
}
