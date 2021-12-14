using Entities.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Mappings
{
    public class ResponseMap : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.ToTable(@"responses", @"public");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Content).HasColumnName("content").IsRequired().HasMaxLength(4000); 
            builder.Property(x => x.RequestId).HasColumnName("request_id");
            builder.Property(x => x.Created).HasColumnName("created").IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsActive).HasColumnName("is_active").IsRequired().HasDefaultValue(true);
        }
    }
}
