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
using System.Windows.Media.Imaging;

namespace SoapBox.Demo.PinBall
{
    /// <summary>
    /// This just makes sure that when we startup, the Pin Ball Table is always visible
    /// </summary>
    [Export(SoapBox.Core.ExtensionPoints.Host.StartupCommands, typeof(IExecutableCommand))]
    class StartupCommand : AbstractExtension, IExecutableCommand
    {

        [Import(SoapBox.Core.CompositionPoints.Host.MainWindow, typeof(Window))]
        private Lazy<Window> mainWindow { get; set; }

        [Import(SoapBox.Core.Services.Layout.LayoutManager, typeof(ILayoutManager))]
        private Lazy<ILayoutManager> layoutManager { get; set; }

        [Import(CompositionPoints.PinBall.PinBallTable, typeof(PinBallTable))]
        private Lazy<PinBallTable> pinBallTable { get; set; }

        [Import(CompositionPoints.Workbench.Pads.Instructions, typeof(Instructions))]
        private Lazy<Instructions> instructionsPad { get; set; }

        public void Run(params object[] args)
        {
            mainWindow.Value.Title = Resources.Strings.Workbench_Title;

            layoutManager.Value.ShowDocument(pinBallTable.Value); // Makes sure it's shown every time
            layoutManager.Value.ShowPad(instructionsPad.Value);   // Makes sure it's shown the first time they use this app

            // Sorry, this is a really hacky way of setting the icon on the
            // main window, and only because I can't seem to convert from
            // a PNG to an icon any other way.
            Dummy dummy = new Dummy();
            mainWindow.Value.Icon = dummy.Icon;
            dummy.Close();
        }
    }
}
