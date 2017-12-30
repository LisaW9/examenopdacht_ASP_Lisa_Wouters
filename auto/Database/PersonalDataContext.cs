using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;

namespace auto.Database
{
    public class PersonalDataContext : DbContext
    {
        public PersonalDataContext(DbContextOptions<PersonalDataContext> options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<CarType> Types { get; set; }
    }
}