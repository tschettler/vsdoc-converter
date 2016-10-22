namespace VSDocConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using VSDocConverter.Adapters;
    using VSDocConverter.Elements;
    using VSDocConverter.Extensions;

    public class FileParser
    {
        public FileParser(IDocConverter converter)
            : this(converter, new DocSignatureAdapter(), new DocVarAdapter())
        {
        }

        public FileParser(IDocConverter converter, params IElementAdapter[] adapters)
        {
            this.Converter = converter;
            this.ElementAdapters = new List<IElementAdapter>(adapters);
        }

        public List<IElementAdapter> ElementAdapters { get; protected set; }

        protected IDocConverter Converter { get; set; }

        public void ProcessFiles(IEnumerable<string> files, string rootDirectory)
        {
            var outputDirectory = this.Converter.GetOutputDirectory(files, rootDirectory);

            var prevDirectory = outputDirectory;
            foreach (var file in files)
            {
                Console.WriteLine("Processing {0}...", file);
                var text = File.ReadAllLines(file);
                var newLines = this.Parse(text.ToList());

                var newFileName = this.Converter.GetNewFileName(file);

                var fileDirectory = new FileInfo(file).DirectoryName;
                var newDirectory = fileDirectory.Replace(rootDirectory, outputDirectory);

                if (!(newDirectory == prevDirectory || Directory.Exists(newDirectory)))
                {
                    Directory.CreateDirectory(newDirectory);
                    prevDirectory = newDirectory;
                }

                var newFilePath = Path.Combine(newDirectory, newFileName);

                File.WriteAllLines(newFilePath, newLines);

                Console.WriteLine("Created {0}", newFilePath);
            }
        }

        public List<string> Parse(List<string> lines)
        {
            var buffer = new List<string>();
            var count = lines.Count;
            int? bufferStart = null;
            for (var i = count - 1; i >= 0; i--)
            {
                var line = lines[i];
                if (line.Trim().StartsWith("///"))
                {
                    if (!bufferStart.HasValue)
                    {
                        bufferStart = i;
                    }

                    buffer.Insert(0, this.PrepareLine(line));
                }
                else if (buffer.Any())
                {
                    var bufferStartLine = bufferStart.Value - buffer.Count + 1;
                    var indentLevel = line.GetIndentLevel();
                    var insertAtLine = i;
                    string funcName;
                    if (!this.TryGetFunctionName(line, out funcName))
                    {
                        insertAtLine = bufferStart.Value;
                        indentLevel = lines[bufferStart.Value].GetIndentLevel();
                    }

                    var newLines = GetNewLines(buffer, indentLevel);

                    // replace the vsdoc comments
                    this.Converter.ReplaceVsDoc(lines, buffer, newLines, bufferStartLine, insertAtLine);

                    // run and flush
                    bufferStart = null;
                    buffer.Clear();
                }
            }

            return lines;
        }

        private List<string> GetNewLines(List<string> lines, int indentLevel)
        {
            var joinedLines = string.Join(Environment.NewLine, lines);
            var cleanXml = this.PrepareXml(joinedLines);

            var nodeType = this.GetRootElementType(cleanXml);

            var docElement = cleanXml.Deserialize(nodeType) as DocElement;

            // get lines from deserialized string
            var newLines = this.IndentLines(this.Converter.ConvertToLines(docElement), indentLevel).ToList();

            return newLines;
        }

        private string PrepareLine(string line)
        {
            return line.Replace("///", "").Trim();
        }

        private Type GetRootElementType(string xml)
        {
            var adapter = this.ElementAdapters.FirstOrDefault(a => a.IsMatch(xml));
            var type = adapter == null ? typeof(DocSignature) : adapter.ElementType;

            return type;
        }

        private string PrepareXml(string xml)
        {
            foreach (var adapter in this.ElementAdapters)
            {
                xml = adapter.PrepareXml(xml);
            }

            return xml;
        }

        private IEnumerable<string> IndentLines(IEnumerable<string> lines, int spaces)
        {
            foreach (var line in lines)
            {
                yield return new string(' ', spaces) + line;
            }
        }

        private bool TryGetFunctionName(string line, out string name)
        {
            name = null;

            var patterns = new List<string>()
                {
                    @"^\s*function ([^(]+)\s*\(",
                    @"^\s*(?:var)?\s*([^=\s]+)\s*\=.*function\s*\(",
                    @"^\s*([^:]+):.*function\s*\("
                };

            var result = false;
            foreach (var pattern in patterns)
            {
                var match = Regex.Match(line, pattern);
                result = match.Success;

                if (!result)
                {
                    continue;
                }

                name = match.Groups[1].Value;
                break;
            }

            return result;
        }
    }
}
