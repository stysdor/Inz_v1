using API.Helpers;
using API.ModelDTO;
using AutoMapper;
using BusinessLogic.BaseSpecification;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic.SI;
using BusinessLogic.Specification;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    public class FlatController : BaseApiController
    {

        private readonly IFlatRepository flatRepository;
        private readonly IMapper mapper;

        public FlatController(IFlatRepository flatRepository, IMapper mapper)
        {
            this.flatRepository = flatRepository;
            this.mapper = mapper;
        }

        [HttpGet("flats")]
        public ActionResult<Pagination<FlatDTO>> GetFlats(
            [FromQuery] SpecParams specParams)
        {
            var spec = new FlatSpecification (specParams);
            var countSpec = new FlatForCountingSpecification(specParams);
            var totalCount = flatRepository.Count(countSpec);
            var flats = flatRepository.GetFlats(spec);
            var data = mapper.Map<IReadOnlyList<Flat>, IReadOnlyList<FlatDTO>>(flats);
            return Ok(new Pagination<FlatDTO>(specParams.PageIndex, specParams.PageSize, totalCount, data));
        }

        [HttpGet("downloadLinks")]
        public ActionResult<int> DownloadOfferLinks()
        {
            return Ok(flatRepository.DownloadAndSaveFlatLinks());
        }

        [HttpGet("downloadOffers")]
        public ActionResult<bool> DownloadOffers([FromQuery] int count)
        {
            var links = flatRepository.GetFlatsLinksToGetFlatOffers(count);
            return Ok(flatRepository.DownloadFlatsFromLinks(links));
        }

        [HttpPost("postFlats")]
        public ActionResult<Pagination<FlatDTO>> PostFlats(
           [FromBody] FlatDTO[] flats)
        {
            var data = mapper.Map<IList<FlatDTO>, IList<Flat>>(flats);
            return Ok(flatRepository.AddFlats(data));
        }

        [HttpPost("postFlat")]
        public ActionResult<decimal> PostFlat(
          [FromBody] FlatDTO flat)
        {
            var reader = new ScriptReader();
            var response =  reader.run_cmd();
            var data = mapper.Map<FlatDTO, Flat>(flat);
            return Ok(250300);
        }
    }
}
