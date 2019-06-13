﻿using MahApps.Metro.Controls;
using PresentationLayer.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PresentationLayer.Windows
{
    /// <summary>
    /// Interaction logic for AddCardWindow.xaml
    /// </summary>
    public partial class AddCardWindow : MetroWindow
    {
        public AddCardWindow(int ColId)
        {
            DataContext =  new AddCardViewModel(ColId, this);
            InitializeComponent();
        }
    }
}
