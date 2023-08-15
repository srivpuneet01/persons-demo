using Microsoft.Win32;
using PersonsDemo.WPF.Models;
using PersonsDemo.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonsDemo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly MainWindowViewModel _viewModel;
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = _viewModel = mainWindowViewModel;
            _viewModel.BrowseCsvFileEvent += OnBrowseCsvFileEvent;
        }

        private void OnBrowseCsvFileEvent()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            var fileSelected = ofd.ShowDialog();
            if (!fileSelected.GetValueOrDefault())
            {
                return;
            }
            
            _viewModel.LoadData(ofd.FileName);

        }
    }
}
