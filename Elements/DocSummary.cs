namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    /// <summary>
    /// Support for the <![CDATA[<summary>]]> tag
    /// https://msdn.microsoft.com/en-us/library/hh542723.aspx
    /// </summary>
    public class DocSummary : DocElement
    {
        [XmlText]
        public string Description { get; set; }
    }
}