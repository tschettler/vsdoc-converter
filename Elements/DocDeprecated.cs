namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    public enum DocDeprecatedType
    {
        /// <summary>
        /// Function or method will be removed in a future release
        /// </summary>
        [XmlEnum("deprecate")]
        Deprecated,

        /// <summary>
        /// Function or method has already been removed
        /// </summary>
        [XmlEnum("remove")]
        Removed
    }

    /// <summary>
    /// Support for the <![CDATA[<deprecated>]]> tag
    /// https://msdn.microsoft.com/en-us/library/dn387587.aspx
    /// </summary>
    public class DocDeprecated : DocSummary
    {
        [XmlAttribute("type")]
        public DocDeprecatedType Type { get; set; }
    }
}