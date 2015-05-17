using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

//This program handles the following sections:
// 1. Loading Other Assemblies
// 2. Adding New Assemblies at Runtime

//This example gets a bit more tricky to do
//without a verbal explanation.  I have put
//my interface in an external project so that
//I could also external dlls access to the same
//project.  I added a reference to that Common
//project in this project. I also did the same
//in the ExternalLibrary project. That project
//will be used to create a dll that we can move
//into our folder that we are monitoring with MEF.
namespace Example05
{
    class Program
    {
        #region Imports section

        //Here we are pulling the IStringWriter
        //instances from all of the given assemblies
        [ImportMany(typeof(Common.IStringWriter), AllowRecomposition=true)]
        IEnumerable<Common.IStringWriter> _writers;

        #endregion

        DirectoryCatalog dirCatalog;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        void Run()
        {
            Compose();

            //First, we will run this without moving the dll into the
            //appropriate directory.  That means we shouldn't see the
            //external assembly run, just the internal one.
            Console.WriteLine("Before the dll is in the correct directory");
            foreach (Common.IStringWriter writer in _writers)
            {
                Console.WriteLine(writer.WriteMessage());
            }
            Console.WriteLine();

            //This will pause the application until you can move the
            //dll file into C:\Temp.  You do this by opening up the
            //bin directory of the ExternalLibrary folder and getting
            //the dll out of either the debug or release directory.
            //you only need the ExternalLibrary.dll file, not any of
            //the other files.
            Console.Write("Please move the dll to the Temp folder and then press any key...");
            Console.ReadKey();
            Console.WriteLine();

            //Here is where we recompose our container
            dirCatalog.Refresh();

            //This is the same code as above.  The only difference is
            //that now we have moved the dll to the correct directory
            //and recomposed the container.
            Console.WriteLine("After the dll is in the correct directory");
            foreach (Common.IStringWriter writer in _writers)
            {
                Console.WriteLine(writer.WriteMessage());
            }
            Console.WriteLine();

            Console.ReadKey();
        }

        private void Compose()
        {
            //Notice that we are creating two catalogs and adding
            //them to one aggregate catalog
            dirCatalog = new DirectoryCatalog(@"C:\Temp");
            AssemblyCatalog assemblyCat = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            AggregateCatalog catalog = new AggregateCatalog(assemblyCat, dirCatalog);
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }

    #region Exports section

    //Here we are creating an export of the
    //IStringWriter interface just to show
    //that we can mix parts from different
    //assemblies without issue.
    public class AssemblyStringClass : Common.IStringWriter
    {
        public string WriteMessage()
        {
            return "Hello from inside your assembly.";
        }
    }

    #endregion
}
