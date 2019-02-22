using System;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

using PetClinic.App;
using PetClinic.Data;
using PetClinic.Models;

[TestFixture]
public class Bonus_000_002
{
    private IServiceProvider serviceProvider;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services
            .AddDbContext<PetClinicContext>(
                options => options.UseInMemoryDatabase("FastFood")
            );

        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile<PetClinicProfile>());

        this.serviceProvider = services.BuildServiceProvider();
    }

    [Test]
    public void BonusUpdateVetsZeroTest2()
    {
        var context = this.serviceProvider.GetService<PetClinicContext>();

        SeedDatabase(context);

        var expectedOutput = "Vet with phone number +359887123456 not found!";

        var actualOutput = PetClinic.DataProcessor.Bonus.UpdateVetProfession(context, "+359887123456", "Primary Care");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip, "UpdateVetProfession output is incorrect!");
    }

    private static void SeedDatabase(PetClinicContext context)
    {
        var vets = new Vet[]
        {
            new Vet {Name = "Edmond Halley", PhoneNumber = "+359284566778", Profession = "Veterinary Nursing"},
            new Vet {Name = "Niels Bohr", PhoneNumber = "0879557712",  Profession = "Internal Medicine"},
        };

        context.Vets.AddRange(vets);

        context.SaveChanges();
    }
}