namespace VSDocConverter.Adapters
{
    using System;

    using VSDocConverter.Elements;

    public class BaseElementAdapter<TElement> : IElementAdapter where TElement : DocElement
    {
        public string TagStart { get; set; }

        public Type ElementType
        {
            get
            {
                return typeof(TElement);
            }
        }

        public virtual string PrepareXml(string xml)
        {
            return xml;
        }

        public virtual bool IsMatch(string xml)
        {
            return this.TagStart != null && xml.StartsWith(this.TagStart);
        }
    }
}
