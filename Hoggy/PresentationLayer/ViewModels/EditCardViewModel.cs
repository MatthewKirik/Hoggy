using AutoMapper;
using DataTransferObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class EditCardViewModel : ViewModelBase
    {
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
        
        public EditCardViewModel(CardModel card, Window window)
        {
            _card = card;
            _window = window;

            foreach (var tag in _card.BoardTags)
                tag.AddTagToCardAct = AddTagToCard;
        }

        void AddTagToCard(TagModel tag)
        {
            if (Card.Tags.Where(t => t.Id == tag.Id).FirstOrDefault() != null)
                return;
            else
            {
                LoaderVisible = true;
                Task.Run(() =>
                {
                    try
                    {
                        CardDTO card = Mapper.Map<CardDTO>(_card);
                        if (!NetProxy.CommProxy.AddTagToCard(NetProxy.Token, tag.Id, Card.Id))
                            MessageBox.Show("Tag no added!");
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

        //COMMANDS
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
                            CardDTO card = Mapper.Map<CardDTO>(_card);
                            if (!NetProxy.EditionProxy.EditCard(NetProxy.Token, card))
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
