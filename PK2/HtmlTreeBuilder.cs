using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PK2
{
    internal class HtmlTreeBuilder
    {
        private readonly HtmlHelper _htmlHelper = HtmlHelper.Instance;

        public HtmlElement BuildTree(IEnumerable<string> tags)
        {
            HtmlElement root = new HtmlElement { Name = "root" };
            HtmlElement current = root;

            foreach (var tag in tags)
            {
                if (tag.StartsWith("</"))
                {
                    current = current.Parent; // Move up in the tree
                }
                else
                {
                    var element = CreateHtmlElement(tag);
                    current.Children.Add(element);
                    element.Parent = current;

                    if (!_htmlHelper.SelfClosingTags.Contains(element.Name))
                    {
                        current = element; // Move down in the tree
                    }
                }
            }

            return root;
        }

        private HtmlElement CreateHtmlElement(string tag)
        {
            var element = new HtmlElement();

            // Extract tag name
            var tagNameMatch = Regex.Match(tag, @"<\s*(\w+)");
            if (tagNameMatch.Success)
            {
                element.Name = tagNameMatch.Groups[1].Value;
            }

            // Extract attributes
            var attributeMatches = Regex.Matches(tag, @"(\w+)=""([^""]*)""");
            foreach (Match match in attributeMatches)
            {
                var key = match.Groups[1].Value;
                var value = match.Groups[2].Value;

                if (key == "id")
                    element.Id = value;
                else if (key == "class")
                    element.Classes.AddRange(value.Split(' '));
                else
                    element.Attributes[key] = value;
            }

            return element;
        }
    }
}
