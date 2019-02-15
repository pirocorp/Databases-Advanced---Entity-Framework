namespace Forum.Console
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Ninject;

    using Models;

    public class Startup
    {
        public static void Main()
        {
            Test();
            //var forumImporter = new ForumImporter();
            //forumImporter.BeginImport();
            
        }

        private static void Test()
        {
            var kernel = new StandardKernel();
            //This will load all types that inherits NinjectModule
            kernel.Load(Assembly.GetExecutingAssembly());

            var dp = kernel.Get<MyDataProvider>();

            //How to use GetAll First Lambda is for Where()
            //Second for Order() and Third for Select()
            var resultsAll = dp.Categories.GetAll
                (
                 x => x.ParentCategoryId == 3,
                 x => x.Name,
                 x => x.Name
                )
                .ToArray();

            var result = dp.Categories.GetAll(x => x.Id == 1).First();

            Console.WriteLine(result.Name);

            foreach (var re in resultsAll)
            {
                Console.WriteLine(re);
            }

            dp.Categories.Add(new Category
            {
                Name = "Other Category"
            });

            dp.UnitOfWork().Commit();

            using (var unitOfWork = dp.UnitOfWork())
            {
                unitOfWork.Commit();
            }
        }
    }
}