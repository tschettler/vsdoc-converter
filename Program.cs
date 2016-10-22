namespace VSDocConverter
{
    using System;
    using System.IO;
    using System.Linq;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            var path = args.FirstOrDefault() ?? Directory.GetCurrentDirectory();

            var isFile = File.Exists(path);
            var isDirectory = Directory.Exists(path);

            if (!(isFile || isDirectory))
            {
                Console.WriteLine("The file or directory does not exist!");
                Environment.Exit(1);
            }

            var files = isFile ? new[] { path } : Directory.GetFiles(path, "*.js", SearchOption.AllDirectories);

            if (!files.Any())
            {
                Console.WriteLine("No files to process!");
                Environment.Exit(2);
            }

            var pathDir = new FileInfo(path).DirectoryName;

            var rootDir = isFile ? pathDir : path;

            var parser = new FileParser(new JsDocConverter());

            parser.ProcessFiles(files, rootDir);

            Console.Read();
        }
    }
}
