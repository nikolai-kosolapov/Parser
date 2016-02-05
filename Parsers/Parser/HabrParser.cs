using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using OlympusDataModel;
using myFizzler = Fizzler.Systems.HtmlAgilityPack.HtmlNodeSelection;

namespace Parsers.Parser
{
    class HabrParser: Parser, IParser
    {

        private readonly HabrParserConfig _config;

        public HabrParser(HabrParserConfig config)
        {
            _config = config;
        }

        public News[] GetAllNews()
        {
            var htmlString = Get(_config.Url, "");
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var posts = myFizzler.QuerySelectorAll(htmlDocument.DocumentNode, ".shortcuts_item");
            var tempNews = new List<News>();
            foreach (var htmlNode in posts)
            {
                var newsItem = new News();
                var headerTag = myFizzler.QuerySelector(htmlNode, ".title a");
                newsItem.HeaderText = headerTag.InnerText;
                newsItem.Url = headerTag.Attributes["href"].Value;
                newsItem.ShortText = myFizzler.QuerySelector(htmlNode, ".content").InnerHtml;
                newsItem.Provider = _config.Provider;
                var dateString = myFizzler.QuerySelector(htmlNode, ".published").InnerText;
                var result = dateString.IndexOf("сегодня", StringComparison.Ordinal);
                if (result == -1)
                {
                    result = dateString.IndexOf("вчера", StringComparison.Ordinal);
                    if (result != -1)
                    {
                        var dateParseString = dateString.Substring(8, 5);
                        var dateTime = Convert.ToDateTime(dateParseString);
                        newsItem.Date = dateTime.AddDays(-1);
                    }
                    else
                    {
                        newsItem.Date = DateTime.Now;
                    }
                }
                else
                {
                    var dateParseString = dateString.Substring(10, 5);
                    var dateTime = Convert.ToDateTime(dateParseString);
                    newsItem.Date = dateTime;
                }
                tempNews.Add(newsItem);
            }

            return tempNews.ToArray();
        }
    }
}
