﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zhingur.Chat.Module.ViewModels;

namespace Zhingur.Chat.Module.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ucSettingsView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ucSettingsView : UserControl
    {
        public ucSettingsView()
        {
            InitializeComponent();
        }

        [Import]
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return this.DataContext as SettingsViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
