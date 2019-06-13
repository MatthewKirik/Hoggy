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
    class AddCardViewModel : ViewModelBase
    {
        int _colId;
        Window _window;

        CardModel _card;
        public CardModel Card
        {
            get => _card;
            set
            {
                _card = value;
                RaisePropertyChanged(nameof(Card));
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

        public AddCardViewModel(int colId, Window window)
        {
            _colId = colId;
            _window = window;
            _card = new CardModel();
        }

        private RelayCommand _saveCmd;
        public RelayCommand SaveCmd
        {
            get
            {
                return _saveCmd ?? (_saveCmd = new RelayCommand(() =>
                {
                    _card.CreationDate = DateTime.Now;
                    CardDTO card = Mapper.Map<CardDTO>(_card);
                    LoaderVisible = true;
                    Task.Run(() =>
                    {
                        try
                        {
                            if (!NetProxy.CreationProxy.AddCard(NetProxy.Token, card, _colId))
                                MessageBox.Show("No card added!");
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
