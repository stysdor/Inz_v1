using API.Helpers;
using API.DTO;
using AutoMapper;
using BusinessLogic.BaseSpecification;
using BusinessLogic.Interfaces;
using BusinessLogic.Specification;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class ModelController : BaseApiController
    {
        static HttpClient client = new HttpClient();
        string modelUrl = "http://127.0.0.1:8000/";

        private readonly IFlatRepository flatRepository;
        private readonly IModelRepository modelRepository;
        private readonly IMapper mapper;

        public ModelController(IFlatRepository flatRepository, IModelRepository modelRepository, IMapper mapper)
        {
            this.flatRepository = flatRepository;
            this.mapper = mapper;
            this.modelRepository = modelRepository;
            client.BaseAddress = new Uri(modelUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet("")]
        public ActionResult<List<ModelDTO>> GetModel()
        {
            var models = modelRepository.GetModel();
            var data = mapper.Map<IReadOnlyList<Model>, IReadOnlyList<ModelDTO>>(models);
            return Ok(data);
        }

        [HttpGet("feedModel")]
        public async Task<ActionResult<Model>> FeedModel(
            [FromQuery] SpecParams specParams)
        {
            var flats = getOffersToFeedModel(specParams);
            var request = new FeedModelRequest(mapper.Map<List<FlatToModelDTO>>(flats));
            var httpContent = getStringContentFromFeedModelRequest(request);

            HttpResponseMessage response = await client.PostAsync("/update_model/", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<Model>(responseString);
                var model = modelRepository.SaveModel(responseModel, request.Samples.Count);
                var isUpdated = updateFlatsUsedInModel(flats);
                return model;
            }
            else {                      
                return null;
            }
        }

        private IReadOnlyList<Flat> getOffersToFeedModel(SpecParams specParams) {
            var spec = new FlatSpecification(specParams);
            return flatRepository.GetFlats(spec);
        }

        private StringContent getStringContentFromFeedModelRequest(FeedModelRequest request) {

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string json = JsonConvert.SerializeObject(request, serializerSettings);

            return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }

        private bool updateFlatsUsedInModel(IReadOnlyList<Flat> flats) {
            var flatList = new List<Flat>(flats);
            flatList.ForEach(flat => flat.IsUsedInModel = true);
            return flatRepository.AddFlats(flatList);
        }

    }
}
