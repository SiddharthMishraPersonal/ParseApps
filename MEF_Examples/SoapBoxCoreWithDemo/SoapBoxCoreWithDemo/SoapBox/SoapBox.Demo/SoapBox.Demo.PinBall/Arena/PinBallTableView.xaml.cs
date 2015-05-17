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
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SoapBox.Core.Arena;
using SoapBox.Core;

namespace SoapBox.Demo.PinBall
{
    [Export(SoapBox.Core.ExtensionPoints.Host.Views, typeof(ResourceDictionary))]
    public partial class PinBallTableView : ResourceDictionary 
    {
        public PinBallTableView()
        {
            InitializeComponent();
        }

        #region "Keyboard Input"
        private void Arena_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnKeyDown(sender, e);
                }
            }
        }

        private void Arena_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnKeyUp(sender, e);
                }
            }
        }
        #endregion

        #region "Loaded"
        /// <summary>
        /// When this view loads, grab focus, set the background to slightly
        /// non-transparent so we can receive mouse clicks and hook into 
        /// the viewmodel GotFocus event.
        /// </summary>
        private void Arena_Loaded(object sender, RoutedEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                p.Focus();
                p.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0)); //almost transparent, but allows hit test
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.GotFocus += new RoutedEventHandler(Arena_GotFocus);
                    if (m_canvasLookup.ContainsKey(a))
                    {
                        m_canvasLookup.Remove(a);
                    }
                    m_canvasLookup.Add(a, p);
                }
            }
        }
        #endregion

        #region "Focus"
        private readonly Dictionary<AbstractArena, Panel> m_canvasLookup = new Dictionary<AbstractArena, Panel>();

        // Sometimes the viewmodel tells us that we have focus.
        void Arena_GotFocus(object sender, RoutedEventArgs e)
        {
            AbstractArena a = sender as AbstractArena;
            if (a != null && m_canvasLookup.ContainsKey(a))
            {
                m_canvasLookup[a].Focus();
            }
        }
        #endregion

        #region "Mouse Input"
        private void Arena_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Grab focus
            Panel c = sender as Panel;
            if (c != null)
            {
                c.Focus();
            }

            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseDown(sender, e);
                }
            }
        }

        private void Arena_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseUp(sender, e);
                }
            }
        }

        private void Arena_MouseMove(object sender, MouseEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseMove(sender, e);
                }
            }
        }

        private void Arena_MouseWheel(object sender, MouseEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseWheel(sender, e);
                }
            }
        }

        private void Arena_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseLeftButtonDown(sender, e);
                }
            }
        }

        private void Arena_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseLeftButtonUp(sender, e);
                }
            }
        }

        private void Arena_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseRightButtonDown(sender, e);
                }
            }
        }

        private void Arena_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseRightButtonUp(sender, e);
                }
            }
        }

        private void Arena_MouseEnter(object sender, MouseEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseEnter(sender, e);
                }
            }
        }

        private void Arena_MouseLeave(object sender, MouseEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                AbstractArena a = p.DataContext as AbstractArena;
                if (a != null)
                {
                    a.OnMouseLeave(sender, e);
                }
            }
        }

        #endregion

        private void DockPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                PinBallTable t = p.DataContext as PinBallTable;
                if (t != null)
                {
                    t.Play();
                }
            }
        }

        private void DockPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null)
            {
                PinBallTable t = p.DataContext as PinBallTable;
                if (t != null)
                {
                    t.Pause();
                }
            }
        }

    }
}
