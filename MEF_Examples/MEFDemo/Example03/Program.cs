using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

//This program handles the following sections:
// 1. Inheriting an Export
// 2. Importing Multiple Identical Exports
// 3. Delaying Instance Creation

namespace Example03
{
    class Program
    {
        #region Imports section

        //This is our "normal" part to show
        //when the non lazy-load parts get created.
        //We will never call a method on this part,
        //we will just let it output to the console
        //when it gets created.
        [Import]
        InitializedWhen demo;

        //Importing Multiple Identical Exports section
        //Delaying Instance Creation section
        //I combined two sections here.  We will see
        //how both the ImportsMany works, as well as
        //how we can lazy-load our parts
        [ImportMany(typeof(IAncestor))]
        IEnumerable<Lazy<IAncestor>> ancestors;

        #endregion

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        void Run()
        {
            Compose();

            Console.WriteLine("Starting our code");

            //Loops through each ancestor and runs it
            foreach (Lazy<IAncestor> ancestor in ancestors)
            {
                Console.WriteLine(ancestor.Value.Relation());
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

    //This export will be used simply to contrast
    //when non lazy-loaded parts are created.
    [Export]
    public class InitializedWhen
    {
        public InitializedWhen()
        {
            Console.WriteLine("This is the non lazy-loaded part.");
        }
    }

    //Inheriting an Export section
    //Here we are using an inherited export
    //on an interface that we will use in
    //the two classes directly below this
    //interface.  The two classes won't have
    //an Export statement, but they will still
    //be exported.
    [InheritedExport(typeof(IAncestor))]
    public interface IAncestor
    {
        string Relation();
    }

    public class Parent : IAncestor
    {
        //I included this section so that we can see
        //when the part is created.  This will be used
        //to demonstrate Lazy loading.
        public Parent()
        {
            Console.WriteLine("The parent instance was created");
        }

        public string Relation()
        {
            return "Your parent";
        }
    }

    public class Grandparent : IAncestor
    {
        public Grandparent()
        {
            Console.WriteLine("The grandparent instance was created");
        }

        public string Relation()
        {
            return "Your grandparent";
        }
    }


    #endregion
}
