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
using System.ComponentModel.Composition;
using SoapBox.Core;
using System.Collections.ObjectModel;

namespace SoapBox.Demo.PinBall
{
    [Export(SoapBox.Core.ExtensionPoints.Workbench.StatusBar, typeof(IStatusBarItem))]
    [Export(CompositionPoints.Workbench.StatusBar.BallsScoreSeparator, typeof(BallsScoreSeparator))]
    public class BallsScoreSeparator : AbstractStatusBarSeparator
    {
        public BallsScoreSeparator()
        {
            ID = "BallsScoreSeparator";
            InsertRelativeToID = "BallCounterText";
            BeforeOrAfter = RelativeDirection.After;
        }
    }

    [Export(SoapBox.Core.ExtensionPoints.Workbench.StatusBar, typeof(IStatusBarItem))]
    [Export(CompositionPoints.Workbench.StatusBar.ScoreDisplayHeading, typeof(ScoreDisplayHeading))]
    public class ScoreDisplayHeading : AbstractStatusBarLabel
    {
        public ScoreDisplayHeading()
        {
            ID = "ScoreDisplayHeading";
            Text = Resources.Strings.Workbench_StatusBar_ScoreDisplay_Score;
            InsertRelativeToID = "BallsScoreSeparator";
            BeforeOrAfter = RelativeDirection.After;
        }
    }

    [Export(SoapBox.Core.ExtensionPoints.Workbench.StatusBar, typeof(IStatusBarItem))]
    [Export(CompositionPoints.Workbench.StatusBar.ScoreDisplayText, typeof(ScoreDisplayText))]
    public class ScoreDisplayText : AbstractStatusBarLabel
    {
        public ScoreDisplayText()
        {
            ID = "ScoreDisplayText";
            Text = "";
            InsertRelativeToID = "ScoreDisplayHeading";
            BeforeOrAfter = RelativeDirection.After;
        }

        public void SetScore(int score)
        {
            Text = score.ToString();
        }
    }
}
