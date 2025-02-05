//seed out database
using DealershipApp.Data;
using DealershipApp.Models;

namespace DealershipApp
{
    public class Seeder
    {
        private readonly DataContext dataContext;

        public Seeder(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeederDataContext()
        {
            if (!dataContext.DealershipVehicles.Any()) //if there isnt any data in there already do...
            {
                //declare countries
                var CountryCAN = new Country()
                { Name = "Canada" };
                var CountryUSA = new Country()
                { Name = "USA" };

                var dealershipVehicles = new List<DealershipVehicle>()
                {
                    new DealershipVehicle()
                    {
                        Dealership = new Dealership()
                        {
                            Name = "Parkview BMW",
                            IsShowRoom = true,
                            Staff = new List<Staff>()
                            {
                                new Staff { FirstName="John", LastName="Soros", Profession="Dept Manager"},
                                new Staff { FirstName="Bob", LastName="Smith", Profession="Mechanic"},
                                new Staff { FirstName="Susy", LastName="Loros", Profession="Sales Representative"},
                                new Staff { FirstName="Ashley", LastName="Locks", Profession="Customer Service Rep"},
                                new Staff { FirstName="Frank", LastName="Pitt", Profession="Concierce"}
                            },
                            //Country = new Country()
                            //{
                            //    Name = "Canada" 
                            //}
                            Country = CountryCAN
                        },

                        Vehicle = new Vehicle()
                        {
                            Name = "X5 M Sport",
                            VehicleYear = 2018,
                            Colour = "Blue",
                            IsAutomatic = true,
                            OrderDate = new DateTime(2020,3,1),
                            Customer = new Customer()
                            {
                                FirstName = "Mike",
                                LastName = "Sholz",
                                Phone = "416-906-5739"
                            },
                            Services = new List<Service>()
                            {
                                new Service { TypeofService="Maintenance", RepairDate= new DateTime(2025,11,30), Description="Oil change" },
                                new Service { TypeofService="Repair", RepairDate= new DateTime(2023,1,20), Description="Oxygen sensor" },
                                new Service { TypeofService="Assessment", RepairDate= new DateTime(2021,9,14), Description="Check engine light" }
                                
                            },
                            StoreId = 1
                        }
                    },

                    new DealershipVehicle()
                    {
                        Dealership = new Dealership()
                        {
                            Name = "Honda Odyssey",
                            IsShowRoom = true,
                            Staff = new List<Staff>()
                            {
                                new Staff { FirstName="Sam", LastName="Soros", Profession="Dept Manager"},
                                new Staff { FirstName="Jill", LastName="Smith", Profession="Mechanic"},
                                new Staff { FirstName="Fred", LastName="Loros", Profession="Sales Representative"},
                                new Staff { FirstName="Toby", LastName="Locks", Profession="Customer Service Rep"},
                                new Staff { FirstName="Hugh", LastName="Pitt", Profession="Concierce"}
                            },
                            //Country = new Country()
                            //{
                            //    Name = "USA"
                            //}
                            Country = CountryUSA
                        },

                        Vehicle = new Vehicle()
                        {
                            Name = "Civic Sport",
                            VehicleYear = 2015,
                            Colour = "Green",
                            IsAutomatic = true,
                            OrderDate = new DateTime(2016,6,4),
                            Customer = new Customer()
                            {
                                FirstName = "Sally",
                                LastName = "Chubs",
                                Phone = "416-666-5389"
                            },
                            Services = new List<Service>()
                            {
                                new Service { TypeofService="Maintenance", RepairDate= new DateTime(2025,11,30), Description="Brake change" },
                                new Service { TypeofService="Repair", RepairDate= new DateTime(2023,1,20), Description="Hood damage" },
                                new Service { TypeofService="Assessment", RepairDate= new DateTime(2021,9,14), Description="headlights off" }

                            },
                            StoreId = 2
                        }
                    },
                    new DealershipVehicle()
                    {
                        Dealership = new Dealership()
                        {
                            Name = "Mercedes Downtown",
                            IsShowRoom = true,
                            Staff = new List<Staff>()
                            {
                                new Staff { FirstName="George", LastName="Soros", Profession="Dept Manager"},
                                new Staff { FirstName="Yulia", LastName="Smith", Profession="Mechanic"},
                                new Staff { FirstName="Robert", LastName="Loros", Profession="Sales Representative"},
                                new Staff { FirstName="Walter", LastName="Locks", Profession="Customer Service Rep"},
                                new Staff { FirstName="Nadia", LastName="Pitt", Profession="Concierce"}
                            },
                            //Country = new Country()
                            //{
                            //    Name = "Canada"
                            //}
                            Country = CountryCAN
                        },

                        Vehicle = new Vehicle()
                        {
                            Name = "C230",
                            VehicleYear = 2023,
                            Colour = "White",
                            IsAutomatic = true,
                            OrderDate = new DateTime(2024,6,8),
                            Customer = new Customer()
                            {
                                FirstName = "Olga",
                                LastName = "Heinrech",
                                Phone = "416-876-6639"
                            },
                            Services = new List<Service>()
                            {
                                new Service { TypeofService="Maintenance", RepairDate= new DateTime(2025,11,30), Description="Brake fluid" },
                                new Service { TypeofService="Repair", RepairDate= new DateTime(2023,1,20), Description="Flat tire" },
                                new Service { TypeofService="Assessment", RepairDate= new DateTime(2021,9,14), Description="Bumper scratch" }

                            },
                            StoreId = 3
                        }
                    }
                };
                dataContext.DealershipVehicles.AddRange(dealershipVehicles);
                dataContext.SaveChanges();
            }
        }
    }
}
