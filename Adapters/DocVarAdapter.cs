namespace VSDocConverter.Adapters
{
    using VSDocConverter.Elements;

    public class DocVarAdapter : BaseElementAdapter<DocVar>
    {
        public DocVarAdapter()
        {
            this.TagStart = "<var";
        }
    }
}
