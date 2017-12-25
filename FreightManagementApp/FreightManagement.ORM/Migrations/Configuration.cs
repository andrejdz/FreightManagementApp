namespace FreightManagement.ORM.Migrations
{
    using FreightManagement.Domain.Model;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FreightManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FreightManagementContext context)
        {
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Customers', RESEED, 6);"); //6
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Trucks', RESEED, 8);"); //8
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Orders', RESEED, 6);"); //6
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Cargoes', RESEED, 21);"); //21

            //#region Customers
            //List<Customer> customerList = new List<Customer>();

            //customerList.Add(
            //    new Customer()
            //    {
            //        Name = "Activision Blizzard",
            //        Manager = "Brian Kelly",
            //        Telephone = "+1-315-255-200",
            //        Fax = "+1-315-255-200"
            //    });

            //customerList.Add(
            //    new Customer()
            //    {
            //        Name = "Walmart",
            //        Manager = "Sam Walton",
            //        Telephone = "+1-800-925-6278",
            //        Fax = string.Empty
            //    });

            //customerList.Add(
            //    new Customer()
            //    {
            //        Name = "Apple Inc.",
            //        Manager = "Tim Cook",
            //        Telephone = "+1–800–854–3680",
            //        Fax = "+1–800–854–3685"
            //    });

            //customerList.Add(
            //    new Customer()
            //    {
            //        Name = "ExxonMobil",
            //        Manager = "Darren Woods",
            //        Telephone = "+1–800–245–3030",
            //        Fax = "+1–800–245-3040"
            //    });

            //customerList.Add(
            //    new Customer()
            //    {
            //        Name = "Atari",
            //        Manager = "Nolan Bushnell",
            //        Telephone = "+1–800–515–8120",
            //        Fax = "+1–800–515-8110"
            //    });

            //customerList.Add(
            //    new Customer()
            //    {
            //        Name = "IBM",
            //        Manager = "Ginni Rometty",
            //        Telephone = "+1–800–645–4786",
            //        Fax = "+1–800–645-4787"
            //    });

            //foreach(var item in customerList)
            //{
            //    context.Customers
            //        .AddOrUpdate(c => new { c.Name, c.Manager }, item);
            //}
            //#endregion

            //#region Trucks
            //List<Truck> truckList = new List<Truck>();

            //truckList.Add(
            //    new Truck()
            //    {
            //        Name = "Mercedes Benz 1844 LS",
            //        CarryingCapacity = 30,
            //        CostPerKm = 12,
            //        Status = AvailabilityEnum.Busy
            //    });

            //truckList.Add(
            //    new Truck()
            //    {
            //        Name = "DAF DAF95.480XF",
            //        CarryingCapacity = 26,
            //        CostPerKm = 10,
            //        Status = AvailabilityEnum.Busy
            //    });

            //truckList.Add(
            //    new Truck()
            //    {
            //        Name = "Volvo FH12 6x2",
            //        CarryingCapacity = 23,
            //        CostPerKm = 8,
            //        Status = AvailabilityEnum.Busy
            //    });

            //truckList.Add(
            //    new Truck()
            //    {
            //        Name = "Mercedes Benz 1218 NL54",
            //        CarryingCapacity = 16,
            //        CostPerKm = 5,
            //        Status = AvailabilityEnum.Busy
            //    });

            //truckList.Add(
            //    new Truck()
            //    {
            //        Name = "Scania S730",
            //        CarryingCapacity = 32,
            //        CostPerKm = 15,
            //        Status = AvailabilityEnum.Busy
            //    });

            //truckList.Add(
            //    new Truck()
            //    {
            //        Name = "Renault PREMIUM 410 DXI",
            //        CarryingCapacity = 28,
            //        CostPerKm = 13,
            //        Status = AvailabilityEnum.Busy
            //    });

            //truckList.Add(
            //    new Truck()
            //    {
            //        Name = "Iveco Eurotech 440",
            //        CarryingCapacity = 24,
            //        CostPerKm = 10,
            //        Status = AvailabilityEnum.Free
            //    });

            //truckList.Add(
            //    new Truck()
            //    {
            //        Name = "DAF CF75",
            //        CarryingCapacity = 31,
            //        CostPerKm = 14,
            //        Status = AvailabilityEnum.Free
            //    });

            //foreach(var item in truckList)
            //{
            //    context.Trucks
            //        .AddOrUpdate(t => new { t.Name, t.CarryingCapacity }, item);
            //}
            //#endregion

            //#region Orders
            //List<Order> orderList = new List<Order>();

            //orderList.Add(
            //    new Order()
            //    {
            //        Price = 8976,
            //        Distance = 748,
            //        Country = "USA",
            //        City = "Chicago",
            //        Address = "2525 S Michigan Ave",
            //        Term = 15,
            //        Status = StatusEnum.Processing,
            //        TruckId = 1,
            //        CustomerId = 2
            //    });

            //orderList.Add(
            //    new Order()
            //    {
            //        Price = 5136,
            //        Distance = 642,
            //        Country = "USA",
            //        City = "Washington",
            //        Address = "1145 17th St NW",
            //        Term = 12,
            //        Status = StatusEnum.Processing,
            //        TruckId = 3,
            //        CustomerId = 3,
            //    });

            //orderList.Add(
            //    new Order()
            //    {
            //        Price = 12780,
            //        Distance = 852,
            //        Country = "USA",
            //        City = "Las Vegas",
            //        Address = "301 Fremont St",
            //        Term = 23,
            //        Status = StatusEnum.Processing,
            //        TruckId = 5,
            //        CustomerId = 4,
            //    });

            //orderList.Add(
            //    new Order()
            //    {
            //        Price = 5490,
            //        Distance = 549,
            //        Country = "USA",
            //        City = "San Francisco",
            //        Address = "2000 Mission St",
            //        Term = 18,
            //        Status = StatusEnum.Processing,
            //        TruckId = 2,
            //        CustomerId = 5
            //    });

            //orderList.Add(
            //    new Order()
            //    {
            //        Id = 5,
            //        Price = 3757,
            //        Distance = 289,
            //        Country = "USA",
            //        City = "Seattle",
            //        Address = "2701 Beacon Ave S",
            //        Term = 7,
            //        Status = StatusEnum.Processing,
            //        TruckId = 6,
            //        CustomerId = 1,
            //    });

            //orderList.Add(
            //    new Order()
            //    {
            //        Id = 6,
            //        Price = 1820,
            //        Distance = 364,
            //        Country = "USA",
            //        City = "Atlanta",
            //        Address = "2701 Beacon Ave S",
            //        Term = 12,
            //        Status = StatusEnum.Processing,
            //        TruckId = 4,
            //        CustomerId = 6
            //    });

            //foreach(var item in orderList)
            //{
            //    context.Orders
            //        .AddOrUpdate(o => new
            //        {
            //            o.Price,
            //            o.Distance,
            //            o.City,
            //            o.Address,
            //            o.Term
            //        },
            //        item);
            //}
            //#endregion

            //#region Cargoes
            //List<Cargo> cargoList = new List<Cargo>();

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Apples",
            //        Weight = 10,
            //        OrderId = 1,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Oranges",
            //        Weight = 7,
            //        OrderId = 1,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Pineapples",
            //        Weight = 5,
            //        OrderId = 1,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Peaches",
            //        Weight = 6,
            //        OrderId = 1,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Pork",
            //        Weight = 12,
            //        OrderId = 2,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Beaf",
            //        Weight = 9,
            //        OrderId = 2,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Phones",
            //        Weight = 11,
            //        OrderId = 3,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Laptops",
            //        Weight = 9,
            //        OrderId = 3,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Screens",
            //        Weight = 8,
            //        OrderId = 3,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Keyboards and mouses",
            //        Weight = 3,
            //        OrderId = 3,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Furniture",
            //        Weight = 20,
            //        OrderId = 4,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Mirrors",
            //        Weight = 4,
            //        OrderId = 4,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Water",
            //        Weight = 12,
            //        OrderId = 5,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Juice",
            //        Weight = 9,
            //        OrderId = 5,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Beverage",
            //        Weight = 6,
            //        OrderId = 5,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Alcohol",
            //        Weight = 9,
            //        OrderId = 6,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Beer",
            //        Weight = 5,
            //        OrderId = 6,
            //        Status = StatusCargoEnum.InOrder
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Dog's food",
            //        Weight = 10,
            //        Status = StatusCargoEnum.Waiting
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Dog's toys",
            //        Weight = 7,
            //        Status = StatusCargoEnum.Waiting
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Cat's food",
            //        Weight = 6,
            //        Status = StatusCargoEnum.Waiting
            //    });

            //cargoList.Add(
            //    new Cargo()
            //    {
            //        Description = "Cat's toys",
            //        Weight = 3,
            //        Status = StatusCargoEnum.Waiting
            //    });

            //foreach(var item in cargoList)
            //{
            //    context.Cargoes
            //        .AddOrUpdate(c => new
            //        {
            //            c.Description,
            //            c.Weight
            //        },
            //        item);
            //}
            //#endregion
        }
    }
}
