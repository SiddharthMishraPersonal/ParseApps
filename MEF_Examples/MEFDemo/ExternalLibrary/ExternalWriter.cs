using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExternalLibrary
{
    //This inherits the export from the interface.
    //This class will be packaged up in an external
    //dll file that will be consumed by Example05.
    public class ExternalWriter : Common.IStringWriter
    {
        public string WriteMessage()
        {
            return "Hello from your external class.";
        }
    }
}
