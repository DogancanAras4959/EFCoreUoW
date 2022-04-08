using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.LOG.Models;

namespace UoW.LOG
{
    public class UoWDbLogContext : DbContext
    {
        public UoWDbLogContext(DbContextOptions<UoWDbLogContext> options) : base(options)
        {

        }
        public virtual DbSet<Loglar> logs { get; set; }
        public virtual DbSet<Durumlar> durumlars { get; set; }
        public virtual DbSet<Islemler> islemlers { get; set; }
        public virtual DbSet<Yonetici> yoneticilers { get; set; }
    }
}
