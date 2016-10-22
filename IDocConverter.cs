namespace VSDocConverter
{
    using System.Collections.Generic;

    using VSDocConverter.Elements;

    public interface IDocConverter
    {
        string GetOutputDirectory(IEnumerable<string> files, string rootDirectory);

        string GetNewFileName(string filePath);
 
        void ReplaceVsDoc(List<string> lines, List<string> buffer, List<string> newLines, int bufferStartLine, int currentLine);

        string Convert(DocDeprecated element);

        string Convert(DocField element);

        string Convert(DocLoc element);

        string Convert(DocParam element);

        string Convert(DocReference element);

        string Convert(DocReturn element);

        string Convert(DocSignature element);

        string Convert(DocSummary element);

        string Convert(DocValue element);

        string Convert(DocVar element);

        string Convert(DocElement element);
    }
}
