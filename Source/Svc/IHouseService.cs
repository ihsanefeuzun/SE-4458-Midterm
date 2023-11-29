using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using WebApplication1.Model;
using WebApplication1.Source.Db;
using System.Threading.Tasks;

namespace WebApplication1.Source.Svc
{
    public interface IHouseService
    {

        public List<House> getHouses();
        public List<House> getHousesWithCache();
        public House getHousebyNumberofPeople(int numberOfPeople);

        public House getHouseById(long id);

        public int insertHouse(House house);

        public void updateHouse(House house);

        public void deleteHouse(House house);

    }
}
