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
    [Export(CompositionPoints.Workbench.StatusBar.BallCounterHeading, typeof(BallCounterHeading))]
    public class BallCounterHeading : AbstractStatusBarLabel
    {
        public BallCounterHeading()
        {
            ID = "BallCounterHeading";
            Text = Resources.Strings.Workbench_StatusBar_BallCounter_Balls;
            InsertRelativeToID = "PlaceHolder";
            BeforeOrAfter = RelativeDirection.After;
        }
    }

    [Export(SoapBox.Core.ExtensionPoints.Workbench.StatusBar, typeof(IStatusBarItem))]
    [Export(CompositionPoints.Workbench.StatusBar.BallCounterText, typeof(BallCounterText))]
    public class BallCounterText : AbstractStatusBarLabel
    {
        public BallCounterText()
        {
            ID = "BallCounterText";
            Text = "";
            InsertRelativeToID = "BallCounterHeading";
            BeforeOrAfter = RelativeDirection.After;
        }

        public void SetBallCount(int ballCount)
        {
            Text = ballCount.ToString();
        }
    }
}
