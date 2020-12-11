using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AssetTracking.DataAccess
{
    public class Asset
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PriceInUsd { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }

    public class AssetConfig : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(p => p.Id);
           
        }
    }
}
