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

namespace SoapBox.Core
{
    public abstract class AbstractControl : AbstractExtension, IControl 
    {
        public AbstractControl()
        {
        }

        #region " ToolTip "
        /// <summary>
        /// This is the tool tip displayed when the mouse hovers over the control.
        /// Best practice is to set this in the constructor of the derived
        /// class.
        /// </summary>
        public string ToolTip
        {
            get
            {
                return m_ToolTip;
            }
            protected set
            {
                if (m_ToolTip != value)
                {
                    if (value == string.Empty)
                    {
                        m_ToolTip = null;
                    }
                    else
                    {
                        m_ToolTip = value;
                    }
                    NotifyPropertyChanged(m_ToolTipArgs);
                }
            }
        }
        private string m_ToolTip = null;
        static readonly PropertyChangedEventArgs m_ToolTipArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractControl>(o => o.ToolTip);
        #endregion

        #region " Visible "
        /// <summary>
        /// Defaults to true. Set to false to make the control disappear.
        /// </summary>
        public bool Visible
        {
            get
            {
                return m_Visible;
            }
            private set
            {
                if (m_Visible != value)
                {
                    m_Visible = value;
                    NotifyPropertyChanged(m_VisibleArgs);
                }
            }
        }
        private bool m_Visible = true;
        static readonly PropertyChangedEventArgs m_VisibleArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractControl>(o => o.Visible);
        #endregion

        #region " VisibleCondition "
        /// <summary>
        /// Set this to any ICondition object, and it will control
        /// the Visible property.
        /// </summary>
        public ICondition VisibleCondition
        {
            get
            {
                return m_VisibleCondition;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(m_VisibleConditionName);
                }
                if (m_VisibleCondition != value)
                {
                    if (m_VisibleCondition != null)
                    {
                        //remove the old event handler
                        m_VisibleCondition.ConditionChanged -= OnVisibleConditionChanged;
                    }
                    m_VisibleCondition = value;
                    //add the new event handler
                    m_VisibleCondition.ConditionChanged += OnVisibleConditionChanged;
                    Visible = m_VisibleCondition.Condition;

                    NotifyPropertyChanged(m_VisibleConditionArgs);
                }
            }
        }
        private ICondition m_VisibleCondition = null;
        static readonly PropertyChangedEventArgs m_VisibleConditionArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractControl>(o => o.VisibleCondition);
        static readonly string m_VisibleConditionName =
            NotifyPropertyChangedHelper.GetPropertyName<AbstractControl>(o => o.VisibleCondition);

        private void OnVisibleConditionChanged(object sender, EventArgs e)
        {
            Visible = m_VisibleCondition.Condition;
        }
        #endregion

    }
}
