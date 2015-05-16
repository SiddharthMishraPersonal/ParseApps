using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModuleWPFTest.UnitTest
{
    using System.Windows.Controls;

    using ModulerWPFTest.MEF;
    using ModulerWPFTest.ViewModels;

    class FakeModule : IModule
    {

        public string Name
        {
            get
            {
                return "Fake Module.";
            }
        }

        public UserControl UserInterface
        {
            get
            {
                return null;
            }
        }
    }

    [TestClass]
    public class MainWindowViewModelTest
    {
        [TestMethod]
        public void FirstModuleShouldBeSelectedByDefault()
        {
            FakeModule faleModule = new FakeModule();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(new FakeModule[] { faleModule, new FakeModule(), new FakeModule() });
            Assert.AreSame(mainWindowViewModel.SelectedModule,faleModule,"Not same.");
        }
    }
}
