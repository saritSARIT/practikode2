using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace PK2
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; } = new();
        public Selector Child { get; set; }

        public static Selector Parse(string query)
        {
            var parts = query.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            Selector root = null, current = null;

            foreach (var part in parts)
            {
                var selector = new Selector();
                foreach (var subPart in part.Split(new[] { '#', '.' }, System.StringSplitOptions.RemoveEmptyEntries))
                {
                    if (part.StartsWith("#"))
                        selector.Id = subPart;
                    else if (part.StartsWith("."))
                        selector.Classes.Add(subPart);
                    else
                        selector.TagName = subPart;
                }

                if (root == null)
                {
                    root = selector;
                    current = root;
                }
                else
                {
                    current.Child = selector;
                    current = selector;
                }
            }

            return root;
        }

        public bool IsMatch(HtmlElement element)
        {
            if (!string.IsNullOrEmpty(TagName) && element.Name != TagName) return false;
            if (!string.IsNullOrEmpty(Id) && element.Id != Id) return false;
            if (Classes.Any() && !Classes.All(cls => element.Classes.Contains(cls))) return false;
            return true;
        }

    }
}
