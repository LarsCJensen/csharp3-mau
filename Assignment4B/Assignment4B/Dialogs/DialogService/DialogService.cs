using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4B.Dialogs.DialogService
{
    public static class DialogService
    {
        /// <summary>
        /// Class for handling dialogs
        /// </summary>
        /// <param name="vm">Which viewmodel to bind</param>
        /// <returns>Which button clicked</returns>
        public static DialogResult OpenDialog(DialogViewModelBase vm)
        {
            DialogWindow win = new DialogWindow();
            win.DataContext = vm;
            win.ShowDialog();
            DialogResult result = (win.DataContext as DialogViewModelBase).UserDialogResult;
            return result;
        }
    }
}
