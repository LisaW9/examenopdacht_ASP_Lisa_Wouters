using System;
using System.Linq;

namespace auto.Database
{
    public static class DataInitializer
    {

        public static void InitializeDatabase(PersonalDataContext database)
        {
            // de database aanmaken als ze niet bestaat.
            database.Database.EnsureCreated();

            InitializeCarTypes(database);
            InitializeOwners(database);
            InitializeCars(database);
        }

        private static void InitializeCarTypes(PersonalDataContext database)
        {
            // een item met deze structuur noemt men een tuple. Zie verder in de code hoe je deze dient te gebruiken. Op deze manier moet je
            // geen dedicated class hiervoor aanmaken.
            var data = new[]
            {
                ("Ford", "Fiesta"),
                ("Opel", "Astra"),
                ("Opel", "Corsa"),
            };

            foreach (var d in data)
            {
                // volgende code checked of er een persoon bestaat met die voornaam, achternaam combinatie van de tuple hierboven. Indien "ja",
                // komt hij niét aan de add methode.
                if (!database.Types.Any(x => x.Brand == d.Item1 && x.Model ==d.Item2))
                {
                    database.Types.Add(new CarType()
                    {
                        Brand = d.Item1,
                        Model = d.Item2,
                    });
                }

                //Na een persist of een delete dien je altijd SaveChanges() aan te roepen; dit commit de data naar de database.
                database.SaveChanges();
            }
        }

        private static void InitializeOwners(PersonalDataContext database)
        {
            // een item met deze structuur noemt men een tuple. Zie verder in de code hoe je deze dient te gebruiken. Op deze manier moet je
            // geen dedicated class hiervoor aanmaken.
            var data = new[]
            {
                ("Wouters", "Lisa"),
                ("Turrion", "Nico"),
                ("Wouters", "Elke"),
            };

            foreach (var d in data)
            {
                // volgende code checked of er een persoon bestaat met die voornaam, achternaam combinatie van de tuple hierboven. Indien "ja",
                // komt hij niét aan de add methode.
                if (!database.Owners.Any(x => x.Name == d.Item1 && x.FirstName == d.Item2))
                {
                    database.Owners.Add(new Owner()
                    {
                        Name = d.Item1,
                        FirstName = d.Item2
                    });
                }

                //Na een persist of een delete dien je altijd SaveChanges() aan te roepen; dit commit de data naar de database.
                database.SaveChanges();
            }
        }

        private static void InitializeCars(PersonalDataContext database)
        {
            // een item met deze structuur noemt men een tuple. Zie verder in de code hoe je deze dient te gebruiken. Op deze manier moet je
            // geen dedicated class hiervoor aanmaken.
            var data = new[]
            {
                ("Silver", new DateTime(2002, 01, 02), "1 NDB 357", database.Owners.FirstOrDefault(x => x.FirstName == "Lisa"), database.Types.FirstOrDefault(x => x.Brand == "Ford" && x.Model == "Fiesta")),
                ("Blue", new DateTime(1998, 05, 13), "1 SGX 186", database.Owners.FirstOrDefault(x => x.FirstName == "Nico"), database.Types.FirstOrDefault(x => x.Brand == "Opel" && x.Model == "Astra")),
                ("Black", new DateTime(1996, 12, 05), "1 DRO 007", database.Owners.FirstOrDefault(x => x.FirstName == "Elke"), database.Types.FirstOrDefault(x => x.Brand == "Opel" && x.Model == "Corsa")),
            };
            
            foreach (var d in data)
            {
                // volgende code checked of er een persoon bestaat met die voornaam, achternaam combinatie van de tuple hierboven. Indien "ja",
                // komt hij niét aan de add methode.
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

                //Na een persist of een delete dien je altijd SaveChanges() aan te roepen; dit commit de data naar de database.
                database.SaveChanges();
            }
        }
    }
}