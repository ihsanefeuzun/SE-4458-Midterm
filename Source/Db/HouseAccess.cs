using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Context;
using WebApplication1.Model;

namespace WebApplication1.Source.Db
{
    public class HouseAccess
    {
        private HouseContext _context;

        public HouseAccess(HouseContext context)
        {
            _context = context;
        }

        public House getHouse(long id)
        {
            return _context.Houses.FirstOrDefault(s => s.Id == id);
        }

        public House getHousebyNumberofPeople(int numberOfPeople)
        {
            return _context.Houses.FirstOrDefault(s => s.NumberofPeople == numberOfPeople);
        }

        public int insertHouse(House data)
        {
            _context.Houses.Add(data);
            return _context.SaveChanges();

        }
        public void updateHouse(House house)
        {
            // Implementation to update a student in the database
            _context.Houses.Update(house);
            _context.SaveChanges();
        }

        public int deleteHouse(House house)
        {
            _context.Houses.Remove(house);
            return _context.SaveChanges();
        }
        public IEnumerable<House> getAllHouses()
        {
            return _context.Houses;
        }
    }
}
