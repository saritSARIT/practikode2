using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PK2
{
    internal class HtmlParser
    {
        public IEnumerable<string> SplitTags(string html)
        {
            var regex = new Regex(@"<[^>]+>");
            var matches = regex.Matches(html);
            foreach (Match match in matches)
            {
                yield return match.Value.Trim();
            }
        }
    }
}
