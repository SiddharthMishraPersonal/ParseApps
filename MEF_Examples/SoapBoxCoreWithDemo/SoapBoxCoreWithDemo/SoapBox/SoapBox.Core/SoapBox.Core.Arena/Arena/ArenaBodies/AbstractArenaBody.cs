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
using System.Windows.Media;
using System.ComponentModel;

namespace SoapBox.Core.Arena
{
    public abstract class AbstractArenaBody : AbstractViewModel, IArenaBody 
    {
        #region "InitialX"
        public float InitialX
        {
            get
            {
                return m_InitialX;
            }
            protected set
            {
                if (m_InitialX != value)
                {
                    m_InitialX = value;
                    NotifyPropertyChanged(m_InitialXArgs);
                }
            }
        }
        private float m_InitialX;
        static readonly PropertyChangedEventArgs m_InitialXArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaBody>(o => o.InitialX);
        #endregion
        #region "InitialY"
        public float InitialY
        {
            get
            {
                return m_InitialY;
            }
            protected set
            {
                if (m_InitialY != value)
                {
                    m_InitialY = value;
                    NotifyPropertyChanged(m_InitialYArgs);
                }
            }
        }
        private float m_InitialY;
        static readonly PropertyChangedEventArgs m_InitialYArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaBody>(o => o.InitialY);
        #endregion
        #region "InitialAngle"
        public float InitialAngle
        {
            get
            {
                return m_InitialAngle;
            }
            protected set
            {
                if (m_InitialAngle != value)
                {
                    m_InitialAngle = value;
                    NotifyPropertyChanged(m_InitialAngleArgs);
                }
            }
        }
        private float m_InitialAngle;
        static readonly PropertyChangedEventArgs m_InitialAngleArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaBody>(o => o.InitialAngle);
        #endregion

        #region "Restitution"
        public float Restitution
        {
            get
            {
                return m_Restitution;
            }
            protected set
            {
                if (m_Restitution != value)
                {
                    if (value <= 0f || value > 1.0f)
                    {
                        throw new ArgumentOutOfRangeException(m_RestitutionName);
                    }
                    m_Restitution = value;
                    NotifyPropertyChanged(m_RestitutionArgs);
                }
            }
        }
        private float m_Restitution = 0.5f;
        static readonly PropertyChangedEventArgs m_RestitutionArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaBody>(o => o.Restitution);
        static readonly string m_RestitutionName =
            NotifyPropertyChangedHelper.GetPropertyName<AbstractArenaBody>(o => o.Restitution);
        #endregion
        #region "Friction"
        public float Friction
        {
            get
            {
                return m_Friction;
            }
            protected set
            {
                if (m_Friction != value)
                {
                    if (value <= 0f || value > 1.0f)
                    {
                        throw new ArgumentOutOfRangeException(m_FrictionName);
                    } 
                    m_Friction = value;
                    NotifyPropertyChanged(m_FrictionArgs);
                }
            }
        }
        private float m_Friction = 0.5f;
        static readonly PropertyChangedEventArgs m_FrictionArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaBody>(o => o.Friction);
        static readonly string m_FrictionName =
            NotifyPropertyChangedHelper.GetPropertyName<AbstractArenaBody>(o => o.Friction);
        #endregion

        #region "State"
        public ArenaBodyState State
        {
            get
            {
                return m_State;
            }
        }
        private ArenaBodyState m_State = new ArenaBodyState();
        #endregion

        #region "Sprite"
        public ISprite Sprite
        {
            get
            {
                return m_Sprite;
            }
            protected set
            {
                if (m_Sprite != value)
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(m_SpriteName);
                    }
                    m_Sprite = value;
                    NotifyPropertyChanged(m_SpriteArgs);
                }
            }
        }
        private ISprite m_Sprite = new PointSprite();
        static readonly PropertyChangedEventArgs m_SpriteArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArenaBody>(o => o.Sprite);
        static readonly string m_SpriteName =
            NotifyPropertyChangedHelper.GetPropertyName<AbstractArenaBody>(o => o.Sprite);
        #endregion

        /// <summary>
        /// Override this in the derived class to take an action
        /// based on the new state (each iteration of the simulation)
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// Override this in the derived class to take an action
        /// when this body collides with another body in the arena
        /// </summary>
        /// <param name="otherBody"></param>
        public virtual void OnCollision(IArenaBody otherBody) { }
    }
}
