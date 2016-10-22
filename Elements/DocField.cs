namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    /// <summary>
    /// Support for the <![CDATA[<field>]]> tag
    /// https://msdn.microsoft.com/en-us/library/hh542725.aspx
    /// </summary>
    public class DocField : DocValue
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("static")]
        public bool Static { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("helpKeyword")]
        public string HelpKeyword { get; set; }
    }
}