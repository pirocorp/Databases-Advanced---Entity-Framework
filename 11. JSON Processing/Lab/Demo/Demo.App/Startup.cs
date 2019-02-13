namespace Demo.App
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;

    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Startup
    {
        public static void Main()
        {
            //SerializeAndDeserializeObjectsWithDataContractJsonSerializer();
            //SerializeAndDeserializeWithNewtonsoftJson();
            //DeserializingToAnonymousTypesWithNewtonsoftJson();
            //LinqToJson();
        }

        private static void LinqToJson()
        {
            //Create From JSON string:
            var json = @"{ 'firstName': 'Vladimir','lastName': 'Georgiev','jobTitle': 'Technical Trainer', 'Age': 30 }";
            var obj = JObject.Parse(json);

            //Create From File
            var persons = JObject.Parse(File.ReadAllText(@"../../../teacher.json"));

            //Using JObject:
            foreach (var person in persons["Persons"])
            {
                Console.WriteLine(person["firstName"]);
                Console.WriteLine(person["lastName"]);
                Console.WriteLine("--------------");
            }

            //JObjects can be queried with LINQ
            var json2 = JObject.Parse(@"{'products': [{'name': 'Fruits', 'products': ['apple', 'banana']},{'name': 'Vegetables', 'products': ['cucumber']}]}");

            var products = json2["products"]
                .Select(t => $"{t["name"]} ({string.Join(", ", t["products"])})")
                .ToArray();

            Console.WriteLine(string.Join(Environment.NewLine, products));
        }

        private static void DeserializingToAnonymousTypesWithNewtonsoftJson()
        {
            var json = @"{ 'firstName': 'Vladimir','lastName': 'Georgiev','jobTitle': 'Technical Trainer' }";

            var template = new
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                JobTitle = string.Empty
            };

            var person = JsonConvert.DeserializeAnonymousType(json, template);
            Console.WriteLine($"{person.FirstName} {person.LastName} - {person.JobTitle}");
        }

        private static void SerializeAndDeserializeWithNewtonsoftJson()
        {
            //JSON.NET can be configured to:
            //Indent the output JSON string
            //    To convert JSON to anonymous types
            //    To control the casing and properties to parse
            //    To skip errors
            //JSON.NET also supports:
            //    LINQ - to - JSON
            //    Direct parsing between XML and JSON
            var product = new ProductDto("Air conditioner", "Adjust temperature in room", 2545.50M);
            var jsonProduct = JsonConvert.SerializeObject(product, Formatting.Indented);
            Console.WriteLine(jsonProduct);
            var deserializedJsonProduct = JsonConvert.DeserializeObject<ProductDto>(jsonProduct);
            Console.WriteLine($"{deserializedJsonProduct.Name} - {deserializedJsonProduct.Price}");
        }

        private static void SerializeAndDeserializeObjectsWithDataContractJsonSerializer()
        {
            var product = new ProductDto("Air conditioner", "Adjust temperature in room", 2545.50M);
            var serializedProduct = SerializeJson(product);
            Console.WriteLine(serializedProduct);
            var deserializedProduct = DeserializeJson<ProductDto>(serializedProduct);
            Console.WriteLine($"{deserializedProduct.Name} {deserializedProduct.Description} - {deserializedProduct.Price}");
        }

        //DataContractJsonSerializer can serialize an object:
        static string SerializeJson<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                var result = Encoding.UTF8.GetString(stream.ToArray());
                return result;
            }
        }

        //DataContractJsonSerializer can deserialize a JSON string:
        static T DeserializeJson<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var jsonStringBytes = Encoding.UTF8.GetBytes(jsonString);
            using (var stream = new MemoryStream(jsonStringBytes))
            {
                var result = (T)serializer.ReadObject(stream);
                return result;
            }
        }
    }
}