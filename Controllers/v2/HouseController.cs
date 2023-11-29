using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Model.Dto;
using WebApplication1.Model;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers.v2
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class HouseController : ControllerBase
    {
        private IHouseService _houseService;

        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        [HttpGet]
        public List<HouseDto> Get()
        {
            List<House> datas = _houseService.getHouses();
            List<HouseDto> ret = new List<HouseDto>();
            datas.ForEach(data => ret.Add(createHouseDto(data)));
            return ret;
        }

        private HouseDto createHouseDto(House house)
        {
            HouseDto ret = new HouseDto()
            {
                Id = house.Id,
                Name = house.Name,
                //BookedDates = house.BookedDates,
                OwnerName = house.OwnerName,
                Address = house.Address,
                NumberofPeople = house.NumberofPeople,
                Amenities = house.Amenities
            };
            return ret;
        }

        private House createHouse(HouseDto houseDto)
        {
            House ret = new House()
            {
                Id = houseDto.Id,
                Name = houseDto.Name,
                //BookedDates = houseDto.BookedDates,
                OwnerName = houseDto.OwnerName,
                Address = houseDto.Address,
                NumberofPeople = houseDto.NumberofPeople,
                Amenities = houseDto.Amenities
            };
            return ret;
        }
    }
}
