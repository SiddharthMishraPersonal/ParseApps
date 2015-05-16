using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulerWPFTest.MEF
{
    using System.Windows.Controls;

    public interface IModule
    {
        string Name { get; }
        UserControl UserInterface { get; }
    }
}
