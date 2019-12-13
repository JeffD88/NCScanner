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
    using NCScanner.Resources;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private readonly IFileService fileService = null;

        private readonly INCFileScanner ncFileScanner = null;

        private string ncFilePath = string.Empty;

        private string toolList = string.Empty;

        private double xMin;

        private double yMin;

        private double zMin;

        private double xMax;

        private double yMax;

        private double zMax;

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

        public string ToolList
        {
            get => this.toolList;
            set
            {
                this.toolList = value;
                OnPropertyChanged();
            }
        }

        public double XMin
        {
            get => this.xMin;
            set
            {
                this.xMin = value;
                OnPropertyChanged();
            }
        }

        public double YMin
        {
            get => this.yMin;
            set
            {
                this.yMin = value;
                OnPropertyChanged();
            }
        }

        public double ZMin
        {
            get => this.zMin;
            set
            {
                this.zMin = value;
                OnPropertyChanged();
            }
        }

        public double XMax
        {
            get => this.xMax;
            set
            {
                this.xMax = value;
                OnPropertyChanged();
            }
        }

        public double YMax
        {
            get => this.yMax;
            set
            {
                this.yMax = value;
                OnPropertyChanged();
            }
        }

        public double ZMax
        {
            get => this.zMax;
            set
            {
                this.zMax = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Construction

        public MainWindowViewModel(IFileService fileService, INCFileScanner ncFileScanner)
        {
            this.fileService = fileService;
            this.ncFileScanner = ncFileScanner;

            BrowseCommand = new DelegateCommand(OnBrowseCommand);
            ReportCommand = new DelegateCommand(OnReportCommand);
            ScanCommand = new DelegateCommand(OnScanCommand);
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
            NCFilePath = fileService.BrowseForFile(Strings.BrowseForNCTitle,
                                                   Strings.BrowseForNCFilter,
                                                   false);
        }

        private void OnReportCommand(object parameter)
        {
            fileService.SaveFileAs(Strings.SaveReportTitle,
                                   Strings.SaveReportFilter,
                                   true);
        }

        private void OnScanCommand(object parameter)
        {
            if (fileService.FileExists(NCFilePath))
            {
                var ncData = ncFileScanner.ScanNCFile(NCFilePath);

                ToolList = ncData.ToolList;
                XMin = ncData.XMin;
                YMin = ncData.YMin;
                ZMin = ncData.ZMin;
                XMax = ncData.XMax;
                YMax = ncData.YMax;
                ZMax = ncData.ZMax;
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