using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace TeslaSolarWeb.Models
{
    public class MyDbContex : DbContext
    {
        public MyDbContex(): base("SmartAspTeslaSolar")
        {
            
        }

        public DbSet<Project> Projects { get; set; }
    }
}