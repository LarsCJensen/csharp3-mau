
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4B.Utilities;
using CommunityToolkit.Mvvm.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Assignment4B.ViewModel
{
    /// <summary>
    /// Base ViewModel
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        // Property changed event handler
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, public, instance property on this object. 
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                throw new VerifyPropertyException(msg);
            }
        }
        #endregion      
        #region EventHandlers
        public event EventHandler OnClose;
        #endregion
        protected BaseViewModel()
        {
            // If I want general commands I can add them here
            RegisterCommands();
        }
        protected virtual void RegisterCommands() {}
        public void Close()
        {
            if (OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }
        }
    }
}
