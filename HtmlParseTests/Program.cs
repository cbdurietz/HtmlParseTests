using CsQuery;
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Linq;

namespace HtmlParseTests
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var filePath = args[0];
                var htmlFragment = System.IO.File.ReadAllText(filePath);
                var iterations = 1;
                if (args.Length > 1)
                {
                    int.TryParse(args[1], out iterations);
                }

                var stopwatchCsQuery = new Stopwatch();
                var stopwatchAgilityPack = new Stopwatch();

                Console.WriteLine($"* Performance testing CsQuery and HtmlAgilityPack *****************");
                Console.WriteLine($"*** INDATA ********************************************************");
                Console.WriteLine($"* Running {iterations} iterations on the same html fragment.");
                Console.WriteLine($"* File used: {filePath}");
                Console.WriteLine("*******************************************************************");
                Console.WriteLine("");

                // Running the test with CsQuery.
                stopwatchCsQuery.Start();
                for (var i = 0; i < iterations; i++)
                {
                    CQ cs = htmlFragment;
                    var pTags = cs["p"];

                    var ingress = pTags.First().Text();
                    //Console.WriteLine(ingress);
                }
                stopwatchCsQuery.Stop();

                // Running the test with HtmlAgilityPack.
                stopwatchAgilityPack.Start();
                for (var i = 0; i < iterations; i++)
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlFragment);

                    var ingress = htmlDoc.DocumentNode.SelectNodes("//p").First().InnerText;
                    //Console.WriteLine(ingress);
                }
                stopwatchAgilityPack.Stop();
                Console.WriteLine("*** RESULT ********************************************************");
                Console.WriteLine($"* CsQuery");
                Console.WriteLine($"*   Time: {stopwatchCsQuery.ElapsedMilliseconds} ms.");
                Console.WriteLine($"*");
                Console.WriteLine($"* HtmlAgilityPack");
                Console.WriteLine($"*   Time: {stopwatchAgilityPack.ElapsedMilliseconds} ms.");
                Console.WriteLine("*******************************************************************");
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }
}
