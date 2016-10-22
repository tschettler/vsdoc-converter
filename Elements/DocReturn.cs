namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    /// <summary>
    /// Support for the <![CDATA[<returns>]]> tag
    /// https://msdn.microsoft.com/en-us/library/hh542719.aspx
    /// </summary>
    public class DocReturn : DocValue
    {
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}