using Microsoft.EntityFrameworkCore;
using RegistroDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroDetalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> Persona { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlite(@"Data Source = DetallePersona.db");
        }
    }
}
