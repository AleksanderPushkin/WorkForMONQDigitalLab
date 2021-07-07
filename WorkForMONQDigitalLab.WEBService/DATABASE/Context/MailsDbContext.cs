
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkForMONQDigitalLab.WEBService.Context.Entity;

namespace WorkForMONQDigitalLab.WEBService.Context
{
    public class MailsDbContext: DbContext
    { 
        public DbSet<Mail> Mails { get; set; }

        public MailsDbContext(DbContextOptions<MailsDbContext> options) : base(options)
        {
        }
        //public MailsDbContext() : base()
        //{
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder().Build();
        //        var connectionString = "server=Localhost;port=3306;database=MONQDigitalLab.DB;Uid=root;Pwd=Bighappy123";
        //        optionsBuilder.UseMySql(connectionString);
        //    }
        //}
    }
}
