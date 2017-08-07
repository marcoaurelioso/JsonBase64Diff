using JsonBase64Diff.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonBase64Diff.Infrastructure.Context
{
    public class JsonBase64Context: DbContext
    {
        public JsonBase64Context(DbContextOptions<JsonBase64Context> options) : base(options)
        {
            var builder = new DbContextOptionsBuilder<JsonBase64Context>();
            builder.UseInMemoryDatabase();
            options = builder.Options;
        }

        public DbSet<JsonBase64Item> JsonBase64Items { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<JsonBase64Item>().HasKey(table => new { table.Id, table.Position });
        }
    }
}
