using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Step 2
using System.ComponentModel.Composition;
//Step 5
using System.ComponentModel.Composition.Hosting;

//This program handles the following sections:
// 1. Simple Example

namespace Example01
{
    class Program
    {
        //This is step 3
        //Here we are defining our "port", where we will insert 
        //a "plug" of type string (which is implied)
        [Import]
        string message;

        //This is the completed in the setup stage
        static void Main(string[] args)
        {
            //We are instantiating this program and calling
            //it so we can properly use MEF
            Program p = new Program();
            p.Run();
        }

        //This is our helper method that runs everything
        void Run()
        {
            Compose();
            Console.WriteLine(message);
            Console.ReadKey();
        }

        //Step 5
        private void Compose()
        {
            //We are loading the currently-executing assembly
            AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);
            
            //Here we are hooking up the "plugs"
            //to the "ports".  This is one of the 
            //options to hook everything up.  I've 
            //commented out the other option below.
            container.SatisfyImportsOnce(this);
            //container.ComposeParts(this);
        }
    }

    //Step 4
    public class MessageBox
    {
        //This is the string property that we are
        //exporting (making it a "plug")
        [Export()]
        public string MyMessage
        {
            get { return "This is my example message."; }
        }
    }
}
