//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using VaporStore;
using VaporStore.Data;

[TestFixture]
public class Import_000_001
{
	private IServiceProvider serviceProvider;

	private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

	[SetUp]
	public void Setup()
	{
		Mapper.Reset();
		Mapper.Initialize(cfg => cfg.AddProfile(GetType("VaporStoreProfile")));

		this.serviceProvider = ConfigureServices<VaporStoreDbContext>("VaporStore");
	}

	[Test]
	public void ImportGamesZeroTest()
	{
		var context = this.serviceProvider.GetService<VaporStoreDbContext>();

		var inputJson =
			@"[{'Price':0,'ReleaseDate':'2013-07-09','Developer':'Valid Dev','Genre':'Valid Genre','Tags':['Valid Tag']},{'Name':'Invalid','Price':-5,'ReleaseDate':'2013-07-09','Developer':'Valid Dev','Genre':'Valid Genre','Tags':['Valid Tag']},{'Name':'Invalid','Price':0,'ReleaseDate':'2013-07-09','Genre':'Valid Genre','Tags':['Valid Tag']},{'Name':'Invalid','Price':0,'ReleaseDate':'2013-07-09','Developer':'Valid Dev','Tags':['Valid Tag']},{'Name':'Invalid','Price':0,'ReleaseDate':'2013-07-09','Developer':'Valid Dev','Genre':'Valid Genre','Tags':[]},{'Name':'Dota 2','Price':0,'ReleaseDate':'2013-07-09','Developer':'Valve','Genre':'Action','Tags':['Multi-player','Co-op','Steam Trading Cards','Steam Workshop','SteamVR Collectibles','In-App Purchases','Valve Anti-Cheat enabled']},{'Name':'MONSTER HUNTER: WORLD','Price':59.99,'ReleaseDate':'2018-08-09','Developer':'CAPCOM Co., Ltd.','Genre':'Action','Tags':['Single-player','Multi-player','Co-op','Steam Achievements','Partial Controller Support','Stats']}]";

		var actualOutput = 
			VaporStore.DataProcessor.Deserializer.ImportGames(context, inputJson).TrimEnd();
		var expectedOutput =
			"Invalid Data\r\nInvalid Data\r\nInvalid Data\r\nInvalid Data\r\nInvalid Data\r\nAdded Dota 2 (Action) with 7 tags\r\nAdded MONSTER HUNTER: WORLD (Action) with 6 tags";

		var assertContext = this.serviceProvider.GetService<VaporStoreDbContext>();

		const int expectedGameCount = 2;
		var actualGameCount = assertContext.Games.Count();

		Assert.That(actualGameCount, Is.EqualTo(expectedGameCount),
			$"Inserted {nameof(context.Games)} count is incorrect!");

		const int expectedDeveloperCount = 2;
		var actualDeveloperCount = assertContext.Developers.Count();

		Assert.That(actualDeveloperCount, Is.EqualTo(expectedDeveloperCount),
			$"Inserted {nameof(context.Developers)} count is incorrect!");

		const int expectedGenreCount = 1;
		var actualGenreCount = assertContext.Genres.Count();

		Assert.That(actualGenreCount, Is.EqualTo(expectedGenreCount),
			$"Inserted {nameof(context.Genres)} count is incorrect!");

		const int expectedTagCount = 11;
		var actualTagCount = assertContext.Tags.Count();

		Assert.That(actualTagCount, Is.EqualTo(expectedTagCount),
			$"Inserted {nameof(context.Tags)} count is incorrect!");

		Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
			$"{nameof(VaporStore.DataProcessor.Deserializer.ImportGames)} output is incorrect!");
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