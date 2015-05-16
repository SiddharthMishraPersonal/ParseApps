// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMain.cs" company="">
//   
// </copyright>
// <summary>
//   It will export the interface and all the classes which inherits this Interface
//   so need to write Export on classes which inherits this interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Zhingur.Contract.InheritedContract
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// It will export the interface and all the classes which inherits this Interface 
    /// so need to write Export on classes which inherits this interface.
    /// </summary>
    [InheritedExport(typeof(IMain))]
    public interface IMain
    {
        string NameOfUserControl { get; }
    }
}
