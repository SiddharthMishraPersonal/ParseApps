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

namespace SoapBox.Demo.PinBall
{
    public class PinBallLauncherFlipper : PinBallFlipper
    {
        public PinBallLauncherFlipper(PinBallTable table)
            : base( table.DimensionX / 2f - PinBallLauncher.LAUNCHER_WIDTH + PinBallFlipper.CENTER_TO_CENTER/2f - PinBallFlipper.WIDE_WIDTH*2.4f, //x
                    table.DimensionY / 2f - 279f, //y
                    0,   //angle
                    0.3f) //mass
        {
            IgnoresGravity = true;
            ControlTorque = -20000f; // keeps it shut
        }
    }
}
