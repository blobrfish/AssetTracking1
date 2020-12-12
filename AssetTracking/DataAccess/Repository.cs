using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AssetTracking.DataAccess
{
    public class Repository
    {
        private  AppDbContext DBContext;

        public Repository()
        {
            DBContext = new AppDbContext();
        }

        public IEnumerable<AssetTracking.Office> GetOffices()
        {
            return DBContext.Offices.Select(o => new AssetTracking.Office());
        }

        public IEnumerable<AssetTracking.Office> AllCompanyAssets()
        {
            return DBContext.Offices.Select(o => new AssetTracking.Office( o.Location, 
                o.Assets.Select(a => new AssetTracking.Asset(  a.Model,a.PurchaseDate, a.PriceInUsd))
                ,o.LocalCurrencyCode));
        }

        public void AddAsset(Office office, AssetTracking.Asset newAsset)
        {
            var existingOffice  = DBContext.Offices.Where(o => o.Id == office.Id).FirstOrDefault();
            existingOffice.Assets.Add(new DataAccess.Asset { Model = newAsset.ModelName,  PriceInUsd = newAsset.PriceInUsd});
            DBContext.SaveChanges();
        }

        public void AddOffice(Office office)
        {
             DBContext.Offices.Add(new DataAccess.Office { Location = office.Location, LocalCurrencyCode = office.LocalCurrencyCode });
             DBContext.SaveChanges();
        }

        public void DeleteAsset(Asset asset)
        {
            var assetToDelete = DBContext.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();
            if(assetToDelete != null)
            {
                DBContext.Assets.Remove(assetToDelete);
                DBContext.SaveChanges();
            }
        }

        public void DeleteOffice(Office office)
        {
            var assetToDelete = DBContext.Offices.Where(a => a.Id == office.Id).FirstOrDefault();
            if (assetToDelete != null)
            {
                DBContext.Offices.Remove(office);
                DBContext.SaveChanges();
            }
        }
    }
}
