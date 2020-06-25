using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic.Web
{
    public class TextAnalyzer
    {
        public List<string> mediaValue = new List<string>
        {
            "w drodze", "miejska", "miejski", "na działce", "jest","w działce", "na dzialce", "w dzialce","na sąsiedniej działce", "na sasiedniej działce", "na sasiedniej dzialce", "na sąsiedniej dzialce"
        };

        public bool CheckIfMediaAvailable(string text, string media)
        {
            int index  = text.IndexOf(media);
            string lookingArea = text.Substring(index, media.Length + 200);
            bool isAvailable = false;
            foreach (var value in mediaValue)
            {
                if (lookingArea.Contains(value))
                    isAvailable = true;
            }

            return isAvailable;
        }

        public bool CheckIfMediaAvailable(string text)
        {
            bool isAvailable = false;
            foreach (var value in mediaValue)
            {
                if (text.Contains(value))
                    isAvailable = true;
            }

            return isAvailable;
        }
    }
}
