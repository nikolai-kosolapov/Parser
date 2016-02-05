using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using OlympusDataModel;
using myFizzler = Fizzler.Systems.HtmlAgilityPack.HtmlNodeSelection;

namespace Parsers.Parser
{
    class LentaParser: Parser, IParser
    {
        private readonly LentaParserConfig _config;

        public LentaParser(LentaParserConfig config)
        {
            _config = config;
        }

        public News[] GetAllNews()
        {
            var htmlString = Get(_config.Url, "");
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var nodes = myFizzler.QuerySelectorAll(htmlDocument.DocumentNode, ".bordered-title");
            var main = nodes.ToList().Find(x => x.InnerText == "Главное за сутки");
            var itemList = myFizzler.QuerySelectorAll(main.ParentNode, ".item");

            var ret = new List<News>();

            foreach (var htmlNode in itemList)
            {             
                var aTag = myFizzler.QuerySelector(htmlNode, "a");
                var url = _config.Url + aTag.Attributes["href"].Value;
                ret.Add(ParseNews(url, aTag.InnerText)); 
            }
            return ret.ToArray();
        }

        private News ParseNews(string url, string headerText = "")
        {
            var tempNews = new News();
            var html = Get(url, "");
            var newsHtmlDocument = new HtmlDocument();
            newsHtmlDocument.LoadHtml(html);
            var titleTag = myFizzler.QuerySelector(newsHtmlDocument.DocumentNode, ".b-topic__title");
            tempNews.HeaderText = headerText == "" ? titleTag.InnerText : headerText;
           // var imageTag = myFizzler.QuerySelector(newsHtmlDocument.DocumentNode, ".b-topic__title-image");
        //    tempNews.ImageUrl = myFizzler.QuerySelector(imageTag, "img").Attributes["src"].Value;
            var t = myFizzler.QuerySelector(newsHtmlDocument.DocumentNode, ".b-text");
            if (t != null)
                tempNews.ShortText = t.InnerText;
            tempNews.Url = url;
            tempNews.Provider = _config.Provider;
            var dateTag = myFizzler.QuerySelector(newsHtmlDocument.DocumentNode, ".g-date");
            tempNews.Date = dateTag != null ? DateTime.Parse(dateTag.Attributes["datetime"].Value) : DateTime.Now;
            return tempNews;
        }
    }
}
