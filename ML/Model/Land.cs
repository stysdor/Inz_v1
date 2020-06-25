using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Model
{
    public class Land
    {
        [LoadColumn(0)]
        public float Area;
      
        [LoadColumn(1)]
        public string Road;
        [LoadColumn(2)]
        public string N_latitude;
        [LoadColumn(3)]
        public string E_longitude;

        [LoadColumn(4)]
        public float Price;

        //public bool IsElectricity;
        //public bool IsGas;
        //public bool IsWater;
        //public bool IsSewers;

    }

    public class LandPricePrediction
    {
        [ColumnName("Score")]
        public float Price;

    }

    // Define a class for all the input columns that we intend to consume.
    class InputRow
    {
        public string N_latitude { get; set; }
        public string E_longitude { get; set; }
    }

    // Define a class for all output columns that we intend to produce.
    class OutputRow
    {
        public float N_latitudeConverted { get; set; }
        public float E_longitudeConverted { get; set; }
    }
}
