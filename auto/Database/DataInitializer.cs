using System;
using System.Linq;

namespace auto.Database
{
    public static class DataInitializer
    {

        public static void InitializeDatabase(PersonalDataContext database)
        {
            database.Database.EnsureCreated();

            InitializeCarTypes(database);
            InitializeOwners(database);
            InitializeCars(database);
        }

        private static void InitializeCarTypes(PersonalDataContext database)
        {
            var data = new[]
            {
                ("Chevrolet", "Camaro"),
                ("Ford", "Fiesta"),
                ("Ford", "Mustang"),
                ("Opel", "Astra"),
                ("Opel", "Corsa"),
                ("Mercedes-Benz", "C-Klasse"),
            };

            foreach (var d in data)
            {
                if (!database.Types.Any(x => x.Brand == d.Item1 && x.Model ==d.Item2))
                {
                    database.Types.Add(new CarType()
                    {
                        Brand = d.Item1,
                        Model = d.Item2,
                    });
                }

                database.SaveChanges();
            }
        }

        private static void InitializeOwners(PersonalDataContext database)
        {
            var data = new[]
            {
                ("Wouters", "Lisa"),
                ("Turrion", "Nico"),
                ("Wouters", "Elke"),
                ("Wouters", "Tony"),
                ("eigenaar", "Geen"),
            };

            foreach (var d in data)
            {
                if (!database.Owners.Any(x => x.Name == d.Item1 && x.FirstName == d.Item2))
                {
                    database.Owners.Add(new Owner()
                    {
                        Name = d.Item1,
                        FirstName = d.Item2
                    });
                }

                database.SaveChanges();
            }
        }

        private static void InitializeCars(PersonalDataContext database)
        {
            var data = new[]
            {
                ("Silver", new DateTime(2002, 01, 02), "1 NDB 357", database.Owners.FirstOrDefault(x => x.FirstName == "Lisa"), database.Types.FirstOrDefault(x => x.Brand == "Ford" && x.Model == "Fiesta")),
                ("Blue", new DateTime(1998, 05, 13), "1 SGX 186", database.Owners.FirstOrDefault(x => x.FirstName == "Nico"), database.Types.FirstOrDefault(x => x.Brand == "Opel" && x.Model == "Astra")),
                ("Black", new DateTime(1996, 12, 05), "1 ODR 573", database.Owners.FirstOrDefault(x => x.FirstName == "Elke"), database.Types.FirstOrDefault(x => x.Brand == "Opel" && x.Model == "Corsa")),
                ("White", new DateTime(2016, 08, 12), "1 ULR 648", database.Owners.FirstOrDefault(x => x.FirstName == "Tony"), database.Types.FirstOrDefault(x => x.Brand == "Mercedes" && x.Model == "C-Klasse Berline")),
            };
            
            foreach (var d in data)
            {
                if (!database.Cars.Any(x => x.LicensePlate == d.Item3))
                {
                    database.Cars.Add(new Car()
                    {
                        Color = d.Item1,
                        PurchaseDate = d.Item2,
                        LicensePlate = d.Item3,
                        Owner = d.Item4,
                        Type = d.Item5,
                    });
                }

                database.SaveChanges();
            }
        }
    }
}