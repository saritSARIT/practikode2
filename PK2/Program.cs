using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PK2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var loader = new HtmlLoader();
            var parser = new HtmlParser();
            var treeBuilder = new HtmlTreeBuilder();

            var html = await loader.Load("https://example.com");
            var tags = parser.SplitTags(html);

            var tree = treeBuilder.BuildTree(tags);
            var elements = tree.FindBySelector("div#main.class-name");

            foreach (var element in elements)
            {
                Console.WriteLine($"Found element: {element.Name}, ID: {element.Id}");
            }
        }
    }
}
