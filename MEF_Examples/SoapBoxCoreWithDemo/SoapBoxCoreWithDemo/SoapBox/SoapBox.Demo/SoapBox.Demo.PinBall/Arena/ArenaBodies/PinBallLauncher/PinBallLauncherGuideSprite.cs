﻿#region "SoapBox.Demo License"
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
using SoapBox.Core.Arena;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using System.Media;

namespace SoapBox.Demo.PinBall
{

    public class PinBallLauncherGuideSprite : AbstractSprite
    {
        public PinBallLauncherGuideSprite(PinBallTable table)
        {
            Rect rect = new Rect(new Point(table.DimensionX / 2f - PinBallLauncher.LAUNCHER_WIDTH - PinBallTableEdge.SIDE_WIDTH, -table.DimensionY/2f + 70f),
                                new Point(table.DimensionX / 2f - PinBallLauncher.LAUNCHER_WIDTH, table.DimensionY/2f - 350f));
            Geometry = new RectangleGeometry(rect);
        }
    }

}
