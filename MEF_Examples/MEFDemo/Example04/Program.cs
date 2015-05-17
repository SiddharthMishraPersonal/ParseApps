using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

//This program handles the following sections:
// 1. Describing the Export
// 2. Advanced Metadata

namespace Example04
{
    class Program
    {
        #region Imports section

        //Describing the Export section
        //We will load the set of IPerson instances with
        //their corresponding metadata.
        [ImportMany(typeof(IPerson))]
        IEnumerable<Lazy<IPerson, Dictionary<string, object>>> _magicStringMeta;
 
        //Advanced Metadata section
        //This will do the same thing to the same instances
        //of IPerson, but we will use the interface to describe
        //the metadata, which will allow for design-time
        //Intellisense.
        [ImportMany(typeof(IPerson))]
        IEnumerable<Lazy<IPerson, IPersonMetadata>> _interfaceMeta;

        #endregion

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        void Run()
        {
            Compose();

            //Describing the Export section
            //We will simply loop through the list and check the
            //metadata to find the "Tim" entry.
            Console.WriteLine("Describing the Export section");
            foreach (Lazy<IPerson, Dictionary<string, object>> person in _magicStringMeta)
            {
                if (person.Metadata["Name"].ToString() == "Tim")
                {
                    Console.WriteLine(person.Value.FullName());
                }
            }
            Console.WriteLine();

            //Advanced Metadata section
            //We are going to do the same loop as above, but use the
            //interface to allow for design-time Intellisense
            Console.WriteLine("Advanced Metadata section");
            foreach (Lazy<IPerson, IPersonMetadata> person in _interfaceMeta)
            {
                if (person.Metadata.Name == "Tim")
                {
                    Console.WriteLine(person.Value.FullName());
                }
            }
            Console.WriteLine();

            Console.ReadKey();
        }

        private void Compose()
        {
            AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
        }
    }

    #region Exports section

    public interface IPerson
    {
        string FullName();
    }

    //Describing the Export section
    [Export(typeof(IPerson))]
    [ExportMetadata("Name", "Tim")]
    [ExportMetadata("Age", 33)]
    public class Author : IPerson
    {
        public string FullName()
        {
            return "Timothy Corey";
        }
    }

    [Export(typeof(IPerson))]
    [ExportMetadata("Name", "Jon")]
    [ExportMetadata("Age", 48)]
    public class RandomGuy : IPerson
    {
        public string FullName()
        {
            return "Jon Dough";
        }
    }

    //Advanced Metadata section
    //This is the interface we will use to
    //describe the metadata being applied to
    //our exports.
    public interface IPersonMetadata
    {
        string Name { get; }
        int Age { get; }
    }

    #endregion
}