using AutoMapper;
using DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class AddBoardViewModel : ViewModelBase
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

        Window _window;

        public AddBoardViewModel(Window window)
        {
            _board = new BoardModel();
            _window = window;
        }

        private RelayCommand _saveCmd;
        public RelayCommand SaveCmd
        {
            get
            {
                return _saveCmd ?? (_saveCmd = new RelayCommand(() =>
                {
                    LoaderVisible = true;
                    Task.Run(() =>
                    {
                        try
                        {
                            BoardDTO boardDTO = Mapper.Map<BoardDTO>(_board);
                            
                            if (!NetProxy.CreationProxy.AddBoard(NetProxy.Token, boardDTO))
                                MessageBox.Show("Can't add board!");
                            else
                                App.Current.Dispatcher.Invoke(() => { _window.Close(); });
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
                }));
            }
        }
    }
}
