#region "SoapBox.Core License"
/// <header module="SoapBox.Core"> 
/// Copyright (C) 2009 SoapBox Automation Inc., All Rights Reserved.
/// Contact: SoapBox Automation Licencing (license@soapboxautomation.com)
/// 
/// This file is part of SoapBox Core.
/// 
/// Commercial Usage
/// Licensees holding valid SoapBox Automation Commercial licenses may use  
/// this file in accordance with the SoapBox Automation Commercial License
/// Agreement provided with the Software or, alternatively, in accordance 
/// with the terms contained in a written agreement between you and
/// SoapBox Automation Inc.
/// 
/// GNU Lesser General Public License Usage
/// SoapBox Core is free software: you can redistribute it and/or modify 
/// it under the terms of the GNU Lesser General Public License
/// as published by the Free Software Foundation, either version 3 of the
/// License, or (at your option) any later version.
/// 
/// SoapBox Core is distributed in the hope that it will be useful, 
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Lesser General Public License for more details.
/// 
/// You should have received a copy of the GNU Lesser General Public License 
/// along with SoapBox Core. If not, see <http://www.gnu.org/licenses/>.
/// </header>
#endregion
        
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SoapBox.Core.Arena
{
    public abstract class AbstractArenaDynamicBody : AbstractArenaBody, IArenaDynamicBody 
    {

        #region "Mass"
        public float Mass
        {
            get
            {
                return m_Mass;
            }
            protected set
            {
                if (m_Mass != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(m_MassName);
                    }
                    m_Mass = value;
                    NotifyPropertyChanged(m_MassArgs);
                }
            }
        }
        private float m_Mass;
        static readonly PropertyChangedEventArgs m_MassArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaDynamicBody>(o => o.Mass);
        static readonly string m_MassName =
            NotifyPropertyChangedHelper.GetPropertyName<AbstractArenaDynamicBody>(o => o.Mass);
        #endregion
        #region "IgnoresGravity"
        public bool IgnoresGravity
        {
            get
            {
                return m_IgnoresGravity;
            }
            protected set
            {
                if (m_IgnoresGravity != value)
                {
                    m_IgnoresGravity = value;
                    NotifyPropertyChanged(m_IgnoresGravityArgs);
                }
            }
        }
        private bool m_IgnoresGravity;
        static readonly PropertyChangedEventArgs m_IgnoresGravityArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaDynamicBody>(o => o.IgnoresGravity);
        #endregion

        #region "Torque"
        public float Torque
        {
            get
            {
                return m_Torque;
            }
            protected set
            {
                if (m_Torque != value)
                {
                    m_Torque = value;
                    NotifyPropertyChanged(m_TorqueArgs);
                }
            }
        }
        private float m_Torque;
        static readonly PropertyChangedEventArgs m_TorqueArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaDynamicBody>(o => o.Torque);
        #endregion
        #region "Force"
        public ArenaVector Force
        {
            get
            {
                return m_Force;
            }
        }
        private readonly ArenaVector m_Force = new ArenaVector();
        #endregion
        #region "ForcePosition"
        public ArenaPoint ForcePosition
        {
            get
            {
                return m_ForcePosition;
            }
        }
        private readonly ArenaPoint m_ForcePosition = new ArenaPoint();
        #endregion
        #region "Impulse"
        public ArenaVector Impulse
        {
            get
            {
                return m_Impulse;
            }
        }
        private readonly ArenaVector m_Impulse = new ArenaVector();
        #endregion
        #region "ImpulsePosition"
        public ArenaPoint ImpulsePosition
        {
            get
            {
                return m_ImpulsePosition;
            }
        }
        private ArenaPoint m_ImpulsePosition = new ArenaPoint();
        #endregion

    }
}
