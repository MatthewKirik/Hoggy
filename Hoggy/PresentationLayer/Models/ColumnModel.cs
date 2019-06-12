using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using PresentationLayer.Helpers;
using PresentationLayer.ViewModels;
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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
                NameErr = Validator.Check<int>(CheckType.Empty, Name);
            }
        }

        private string _nameErr;
        public string NameErr
        {
            get => _nameErr;
            set
            {
                _nameErr = value;
                RaisePropertyChanged(nameof(NameErr));
                CanSave = Validator.EmptyStrings(NameErr, DescErr);
            }
        }

        bool _canSave;
        public bool CanSave
        {
            get => _canSave;
            set
            {
                _canSave = value;
                RaisePropertyChanged(nameof(CanSave));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
                DescErr = Validator.Check<int>(CheckType.Empty, Description);
            }
        }

        private string _descErr;
        public string DescErr
        {
            get => _descErr;
            set
            {
                _descErr = value;
                RaisePropertyChanged(nameof(DescErr));
                CanSave = Validator.EmptyStrings(NameErr, DescErr);
            }
        }


        public ObservableCollection<CardModel> Cards { get; set; }
        public Action<int, int, int> MoveCardAct { get; set; }
        public Window MainWindow{get; set;}

        public ColumnModel()
        {
            Cards = new ObservableCollection<CardModel>();
            _nameErr = "Empty field!";
            _descErr = "Empty field!";
        }

        public void DragOver(IDropInfo dropInfo)
        {
            GongSolutions.Wpf.DragDrop.DragDrop.DefaultDropHandler.DragOver(dropInfo);
        }

        public void Drop(IDropInfo dropInfo)
        {
            MoveCardAct.Invoke((dropInfo.DragInfo.SourceItem as CardModel).Id,
                (dropInfo.DragInfo.SourceItem as CardModel).ColumnId, Id);
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
        
        private RelayCommand _editColumnCmd;
        public RelayCommand EditColumnCmd
        {
            get
            {
                return _editColumnCmd ?? (_editColumnCmd = new RelayCommand(() =>
                {
                    AddEditColumnWindow editColWin = new AddEditColumnWindow(
                         (MainWindow.DataContext as MainWindowViewModel).CurBoard.Id,
                         this);
                    editColWin.ShowDialog();
                }));
            }
        }

        private RelayCommand _delColumnCmd;
        public RelayCommand DeleteColumnCmd
        {
            get
            {
                return _delColumnCmd ?? (_delColumnCmd = new RelayCommand(() =>
                {
                    MessageBoxResult res = MessageBox.Show(
                        "Adre you sure? Delete column?", "Delete column", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (res != MessageBoxResult.Yes)
                        return;

                    (MainWindow.DataContext as MainWindowViewModel).LoaderVisible = true;
                    Task.Run(() =>
                    {
                        try
                        {
                            if (!NetProxy.DeletionProxy.DeleteColumn(NetProxy.Token, Id))
                                MessageBox.Show("Column not deleted!");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!",
                                       MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        finally
                        {
                            (MainWindow.DataContext as MainWindowViewModel).LoaderVisible = false;
                        }
                    });
                }));
            }
        }
    }
}
