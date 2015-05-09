﻿using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;

namespace Bricelam.TextTemplating
{
    public interface ITextTemplatingEngineHost
    {
        IList<string> StandardAssemblyReferences { get; }
        IList<string> StandardImports { get; }

        void LogErrors(EmitResult result);
        MetadataReference ResolveAssemblyReference(string assemblyReference);
        void SetFileExtension(string extension);
        void SetOutputEncoding(Encoding encoding, bool fromOutputDirective);
        string TemplateFile { get; }
        string LoadIncludeFile(string fileName);
    }
}
