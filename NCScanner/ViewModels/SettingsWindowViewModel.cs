using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

using NCScanner.Commands;
using NCScanner.Settings;


namespace NCScanner.ViewModels
{
    class SettingsWindowViewModel : INotifyPropertyChanged
    {

        #region Private Fields

        private string toolRegex = string.Empty;

        private string workOffsetRegex = string.Empty;

        #endregion

        #region Public Properties

        public string ToolRegex
        {
            get => this.toolRegex;
            set
            {
                this.toolRegex = value;
                OnPropertyChanged();
            }
        }

        public string WorkOffsetRegex
        {
            get => this.workOffsetRegex;
            set
            {
                this.workOffsetRegex = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Construction

        public SettingsWindowViewModel()
        {
            OkCommand = new DelegateCommand(OnOkCommand);
            ApplyCommand = new DelegateCommand(OnApplyCommand);

            ToolRegex = NCScannerSettings.Default.ToolRegex;
            WorkOffsetRegex = NCScannerSettings.Default.WorkOffsetRegex;
        }

        #endregion

        #region Commands

        public ICommand OkCommand { get; }

        public ICommand ApplyCommand { get; }

        #endregion

        #region Private Methods 

        private void OnOkCommand(object parameter)
        {
            var view = (Window)parameter;

            NCScannerSettings.Default.ToolRegex = ToolRegex;
            NCScannerSettings.Default.WorkOffsetRegex = WorkOffsetRegex;

            NCScannerSettings.Default.Save();

            view?.Close();
        }

        private void OnApplyCommand(object parameter)
        {
            NCScannerSettings.Default.ToolRegex = ToolRegex;
            NCScannerSettings.Default.WorkOffsetRegex = WorkOffsetRegex;

            NCScannerSettings.Default.Save();
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
