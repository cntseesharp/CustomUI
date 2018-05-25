using CustomUI.Core.Models;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace CustomUI.Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        public ObservableCollection<Country> Countries { get; set; } = new ObservableCollection<Country>();

        public FirstViewModel()
        {
            for (int i = 0; i < 200; i++)
                Countries.Add(new Country { Title = "Country " + i });
            RaisePropertyChanged(() => Countries);
        }

        private int _selectedCountryIndex;
        public int SelectedCountryIndex
        {
            get => _selectedCountryIndex; set
            {
                SetProperty(ref _selectedCountryIndex, value);
                if (value < 0) return;
                Cities = new ObservableCollection<City> { new City { Title = "City 17" }, new City { Title = "Another one" }, new City { Title = "A happy little barrel" } };

                for (int i = 0; i < value; i++)
                    Cities.Add(new City { Title = "City #" + i });

                RaisePropertyChanged(() => Cities);
            }
        }

        public ObservableCollection<City> Cities { get; set; }
    }
}
