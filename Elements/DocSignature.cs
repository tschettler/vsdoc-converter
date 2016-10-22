namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    /// <summary>
    /// Support for the <![CDATA[<signature>]]> tag
    /// https://msdn.microsoft.com/en-us/library/hh542721.aspx
    /// </summary>
    [XmlRoot("signature")]
    public class DocSignature : DocElement
    {
        #region Attributes

        [XmlAttribute("externalFile")]
        public string ExternalFile { get; set; }

        [XmlAttribute("externalId")]
        public string ExternalId { get; set; }

        [XmlAttribute("helpKeyword")]
        public string HelpKeyword { get; set; }

        #endregion Attributes

        [XmlElement("deprecated")]
        public DocDeprecated Deprecated { get; set; }

        [XmlElement("summary")]
        public DocSummary Summary { get; set; }

        [XmlElement("param")]
        public DocParam[] Params { get; set; }

        [XmlElement("returns")]
        public DocReturn Return { get; set; }
    }
}