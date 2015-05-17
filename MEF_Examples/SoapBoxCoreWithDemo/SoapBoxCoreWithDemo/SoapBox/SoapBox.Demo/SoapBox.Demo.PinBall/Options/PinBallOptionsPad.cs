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
using System.ComponentModel;

namespace SoapBox.Demo.PinBall
{
    [Export(CompositionPoints.PinBall.OptionsPad, typeof(PinBallOptionsPad))]
    class PinBallOptionsPad : AbstractOptionsPad
    {
        public PinBallOptionsPad()
        {
            Name = "PinBallOptionsPad";
        }

        public override void Commit()
        {
            base.Commit();
            Properties.Settings.Default.Save();
        }

        #region "Sound"

        public bool SoundEdit
        {
            get
            {
                return m_SoundEdit;
            }
            set
            {
                if (m_SoundEdit != value)
                {
                    m_SoundEdit = value;
                    CommitActions.Add(
                        () => Properties.Settings.Default.Sound = m_SoundEdit);
                    CancelActions.Add(
                        () =>
                        {
                            m_SoundEdit = Properties.Settings.Default.Sound;
                            NotifyPropertyChanged(m_SoundEditArgs);
                            NotifyPropertyChanged(m_SoundArgs);
                        });
                    NotifyOptionChanged();
                    NotifyPropertyChanged(m_SoundEditArgs);
                }
            }
        }
        private bool m_SoundEdit = Properties.Settings.Default.Sound;
        static readonly PropertyChangedEventArgs m_SoundEditArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallOptionsPad>(o => o.SoundEdit);

        public bool Sound
        {
            get
            {
                return Properties.Settings.Default.Sound;
            }
        }
        static readonly PropertyChangedEventArgs m_SoundArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallOptionsPad>(o => o.Sound);
        #endregion

    }
}
