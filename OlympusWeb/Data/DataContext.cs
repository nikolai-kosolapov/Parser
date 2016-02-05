
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using OlympusDataModel;

namespace OlympusWeb.Data
{
    public class DataContext
    {

        private  BackgroundWorker _worker = new BackgroundWorker();

        private News[] _newsCache;

        public DataContext()
        {
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerAsync();
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var db = new OlympusDbModelContainer())
            {
                while (true)
                {
                   _newsCache = db.NewsSet.ToArray();
                    Thread.Sleep(3000);
                }
            }
        }

        //private const string ConnectionString = "OlympusDbCs";

        public News[] GetAllNews()
        {
            return _newsCache ?? new []
                {
                    new News
                        {
                            Date = DateTime.Now,
                            HeaderText = "Сайт запускается, перезагрузите страницу позже.",
                            Order = 1,
                        }
                };
        }
    }
}