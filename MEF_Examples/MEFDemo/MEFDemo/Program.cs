using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
//using System.Reflection;

namespace MEFDemo
{
    class Program
    {
        CompositionContainer container;
        DirectoryCatalog dirCatalog;

        [Import(AllowDefault = true, AllowRecomposition=true)]
        string test;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        void Run()
        {            
            Compose();

            if (test == null)
            {
                Console.WriteLine("Yep, null");
            }
            else
            {
                Console.WriteLine(test);
            }
            //Console.WriteLine(myImport.Value.MyMethod());

            Console.ReadKey();

            dirCatalog.Refresh();

            if (test == null)
            {
                Console.WriteLine("Yep, null");
            }
            else
            {
                Console.WriteLine(test);
            }
            //Console.WriteLine(myImport.Value.MyMethod());

            Console.ReadKey();
        }

        private void Compose()
        {
            //AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            dirCatalog = new DirectoryCatalog(@"C:\Temp");
            AggregateCatalog catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()), dirCatalog);

            container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            
        }
    }

}