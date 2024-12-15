using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PK2
{
    internal class HtmlHelper
    {
        public static HtmlHelper Instance { get; } = new HtmlHelper();
        public string[] HtmlTags { get; private set; }
        public string[] SelfClosingTags { get; private set; }

        private HtmlHelper()
        {
            HtmlTags = LoadTags("HtmlTags.json");
            SelfClosingTags = LoadTags("SelfClosingTags.json");
        }

        private string[] LoadTags(string fileName)
        {
            var json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<string[]>(json);
        }
    }
}
