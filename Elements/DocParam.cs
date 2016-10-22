namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    /// <summary>
    /// Support for the <![CDATA[<param>]]> tag
    /// https://msdn.microsoft.com/en-us/library/hh542720.aspx
    /// </summary>
    public class DocParam : DocValue
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parameterArray")]
        public bool ParameterArray { get; set; }

        [XmlAttribute("optional")]
        public bool Optional { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}