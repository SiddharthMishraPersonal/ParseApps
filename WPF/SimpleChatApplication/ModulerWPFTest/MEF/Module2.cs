// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Module2.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Module2 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ModulerWPFTest.MEF
{
    using System.ComponentModel.Composition;
    using System.Windows.Controls;

    using ModulerWPFTest.Views;

    [Export(typeof(IModule))]
    public class Module2 : IModule
    {
        public string Name
        {
            get
            {
                return "Module 2";
            }
        }

        public UserControl UserInterface
        {
            get { return new Module2View(); }
        }
    }
}
