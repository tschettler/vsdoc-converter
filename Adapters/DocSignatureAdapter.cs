namespace VSDocConverter.Adapters
{
    using VSDocConverter.Elements;

    public class DocSignatureAdapter : BaseElementAdapter<DocSignature>
    {
        public DocSignatureAdapter()
        {
            this.TagStart = "<signature";
        }

        public override string PrepareXml(string xml)
        {
            if (xml.StartsWith("<summary") || xml.StartsWith("<deprecated"))
            {
                xml = string.Format("<signature>{0}</signature>", xml);
            }

            return xml;
        }
    }
}
