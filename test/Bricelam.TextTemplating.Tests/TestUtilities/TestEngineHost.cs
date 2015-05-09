using System;
using System.Collections.Generic;
using System.Text;
using Bricelam.TextTemplating.Parsing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;

namespace Bricelam.TextTemplating.TestUtilities
{
    public class TestEngineHost : ITextTemplatingEngineHost
    {
        private readonly string _includeFileContent;

        public TestEngineHost()
        {

        }

        public TestEngineHost(string includeFileContent)
        {
            _includeFileContent = includeFileContent;
        }

        public virtual string FileExtension { get; } = ".cs";
        public IList<string> StandardAssemblyReferences { get; } = new List<string>();
        public IList<string> StandardImports { get; } = new List<string>();

        public string TemplateFile => null;

        public string LoadIncludeFile(string fileName)
        {
            return _includeFileContent;
        }

        public virtual void LogErrors(EmitResult result)
        {
        }

        public IList<MetadataReference> ResolveAssemblyReference(string assemblyReference) => null;

        public void SetFileExtension(string extension)
        {
        }

        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
        }

        internal static Parser CreateParser(string includeFileContent = default(string))
        {
            return new Parser(new TestEngineHost(includeFileContent));
        }

        public string ResolvePath(string path)
        {
            return path;
        }
    }
}