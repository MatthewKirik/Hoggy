using MahApps.Metro.Controls;
using PresentationLayer.Models;
using PresentationLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditCardWindow.xaml
    /// </summary>
    public partial class EditCardWindow : MetroWindow
    {
        public EditCardWindow(CardModel card)
        {
            DataContext = new EditCardViewModel(card, this);
            InitializeComponent();
        }
    }
}
