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
using SoapBox.Core;
using System.ComponentModel.Composition;

namespace SoapBox.Demo.HighScores
{
    /// <summary>
    /// Add a menu item to the view menu to launch the HighScores "Pad"
    /// </summary>
    [Export(SoapBox.Core.ExtensionPoints.Workbench.MainMenu.ViewMenu, typeof(IMenuItem))]
    class ViewMenuHighScores : AbstractMenuItem
    {
        public ViewMenuHighScores()
        {
            ID = "HighScores";

            InsertRelativeToID = "ToolBars";
            BeforeOrAfter = RelativeDirection.Before;

            Header = Resources.Strings.Workbench_MainMenu_View_HighScores;
            ToolTip = Resources.Strings.Workbench_MainMenu_View_HighScores_ToolTip;
        }

        [Import(SoapBox.Core.Services.Layout.LayoutManager, typeof(ILayoutManager))]
        private Lazy<ILayoutManager> layoutManager { get; set; }

        [Import(CompositionPoints.Workbench.Pads.HighScores, typeof(HighScores))]
        private Lazy<HighScores> highScoresPad { get; set; }

        protected override void Run()
        {
            layoutManager.Value.ShowPad(highScoresPad.Value);
        }
    }
}
