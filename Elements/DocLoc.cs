namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    public enum LocFormat
    {
        /// <summary>
        /// Standard .NET Framework localization format that is used by Microsoft Ajax and Windows Runtime
        /// </summary>
        [XmlEnum("vsdoc")]
        VsDoc,

        /// <summary>
        /// Use of message bundles defined by Open Ajax metadata
        /// </summary>
        [XmlEnum("messagebundle")]
        MessageBundle
    }

    /// <summary>
    /// Support for the <![CDATA[<loc>]]> tag
    /// https://msdn.microsoft.com/en-us/library/hh500491.aspx
    /// </summary>
    public class DocLoc : DocElement
    {
        /// <summary>
        /// The root name of the sidecar file that contains localization information for the neutral culture
        /// </summary>
        [XmlAttribute("filename")]
        public string FileName { get; set; }

        /// <summary>
        /// The type of sidecar file used for localization
        /// </summary>
        [XmlAttribute("format")]
        public LocFormat Format { get; set; }
    }
}