using RickandMorty.Models;
using RickandMorty.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace RickandMorty.ViewModels
{
    public class CharactersVM : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<Character> chractarsList;
        private bool isBusy;
        public CharactersList response;
        public ApiService service;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion


        #region Constructor

        public CharactersVM()
        {
            InitComponents();

            GetData(Utility.BaseUrl);
        }

        #endregion


        #region Methods

        private void InitComponents()
        {
            IsBusy = false;
            service = new ApiService();
            ChractarsList = new ObservableCollection<Character>();
            response = new CharactersList();

            MessagingCenter.Subscribe<FilterVM>(this, "FadeOut", (obj) =>
            {
                if (!string.IsNullOrEmpty(obj.Status) || !string.IsNullOrEmpty(obj.Gender))
                    GetData($"{Utility.BaseUrl}?status={obj.Status}&gender={obj.Gender}", true);
            });
        }

        public async void GetData(string url, bool isFilter = false)
        {
            try
            {
                IsBusy = true;

                if (isFilter) ChractarsList.Clear();

                response = await service.GetAllCharacters(url);

                if (response?.Results?.Count > 0)
                {
                    response.Results.ForEach(x => ChractarsList.Add(x));

                    IsBusy = false;
                }
                else
                {
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }

        }

        public void HandleSearchBarTextChanged(string searchBarText)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchBarText))
                {
                    var filteredCharacters = response.Results.Where(x => x.Name.Contains(searchBarText)).ToList();

                    if (filteredCharacters.Count > 0)
                    {
                        ChractarsList.Clear();

                        foreach (var item in filteredCharacters)
                            ChractarsList.Add(item);
                    }
                }

                //TODo Test it
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }   
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

        public ObservableCollection<Character> ChractarsList
        {
            get
            {
                return this.chractarsList;
            }

            set
            {
                if (this.chractarsList == value)
                {
                    return;
                }

                this.chractarsList = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(CharactersList)));
            }
        }

        #endregion
    }
}
