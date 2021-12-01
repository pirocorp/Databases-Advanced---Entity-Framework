using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SoftJail;
using SoftJail.Data;
using SoftJail.DataProcessor;

[TestFixture]
public class Import_000_005
{
	private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

	private IServiceProvider serviceProvider;

	[SetUp]
	public void Setup()
	{
		Mapper.Reset();
		Mapper.Initialize(cfg => cfg.AddProfile(GetType("SoftJailProfile")));

		this.serviceProvider = ConfigureServices<SoftJailDbContext>("SoftJail");
	}

	[Test]
	public void ImportOfficersPrisonersZeroTest1()
	{
		var context = serviceProvider.GetService<SoftJailDbContext>();

		var inputXml = @"<Officers><Officer><Name>Minerva Kitchingman</Name><Money>2582</Money><Position>Invalid</Position><Weapon>ChainRifle</Weapon><DepartmentId>2</DepartmentId><Prisoners><Prisoner id=""15"" /></Prisoners></Officer><Officer><Name>N</Name><Money>2582</Money><Position>Overseer</Position><Weapon>ChainRifle</Weapon><DepartmentId>2</DepartmentId><Prisoners><Prisoner id=""3"" /></Prisoners></Officer><Officer><Name>MnooooooooogoDulgoImeeeeeeeeeeeeeeeeeeeeeeeee</Name><Money>2582</Money><Position>Overseer</Position><Weapon>ChainRifle</Weapon><DepartmentId>2</DepartmentId><Prisoners><Prisoner id=""3"" /></Prisoners></Officer><Officer><Name>Holl Markson</Name><Money>-2582</Money><Position>Overseer</Position><Weapon>ChainRifle</Weapon><DepartmentId>2</DepartmentId><Prisoners><Prisoner id=""3"" /></Prisoners></Officer></Officers>";

		var actualOutput = Deserializer.ImportOfficersPrisoners(context, inputXml).TrimEnd();
	    var expectedOutput = "Invalid Data\r\nInvalid Data\r\nInvalid Data\r\nInvalid Data";
		var assertContext = serviceProvider.GetService<SoftJailDbContext>();

		var expectedOfficersCount = 0;
		var actualOfficersCount = assertContext.Officers.Count();

		var expectedOfficersPrisonersCount = 0;
		var actualOfficersPrisonersCount = assertContext.Prisoners.Count();

		Assert.That(actualOfficersCount, Is.EqualTo(expectedOfficersCount),
			"Number of inserted officers is incorrect!");

		Assert.That(actualOfficersPrisonersCount, Is.EqualTo(expectedOfficersPrisonersCount), 
			"Number of inserted officer prisoners items is incorrect!");

		Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip, 
			$"{nameof(Deserializer.ImportOfficersPrisoners)} output is incorrect!");
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