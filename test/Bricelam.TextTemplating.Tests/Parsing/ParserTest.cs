using System.Diagnostics;
using Bricelam.TextTemplating.TestUtilities;
using Xunit;
using System.Linq;

namespace Bricelam.TextTemplating.Parsing
{
    public class ParserTest
    {
        [Fact]
        public void Parse_works_when_empty()
        {
            var result = TestEngineHost.CreateParser().Parse(string.Empty);

            Assert.Empty(result.ContentBlocks);
            Assert.Empty(result.FeatureBlocks);
        }

        [Fact]
        public void Parse_works_when_text_block()
        {
            var content = "Text block";

            var result = TestEngineHost.CreateParser().Parse(content);

            Assert.Collection(
                result.ContentBlocks,
                b =>
                {
                    Assert.Equal(BlockType.TextBlock, b.BlockType);
                    Assert.Equal("Text block", b.Content);
                });
            Assert.Empty(result.FeatureBlocks);
        }

        [Fact]
        public void Parse_works_when_template_directive()
        {
            Debugger.Launch();

            var content = "<#@ template visibility=\"internal\" #>";

            var result = TestEngineHost.CreateParser().Parse(content);

            Assert.Equal("internal", result.Visibility);
            Assert.Empty(result.ContentBlocks);
            Assert.Empty(result.FeatureBlocks);
        }

        [Fact]
        public void Parse_works_when_include_directive()
        {
            var content = "<#@ include file=\"test\" #>";

            var result = TestEngineHost.CreateParser("Hello World").Parse(content);

            Assert.Equal(1, result.ContentBlocks.Count);
            Assert.Equal("Hello World", result.ContentBlocks.First().Content);          
        }

        [Fact]
        public void Parse_works_when_include_directive_once()
        {
            var content = "<#@ include file=\"test\" once=\"true\" #>";

            var result = TestEngineHost.CreateParser("Hello World").Parse(content);

            Assert.Equal(1, result.ContentBlocks.Count);
            Assert.Equal("Hello World", result.ContentBlocks.First().Content);
        }        
    }
}
