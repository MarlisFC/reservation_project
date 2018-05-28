namespace RES.BusinessLogic.Core.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using RES.BusinessLogic.Core.Entities;

    public class ResEntities : DbContext
    {
        // Your context has been configured to use a 'ResEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'RES.BusinessLogic.Core.ResEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ResEntities' 
        // connection string in the application configuration file.
        //public ResEntities()
        //    : base("name=ResEntities")
        //{
        //}

        public ResEntities(string conString)
        : base(conString)
        {
            Database.SetInitializer<ResEntities>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }

        public virtual DbSet<Place> Places { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}