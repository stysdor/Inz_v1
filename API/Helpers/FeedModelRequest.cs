using API.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Helpers
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FeedModelRequest
    {
        public FeedModelRequest(IReadOnlyList<FlatToModelDTO> samples)
        {
            Samples = samples;
        }

        public IReadOnlyList<FlatToModelDTO> Samples { get; set; }
    }
}
