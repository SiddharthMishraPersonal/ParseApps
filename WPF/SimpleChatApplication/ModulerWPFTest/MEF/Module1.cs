// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Module1.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Module1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ModulerWPFTest.MEF
{
    using System.ComponentModel.Composition;
    using System.Windows.Controls;

    using ModulerWPFTest.Views;

    [Export(typeof(IModule))]
    public class Module1 : IModule
    {
        public string Name
        {
            get
            {
                return "Module 1";
            }
        }

        public UserControl UserInterface
        {
            get { return new Module1View(); }
        }
    }
}
