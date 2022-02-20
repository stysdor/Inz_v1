using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class FlatLink
    {
        public FlatLink()
        {
        }

        public FlatLink(string urlString)
        {
            UrlString = urlString;
            UrlDate = DateTime.Today;
        }

        public virtual int Id { get; set; }
        public virtual string UrlString { get; set; }
        public virtual DateTime UrlDate { get; set; }


    }
}
