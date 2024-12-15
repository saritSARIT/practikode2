using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PK2
{
 

    public static class HtmlElementExtensions
    {
        public static IEnumerable<HtmlElement> FindBySelector(this HtmlElement root, string selector)
        {
            var parsedSelector = Selector.Parse(selector);
            return Search(root, parsedSelector);
        }

        private static IEnumerable<HtmlElement> Search(HtmlElement element, Selector selector)
        {
            if (selector.IsMatch(element))
            {
                if (selector.Child == null)
                    yield return element;
                else
                {
                    foreach (var child in element.Children)
                    {
                        foreach (var match in Search(child, selector.Child))
                        {
                            yield return match;
                        }
                    }
                }
            }
            else
            {
                foreach (var child in element.Children)
                {
                    foreach (var match in Search(child, selector))
                    {
                        yield return match;
                    }
                }
            }
        }
    }

}
