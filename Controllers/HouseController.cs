using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Model.Dto;
using WebApplication1.Source.Svc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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

        [HttpPost("GetAllWithPaging")]
        public List<HouseDto> GetWithPaging(QueryWithPagingDto query)
        {
            List<House> datas = _houseService.getHouses();
            List<House> datasFiltered = datas.Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

            List<HouseDto> ret = new List<HouseDto>();
            datasFiltered.ForEach(data => ret.Add(createHouseDto(data)));
            return ret;
        }

        [HttpPost("GetAllWithCaching")]
        public List<HouseDto> GetAllWithCaching(QueryWithPagingDto query)
        {
            List<House> datas = _houseService.getHousesWithCache();
            List<House> datasFiltered = datas.Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

            List<HouseDto> ret = new List<HouseDto>();
            datasFiltered.ForEach(data => ret.Add(createHouseDto(data)));
            return ret;
        }

        [HttpGet("{id}")]
        public HouseDto Get(long id)
        {
            House data = _houseService.getHouseById(id);
            return createHouseDto(data);
        }

        [HttpPost("CreateHouse")]
        public HouseResultDto CreateHouse([FromBody] HouseDto house)
        {
            HouseResultDto ret = new HouseResultDto();

            if (!ModelState.IsValid)
            {
                ret.Status = "FAILURE";
                ret.Message = "Invalid Model";
                return ret;
            }

            try
            {
                int cnt = _houseService.insertHouse(createHouse(house));

                if (cnt > 0)
                {
                    ret.Id = _houseService.getHousebyNumberofPeople(house.NumberofPeople).Id;
                    ret.Status = "SUCCESS";
                }
                else
                {
                    ret.Status = "FAILURE";
                    ret.Message = "Failed to insert the house.";
                }
            }
            catch (Exception ex)
            {
                ret.Status = "FAILURE";
                ret.Message = $"An error occurred: {ex.Message}";

                // Include details of the inner exception, if available
                if (ex.InnerException != null)
                {
                    ret.InnerExceptionMessage = ex.InnerException.Message;
                }
            }

            return ret;
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] HouseDto houseDto)
        {
            if (houseDto.Id == 0)
            {
                return BadRequest("Cant delete a house with no id");
            }
            if (houseDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        _houseService.deleteHouse(createHouse(houseDto));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpPut]
        public IActionResult Put([FromBody] HouseDto houseDto)
        {
            if (houseDto.Id == 0)
            {
                return BadRequest("Cant update a house with no id");
            }
            if (houseDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        _houseService.updateHouse(createHouse(houseDto));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
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
