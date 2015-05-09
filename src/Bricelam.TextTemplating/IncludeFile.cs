using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bricelam.TextTemplating
{
    public class IncludeFile
    {
        private readonly string _file;
        private readonly bool _once;

        public string File => _file;

        public bool Once => _once;

        public IncludeFile(string file, bool once)
        {
            _file = file;
            _once = once;
        }
    }
}
