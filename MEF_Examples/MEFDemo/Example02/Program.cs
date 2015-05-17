using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

//This program handles the following sections:
// 1. Allowing null Imports
// 2. Object Instancing
// 3. Specifying Export Types

namespace Example02
{
    class Program
    {
        #region Imports section
        
        //Allow null Imports section
        //This is a constructor that doesn't
        //have to be satisfied.  In this case,
        //we won't satisfy it intentionally.
        [Import(AllowDefault = true)]
        string notGoingToBeFound;

        //Object Instancing section
        //We will use the default, which means it
        //will be a Singleton
        [Import(typeof(AnyWayGoesClass))]
        AnyWayGoesClass singleImplicit;

        //Object Instancing section
        //We will explicitly make this a Singleton
        //by specifying Shared.
        [Import(typeof(AnyWayGoesClass), RequiredCreationPolicy = CreationPolicy.Shared)]
        AnyWayGoesClass singleExplicit;

        //Object Instancing section
        //We will make this instance its own
        //copy (non-shared)
        [Import(typeof(AnyWayGoesClass), RequiredCreationPolicy = CreationPolicy.NonShared)]
        AnyWayGoesClass uniqueExplicit;

        //Specifying Export Types section
        //We are looking for the specific export by
        //specifying both the contract name and
        //contract type.  If we only included one of
        //these, we would not find the part, since the
        //part specified both
        [Import("MyContractName", typeof(ExportTypeClass))]
        ExportTypeClass reallySpecific;

        #endregion

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        void Run()
        {
            Compose();
            
            //Allow null Imports section
            //Checks to see if our variable has
            //been populated
            Console.WriteLine("Allow null Imports section");
            if (notGoingToBeFound == null)
            {
                Console.WriteLine("Our notGoingToBeFound variable has not been hooked up.");
            }
            else
            {
                Console.WriteLine("Our notGoingToBeFound contains {0}", notGoingToBeFound);
            }
            Console.WriteLine();

            //Object Instancing section
            //We will call each instance in order.  The first
            //two should operate on the same instance (Singleton)
            //so the second call should return a 2 instead of a
            //1, where the last one should return a 1 again because
            //it is its own copy.
            Console.WriteLine("Object Instancing section");
            Console.WriteLine(singleImplicit.TimesRun());
            Console.WriteLine(singleExplicit.TimesRun());
            Console.WriteLine(uniqueExplicit.TimesRun());
            Console.WriteLine();

            //Specifying Export Types section
            //This just shows that MEF properly hooked
            //up the class
            Console.WriteLine("Specifying Export Types section");
            Console.WriteLine(reallySpecific.Run());
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

    #region Export section

    //Object Instancing section
    //This class has been set for a part creation policy
    //of Any, which means you can make it a Singleton
    //or per-instance (non-shared).  I have set the method
    //up so that it will tell us how many times it has been run.
    //This will show whether it is acting as a Singleton or
    //its own instance.
    [Export, PartCreationPolicy(CreationPolicy.Any)]
    public class AnyWayGoesClass
    {
        private int _count = 0;

        public string TimesRun() 
        {
            _count += 1;
            return string.Format("AnyWayGoesClass has been run {0} time(s)", _count);
        }
    }

    //Specifying Export Types section
    //Here we are specifying the contract name (MyContractName)
    //and the contract type (ExportTypeClass).  This means
    //that only Import statements that are looking
    //for these specific criteria can find this part
    [Export("MyContractName",typeof(ExportTypeClass))]
    public class ExportTypeClass
    {
        public string Run()
        {
            return "The Specifying Export Types class hooked up properly.";
        }
    }

    //[InheritedExport(typeof(IStateRule))]
    public interface IStateRule
    {
        string StateBird();
    }

    [Export(typeof(IStateRule))]
    [ExportMetadata("StateName", "Utah")]
    [ExportMetadata("ActiveRule", true)]
    public class UtahRule : IStateRule
    {
        public string StateBird()
        {
            return "Common American Gull";
        }
    }

    [Export(typeof(IStateRule))]
    [ExportMetadata("StateName", "Ohio")]
    [ExportMetadata("ActiveRule", true)]
    public class OhioRule : IStateRule
    {
        public string StateBird()
        {
            return "Cardinal";
        }
    }

    public interface IStateRuleMetadata
    {
        string StateName { get; }
    }

    #endregion
}
