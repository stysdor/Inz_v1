using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Model
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual float MseTest { get; set; }
        public virtual float MseTrain { get; set; }
        public virtual float RmseTrain { get; set; }
        public virtual float RmseTest { get; set; }
        public virtual float MaeTest { get; set; }
        public virtual float MaeTrain { get; set; }
        public virtual int OfferCount { get; set; }
    }
}
