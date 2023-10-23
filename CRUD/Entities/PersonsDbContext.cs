using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            //Seed to Countries
            modelBuilder.Entity<Country>().HasData(
                new Country() { CountryId = Guid.Parse("14629847-905a-4a0e-9abe-80b61655c5cb"), CountryName = "Philippines" },
                new Country() { CountryId = Guid.Parse("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"), CountryName = "Thailand" },
                new Country() { CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"), CountryName = "China" },
                new Country() { CountryId = Guid.Parse("8f30bedc-47dd-4286-8950-73d8a68e5d41"), CountryName = "Palestinian Territory" },
                new Country() { CountryId = Guid.Parse("501c6d33-1bbe-45f1-8fbd-2275913c6218"), CountryName = "China" }
                );

            //Seed to Persons
            modelBuilder.Entity<Person>().HasData(
                new Person()
                {
                    PersonId = Guid.Parse("c03bbe45-9aeb-4d24-99e0-4743016ffce9"),
                    PersonName = "Marguerite",
                    Email = "mwebsdale0@people.com.cn",
                    DateOfBirth = DateTime.Parse("1989-08-28"),
                    Gender = "Female",
                    CountryId = Guid.Parse("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                    Address = "4 Parkside Point",
                    ReceiveNewsLetters = false
                },
                new Person()
                {
                    PersonId = Guid.Parse("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"),
                    PersonName = "Ursa",
                    Email = "ushears1@globo.com",
                    DateOfBirth = DateTime.Parse("1990-10-05"),
                    Gender = "Female",
                    CountryId = Guid.Parse("14629847-905a-4a0e-9abe-80b61655c5cb"),
                    Address = "6 Morningstar Circle",
                    ReceiveNewsLetters = false
                },
                new Person()
                {
                    PersonId = Guid.Parse("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"),
                    PersonName = "Franchot",
                    Email = "fbowsher2@howstuffworks.com",
                    DateOfBirth = DateTime.Parse("1995-02-10"),
                    Gender = "Male",
                    CountryId = Guid.Parse("14629847-905a-4a0e-9abe-80b61655c5cb"),
                    Address = "73 Heath Avenue",
                    ReceiveNewsLetters = true
                },
                new Person()
                {
                    PersonId = Guid.Parse("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"),
                    PersonName = "Angie",
                    Email = "asarvar3@dropbox.com",
                    DateOfBirth = DateTime.Parse("1987-01-09"),
                    Gender = "Male",
                    CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    Address = "83187 Merry Drive",
                    ReceiveNewsLetters = true
                },
                new Person()
                {
                    PersonId = Guid.Parse("89e5f445-d89f-4e12-94e0-5ad5b235d704"),
                    PersonName = "Tani",
                    Email = "ttregona4@stumbleupon.com",
                    DateOfBirth = DateTime.Parse("1995-02-11"),
                    Gender = "Gender",
                    CountryId = Guid.Parse("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                    Address = "50467 Holy Cross Crossing",
                    ReceiveNewsLetters = false
                },
                new Person()
                {
                    PersonId = Guid.Parse("2a6d3738-9def-43ac-9279-0310edc7ceca"),
                    PersonName = "Mitchael",
                    Email = "mlingfoot5@netvibes.com",
                    DateOfBirth = DateTime.Parse("1988-01-04"),
                    Gender = "Male",
                    CountryId = Guid.Parse("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                    Address = "97570 Raven Circle",
                    ReceiveNewsLetters = false
                },
                new Person()
                {
                    PersonId = Guid.Parse("29339209-63f5-492f-8459-754943c74abf"),
                    PersonName = "Maddy",
                    Email = "mjarrell6@wisc.edu",
                    DateOfBirth = DateTime.Parse("1983-02-16"),
                    Gender = "Male",
                    CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    Address = "57449 Brown Way",
                    ReceiveNewsLetters = false
                },
                new Person()
                {
                    PersonId = Guid.Parse("ac660a73-b0b7-4340-abc1-a914257a6189"),
                    PersonName = "Pegeen",
                    Email = "pretchford7@virginia.edu",
                    DateOfBirth = DateTime.Parse("1998-12-02"),
                    Gender = "Female",
                    CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    Address = "4 Stuart Drive",
                    ReceiveNewsLetters = true
                },
                new Person()
                {
                    PersonId = Guid.Parse("012107df-862f-4f16-ba94-e5c16886f005"),
                    PersonName = "Hansiain",
                    Email = "hmosco8@tripod.com",
                    DateOfBirth = DateTime.Parse("1990-09-20"),
                    Gender = "Male",
                    CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    Address = "413 Sachtjen Way",
                    ReceiveNewsLetters = true
                },
                new Person()
                {
                    PersonId = Guid.Parse("cb035f22-e7cf-4907-bd07-91cfee5240f3"),
                    PersonName = "Lombard",
                    Email = "lwoodwing9@wix.com",
                    DateOfBirth = DateTime.Parse("1997-09-25"),
                    Gender = "Male",
                    CountryId = Guid.Parse("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                    Address = "484 Clarendon Court",
                    ReceiveNewsLetters = false
                },
                new Person()
                {
                    PersonId = Guid.Parse("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                    PersonName = "Minta",
                    Email = "mconachya@va.gov",
                    DateOfBirth = DateTime.Parse("1990-05-24"),
                    Gender = "Female",
                    CountryId = Guid.Parse("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                    Address = "2 Warrior Avenue",
                    ReceiveNewsLetters = true
                },
                new Person()
                {
                    PersonId = Guid.Parse("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                    PersonName = "Verene",
                    Email = "vklussb@nationalgeographic.com",
                    DateOfBirth = DateTime.Parse("1987-01-19"),
                    Gender = "Female",
                    CountryId = Guid.Parse("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                    Address = "9334 Fremont Street",
                    ReceiveNewsLetters = true
                }
                );

            //Fluent API
            modelBuilder.Entity<Person>().Property(temp => temp.TIN)
                .HasColumnName("TaxIdentificationNumber")
                .HasColumnType("varchar(8)")
                .HasDefaultValue("ABC12345");

            /*modelBuilder.Entity<Person>().HasIndex(temp => temp.TIN)
                .IsUnique();*/

            modelBuilder.Entity<Person>().HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8");

            //Table Relations
            /*modelBuilder.Entity<Person>(entity =>
            {
                entity.HasOne<Country>(c => c.Countries)
                .WithMany(p => p.Persons)
                .HasForeignKey(p => p.CountryId);
            });*/
        }

        public List<Person> sp_GetAllPersons()
        {
            return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        }

        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonId", person.PersonId),
                new SqlParameter("@PersonName", person.PersonName),
                new SqlParameter("@Email", person.Email),
                new SqlParameter("@DateOfBirth", person.DateOfBirth),
                new SqlParameter("@Gender", person.Gender),
                new SqlParameter("@CountryId", person.CountryId),
                new SqlParameter("@Address", person.Address),
                new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters)
            };

            return Database.ExecuteSqlRaw(@"
            EXECUTE [dbo].[InsertPerson]
            @PersonId, @PersonName, @Email, @DateOfBirth, @Gender, @CountryId,
            @Address, @ReceiveNewsLetters
            ", parameters);
        }
    }
}
