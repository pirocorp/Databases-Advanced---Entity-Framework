namespace Forum.Console
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class ForumImporter
    {
        public void BeginImport()
        {
            var instances = Assembly.GetAssembly(typeof(IImporter))
                .GetTypes()
                .Where(x => typeof(IImporter).IsAssignableFrom(x) &&
                            !x.IsInterface &&
                            !x.IsAbstract)
                .Select(t => (IImporter)Activator.CreateInstance(t))
                .OrderBy(i => i.Order)
                .ToList();

            foreach (var instance in instances)
            {
                Console.WriteLine(instance.Message);
                instance.Import();
            }
        }
    }
}