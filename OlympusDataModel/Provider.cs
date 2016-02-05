using System.Collections.Generic;

namespace OlympusDataModel
{
    public  class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool HasApi { get; set; }
    
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }
}

