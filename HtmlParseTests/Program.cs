using AngleSharp;
using CsQuery;
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Text;

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

                string resultingStringCsQuery = "";
                string resultingStringHtmlAgilityPack = "";
                string resultingStringAngleSharp = "";

                if (args.Length > 1)
                {
                    int.TryParse(args[1], out iterations);
                }

                var stopwatchCsQuery = new Stopwatch();
                var stopwatchAgilityPack = new Stopwatch();
                var stopwatchAngleSharp = new Stopwatch();

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
                    // Retrieving fragment to parse. 
                    CQ cs = htmlFragment;

                    // Parse fragment.
                    var pTags = cs["p"];
                    resultingStringCsQuery = pTags.Length > 0 ? pTags.First().Text() : htmlFragment;
                    //Console.WriteLine(resultingString);
                }
                stopwatchCsQuery.Stop();

                // Running the test with HtmlAgilityPack.
                stopwatchAgilityPack.Start();
                for (var i = 0; i < iterations; i++)
                {
                    // Retrieving fragment to parse. 
                    var htmlDoc = new HtmlDocument()
                    {
                        OptionEmptyCollection = true,
                        OptionDefaultStreamEncoding = Encoding.UTF8
                    };
                    htmlDoc.LoadHtml(htmlFragment);

                    // Parse fragment.
                    var firstElement = htmlDoc.DocumentNode.SelectSingleNode("//p");
                    resultingStringHtmlAgilityPack = firstElement != null ? firstElement.InnerText : htmlFragment;
                    //Console.WriteLine(resultingString);
                }

                stopwatchAngleSharp.Start();
                for (var i = 0; i < iterations; i++)
                {
                    // Retrieving fragment to parse. 
                    var context = BrowsingContext.New(Configuration.Default);
                    var document = context.OpenAsync(req => req.Content(htmlFragment));

                    // Parse fragment.
                    var firstElement = document.Result.QuerySelector("p");
                    resultingStringAngleSharp = firstElement != null ? firstElement.TextContent : htmlFragment;
                    //Console.WriteLine(resultingString);
                }
                stopwatchAngleSharp.Stop();


                stopwatchAgilityPack.Stop();
                Console.WriteLine("*** RESULT ********************************************************");
                Console.WriteLine($"* CsQuery");
                Console.WriteLine($"*   Text: {resultingStringCsQuery}");
                Console.WriteLine($"*   Time: {stopwatchCsQuery.ElapsedMilliseconds} ms.");
                Console.WriteLine($"*");
                Console.WriteLine($"* HtmlAgilityPack");
                Console.WriteLine($"*   Text: {resultingStringHtmlAgilityPack}");
                Console.WriteLine($"*   Time: {stopwatchAgilityPack.ElapsedMilliseconds} ms.");
                Console.WriteLine($"*");
                Console.WriteLine($"* AngleSharp");
                Console.WriteLine($"*   Text: {resultingStringAngleSharp}");
                Console.WriteLine($"*   Time: {stopwatchAngleSharp.ElapsedMilliseconds} ms.");
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
