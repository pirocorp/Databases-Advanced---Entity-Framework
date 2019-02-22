using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PetClinic.App;
using PetClinic.Data;

[TestFixture]
public class Import_000_010
{
    private IServiceProvider serviceProvider;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services
            .AddDbContext<PetClinicContext>(
                options => options.UseInMemoryDatabase("PetClinic")
            );

        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile<PetClinicProfile>());

        this.serviceProvider = services.BuildServiceProvider();
    }

    [Test]
    public void ImportAnimalsZeroTest7()
    {
        var context = this.serviceProvider.GetService<PetClinicContext>();

        var inputJson = @"[{'Name':'DontInsertMe','Type':'cat','Age':3,'Passport':{'SerialNumber':'anothev150','OwnerName':'Owner','OwnerPhoneNumber':'12345','RegistrationDate':'15-04-2015'}},{'Name':'DontInsertMe','Type':'cat','Age':3,'Passport':{'SerialNumber':'anothev650','OwnerName':'Owner','OwnerPhoneNumber':'1234567890','RegistrationDate':'15-04-2015'}},{'Name':'DontInsertMe','Type':'cat','Age':3,'Passport':{'SerialNumber':'anothev950','OwnerName':'Owner','OwnerPhoneNumber':'+123456789123','RegistrationDate':'15-04-2015'}}]";

        var actualOutput = PetClinic.DataProcessor.Deserializer.ImportAnimals(context, inputJson).TrimEnd();
        var expectedOutput = "Error: Invalid data.\r\nError: Invalid data.\r\nError: Invalid data.";

        var assertContext = this.serviceProvider.GetService<PetClinicContext>();

        var expectedAnimalsCount = 0;
        var actualAnimalsCount = assertContext.Animals.Count();

        var expectedPassportsCount = 0;
        var actualPassportsCount = assertContext.Passports.Count();

        Assert.That(actualAnimalsCount, Is.EqualTo(expectedAnimalsCount), "Number of inserted Animals is incorrect!");

        Assert.That(expectedPassportsCount, Is.EqualTo(actualPassportsCount), "Number of inserted Passports is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip, "ImportAnimals output is incorrect!");
    }
}