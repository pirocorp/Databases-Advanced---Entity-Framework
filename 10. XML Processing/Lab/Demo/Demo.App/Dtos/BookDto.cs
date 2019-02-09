namespace Demo.App.Dtos
{
    using System.Xml.Serialization;

    //XML Attributes
    //We can use several attributes to control serialization to XML
    //    [XmlType("Name")] – Specifies the type’s name in XML
    //    [XmlAttribute("name")] – Serialize as XML Attribute
    //    [XmlElement] – Serialize as XML Element
    //    [XmlIgnore] – Do not serialize
    //    [XmlArray] – Serialize as an array of XML elements
    //    [XmlRoot] – Specifies the root element name
    //    [XmlText] – Serialize multiple xml elements on one line
    [XmlType("Book")]
    public class BookDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Author")]
        public string Author { get; set; }

        [XmlIgnore]
        public decimal Price { get; set; }
    }
}