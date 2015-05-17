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

namespace SoapBox.Demo.PinBall
{
    [Export(SoapBox.Core.ExtensionPoints.Workbench.Pads, typeof(IPad))]
    [Export(CompositionPoints.Workbench.Pads.Instructions, typeof(Instructions))]
    [Pad(Name = Instructions.PAD_NAME)]
    class Instructions : AbstractPad
    {
        public const string PAD_NAME = "Instructions";

        public Instructions()
        {
            Name = PAD_NAME;
            Title = Resources.Strings.Pads_Instructions_Title;
        }

        public string Text
        {
            get
            {
                return Resources.Strings.Pads_Instructions_Text;
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
