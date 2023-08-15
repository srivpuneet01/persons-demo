using GalaSoft.MvvmLight.Command;
using PersonsDemo.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonsDemo.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<Person> _allPersons;
        const string ALL = "--All--";
        private string _selectedSorting;

        public string SelectedSorting
        {
            get { return _selectedSorting; }
            set
            {
                _selectedSorting = value;
                OnPropertyChanged(nameof(SelectedSorting));
                SortPersons();
            }
        }

        public List<string> Countries { get; private set; }

        private string _selectedCountry;

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
                FilterBasedOnSelectedCountry();
            }
        }

        private void FilterBasedOnSelectedCountry()
        {
            if (_allPersons == null || !_allPersons.Any() || string.IsNullOrWhiteSpace(_selectedCountry))
            {
                return;
            }

            Persons = _allPersons.Where(p => _selectedCountry == ALL || p.Country.Equals(_selectedCountry)).ToList();
            SortPersons();
        }

        public List<string> SortingOptions => new List<string> { "Name", "Countries" };

        private void SortPersons()
        {
            if (Persons == null || !Persons.Any() || string.IsNullOrWhiteSpace(_selectedSorting))
            {
                OnPropertyChanged(nameof(Persons));
                return;
            }

            if (_selectedSorting == "Name")
            {
                Persons = Persons.OrderBy(p => p.Name).ToList();
            }
            else
            {
                Persons = Persons.OrderBy(p => p.Country).ToList();
            }
            OnPropertyChanged(nameof(Persons));
        }

        public List<Person> Persons { get; private set; }

        public event Action BrowseCsvFileEvent;
        public ICommand BrowseCsvFileCommand => new RelayCommand(BrowseCsvFile);

        private void BrowseCsvFile()
        {
            BrowseCsvFileEvent?.Invoke();
        }

        internal void LoadData(string fileName)
        {
            _allPersons = Persons = CsvLoader.LoadPersons(fileName).ToList();
            LoadcountriesFilterList();
            OnPropertyChanged(nameof(Persons));
        }

        private void LoadcountriesFilterList()
        {
            if (Persons == null || !Persons.Any())
            {
                return;
            }

            Countries = Persons.Select(x => x.Country).Distinct().OrderBy(o => o).ToList();
            Countries.Insert(0, ALL);
            OnPropertyChanged(nameof(Countries));
        }
    }
}
