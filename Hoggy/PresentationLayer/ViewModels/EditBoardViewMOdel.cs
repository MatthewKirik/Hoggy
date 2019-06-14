using GalaSoft.MvvmLight;
using PresentationLayer.Helpers;
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
    public class EditBoardViewModel : ViewModelBase
    {
        BoardModel _board;
        public BoardModel Board
        {
            get => _board;
            set
            {
                _board = value;
                RaisePropertyChanged(nameof(Board));
            }
        }

        bool _loaderVisible;
        public bool LoaderVisible
        {
            get => _loaderVisible;
            set
            {
                _loaderVisible = value;
                RaisePropertyChanged(nameof(LoaderVisible));
            }
        }

        public ObservableCollection<UserModel> Users { get; set; }
        
        Window _window;
        public EditBoardViewModel(BoardModel board, Window window)
        {
            _board = (BoardModel)board.Clone();
            _window = window;
            LoadAllUsers();
        }

        void LoadAllUsers()
        {
            LoaderVisible = true;
            Task.Run(() =>
            {
                try
                {
                    //NetProxy.DataExchProxy.U
                    //if ();
                    //else
                    //App.Current.Dispatcher.Invoke(() => { _window.Close(); });
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Error!",
                               MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    LoaderVisible = false;
                }
            });
        }
    }
}