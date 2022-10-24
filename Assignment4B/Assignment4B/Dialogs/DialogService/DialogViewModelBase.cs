using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment4B.Dialogs.DialogService
{
    public abstract class DialogViewModelBase
    {
        /// <summary>
        /// Base viewmodel for all dialogs
        /// </summary>
        public DialogResult UserDialogResult { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public DialogViewModelBase(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }
        /// <summary>
        /// Method for Close Dialog
        /// </summary>
        /// <param name="dialog">Which dialog</param>
        /// <param name="result">Which button was clicked</param>
        public void CloseDialogWithResult(Window dialog, DialogResult result)
        {
            this.UserDialogResult = result;
            if(dialog != null)
            {
                dialog.DialogResult = true;
            }
        }
    }
}
