namespace Demo.App
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Dtos;

    public class Startup
    {
        public static void Main()
        {
            //var xml = XmlExample();
            //LinqToXml(xml);
            //WorkingWithXDocument();
            //SearchingWithLinq();
            //var createdXml = CreatingXmlWithXElement();
            //SerializingXmlToFile(createdXml);
            //DeserializeXmlFromStringXml();
            //XmlAttributesExample();
        }

        private static string XmlExample()
        {
            //EXtensible Markup Language
            //    Universal notation(data format / language) for describing structured data using text with tags
            //    Designed to store and transport data
            //    The data is stored together with the meta - data about it
            //Similarities between XML and HTML
            //    Both are text based notations
            //    Both use tags and attributes
            //Differences between XML and HTML
            //    HTML describes documents, XML is a syntax for describing other languages(meta - language)
            //    HTML describes the layout of information, XML describes the structure of information
            //    XML requires the documents to be well - formatted
            //Advantages of XML:
            //    XML is human - readable(unlike binary formats)
            //    Store any kind of structured data
            //    Data comes with self-describing meta - data
            //    Full Unicode support
            //    Custom XML - based languages can be designed for certain apps
            //    Parsers available for virtually all languages and platforms
            //Disadvantages of XML:
            //    XML data is bigger (takes more space) than binary or JSON
            //    More memory consumption, more network traffic, more hard-disk space, more resources, etc.
            //    Decreased performance
            //    CPU consumption: need of parsing / constructing the XML tags
            //    XML is not suitable for all kinds of data
            //    E.g.binary data: graphics, images, videos, etc.

            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <library name = ""Developer's Library""> 
                    <book> 
                        <title lang=""en""> Professional C# 4.0 and .NET 4</title>
                        <author> Christian Nagel </author> 
                        <isbn> 978 - 0 - 470 - 50225 - 9 </isbn> 
                    </book> 
                    <book> 
                        <title lang=""en""> Teach Yourself XML in 10 Minutes </title>    
                        <author> Andrew H.Watt </author>       
                        <isbn> 978 - 0 - 672 - 32471 - 0 </isbn>       
                    </book>
                </library>";

            return xml;
        }

        private static void LinqToXml(string xml)
        {
            //LINQ to XML
            //    Use the power of LINQ to process XML data
            //    Easily read, search, write, modify XML documents
            //LINQ to XML classes:
            //    XDocument – represents a LINQ - enabled XML document(containing prolog, root element, …)
            //    XElement – main component holding information

            //To process an XML string
            var doc = XDocument.Parse(xml);

            //Loading XML directly from file
            var xmlDoc = XDocument.Load("../../../cars.xml");

            //Read all elements in the root element
            var cars = xmlDoc.Root?.Elements();

            if (cars != null)
            {
                foreach (var car in cars)
                {
                    var make = car.Element("make")?.Value;
                    var model = car.Element("model")?.Value;
                    Console.WriteLine($"{make} - {model}");
                }
            }
        }

        private static void WorkingWithXDocument()
        {
            var xmlDoc = XDocument.Load("../../../customers.xml");

            //Take first element in root element
            var customer = xmlDoc.Root?.Elements().ToArray().First();

            //Take Attributes in first element in root element
            var attributes = customer?.Attributes().ToArray();

            //Print attribute value with name = name
            Console.WriteLine(customer?.Attribute("name")?.Value);

            //Print element birth-date of root customer
            Console.WriteLine(customer?.Element("birth-date")?.Value);

            //Set element value by name
            //    If it doesn't exist, it will be added
            //    If set to null, will be removed
            customer?.SetElementValue("birth-date", "1990-10-04T00:00:00");
            Console.WriteLine(customer?.Element("birth-date")?.Value);

            //Take all elements from root
            var customers = xmlDoc.Root?.Elements().ToArray();

            //Remove element from it's parent
            Console.WriteLine(customer?.Element("is-young-driver")?.Value);
            var youngDriver = customer?.Element("is-young-driver");
            youngDriver?.Remove();
            Console.WriteLine(customer?.Element("is-young-driver")?.Value);

            //Get or set element attribute by name
            Console.WriteLine(customer?.Attribute("name")?.Value);

            if (customer?.Attribute("name")?.Value != null)
            {
                customer.Attribute("name").Value = "Piroman";
            }

            Console.WriteLine(customer?.Attribute("name")?.Value);

            //Get a list of all attributes for an element
            var attrs = customer?.Attributes().ToArray();
            Console.WriteLine(attrs?.Length);

            //Set attribute value by name
            //    If it doesn't exist, it will be added
            //    If set to null, will be removed
            customer?.SetAttributeValue("age", "21");
            attrs = customer?.Attributes().ToArray();
            Console.WriteLine(attrs?.Length);

            //All customers
            Console.WriteLine(customers?.Length);
        }

        private static void SearchingWithLinq()
        {
            var xmlDoc = XDocument.Load("../../../cars.xml");

            var firstCar = xmlDoc.Root?.Elements().First();

            //Taking value with cast
            var cast = (long)firstCar?.Element("travelled-distance");
            var cast2 = (long)firstCar?.Element("travelled-distance");
            var cast3 = (double)firstCar?.Element("travelled-distance");
            var cast4 = (decimal)firstCar?.Element("travelled-distance");
            var cast5 = (string)firstCar?.Element("travelled-distance");

            //Comparing numerical value
            var element = (long)firstCar?.Element("travelled-distance") >= 30000;
            var element2 = long.Parse(firstCar?.Element("travelled-distance")?.Value) >= 30000;

            //Searching in XML with LINQ is like searching with LINQ in array
            var cars = xmlDoc.Root?.Elements()
                .Where(e => e.Element("make")?.Value == "Opel" &&
                            (long)e.Element("travelled-distance") >= 300000)
                .Select(c => new
                {
                    Model = c.Element("model")?.Value,
                    Traveled = c.Element("travelled-distance")?.Value
                })
                .ToList();

            foreach (var car in cars)
                Console.WriteLine(car.Model + " " + car.Traveled);
        }

        private static XDocument CreatingXmlWithXElement()
        {
            //XDocuments can be composed from XElements and XAttributes
            var xmlDoc = new XDocument();
            xmlDoc.Add(
                new XElement("books",
                    new XElement("book",
                        new XElement("author", "Don Box"),
                        new XElement("title", "ASP.NET", new XAttribute("lang", "en"))),
                    new XElement("book",
                        new XElement("author", "Stephen King"),
                        new XElement("title", "It", new XAttribute("lang", "en")))
                ));

            Console.WriteLine(xmlDoc);
            return xmlDoc;
        }

        private static void SerializingXmlToFile(XDocument xmlDoc)
        {
            //To flush XDocument to file with default settings
            xmlDoc.Save("../../../xmlDocWithIndentation.xml");

            //To disable automatic indentation
            xmlDoc.Save("../../../xmlDocWithoutIndentation.xml", SaveOptions.DisableFormatting);

            //To serialize any object to file
            var serializer = new XmlSerializer(typeof(ProductDto));

            var product = new ProductDto()
            {
                Id = 1,
                Name = "Book",
                Description = "Good Book",
                Price = 24.95M
            };

            using (var writer = new StreamWriter("../../../myProductDto.xml"))
            {
                serializer.Serialize(writer, product);
            }
        }

        private static void DeserializeXmlFromStringXml()
        {
            //To deserialize object from XML string
            var serializer = new XmlSerializer(typeof(ProductDto));

            using (var sr = new StreamReader("../../../myProductDto.xml"))
            {
                var deserializedProductDto = (ProductDto)serializer.Deserialize(sr);

                Console.WriteLine($"{deserializedProductDto.Id}: {deserializedProductDto.Name} - {deserializedProductDto.Description} {deserializedProductDto.Price:F2}");
            }

            //Specifying root attribute name
            var attr = new XmlRootAttribute("ProductDto");
            serializer = new XmlSerializer(typeof(ProductDto), attr);

            using (var sr = new StreamReader("../../../myProductDto.xml"))
            {
                var deserializedProductDto = (ProductDto)serializer.Deserialize(sr);

                Console.WriteLine($"{deserializedProductDto.Id}: {deserializedProductDto.Name} - {deserializedProductDto.Description} {deserializedProductDto.Price:F2}");
            }
        }

        private static void XmlAttributesExample()
        {
            var books = GetBooks();

            //To serialize any object to file
            var serializer = new XmlSerializer(typeof(BookDto[]));

            //In BookDto Class there is attributes
            using (var writer = new StreamWriter("../../../dookDtoWithAttributes.xml"))
            {
                serializer.Serialize(writer, books);
            }
        }

        private static BookDto[] GetBooks()
        {
            var books = new[]
            {
                new BookDto() { Name = "It", Author = "Stephen King", Price = 25.65M},
                new BookDto() { Name = "Frankenstein", Author = "Mary Shelley", Price = 15.55M},
                new BookDto() { Name = "Queen Lucia", Author = "E.F. Benson", Price = 19.90M},
                new BookDto() { Name = "Paper Towns", Author = "John Green", Price = 29.95M},
            };

            return books;
        }
    }
}