namespace NCScanner.ViewModels
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Reflection;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Runtime.CompilerServices;

    using NCScanner.Services.Interfaces;
    using NCScanner.Commands;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private readonly IFileService fileService = null;

        private string ncFilePath = string.Empty;

        #endregion

        #region Public Properties

        public string NCFilePath
        {
            get => this.ncFilePath;
            set
            {
                this.ncFilePath = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Construction

        public MainWindowViewModel(IFileService fileService)
        {
            this.fileService = fileService;

            this.BrowseCommand = new DelegateCommand(OnBrowseCommand);
            this.ReportCommand = new DelegateCommand(OnReportCommand);
            this.ScanCommand = new DelegateCommand(OnScanCommand);
        }

        #endregion

        #region Commands

        public ICommand BrowseCommand { get; }

        public ICommand ReportCommand { get; }

        public ICommand ScanCommand { get; }

        #endregion

        #region Private Methods  

        private void OnBrowseCommand(object parameter)
        {
            NCFilePath = fileService.BrowseForFile("Select an NC File",
                                                   "NC Files(*.nc) | *.nc",
                                                   false);
        }

        private void OnReportCommand(object parameter)
        {
            fileService.SaveFileAs("Save Report As...",
                                   "CSV File(*.csv) | *.csv",
                                   true);
        }

        private void OnScanCommand(object parameter)
        {
            if (fileService.FileExists(NCFilePath))
            {
                
            }
            else
            {

            }
        }

        #endregion

        #region Notify Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}