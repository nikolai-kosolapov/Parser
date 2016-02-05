using System.Collections.Generic;

namespace OlympusDataModel
{
    public  class News
    {

    
        public int Id { get; set; }
        public string Text { get; set; }
        public System.DateTime Date { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string HeaderText { get; set; }
        public string ShortText { get; set; }
        public int Order { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }
}

