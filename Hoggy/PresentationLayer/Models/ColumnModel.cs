using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using PresentationLayer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.Models
{
    public class ColumnModel : ViewModelBase, IDropTarget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<CardModel> Cards { get; set; }
        public Action<int, int, int> MoveCardAct{ get; set; }

        public ColumnModel()
        {
            Cards = new ObservableCollection<CardModel>();
        }

        public void DragOver(IDropInfo dropInfo)
        {
            GongSolutions.Wpf.DragDrop.DragDrop.DefaultDropHandler.DragOver(dropInfo);
        }

        public void Drop(IDropInfo dropInfo)
        {
            MoveCardAct.Invoke((dropInfo.DragInfo.SourceItem as CardModel).Id,
                Id, dropInfo.InsertIndex);
        }

        //COMMANDS
        private RelayCommand _addCardCmd;
        public RelayCommand AddCardCmd
        {
            get
            {
                return _addCardCmd ?? (_addCardCmd = new RelayCommand(() =>
                {
                    AddEditCardWindow addCard = new AddEditCardWindow(null, Id);
                    addCard.ShowDialog();
                }));
            }
        }

        private RelayCommand _dropH;
        public RelayCommand DropH
        {
            get
            {
                return _dropH ?? (_dropH = new RelayCommand(() =>
                {
                    MessageBox.Show("Hello!");
                }));
            }
        }
    }
}
