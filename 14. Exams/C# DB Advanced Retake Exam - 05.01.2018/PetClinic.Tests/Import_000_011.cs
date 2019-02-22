using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PetClinic.App;
using PetClinic.Data;

[TestFixture]
public class Import_000_011
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
    public void ImportVetsZeroTest1()
    {
        var context = this.serviceProvider.GetService<PetClinicContext>();

        var inputXml = @"<Vets><Vet><Name>M</Name><Profession>Emergency and Critical Care</Profession><Age>45</Age><PhoneNumber>0897665544</PhoneNumber></Vet><Vet><Name>InvalidNameInvalidNameInvalidNameInvalidNameInvalidName</Name><Profession>Emergency and Critical Care</Profession><Age>45</Age><PhoneNumber>0887665544</PhoneNumber></Vet></Vets>";

        var actualOutput = PetClinic.DataProcessor.Deserializer.ImportVets(context, inputXml).TrimEnd();
        var expectedOutput = "Error: Invalid data.\r\nError: Invalid data.";

        var assertContext = this.serviceProvider.GetService<PetClinicContext>();

        var expectedVetsCount = 0;
        var actualVetsCount = assertContext.Vets.Count();

        Assert.That(actualVetsCount, Is.EqualTo(expectedVetsCount), "Number of inserted Vets is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip, "ImportVets output is incorrect!");
    }
}