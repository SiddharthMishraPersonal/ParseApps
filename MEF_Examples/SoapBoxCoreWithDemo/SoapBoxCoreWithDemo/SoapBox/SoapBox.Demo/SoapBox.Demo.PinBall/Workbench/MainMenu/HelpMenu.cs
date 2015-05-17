#region "SoapBox.Demo License"
/// <header module="SoapBox.Demo"> 
/// Copyright (C) 2009 SoapBox Automation Inc., All Rights Reserved.
/// Contact: SoapBox Automation Licencing (license@soapboxautomation.com)
/// 
/// This file is part of SoapBox Demo.
/// 
/// GNU Lesser General Public License Usage
/// SoapBox Demo is free software: you can redistribute it and/or modify 
/// it under the terms of the GNU Lesser General Public License
/// as published by the Free Software Foundation, either version 3 of the
/// License, or (at your option) any later version.
/// 
/// SoapBox Demo is distributed in the hope that it will be useful, 
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Lesser General Public License for more details.
/// 
/// You should have received a copy of the GNU Lesser General Public License 
/// along with SoapBox Demo. If not, see <http://www.gnu.org/licenses/>.
/// </header>
#endregion
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoapBox.Core;
using System.ComponentModel.Composition;
using System.Windows;

namespace SoapBox.Demo.PinBall
{
    [Export(SoapBox.Core.ExtensionPoints.Workbench.MainMenu.HelpMenu, typeof(IMenuItem))]
    class HelpMenuInstructions : AbstractMenuItem
    {
        public HelpMenuInstructions()
        {
            ID = "Instructions";
            Header = Resources.Strings.Workbench_MainMenu_Help_Instructions;
        }

        [Import(CompositionPoints.Workbench.Pads.Instructions, typeof(Instructions))]
        private Lazy<Instructions> instructionsPad { get; set; }

        [Import(SoapBox.Core.Services.Layout.LayoutManager, typeof(ILayoutManager))]
        private Lazy<ILayoutManager> layoutManager { get; set; }

        protected override void Run()
        {
            base.Run();
            layoutManager.Value.ShowPad(instructionsPad.Value);
        }
    }

    [Export(SoapBox.Core.ExtensionPoints.Workbench.MainMenu.HelpMenu, typeof(IMenuItem))]
    class HelpMenuAbout : AbstractMenuItem
    {
        public HelpMenuAbout()
        {
            ID = "About";
            Header = Resources.Strings.Workbench_MainMenu_Help_About;
        }

        [Import(SoapBox.Core.CompositionPoints.Host.MainWindow)]
        private Lazy<Window> mainWindowExport { get; set; }

        protected override void Run()
        {
            base.Run();
            AboutBoxView dlg = new AboutBoxView();
            dlg.Owner = mainWindowExport.Value;
            dlg.ShowDialog();
        }
    }
}
