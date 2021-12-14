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
    public class RequestMap : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable(@"requests", @"public");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Content).HasColumnName("content").IsRequired().HasMaxLength(4000);
            builder.Property(x => x.Created).HasColumnName("created").IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsActive).HasColumnName("is_active").IsRequired().HasDefaultValue(true);

            builder.HasOne(req => req.Response).WithOne(res => res.Request).HasForeignKey<Response>(res => res.RequestId);
        }
    }
}
