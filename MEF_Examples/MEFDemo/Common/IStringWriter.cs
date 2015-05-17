using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Common
{
    [InheritedExport(typeof(IStringWriter))]
    public interface IStringWriter
    {
        string WriteMessage();
    }
}
