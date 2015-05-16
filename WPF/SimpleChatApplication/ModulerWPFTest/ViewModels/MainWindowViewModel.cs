using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulerWPFTest.ViewModels
{
    using System.Windows.Controls;

    using ModulerWPFTest.MEF;

    public class MainWindowViewModel : BaseViewModel
    {
        private IModule selectedModule;

        public IModule SelectedModule
        {
            get
            {
                return this.selectedModule;
            }
            set
            {
                this.selectedModule = value;
                this.RaisePropertyChanged("UserInterfaceControl");
                this.RaisePropertyChanged("SelectedModule");
            }
        }

        public List<IModule> Modules { get; set; }

        public UserControl UserInterfaceControl
        {
            get
            {
                if (selectedModule == null) return null;
                return selectedModule.UserInterface;
            }
        }

        public MainWindowViewModel(IEnumerable<IModule> modules)
        {
            this.Modules = modules.OrderBy(s => s.Name).ToList();

            if (this.Modules.Any())
            {
                this.SelectedModule = this.Modules[0];
            }
        }
    }
}
