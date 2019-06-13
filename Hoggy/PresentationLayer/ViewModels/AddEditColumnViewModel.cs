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
    class AddEditColumnViewModel : ViewModelBase
    {
        bool _isNewColumn;
        int _boardId;

        ColumnModel _column;
        public ColumnModel Column
        {
            get => _column;
            set
            {
                _column = value;
                RaisePropertyChanged(nameof(Column));
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

        public AddEditColumnViewModel(Window window, int boardId, ColumnModel column = null)
        {
            _window = window;
            _boardId = boardId;
            if (column == null)
            {
                _isNewColumn = true;
                _column = new ColumnModel();
            }
            else
                _column = (ColumnModel)column.Clone();
        }

        void AddColumn()
        {
            LoaderVisible = true;
            Task.Run(() =>
            {
                try
                {
                    if (!NetProxy.CreationProxy.AddColumn(NetProxy.Token,
                        Mapper.Map<ColumnDTO>(_column), _boardId))
                        MessageBox.Show("Can't add column!");
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
        }

        void EditColumn()
        {
            LoaderVisible = true;
            Task.Run(() =>
            {
                try
                {
                    MessageBox.Show(Column.Id.ToString());
                    if (!NetProxy.EditionProxy.EditColumn(NetProxy.Token, Mapper.Map<ColumnDTO>(Column)))
                        App.Current.Dispatcher.Invoke(() => { _window.Close(); });
                    else
                        MessageBox.Show("Can't edit column!");
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

        //COMMANDS
        private RelayCommand _saveCmd;
        public RelayCommand SaveCmd
        {
            get
            {
                return _saveCmd ?? (_saveCmd = new RelayCommand(() =>
                {
                    if (_isNewColumn)
                        AddColumn();
                    else
                        EditColumn();
                }));
            }
        }

        private RelayCommand _cancelCmd;
        public RelayCommand CancelCmd
        {
            get
            {
                return _cancelCmd ?? (_cancelCmd = new RelayCommand(() =>
                {
                    App.Current.Dispatcher.Invoke(() => { _window.Close(); });
                }));
            }
        }

    }
}