namespace VSDocConverter.Adapters
{
    using System;

    public interface IElementAdapter
    {
        string TagStart { get; set; }

        Type ElementType { get; }

        string PrepareXml(string xml);

        bool IsMatch(string xml);
    }
}
