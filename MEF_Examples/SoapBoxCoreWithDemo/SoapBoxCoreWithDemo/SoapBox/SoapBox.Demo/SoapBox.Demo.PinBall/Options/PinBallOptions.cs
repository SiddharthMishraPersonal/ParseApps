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
    [Export(SoapBox.Core.ExtensionPoints.Options.OptionsDialog.OptionsItems, typeof(IOptionsItem))]
    class PinBallOptions : AbstractOptionsItem, IPartImportsSatisfiedNotification
    {
        public PinBallOptions()
        {
            Header = Resources.Strings.Options_PinBallOptions_Header;
        }

        [Import(SoapBox.Core.Services.Host.ExtensionService)] 
        private IExtensionService extensionService { get; set; }

        [ImportMany(ExtensionPoints.PinBall.PinBallOptionItems, typeof(IOptionsItem), AllowRecomposition=true)] 
        private IEnumerable<IOptionsItem> items { get; set; }

        [Import(CompositionPoints.PinBall.OptionsPad, typeof(PinBallOptionsPad))] 
        private PinBallOptionsPad pad { get; set; }

        public void OnImportsSatisfied()
        {
            Items = extensionService.Sort(items);
            Pad = pad;
        }
    }

    /// <summary>
    /// By default, most options pages in Windows have a first sub-item
    /// that is the "General" one, which is the same options pad as the
    /// parent in the tree.  This just mimics that.
    /// </summary>
    [Export(ExtensionPoints.PinBall.PinBallOptionItems, typeof(IOptionsItem))]
    class PinBallOptionsGeneral : AbstractOptionsItem, IPartImportsSatisfiedNotification
    {
        public PinBallOptionsGeneral()
        {
            Header = Resources.Strings.Options_PinBallOptions_General;
        }

        [Import(CompositionPoints.PinBall.OptionsPad, typeof(PinBallOptionsPad))]
        private PinBallOptionsPad pad { get; set; }

        public void OnImportsSatisfied()
        {
            Pad = pad;
        }
    }
}
