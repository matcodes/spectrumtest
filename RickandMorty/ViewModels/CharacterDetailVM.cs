using RickandMorty.Models;
using RickandMorty.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RickandMorty.ViewModels
{
    public class CharacterDetailVM : INotifyPropertyChanged
    {
        #region Fields

        private Character character;
        private ApiService service;
        private bool isBusy;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region Constructor

        public CharacterDetailVM(int characterId)
        {
            if(characterId > 0) GetData(characterId);
        }

        #endregion

        #region Methods

        private async void GetData(int characterId)
        {
            try
            {
                InitComponents();
                IsBusy = true;

                Chractar = await service.GetCharacterById(characterId);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
        }

        private void InitComponents()
        {
            character = new Character();
            service = new ApiService();
        }

        #endregion

        #region Properties

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }

            set
            {
                isBusy = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsBusy)));
            }
        }

        public Character Chractar
        {
            get
            {
                return character;
            }

            set
            {
                if (character == value)
                {
                    return;
                }

                character = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Chractar)));
            }
        }

        #endregion
    }
}
