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
using System.Collections.ObjectModel;
using SoapBox.Core.Arena;
using SoapBox.Core;
using System.ComponentModel;

namespace SoapBox.Demo.PinBall
{
    public class PinBallTarget : AbstractViewModel
    {
        public const float WALL_THICKNESS = PinBallTableEdge.SIDE_WIDTH * 1.2f;
        public const float MOVEMENT_DISTANCE = PinBallTableEdge.SIDE_WIDTH;
        public const float TARGET_WIDTH = 60f;

        public PinBallTarget(float x, float y, float angle, bool latched)
        {
            m_Bodies.Add(new PinBallTargetCasing(x, y, angle));
            m_piston = new PinBallTargetPiston(x, y, angle, latched);
            m_Bodies.Add(m_piston);
            m_piston.PropertyChanged += new PropertyChangedEventHandler(m_piston_PropertyChanged);
        }

        public IEnumerable<IArenaBody> Bodies
        {
            get
            {
                return m_Bodies;
            }
        }
        private Collection<IArenaBody> m_Bodies = new Collection<IArenaBody>();

        public bool Hit
        {
            get
            {
                return m_piston.Hit;
            }
            set
            {
                if (m_piston.Hit != value)
                {
                    m_piston.Hit = value;
                    NotifyPropertyChanged(m_HitArgs);
                }
            }
        }
        private bool m_lastHit = false;
        private PinBallTargetPiston m_piston = null;
        static readonly PropertyChangedEventArgs m_HitArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallTarget>(o => o.Hit);

        void m_piston_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == m_HitName)
            {
                if (m_piston.Hit && !m_lastHit)
                {
                    NotifyScore();
                    SoundManager.PlayWav(Resources.Sounds.tack);
                }
                m_lastHit = m_piston.Hit;
                NotifyPropertyChanged(m_HitArgs);
            }
        }
        static readonly string m_HitName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallTargetPiston>(o => o.Hit);

        private void NotifyScore()
        {
            var evt = Score;
            if (evt != null)
            {
                evt(this, new EventArgs());
            }
        }
        public event EventHandler Score;
    }
}
