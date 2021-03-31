using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Discografica.Entity;

namespace Discografica.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Artista> Artista { get; set; }
        public DbSet<Cancion> Cancion { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<Disco> Disco { get; set; }
        public DbSet<Representante> Representante { get; set; }
    }
}
