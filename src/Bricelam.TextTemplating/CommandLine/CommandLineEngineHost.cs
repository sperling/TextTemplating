using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.Runtime.Roslyn;

namespace Bricelam.TextTemplating.CommandLine
{
    public class CommandLineEngineHost : ITextTemplatingEngineHost
    {
        private readonly ILibraryManager _libraryManager;
        private string _fileExtension = ".cs";
        private Encoding _encoding;
        private readonly string _templateFile;

        public CommandLineEngineHost(ILibraryManager libraryManager, string templateFile)
        {
            _libraryManager = libraryManager;
            _templateFile = templateFile;
        }

        public string FileExtension => _fileExtension;
        public Encoding Encoding => _encoding;

        public IList<string> StandardAssemblyReferences { get; } = new List<string>
        {
#if DNXCORE50
            "System.Runtime",
#else
            "mscorlib",
#endif
            "Bricelam.TextTemplating"
        };

        public IList<string> StandardImports { get; } = new List<string>
        {
            "System",
            "Bricelam.TextTemplating"
        };

        public string TemplateFile => _templateFile;

        public void LogErrors(EmitResult result)
        {
            if (!result.Success)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Build failed. Diagnostics: {0}",
                        string.Join(Environment.NewLine, result.Diagnostics)));
            }
        }

        public IList<MetadataReference> ResolveAssemblyReference(string assemblyReference)
        {
            var references = new List<MetadataReference>();

            foreach (var metadataReference in _libraryManager.GetLibraryExport(assemblyReference).MetadataReferences)
            {
                var roslynMetadataReference = metadataReference as IRoslynMetadataReference;
                if (roslynMetadataReference != null)
                {
                    references.Add(roslynMetadataReference.MetadataReference);
                    continue;
                }
                var metadataFileReference = metadataReference as IMetadataFileReference;
                if (metadataFileReference != null)
                {
                    references.Add(MetadataReference.CreateFromFile(metadataFileReference.Path));
                }

                throw new Exception("Unexpected metadata reference type. " + assemblyReference);
            }

            return references;
        }
        
        public void SetFileExtension(string extension) => _fileExtension = extension;        
        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective) => _encoding = encoding;

        public string LoadIncludeFile(string fileName)
        {
            if (Path.IsPathRooted(fileName))
            {
                return File.ReadAllText(fileName);
            }

            return File.ReadAllText(Path.Combine(Path.GetDirectoryName(TemplateFile), fileName));
        }

        public string ResolvePath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            return Path.Combine(Path.GetDirectoryName(TemplateFile), path);
        }
    }
}