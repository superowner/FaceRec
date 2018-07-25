using System;
using System.Data.Entity;
using System.Linq;
using FaceRec.Models;
using System.Data.SQLite;
using System.IO;

namespace FaceRec.Core
{

    public class Store : DbContext
    {
        public const string DbFile = "data/db.sql";

        public Store() : base("name=Store")
        {
            //this.Configuration.LazyLoadingEnabled = true;
        }

        public void EnsureCreateTables()
        {
            try
            {
                using (var fs = new FileStream(DbFile, System.IO.FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        var content = sr.ReadToEnd();

                        this.Database.ExecuteSqlCommand(content);
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public virtual DbSet<UserGroup> UserGroups { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserView> UserViews { get; set; }
    }
}