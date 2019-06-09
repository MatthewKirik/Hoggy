using GalaSoft.MvvmLight;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class TagsWindowViewModel : ViewModelBase
    {
        public ObservableCollection<TagModel> Tags { get; set; }
        public TagsWindowViewModel(ObservableCollection<TagModel> tags, Window window)
        {
            Tags = tags;
        }
    }
}
