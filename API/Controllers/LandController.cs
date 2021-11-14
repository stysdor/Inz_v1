﻿using API.ModelDTO;
using AutoMapper;
using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class LandController : BaseApiController
    {
        private readonly ILandOfferRepository landOfferRepository;
        private readonly ILandRepository landRepository;
        private readonly IMapper mapper;

        public LandController(ILandOfferRepository landOfferRepository, ILandRepository landRepository, IMapper mapper)
        {
            this.landOfferRepository = landOfferRepository;
            this.landRepository = landRepository;
            this.mapper = mapper;
        }

        [HttpGet("landoffers")]
        public ActionResult<IReadOnlyList<LandOfferDTO>> GetLandOffers()
        {
           var data = landOfferRepository.GetLandOffers();
           return Ok(mapper.Map<IReadOnlyList<LandOffer>, IReadOnlyList<LandOfferDTO>>(data));
        }

        [HttpGet("landoffers/download")]
        public ActionResult<bool> DownloadLandOffers()
        {
            return Ok(landOfferRepository.DownloadLandOffers());
        }

        [HttpGet("lands")]
        public ActionResult<IReadOnlyList<LandOfferDTO>> GetLands()
        {
            var data = landRepository.GetLands();
            return Ok(mapper.Map<IReadOnlyList<Land>, IReadOnlyList<LandDTO>>(data));
        }

        [HttpPost("addland")]
        public ActionResult<bool> UpdateLand(LandOfferDTO land)
        {
            try
            {
                landOfferRepository.UpdateLand(mapper.Map<LandOfferDTO, LandOffer>(land));
                return Ok(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}