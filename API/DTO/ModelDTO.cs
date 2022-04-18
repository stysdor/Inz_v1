using System;

namespace API.DTO
{
    public class ModelDTO
    {
        public  int Id { get; set; }
        public  DateTime Date { get; set; }
        public  float MseTest { get; set; }
        public  float MseTrain { get; set; }
        public  float RmseTrain { get; set; }
        public  float RmseTest { get; set; }
        public  float MaeTest { get; set; }
        public  float MaeTrain { get; set; }
        public  int OfferCount { get; set; }
    }
}
