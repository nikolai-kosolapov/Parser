
using System.Collections.Generic;

namespace OlympusDataModel
{
    public  class Category
    {    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}

