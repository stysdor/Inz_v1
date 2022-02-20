using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Tensorflow;
using Tensorflow.NumPy;
using static Tensorflow.Binding;
using static Tensorflow.KerasApi;

namespace AIModule.AIModel
{
    public class DataPreparation
    {
        IDatasetV2 data;
        public IList<Flat> GetDataFromJson(string json) {
            return JsonConvert.DeserializeObject<IList<Flat>>(json);
        }

        public IDatasetV2 TransformData(List<Flat> data) {
            data.ForEach(item => {
            
            });
        }

        private int? transformMarket(string market) {
            if (market.Equals("pierwotny")) return 0;
            else if (market.Equals("wtórny")) return 1;
            else return null;
        }
    }
}
