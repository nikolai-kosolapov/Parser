using System;
using System.Collections.Generic;
using System.Xml;
using OlympusDataModel;

namespace Parsers.Parser
{
    class YaplakalParser:Parser, IParser
    {
        private readonly YaplakalParserConfig _config;

        public YaplakalParser(YaplakalParserConfig config)
        {
            _config = config;
        }

        public News[] GetAllNews()
        {
            var xml = new XmlDocument();
            xml.Load(_config.Url);
            var itemList = xml.GetElementsByTagName("item");
            var tempNews = new List<News>();
            for (var i = 0; i < itemList.Count; i++)
            {
                var newsItem = new News();
                var titleNodes = itemList[i].SelectNodes("title");
                if (titleNodes != null)
                {
                    var titleNode = titleNodes[0];
                    newsItem.HeaderText = titleNode.InnerText;
                }
                var urlNodes = itemList[i].SelectNodes("link");
                if (urlNodes != null)
                {
                    var urlNode = urlNodes[0];
                    newsItem.Url = urlNode.InnerText;
                }
                var textNodes = itemList[i].SelectNodes("comments");
                if (textNodes != null)
                {
                    var textNode = textNodes[0];
                    newsItem.Text = textNode.InnerText;
                }
                var shortTextNodes = itemList[i].SelectNodes("description");
                if (shortTextNodes != null)
                {
                    var shortTextNode = shortTextNodes[0];
                    newsItem.ShortText = shortTextNode.InnerText;
                }
                //var imageNodes = itemList[i].SelectNodes("enclosure");
                //if (imageNodes != null && imageNodes.Count != 0)
                //{
                //    var imageNode = imageNodes[0];
                //    if (imageNode.Attributes != null) newsItem.ImageUrl = imageNode.Attributes["url"].Value;
                //}
                var dateNodes = itemList[i].SelectNodes("pubDate");
                if (dateNodes != null)
                {
                    var dateNode = dateNodes[0];
                    newsItem.Date = Convert.ToDateTime(dateNode.InnerText);
                }
                newsItem.Provider = _config.Provider;
                tempNews.Add(newsItem);
            }
            return tempNews.GetRange(0, 10).ToArray();
        }
    }
}
