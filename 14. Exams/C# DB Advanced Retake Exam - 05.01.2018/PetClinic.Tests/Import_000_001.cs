using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PetClinic.App;
using PetClinic.Data;

[TestFixture]
public class Import_000_001
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
    public void ImportAnimalAidsZeroTest1()
    {
        var context = this.serviceProvider.GetService<PetClinicContext>();

        var inputJson = @"[{'Name':'N','Price':20.00},{'Name':'InvalidNameInvalidNameInvalidName','Price':20.00}]";

        var actualOutput = PetClinic.DataProcessor.Deserializer.ImportAnimalAids(context, inputJson).TrimEnd();
        var expectedOutput = "Error: Invalid data.\r\nError: Invalid data.";

        var assertContext = this.serviceProvider.GetService<PetClinicContext>();

        var expectedAnimalAidsCount = 0;
        var actualAnimalAidsCount = assertContext.AnimalAids.Count();

        Assert.That(actualAnimalAidsCount, Is.EqualTo(expectedAnimalAidsCount), "Number of inserted AnimalAids is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip, "ImportAnimalAids output is incorrect!");
    }
}