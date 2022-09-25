
using DialogService.Dialogs.DialogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace DialogService.Dialogs.DialogYesNo
{
    /// <summary>
    /// ViewModel for DialogYesNo
    /// </summary>
    public class DialogYesNoViewModel: DialogViewModelBase
    {   private RelayCommand<Window> _yesCommand = null;
        public RelayCommand<Window> YesCommand
        {
            get { return _yesCommand; }
            set { _yesCommand = value; }
        }

        private RelayCommand<Window> _noCommand = null;
        public RelayCommand<Window> NoCommand
        {
            get { return _noCommand; }
            set { _noCommand = value; }
        }

        public DialogYesNoViewModel(string title, string message): base(title, message)
        {
            _yesCommand = new RelayCommand<Window>(OnYesClicked);
            _noCommand = new RelayCommand<Window>(OnNoClicked);
        }
        /// <summary>
        /// Method for click on Yes
        /// </summary>
        /// <param name="parameter"></param>
        private void OnYesClicked(object parameter)
        {
            this.CloseDialogWithResult(parameter as Window, DialogResult.Yes);
        }
        /// <summary>
        /// Method for click on No
        /// </summary>
        /// <param name="parameter"></param>
        private void OnNoClicked(object parameter)
        {
            this.CloseDialogWithResult(parameter as Window, DialogResult.No);
        }


    }
}
