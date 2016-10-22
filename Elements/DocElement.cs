namespace VSDocConverter.Elements
{
    using System.Xml.Serialization;

    public abstract class DocElement
    {
        [XmlAttribute("locId")]
        public string LocId { get; set; }

        public virtual bool IsMatch(string xml)
        {
            return false;
        }
    }
}