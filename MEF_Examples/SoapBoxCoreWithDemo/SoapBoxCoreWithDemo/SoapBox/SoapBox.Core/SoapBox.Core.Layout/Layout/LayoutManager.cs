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
using System.Linq;
using System.Text;
using SoapBox.Core;
using System.ComponentModel.Composition;
using AvalonDock;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using System.Windows;

namespace SoapBox.Core.Layout
{
    [Export(Services.Layout.LayoutManager, typeof(ILayoutManager))]
    public class LayoutManager : ILayoutManager 
    {
        private readonly string PADS_SAVEFILE = Path.Combine(AppEnvironment.UserFileDirectory(), "PadsSave.txt");
        private readonly string DOCS_SAVEFILE = Path.Combine(AppEnvironment.UserFileDirectory(), "DocsSave.txt");
        private readonly string LAYOUT_SAVEFILE = Path.Combine(AppEnvironment.UserFileDirectory(), "LayoutManagerSave.xml");

        private DocumentPane m_docPane = new DocumentPane(); // documents are put in here
        private ResizingPanel m_resizingPanel = new ResizingPanel(); // pads are docked to this

        public LayoutManager()
        {
            // Basic configuration:
            // DocumentPane 
            //   in a ResizingPanel 
            //      in a DockingManager
            m_docPane.Name = "DocumentPane";
            m_resizingPanel.Name = "ResizingPanel";
            m_resizingPanel.Children.Add(m_docPane);
            DockManager.Content = m_resizingPanel;
            
            DockManager.Loaded += new System.Windows.RoutedEventHandler(DockManager_Loaded);
            DockManager.LayoutUpdated += new EventHandler(DockManager_LayoutUpdated);

        }

        /// <summary>
        /// Shows a pad.  If it hasn't been shown before, it shows it
        /// docked to the right side.  Otherwise it restores it to the
        /// previous place that it was before hiding.  Doesn't work
        /// correctly for floating panes (yet).
        /// </summary>
        /// <param name="pad"></param>
        public void ShowPad(IPad pad)
        {
            if (!m_padLookup.ContainsKey(pad))
            {
                DockableContent content = new DockableContent();
                content.Content = pad;
                content.Title = pad.Title;
                content.Name = pad.Name;
                m_padLookup.Add(pad, content);
                DockablePane dp = new DockablePane();
                dp.Items.Add(content);
                m_resizingPanel.Children.Add(dp);
                content.GotFocus += new RoutedEventHandler(pad.OnGotFocus);
                content.LostFocus += new RoutedEventHandler(pad.OnLostFocus);
            }
            DockManager.Show(m_padLookup[pad]);
        }

        /// <summary>
        /// Shows a document.  Puts it in the document pane.
        /// </summary>
        /// <param name="document"></param>
        public void ShowDocument(IDocument document)
        {
            if (!m_documentLookup.ContainsKey(document))
            {
                DocumentContent content = new DocumentContent();
                content.Content = document;
                content.Title = document.Title;
                content.Name = document.Name;
                m_documentLookup.Add(document, content);
                m_docPane.Items.Add(content);
                content.Closing += new EventHandler<System.ComponentModel.CancelEventArgs>(document.OnClosing);
                content.Closed += new EventHandler(content_Closed);
                content.GotFocus += new RoutedEventHandler(document.OnGotFocus);
                content.LostFocus += new RoutedEventHandler(document.OnLostFocus);
                document.OnOpened(content, new EventArgs());
            }
            DockManager.Show(m_documentLookup[document]);
        }

        // Handles removing documents from the data structure when closed
        void content_Closed(object sender, EventArgs e)
        {
            DocumentContent content = sender as DocumentContent;
            IDocument document = content.Content as IDocument;
            m_documentLookup.Remove(document);
            document.OnClosed(sender, e);
        }

        /// <summary>
        /// The View binds to this property
        /// </summary>
        public DockingManager DockManager
        {
            get
            {
                return m_Content;
            }
        }
        private readonly DockingManager m_Content = new DockingManager();

        #region " ILayoutManager Members "

        public event EventHandler Loaded;
        public event EventHandler LayoutUpdated;

        /// <summary>
        /// Pass through the LayoutUpdated Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DockManager_LayoutUpdated(object sender, EventArgs e)
        {
            if (LayoutUpdated != null)
            {
                LayoutUpdated(sender, e);
            }
        }

        /// <summary>
        /// Pass through the Loaded Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DockManager_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Loaded != null)
            {
                Loaded(sender, new EventArgs());
            }
        }

        /// <summary>
        /// A collection of tool pads, etc., from the workbench
        /// </summary>
        public ReadOnlyCollection<IPad> Pads
        {
            get
            {
                return new ReadOnlyCollection<IPad>(m_padLookup.Keys.ToList());
            }
        }
        private readonly Dictionary<IPad, DockableContent> m_padLookup = new Dictionary<IPad, DockableContent>();

        /// <summary>
        /// A collection of documents from the document manager
        /// </summary>
        public ReadOnlyCollection<IDocument> Documents
        {
            get
            {
                return new ReadOnlyCollection<IDocument>(m_documentLookup.Keys.ToList());
            }
        }
        private readonly Dictionary<IDocument, DocumentContent> m_documentLookup = new Dictionary<IDocument, DocumentContent>();

        /// <summary>
        /// Returns true if the given pad is visible.
        /// </summary>
        /// <param name="pad"></param>
        /// <returns></returns>
        public bool IsVisible(IPad pad)
        {
            if (m_padLookup.ContainsKey(pad))
            {
                DockableContent content = m_padLookup[pad];
                return (content.State != DockableContentState.Hidden);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the given document is visible.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public bool IsVisible(IDocument document)
        {
            if (m_documentLookup.ContainsKey(document))
            {
                DocumentContent content = m_documentLookup[document];
                return content.IsActiveDocument;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Call this method to persist the current layout to disk.
        /// </summary>
        public void SaveLayout()
        {
            if (DockManager.IsLoaded)
            {
                // Save pads
                List<string> padNamesList = new List<string>();
                foreach (IPad pad in Pads)
                {
                    // We have to save all the pad names that have ever been
                    // shown even if they're hidden now or else the layout
                    // manager won't remember where they are when shown again.
                    padNamesList.Add(pad.Name);
                }

                string padNames = String.Join(",", padNamesList.ToArray());

                File.WriteAllText(PADS_SAVEFILE, padNames); 
                
                // Save documents
                List<string> docNamesList = new List<string>();
                foreach (IDocument doc in Documents)
                {
                    docNamesList.Add(doc.Name);
                }

                string docNames = String.Join(",", docNamesList.ToArray());

                File.WriteAllText(DOCS_SAVEFILE, docNames); 
                
                // Save layout
                DockManager.SaveLayout(LAYOUT_SAVEFILE);
            }
        }

        /// <summary>
        /// Call this method to restore the existing layout from disk.
        /// </summary>
        /// <param name="pads">A collection of all possible Pads</param>
        /// <param name="docs">A collection of all possible Documents</param>
        public void RestoreLayout(
            IEnumerable<Lazy<IPad,IPadMeta>> pads,
            IEnumerable<Lazy<IDocument,IDocumentMeta>> docs)
        {
            if (File.Exists(PADS_SAVEFILE))
            {
                string padNames = File.ReadAllText(PADS_SAVEFILE);
                List<string> padNamesList = padNames.Split(new char[] { ',' }).ToList();
                foreach (var p in pads)
                {
                    if (padNamesList.Contains(p.Metadata.Name))
                    {
                        ShowPad(p.Value);
                    }
                }
            }

            if (File.Exists(DOCS_SAVEFILE))
            {
                string docNames = File.ReadAllText(DOCS_SAVEFILE);
                List<string> docNamesList = docNames.Split(new char[] { ',' }).ToList();
                foreach (var d in docs)
                {
                    if (docNamesList.Contains(d.Metadata.Name))
                    {
                        ShowDocument(d.Value);
                    }
                }
            }

            if (File.Exists(LAYOUT_SAVEFILE))
            {
                DockManager.RestoreLayout(LAYOUT_SAVEFILE);
            }
        }
        #endregion
    }
}
