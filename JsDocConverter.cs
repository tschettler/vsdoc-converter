namespace VSDocConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using VSDocConverter.Elements;
    using VSDocConverter.Extensions;

    public class JsDocConverter : IDocConverter
    {
        public virtual string GetOutputDirectory(IEnumerable<string> files, string rootDirectory)
        {
            var isFile = files.Count() == 1;

            var docdir = isFile ? rootDirectory : Path.Combine(rootDirectory, "jsdoc");

            return docdir;
        }

        public virtual string GetNewFileName(string filePath)
        {
            return string.Concat(Path.GetFileNameWithoutExtension(filePath), ".jsdoc");
        }

        public virtual void ReplaceVsDoc(List<string> lines, List<string> buffer, List<string> newLines, int bufferStartLine, int currentLine)
        {
            // first remove the buffer lines
            lines.RemoveRange(bufferStartLine, buffer.Count);

            // next, insert the new values at the current line
            lines.InsertRange(currentLine, newLines);
        }

        #region Convert Element Methods

        public virtual string Convert(DocDeprecated element)
        {
            var format = " * @deprecated [{0}] {1}";

            var result = string.Format(format, element.Type, element.Description);

            return result;
        }

        public virtual string Convert(DocElement element)
        {
            return null;
        }

        public virtual string Convert(DocField element)
        {
            return null;
        }

        public virtual string Convert(DocLoc element)
        {
            return null;
        }

        public virtual string Convert(DocParam element)
        {
            var result = " * @param";
            if (!string.IsNullOrWhiteSpace(element.Type))
            {
                result += string.Format(" {{{0}}}", element.Type);
            }

            if (!string.IsNullOrWhiteSpace(element.Name))
            {
                result += " " + element.Name;
            }

            if (!string.IsNullOrWhiteSpace(element.Description))
            {
                result += " - " + element.Description;
            }

            return result;
        }

        public virtual string Convert(DocReference element)
        {
            return null;
        }

        public virtual string Convert(DocReturn element)
        {
            var result = " * @returns";
            if (!string.IsNullOrWhiteSpace(element.Type))
            {
                result += string.Format(" {{{0}}}", element.Type);
            }

            if (!string.IsNullOrWhiteSpace(element.Description))
            {
                result += " - " + element.Description;
            }

            return result;
        }

        public virtual string Convert(DocSignature element)
        {
            var childElements = new List<DocElement>
            {
                element.Deprecated, 
                element.Summary
            };

            if (element.Params != null)
            {
                childElements.AddRange(element.Params);
            }

            childElements.Add(element.Return);

            var converted = childElements.Select(this.ConvertElement).Where(e => e != null);

            var content = string.Join(Environment.NewLine, converted);
            var format = "/**{0}{1}{0} */";
            var result = string.Format(format, Environment.NewLine, content);

            return result;
        }

        public virtual string Convert(DocSummary element)
        {
            var result = " * " + element.Description.Trim().AddLeadAsterisk();
            return result;
        }

        public virtual string Convert(DocValue element)
        {
            return null;
        }

        public virtual string Convert(DocVar element)
        {
            var lines = new List<string>();
            if (!string.IsNullOrWhiteSpace(element.Description))
            {
                lines.AddRange(element.Description.SplitByLine());
            }

            var tag = "@member";

            if (!string.IsNullOrWhiteSpace(element.Type))
            {
                tag += string.Format(" {{{0}}}", element.Type);
            }

            lines.Add(tag);

            var multiline = lines.Count > 1;
            var content = multiline ? lines.AddLeadAsterisk() : lines[0];

            var format = multiline ? "/**{1}{0}{1} */" : "/** {0} */";
            var result = string.Format(format, content, Environment.NewLine);

            return result;
        }

        #endregion Convert Element Methods
    }
}
