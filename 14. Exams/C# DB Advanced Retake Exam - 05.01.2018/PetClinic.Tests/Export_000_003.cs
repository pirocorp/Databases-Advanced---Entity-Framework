using System;
using System.Xml.Linq;
using System.Xml.XPath;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

using PetClinic.App;
using PetClinic.Data;
using PetClinic.Models;

[TestFixture]
public class Export_000_003
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
    public void ExportProceduresZeroTest()
    {
        var context = this.serviceProvider.GetService<PetClinicContext>();

        SeedDatabase(context);

        var expectedXml =
        #region Xml 
@"<Procedures>
  <Procedure>
    <Passport>acattee321</Passport>
    <OwnerNumber>0887446123</OwnerNumber>
    <DateTime>14-01-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Internal Deworming</Name>
        <Price>8.00</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Nasal Bordetella</Name>
        <Price>5.60</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>21.10</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>kljsdfk325</Passport>
    <OwnerNumber>0899446676</OwnerNumber>
    <DateTime>19-01-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>H3N2</Name>
        <Price>28.00</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Lyme Test</Name>
        <Price>15.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>43.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>ragjkll456</Passport>
    <OwnerNumber>0897556446</OwnerNumber>
    <DateTime>29-01-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Nasal Bordetella</Name>
        <Price>5.60</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Lepto 4</Name>
        <Price>15.90</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>21.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>barkeer355</Passport>
    <OwnerNumber>0899450766</OwnerNumber>
    <DateTime>03-02-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Injectable Bordetella</Name>
        <Price>8.40</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Canine Heartworm Test</Name>
        <Price>15.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>23.40</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>etyhGgH678</Passport>
    <OwnerNumber>0897556446</OwnerNumber>
    <DateTime>14-02-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>H3N2</Name>
        <Price>28.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>28.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>jessiii355</Passport>
    <OwnerNumber>0887446123</OwnerNumber>
    <DateTime>23-03-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Canine Rabbies Vaccine</Name>
        <Price>10.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>10.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>acattee321</Passport>
    <OwnerNumber>0887446123</OwnerNumber>
    <DateTime>04-04-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Lyme Test</Name>
        <Price>15.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>15.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>bernied355</Passport>
    <OwnerNumber>0889446123</OwnerNumber>
    <DateTime>15-04-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Lyme Test</Name>
        <Price>15.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>22.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>cleodog355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>25-04-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>H3N8</Name>
        <Price>30.00</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Bordetella</Name>
        <Price>7.50</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>37.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>cleodog355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>12-05-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Internal Deworming</Name>
        <Price>8.00</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>15.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>jghjyuu355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>14-05-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Nasal Bordetella</Name>
        <Price>5.60</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Lepto 4</Name>
        <Price>15.90</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>H3N2</Name>
        <Price>28.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>49.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>bergerr355</Passport>
    <OwnerNumber>0887226234</OwnerNumber>
    <DateTime>05-06-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Internal Deworming</Name>
        <Price>8.00</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>15.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>arrowww355</Passport>
    <OwnerNumber>0889650676</OwnerNumber>
    <DateTime>17-06-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>H3N2</Name>
        <Price>28.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>28.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>bernied355</Passport>
    <OwnerNumber>0889446123</OwnerNumber>
    <DateTime>17-06-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Nasal Bordetella</Name>
        <Price>5.60</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Lepto 4</Name>
        <Price>15.90</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>21.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>barkeer355</Passport>
    <OwnerNumber>0899450766</OwnerNumber>
    <DateTime>15-07-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Lepto 4</Name>
        <Price>15.90</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Lyme Test</Name>
        <Price>15.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>30.90</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>cleodog355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>17-07-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Canine Rabbies Vaccine</Name>
        <Price>10.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>17.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>jghjyuu355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>19-07-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>7.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>cleodog355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>22-07-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>H3N8</Name>
        <Price>30.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>30.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>ragjkll456</Passport>
    <OwnerNumber>0897556446</OwnerNumber>
    <DateTime>25-07-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Canine Rabbies Vaccine</Name>
        <Price>10.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>10.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>cleodog355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>18-08-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Nasal Bordetella</Name>
        <Price>5.60</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>PureVax Rabies</Name>
        <Price>7.80</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>13.40</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>jghjyuu355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>19-08-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Injectable Bordetella</Name>
        <Price>8.40</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>8.40</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>arrowww355</Passport>
    <OwnerNumber>0889650676</OwnerNumber>
    <DateTime>04-09-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Lyme Test</Name>
        <Price>15.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>22.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>cleodog355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>29-09-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Canine Rabbies Vaccine</Name>
        <Price>10.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>17.50</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>kljsdfk355</Passport>
    <OwnerNumber>0889647676</OwnerNumber>
    <DateTime>07-10-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Lepto 4</Name>
        <Price>15.90</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Canine Heartworm Test</Name>
        <Price>15.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>30.90</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>acattee321</Passport>
    <OwnerNumber>0887446123</OwnerNumber>
    <DateTime>22-10-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Lyme Test</Name>
        <Price>15.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>15.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>cleodog355</Passport>
    <OwnerNumber>+359897765122</OwnerNumber>
    <DateTime>27-11-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>H3N8</Name>
        <Price>30.00</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>30.00</TotalPrice>
  </Procedure>
  <Procedure>
    <Passport>bergerr355</Passport>
    <OwnerNumber>0887226234</OwnerNumber>
    <DateTime>12-12-2016</DateTime>
    <AnimalAids>
      <AnimalAid>
        <Name>Internal Deworming</Name>
        <Price>8.00</Price>
      </AnimalAid>
      <AnimalAid>
        <Name>Fecal Test</Name>
        <Price>7.50</Price>
      </AnimalAid>
    </AnimalAids>
    <TotalPrice>15.50</TotalPrice>
  </Procedure>
</Procedures>";
        #endregion

        var expectedOutput = XDocument.Parse(expectedXml);

        var actualXml = PetClinic.DataProcessor.Serializer.ExportAllProcedures(context);

        var actualOutput =
            XDocument.Parse(actualXml);

        var prices = actualOutput.XPathSelectElements("/Procedures/Procedure/AnimalAids/AnimalAid/Price");
        foreach (var price in prices)
        {
            price.Value = decimal.Parse(price.Value).ToString("F2");
        }

        var totalPrices = actualOutput.XPathSelectElements("/Procedures/Procedure/TotalPrice");
        foreach (var price in totalPrices)
        {
            price.Value = decimal.Parse(price.Value).ToString("F2");
        }

        var xmlsAreEqual = XNode.DeepEquals(expectedOutput, actualOutput);
        Assert.That(xmlsAreEqual, Is.True, "ExportProcedures output is incorrect!");
    }

    private static void SeedDatabase(PetClinicContext context)
    {
        string animalsJson = "[{'Name':'Bella','Passport':{'SerialNumber':'etyhGgH678','OwnerPhoneNumber':'0897556446','RegistrationDate':'2014-03-12T00:00:00'}},{'Name':'Lucy','Passport':{'SerialNumber':'acattee321','OwnerPhoneNumber':'0887446123','RegistrationDate':'2015-06-10T00:00:00'}},{'Name':'Chloe','Passport':{'SerialNumber':'ragjkll456','OwnerPhoneNumber':'0897556446','RegistrationDate':'2015-08-11T00:00:00'}},{'Name':'Maggie','Passport':{'SerialNumber':'clasdfk325','OwnerPhoneNumber':'0897556676','RegistrationDate':'2015-07-30T00:00:00'}},{'Name':'Shadow','Passport':{'SerialNumber':'kljsdfk325','OwnerPhoneNumber':'0899446676','RegistrationDate':'2015-11-30T00:00:00'}},{'Name':'Alfie','Passport':{'SerialNumber':'kljsdfk355','OwnerPhoneNumber':'0889647676','RegistrationDate':'2015-11-05T00:00:00'}},{'Name':'Arrow','Passport':{'SerialNumber':'arrowww355','OwnerPhoneNumber':'0889650676','RegistrationDate':'2015-11-09T00:00:00'}},{'Name':'Barker','Passport':{'SerialNumber':'barkeer355','OwnerPhoneNumber':'0899450766','RegistrationDate':'2015-11-10T00:00:00'}},{'Name':'Jessy','Passport':{'SerialNumber':'jessiii355','OwnerPhoneNumber':'0887446123','RegistrationDate':'2015-11-05T00:00:00'}},{'Name':'Bernie','Passport':{'SerialNumber':'bernied355','OwnerPhoneNumber':'0889446123','RegistrationDate':'2015-11-05T00:00:00'}},{'Name':'Binky','Passport':{'SerialNumber':'binkBen355','OwnerPhoneNumber':'0883217446','RegistrationDate':'2015-11-05T00:00:00'}},{'Name':'Binky','Passport':{'SerialNumber':'binkdog355','OwnerPhoneNumber':'0887226234','RegistrationDate':'2015-12-19T00:00:00'}},{'Name':'Charlie','Passport':{'SerialNumber':'bergerr355','OwnerPhoneNumber':'0887226234','RegistrationDate':'2015-12-19T00:00:00'}},{'Name':'Cleo','Passport':{'SerialNumber':'cleodog355','OwnerPhoneNumber':'+359897765122','RegistrationDate':'2015-06-22T00:00:00'}},{'Name':'Leo','Passport':{'SerialNumber':'jghjyuu355','OwnerPhoneNumber':'+359897765122','RegistrationDate':'2015-06-22T00:00:00'}}]";
        string vetsJson = "[{'Name':'Michael Jordan','PhoneNumber':'0897665544'},{'Name':'Edmond Halley','PhoneNumber':'+359284566778'},{'Name':'Niels Bohr','PhoneNumber':'0879557712'},{'Name':'Werner Heisenberg','PhoneNumber':'0879535712'},{'Name':'Jennifer Evans','PhoneNumber':'0873935712'}]";
        string animalAidsJson = "[{'Name':'Internal Deworming','Price':8.00},{'Name':'Fecal Test','Price':7.50},{'Name':'H3N8','Price':30.00},{'Name':'Nasal Bordetella','Price':5.60},{'Name':'Bordetella','Price':7.50},{'Name':'Lepto 4','Price':15.90},{'Name':'Injectable Bordetella','Price':8.40},{'Name':'H3N2','Price':28.00},{'Name':'Canine Rabbies Vaccine','Price':10.00},{'Name':'Canine Heartworm Test','Price':15.00},{'Name':'Lyme Test','Price':15.00},{'Name':'PureVax Rabies','Price':7.80}]";
        string proceduresJson = "[{'DateTime':'2016-01-14T00:00:00','AnimalId':2,'VetId':3,'ProcedureAnimalAids':[{'ProcedureId':1,'AnimalAidId':1},{'ProcedureId':1,'AnimalAidId':2},{'ProcedureId':1,'AnimalAidId':4}]},{'DateTime':'2016-05-14T00:00:00','AnimalId':15,'VetId':5,'ProcedureAnimalAids':[{'ProcedureId':2,'AnimalAidId':4},{'ProcedureId':2,'AnimalAidId':6},{'ProcedureId':2,'AnimalAidId':8}]},{'DateTime':'2016-07-19T00:00:00','AnimalId':15,'VetId':5,'ProcedureAnimalAids':[{'ProcedureId':3,'AnimalAidId':2}]},{'DateTime':'2016-01-29T00:00:00','AnimalId':3,'VetId':5,'ProcedureAnimalAids':[{'ProcedureId':4,'AnimalAidId':4},{'ProcedureId':4,'AnimalAidId':6}]},{'DateTime':'2016-01-19T00:00:00','AnimalId':5,'VetId':3,'ProcedureAnimalAids':[{'ProcedureId':5,'AnimalAidId':8},{'ProcedureId':5,'AnimalAidId':11}]},{'DateTime':'2016-10-07T00:00:00','AnimalId':6,'VetId':4,'ProcedureAnimalAids':[{'ProcedureId':6,'AnimalAidId':6},{'ProcedureId':6,'AnimalAidId':10}]},{'DateTime':'2016-04-04T00:00:00','AnimalId':2,'VetId':2,'ProcedureAnimalAids':[{'ProcedureId':7,'AnimalAidId':11}]},{'DateTime':'2016-10-22T00:00:00','AnimalId':2,'VetId':2,'ProcedureAnimalAids':[{'ProcedureId':8,'AnimalAidId':11}]},{'DateTime':'2016-11-27T00:00:00','AnimalId':14,'VetId':4,'ProcedureAnimalAids':[{'ProcedureId':9,'AnimalAidId':3}]},{'DateTime':'2016-07-22T00:00:00','AnimalId':14,'VetId':4,'ProcedureAnimalAids':[{'ProcedureId':10,'AnimalAidId':3}]},{'DateTime':'2016-07-25T00:00:00','AnimalId':3,'VetId':4,'ProcedureAnimalAids':[{'ProcedureId':11,'AnimalAidId':9}]},{'DateTime':'2016-06-17T00:00:00','AnimalId':10,'VetId':4,'ProcedureAnimalAids':[{'ProcedureId':12,'AnimalAidId':4},{'ProcedureId':12,'AnimalAidId':6}]},{'DateTime':'2016-07-17T00:00:00','AnimalId':14,'VetId':3,'ProcedureAnimalAids':[{'ProcedureId':13,'AnimalAidId':2},{'ProcedureId':13,'AnimalAidId':9}]},{'DateTime':'2016-02-14T00:00:00','AnimalId':1,'VetId':4,'ProcedureAnimalAids':[{'ProcedureId':14,'AnimalAidId':8}]},{'DateTime':'2016-04-25T00:00:00','AnimalId':14,'VetId':3,'ProcedureAnimalAids':[{'ProcedureId':15,'AnimalAidId':3},{'ProcedureId':15,'AnimalAidId':5}]},{'DateTime':'2016-08-18T00:00:00','AnimalId':14,'VetId':2,'ProcedureAnimalAids':[{'ProcedureId':16,'AnimalAidId':4},{'ProcedureId':16,'AnimalAidId':12}]},{'DateTime':'2016-08-19T00:00:00','AnimalId':15,'VetId':2,'ProcedureAnimalAids':[{'ProcedureId':17,'AnimalAidId':7}]},{'DateTime':'2016-07-15T00:00:00','AnimalId':8,'VetId':2,'ProcedureAnimalAids':[{'ProcedureId':18,'AnimalAidId':6},{'ProcedureId':18,'AnimalAidId':11}]},{'DateTime':'2016-03-23T00:00:00','AnimalId':9,'VetId':5,'ProcedureAnimalAids':[{'ProcedureId':19,'AnimalAidId':9}]},{'DateTime':'2016-09-04T00:00:00','AnimalId':7,'VetId':4,'ProcedureAnimalAids':[{'ProcedureId':20,'AnimalAidId':2},{'ProcedureId':20,'AnimalAidId':11}]},{'DateTime':'2016-06-17T00:00:00','AnimalId':7,'VetId':4,'ProcedureAnimalAids':[{'ProcedureId':21,'AnimalAidId':8}]},{'DateTime':'2016-12-12T00:00:00','AnimalId':13,'VetId':2,'ProcedureAnimalAids':[{'ProcedureId':22,'AnimalAidId':1},{'ProcedureId':22,'AnimalAidId':2}]},{'DateTime':'2016-06-05T00:00:00','AnimalId':13,'VetId':2,'ProcedureAnimalAids':[{'ProcedureId':23,'AnimalAidId':1},{'ProcedureId':23,'AnimalAidId':2}]},{'DateTime':'2016-02-03T00:00:00','AnimalId':8,'VetId':1,'ProcedureAnimalAids':[{'ProcedureId':24,'AnimalAidId':7},{'ProcedureId':24,'AnimalAidId':10}]},{'DateTime':'2016-04-15T00:00:00','AnimalId':10,'VetId':5,'ProcedureAnimalAids':[{'ProcedureId':25,'AnimalAidId':2},{'ProcedureId':25,'AnimalAidId':11}]},{'DateTime':'2016-05-12T00:00:00','AnimalId':14,'VetId':3,'ProcedureAnimalAids':[{'ProcedureId':26,'AnimalAidId':1},{'ProcedureId':26,'AnimalAidId':2}]},{'DateTime':'2016-09-29T00:00:00','AnimalId':14,'VetId':3,'ProcedureAnimalAids':[{'ProcedureId':27,'AnimalAidId':2},{'ProcedureId':27,'AnimalAidId':9}]}]";

        var animals = JsonConvert.DeserializeObject<Animal[]>(animalsJson);
        var vets = JsonConvert.DeserializeObject<Vet[]>(vetsJson);
        var animalAids = JsonConvert.DeserializeObject<AnimalAid[]>(animalAidsJson);
        var procedures = JsonConvert.DeserializeObject<Procedure[]>(proceduresJson);

        context.Animals.AddRange(animals);
        context.Vets.AddRange(vets);
        context.AnimalAids.AddRange(animalAids);
        context.Procedures.AddRange(procedures);

        context.SaveChanges();
    }
}