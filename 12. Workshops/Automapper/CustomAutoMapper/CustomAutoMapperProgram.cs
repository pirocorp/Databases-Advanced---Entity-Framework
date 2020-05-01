namespace CustomAutoMapper
{
    using System;
    using Newtonsoft.Json;

    public static class CustomAutoMapperProgram
    {
        public static void Main()
        {
            var mapper = new Mapper();

            var source = new Source
            {
                FirstName = "Pesho",
                LastName = "Peshev",
                Collection = new []{ "One", "Two", "Three" },
                Another = new AnotherClass
                {
                    Additional = "Aditional Source info"
                }
            };

            var target = mapper.Map<Target>(source);

            Console.WriteLine(JsonConvert.SerializeObject(target));
            Console.WriteLine(target.GetType().Name);

            Console.WriteLine(new string ('-', Console.WindowWidth));
            Console.WriteLine("Manipulating the separated objects");
            source.FirstName = "First";
            source.Collection = new[] {"1", "2", "3"};

            target.Another.Additional = "Manipulated target";

            Console.WriteLine(JsonConvert.SerializeObject(source));
            Console.WriteLine(source.GetType().Name);
            Console.WriteLine(new string ('-', Console.WindowWidth));
            Console.WriteLine(JsonConvert.SerializeObject(target));
            Console.WriteLine(target.GetType().Name);
        }
    }
}
