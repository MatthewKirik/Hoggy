using MahApps.Metro.Controls;
using PresentationLayer.Models;
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
    /// Interaction logic for AddEditColumnWindow.xaml
    /// </summary>
    public partial class AddEditColumnWindow : MetroWindow
    {
        public AddEditColumnWindow(int boardId, ColumnModel column = null)
        {
            DataContext = new AddEditColumnViewModel(this, boardId, column);
            InitializeComponent();
        }
    }
}
