using GalaSoft.MvvmLight.Command;
using PresentationLayer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ColumnModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<CardModel> Cards { get; set; }

        public ColumnModel()
        {
            Cards = new ObservableCollection<CardModel>();
        }

        //COMMANDS
        private RelayCommand _addCardCmd;
        public RelayCommand AddCardCmd
        {
            get
            {
                return _addCardCmd ?? (_addCardCmd = new RelayCommand(() =>
                {
                    AddEditCardWindow addCard = new AddEditCardWindow();
                    addCard.ShowDialog();
                }));
            }
        }
    }
}
