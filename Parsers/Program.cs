using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OlympusDataModel;
using Parsers.Parser;

namespace Parsers
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Запуск");
            using (var dbContext = new OlympusDbModelContainer())
            {
                var parsers = new List<IParser>
                {
                    new YaplakalParser(new YaplakalParserConfig
                        {
                            Url = "http://www.yaplakal.com/news.xml",
                            Provider = dbContext.ProviderSet.Find(3)
                        }),
                    new HabrParser(new HabrParserConfig
                        {
                            Url = "http://habrahabr.ru/",
                            Provider = dbContext.ProviderSet.Find(2)
                        }),
                    new LentaParser(new LentaParserConfig
                        {
                            Url = "http://lenta.ru",
                            Provider = dbContext.ProviderSet.Find(1)
                        }),
                };


                while (true)
                {
                    try
                    {
                        //  Console.WriteLine("Парсим новости");
                        UpdateAllNewsToDb(parsers, dbContext);
                        //  Console.WriteLine("Готово");
                        Thread.Sleep(10000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка:");
                        Console.WriteLine(ex.Message);
                    }

                }

        }
    }


        static void UpdateAllNewsToDb(IEnumerable<IParser> parsers, OlympusDbModelContainer dbContext)
        {
            var oldNews = dbContext.NewsSet;
            var resultMaxOrder = dbContext.NewsSet;
            var maxOrder = 0;
            var isAnyFlag = true;
            if(resultMaxOrder.Any())
              maxOrder = resultMaxOrder.Max(x => x.Order);
            foreach (var parser in parsers)
            {
                try
                {
                    var result = parser.GetAllNews();

                    foreach (var newsItem in result)
                    {
                        if (oldNews.ToList().All(x => x.Url != newsItem.Url))
                        {
                            if (isAnyFlag)
                            {
                                maxOrder++;
                                isAnyFlag = false;
                            }
                            newsItem.Order = maxOrder;
                            dbContext.NewsSet.Add(newsItem);
                            Console.WriteLine(DateTime.Now + " Добавлена новость(" + newsItem.Provider.Name +  "): " + newsItem.HeaderText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка парсера " + parser.GetType() + "; " + ex);
                }
            }
            dbContext.SaveChanges();
        }


    }
}
