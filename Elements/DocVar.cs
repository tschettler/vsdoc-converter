namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    /// <summary>
    /// Support for the <![CDATA[<var>]]> tag
    /// https://msdn.microsoft.com/en-us/library/hh542722.aspx
    /// </summary>
    [XmlRoot("var")]
    public class DocVar : DocValue
    {
        [XmlAttribute("helpKeyword")]
        public string HelpKeyword { get; set; }
    }
}