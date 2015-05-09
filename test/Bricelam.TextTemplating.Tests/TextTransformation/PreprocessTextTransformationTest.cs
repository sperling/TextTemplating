using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bricelam.TextTemplating.TestUtilities;
using Xunit;

namespace Bricelam.TextTemplating.Tests.TextTransformation
{
    public class PreprocessTextTransformationTest
    {
        [Fact]
        public void TransformText_works_when_include_directive()
        {
            var content = "<#@ include file=\"test\" #>\\nFrom root";

            var parseResult = TestEngineHost.CreateParser("Hello World").Parse(content);

            var result = new PreprocessTextTransformation("Test", "Bar", parseResult).TransformText();
            Assert.Equal("\r\nnamespace Bar\r\n{\r\n    public partial class Test : TextTransformation\r\n    {\r\n        public override string TransformText()\r\n        {\r\n            Write(\"Hello World\");\r\n            Write(\"\\\\nFrom root\");\r\n\r\n            return GenerationEnvironment.ToString();\r\n        }\r\n\r\n    }\r\n}\r\n", result);
        }
    }
}
