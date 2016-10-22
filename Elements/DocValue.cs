namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    /// <summary>
    /// Support for the <![CDATA[<value>]]> tag
    /// https://msdn.microsoft.com/en-us/library/hh542724.aspx
    /// </summary>
    public class DocValue : DocElement
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("integer")]
        public bool Integer { get; set; }

        [XmlAttribute("domElement")]
        public bool DomElement { get; set; }

        [XmlAttribute("mayBeNull")]
        public bool MayBeNull { get; set; }

        [XmlAttribute("elementType")]
        public string ElementType { get; set; }

        [XmlAttribute("elementInteger")]
        public bool ElementInteger { get; set; }

        [XmlAttribute("elementDomElement")]
        public bool ElementDomElement { get; set; }

        [XmlAttribute("elementMayBeNull")]
        public bool ElementMayBeNull { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}