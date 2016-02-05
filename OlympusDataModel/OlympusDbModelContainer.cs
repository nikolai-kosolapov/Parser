using System.Data.Entity;

namespace OlympusDataModel
{
    public class OlympusDbModelContainer : DbContext
    {
        public  OlympusDbModelContainer(){}

        public OlympusDbModelContainer(string name)
            : base(name)
        {}
        public DbSet<News> NewsSet { get; set; }
        public DbSet<Provider> ProviderSet { get; set; }
        public DbSet<Category> CategorySet { get; set; }
    }
}
