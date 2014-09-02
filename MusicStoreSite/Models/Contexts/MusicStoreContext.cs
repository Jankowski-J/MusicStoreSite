using MusicStoreSite.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MusicStoreSite.Models.Contexts
{
    public class MusicStoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        
    }
}