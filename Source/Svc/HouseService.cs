using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Source.Db;

namespace WebApplication1.Source.Svc
{
    public class HouseService : IHouseService
    {
        private HouseContext _context;
        private readonly IMemoryCache _memoryCache;

        public HouseService(HouseContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public List<House> getHouses()
        {
            HouseAccess access = new HouseAccess(_context);
            return access.getAllHouses().ToList();
        }

        public List<House> getHousesWithCache()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.Houses, out List<House> datas))
            {
                datas = getHouses(); ; // Get the data from database
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _memoryCache.Set(CacheKeys.Houses, datas, cacheEntryOptions);
            }
            return datas;
        }
        public House getHousebyNumberofPeople(int numberOfPeople)
        {
            HouseAccess access = new HouseAccess(_context);
            return access.getHousebyNumberofPeople(numberOfPeople);
        }

        public House getHouseById(long id)
        {
            HouseAccess access = new HouseAccess(_context);
            return access.getHouse(id);
        }

        public int insertHouse(House house)
        {
            HouseAccess access = new HouseAccess(_context);
            return access.insertHouse(house);
        }

        public void updateHouse(House house)
        {
            HouseAccess access = new HouseAccess(_context);
            access.updateHouse(house);
        }

        public void deleteHouse(House house)
        {
            HouseAccess access = new HouseAccess(_context);
            access.deleteHouse(house);
        }
    }
}
