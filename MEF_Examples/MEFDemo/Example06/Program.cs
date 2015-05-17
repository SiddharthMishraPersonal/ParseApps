using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

//This program handles the following sections:
// 1. Constructor Injection
// 2. Getting an Export Manually
// 3. Getting an Alert when MEF is Done
// 4. Shut Down MEF Early

namespace Example06
{
    class Program : IPartImportsSatisfiedNotification
    {
        //This is at the program level so that
        //I can access it again directly later.
        //Normally you would protect this better
        //but this is just for demo purposes.
        CompositionContainer container;

        #region Imports section

        //Used simply to allow us to play with
        //the InjectedInfo class in the Demo
        //method below.
        [Import]
        InjectedInfo demo;

        //Constructor Injection section
        //Here I am importing the "Animal" class
        //instance via an importing constructor.
        //I am then exporting the entire class
        //so I can have an instance of it to
        //play with in the Run method.
        [Export]
        public class InjectedInfo
        {
            private Animal _cow;

            [ImportingConstructor]
            public InjectedInfo(Animal cow)
            {
                _cow = cow;
                //I could run other logic here to
                //initialize the cow or other
                //instances if I wanted.
            }

            //This actually calls the method and
            //returns the value just to show that
            //everything worked properly.
            public string GetCowSound()
            {
                return _cow.AnimalSound();
            }
        }

        #endregion

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        void Run()
        {
            Compose();

            //Constructor Injection section
            Console.WriteLine("Constructor Injection section");
            Console.WriteLine(demo.GetCowSound());
            Console.WriteLine();

            //Getting an Export Manually section
            Console.WriteLine("Getting an Export Manually section");
            //Here I am loading up a variable manually by
            //dipping into the container and asking for the
            //specific instance.
            Animal cow = container.GetExportedValue<Animal>();
            Console.WriteLine(cow.AnimalSound());
            Console.WriteLine();

            //Shut Down MEF Early section
            Console.WriteLine("Shut Down MEF Early section");
            //This will close the container and dispose of any
            //parts that MEF still controls.  However, any instances
            //that have already been loaded by MEF will still
            //work.  The exception is the lazy-loaded instance,
            //which, if not loaded, will be closed out.
            container.Dispose();
            Console.WriteLine("The container has now been closed.");
            Console.WriteLine();

            Console.ReadKey();
        }

        private void Compose()
        {
            AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
        }

        //Getting an Alert when MEF is Done section
        //This will fire to let us know that MEF has
        //hooked everything up.  You could put logic
        //here to work with your imports or do other
        //tasks.
        public void OnImportsSatisfied()
        {
            Console.WriteLine("Getting an Alert when MEF is Done section");
            Console.WriteLine("The imports have all been satisfied and our application is ready to go.");
            Console.WriteLine();
        }
    }

    #region Exports section

    //This will be used to demonstrate 
    //constructor injection
    [Export]
    public class Animal
    {
        public string AnimalSound()
        {
            return "The cow says moo.";
        }
    }

    #endregion
}
