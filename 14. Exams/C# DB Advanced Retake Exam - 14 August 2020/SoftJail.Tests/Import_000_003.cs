//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NUnit.Framework;
using SoftJail;
using SoftJail.Data;
using SoftJail.DataProcessor;

[TestFixture]
public class Import_000_003
{
	private IServiceProvider serviceProvider;

	private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

	[SetUp]
	public void Setup()
	{
		Mapper.Reset();
		Mapper.Initialize(cfg => cfg.AddProfile(GetType("SoftJailProfile")));

		this.serviceProvider = ConfigureServices<SoftJailDbContext>("SoftJail");
	}

	[Test]
	public void ImportPrisonersMailsZeroTest1()
	{
		var context = serviceProvider.GetService<SoftJailDbContext>();

		var inputJson = @"[{'FullName':null,'Nickname':'The Null','Age':38,'IncarcerationDate':'12/09/1967','ReleaseDate':'07/02/1989','Bail':93934.2,'CellId':4,'Mails':[]},{'FullName':'Hu','Nickname':'The Who','Age':38,'IncarcerationDate':'12/09/1967','ReleaseDate':'07/02/1989','Bail':93934.2,'CellId':4,'Mails':[]},{'FullName':'Stupidlylongnaaaaaame','Nickname':'The Who','Age':38,'IncarcerationDate':'12/09/1967','ReleaseDate':'07/02/1989','Bail':93934.2,'CellId':4,'Mails':[]},{'FullName':'Gosho','Nickname':'InvalidNicknameMan','Age':38,'IncarcerationDate':'12/09/1967','ReleaseDate':'07/02/1989','Bail':93934.2,'CellId':4,'Mails':[]},{'FullName':'Gosho','Nickname':'The Nickname','Age':17,'IncarcerationDate':'12/09/1967','ReleaseDate':'07/02/1989','Bail':93934.2,'CellId':4,'Mails':[]},{'FullName':'Gosho','Nickname':'The Nickname','Age':66,'IncarcerationDate':'12/09/1967','ReleaseDate':'07/02/1989','Bail':93934.2,'CellId':4,'Mails':[]},{'FullName':'Audrie Billion','Nickname':'The Rose','Age':25,'IncarcerationDate':'22/05/1964','ReleaseDate':'13/10/1990','Bail':59651.38,'CellId':8,'Mails':[{'Description':'make some difference','Sender':'Mikey Bothbie','Address':'This address is invalid! oops!'}]}]";

		var actualOutput = Deserializer.ImportPrisonersMails(context, inputJson).TrimEnd();
		var expectedOutput = "Invalid Data\r\nInvalid Data\r\nInvalid Data\r\nInvalid Data\r\nInvalid Data\r\nInvalid Data\r\nInvalid Data";

		var assertContext = serviceProvider.GetService<SoftJailDbContext>();

		var expectedPrisonerCount = 0;
		var actualPrisonerCount = assertContext.Prisoners.Count();

		var expectedMailCount = 0;
		var actualMailCount = assertContext.Mails.Count();

		Assert.That(actualPrisonerCount, Is.EqualTo(expectedPrisonerCount),
			$"Number of inserted prisoners is incorrect!");

		Assert.That(actualMailCount, Is.EqualTo(expectedMailCount),
			"Number of inserted mails is incorrect!");

		Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
			$"{nameof(Deserializer.ImportPrisonersMails)} output is incorrect!");
	}

	private static Type GetType(string modelName)
	{
		var modelType = CurrentAssembly
			.GetTypes()
			.FirstOrDefault(t => t.Name == modelName);

		Assert.IsNotNull(modelType, $"{modelName} model not found!");

		return modelType;
	}

	private static IServiceProvider ConfigureServices<TContext>(string databaseName)
		where TContext : DbContext
	{
		var services = ConfigureDbContext<TContext>(databaseName);

		var context = services.GetService<TContext>();

		try
		{
			context.Model.GetEntityTypes();
		}
		catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
		{
			services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
		}

		return services;
	}

	private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
		where TContext : DbContext
	{
		var services = new ServiceCollection()
           .AddDbContext<TContext>(t => t
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
	}
}