using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AssetTracking
{
    public class Provider
    {
        public  AppDbContext _dbContext;

        public Provider()
        {
            _dbContext = new AppDbContext();
        }

        public IEnumerable<AssetTracking.Office> GetOffices()
        {
            return _dbContext.Offices.Select(o => new Office());
        }
        public void AddAsset(Asset newAsset)
        {
             _dbContext.Assets.Add(new DataAccess.Asset { Model   = newAsset.ModelName  } );
            _dbContext.SaveChanges();
        }

        public void DeleteAsset(int assetId)
        {
            var assetToDelete = _dbContext.Assets.Where(a => a.Id == assetId).FirstOrDefault();
            if(assetToDelete != null)
            {
                _dbContext.Assets.Remove(assetToDelete);
                _dbContext.SaveChanges();
            }
          
        }





    }
}
