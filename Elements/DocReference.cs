namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    /// <summary>
    /// Support for the <![CDATA[<reference />]]> tag
    /// https://msdn.microsoft.com/en-us/library/bb385682.aspx
    /// </summary>
    public class DocReference : DocElement
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("assembly")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}