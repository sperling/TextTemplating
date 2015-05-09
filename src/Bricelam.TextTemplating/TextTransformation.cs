using System.Text;

namespace Bricelam.TextTemplating
{
    public abstract class TextTransformation
    {
        public StringBuilder GenerationEnvironment { get; } = new StringBuilder();
        public void Write(string textToAppend) => GenerationEnvironment.Append(textToAppend);
        public void WriteLine(string textToAppend) => GenerationEnvironment.AppendLine(textToAppend);

        public void PushIndent(string indent)
        {

        }

        public string PopIndent() => null;
        public ITextTemplatingEngineHost Host { get; set; }

        public abstract string TransformText();
    }
}
