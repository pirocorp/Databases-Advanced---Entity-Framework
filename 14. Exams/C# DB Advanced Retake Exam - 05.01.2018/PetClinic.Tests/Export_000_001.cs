using System;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

using PetClinic.App;
using PetClinic.Data;
using PetClinic.Models;

[TestFixture]
public class Export_000_001
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
    public void ExportAnimalsByOwnerPhoneNumberZeroTest()
    {
        var context = this.serviceProvider.GetService<PetClinicContext>();

        SeedDatabase(context);

        var expectedOutput = JToken.Parse("[{'OwnerName':'Ivan Ivanov','AnimalName':'Jessy','Age':3,'SerialNumber':'jessiii355','RegisteredOn':'05-11-2015'},{'OwnerName':'Ivan Ivanov','AnimalName':'Lucy','Age':6,'SerialNumber':'acattee321','RegisteredOn':'10-06-2015'}]"
        );

        var actualOutput =
            JToken.Parse(PetClinic.DataProcessor.Serializer.ExportAnimalsByOwnerPhoneNumber(context, "0887446123"));

        //var dates = actualOutput.

        var jsonsAreEqual = JToken.DeepEquals(expectedOutput, actualOutput);
        Assert.That(jsonsAreEqual, Is.True, "ExportAnimalsByOwnerPhoneNumber output is incorrect!");
    }

    private static void SeedDatabase(PetClinicContext context)
    {
        string animalsJson = "[{'Name':'Bella','Age':2,'Passport':{'SerialNumber':'etyhGgH678','OwnerName':'Sheldon Cooper','OwnerPhoneNumber':'0897556446','RegistrationDate':'2014-03-12T00:00:00'}},{'Name':'Lucy','Age':6,'Passport':{'SerialNumber':'acattee321','OwnerName':'Ivan Ivanov','OwnerPhoneNumber':'0887446123','RegistrationDate':'2015-06-10T00:00:00'}},{'Name':'Chloe','Age':1,'Passport':{'SerialNumber':'ragjkll456','OwnerName':'Sheldon Cooper','OwnerPhoneNumber':'0897556446','RegistrationDate':'2015-08-11T00:00:00'}},{'Name':'Maggie','Age':4,'Passport':{'SerialNumber':'clasdfk325','OwnerName':'Ivan Petrov','OwnerPhoneNumber':'0897556676','RegistrationDate':'2015-07-30T00:00:00'}},{'Name':'Shadow','Age':8,'Passport':{'SerialNumber':'kljsdfk325','OwnerName':'Simona Hristova','OwnerPhoneNumber':'0899446676','RegistrationDate':'2015-11-30T00:00:00'}},{'Name':'Alfie','Age':10,'Passport':{'SerialNumber':'kljsdfk355','OwnerName':'Jason Borne','OwnerPhoneNumber':'0889647676','RegistrationDate':'2015-11-05T00:00:00'}},{'Name':'Arrow','Age':10,'Passport':{'SerialNumber':'arrowww355','OwnerName':'Leonardo Dicaprio','OwnerPhoneNumber':'0889650676','RegistrationDate':'2015-11-09T00:00:00'}},{'Name':'Barker','Age':5,'Passport':{'SerialNumber':'barkeer355','OwnerName':'Tonny Dobrev','OwnerPhoneNumber':'0899450766','RegistrationDate':'2015-11-10T00:00:00'}},{'Name':'Jessy','Age':3,'Passport':{'SerialNumber':'jessiii355','OwnerName':'Ivan Ivanov','OwnerPhoneNumber':'0887446123','RegistrationDate':'2015-11-05T00:00:00'}},{'Name':'Bernie','Age':3,'Passport':{'SerialNumber':'bernied355','OwnerName':'Annie Ivanov','OwnerPhoneNumber':'0889446123','RegistrationDate':'2015-11-05T00:00:00'}},{'Name':'Binky','Age':3,'Passport':{'SerialNumber':'binkBen355','OwnerName':'Annie Ivanov','OwnerPhoneNumber':'0883217446','RegistrationDate':'2015-11-05T00:00:00'}},{'Name':'Binky','Age':3,'Passport':{'SerialNumber':'binkdog355','OwnerName':'Thomas Berger','OwnerPhoneNumber':'0887226234','RegistrationDate':'2015-12-19T00:00:00'}},{'Name':'Charlie','Age':3,'Passport':{'SerialNumber':'bergerr355','OwnerName':'Thomas Berger','OwnerPhoneNumber':'0887226234','RegistrationDate':'2015-12-19T00:00:00'}},{'Name':'Cleo','Age':7,'Passport':{'SerialNumber':'cleodog355','OwnerName':'Renny Gabriel','OwnerPhoneNumber':'+359897765122','RegistrationDate':'2015-06-22T00:00:00'}},{'Name':'Leo','Age':9,'Passport':{'SerialNumber':'jghjyuu355','OwnerName':'Renny Gabriel','OwnerPhoneNumber':'+359897765122','RegistrationDate':'2015-06-22T00:00:00'}}]";

        var animals = JsonConvert.DeserializeObject<Animal[]>(animalsJson);

        context.Animals.AddRange(animals);

        context.SaveChanges();
    }
}