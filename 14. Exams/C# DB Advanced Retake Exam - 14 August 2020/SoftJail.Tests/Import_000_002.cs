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

[TestFixture]
public class Import_000_002
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
	public void ImportDepartmentsCellsZeroTest2()
	{
		var context = serviceProvider.GetService<SoftJailDbContext>();

		var inputJson =
			@"[{'Name':'Test Department','Cells':[{'CellNumber':101,'HasWindow':true},{'CellNumber':102,'HasWindow':false}]}]";

		var actualOutput = SoftJail.DataProcessor.Deserializer.ImportDepartmentsCells(context, inputJson).TrimEnd();
		var expectedOutput = "Imported Test Department with 2 cells";

		var assertContext = serviceProvider.GetService<SoftJailDbContext>();

		var expectedDepartmentCount = 1;
		var actualDepartmentCount = assertContext.Departments.Count();

		var expectedCellCount = 2;
		var actualCellCount = assertContext.Cells.Count();

		Assert.That(actualDepartmentCount, Is.EqualTo(expectedDepartmentCount),
			"Number of inserted departments is incorrect!");

		Assert.That(actualCellCount, Is.EqualTo(expectedCellCount),
			"Number of inserted cells is incorrect!");

		Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
			$"{nameof(SoftJail.DataProcessor.Deserializer.ImportDepartmentsCells)} output is incorrect!");
	}

	public static Type GetType(string modelName)
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
		var services = new ServiceCollection();

		services
			.AddDbContext<TContext>(
				options => options
					.UseInMemoryDatabase(databaseName)
					.UseLazyLoadingProxies(useLazyLoading)
			);

		var serviceProvider = services.BuildServiceProvider();
		return serviceProvider;
	}
}