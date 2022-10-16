using Assignment3.BLL;
using Assignment3.BLL.Model;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Utilities;

namespace Assignment3.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        // TODO Is this needed?
        public BugManager BugManager = new BugManager();
        public ObservableCollection<Bug> _bugs = new ObservableCollection<Bug>();
        public ObservableCollection<Bug> Bugs
        {
            get { return _bugs; }   
            set 
            { 
                _bugs = value;
                OnPropertyChanged("Bugs");
            }
        }
        private string _title = "You got bugs!";
        public string Title {
            get { return _title;  }
            set 
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        private string _bugsCountText = "0 bugs in the system";
        public string BugsCountText
        {
            get
            {
                return _bugsCountText;
            }
            set
            {
                _bugsCountText = $"{BugManager.Bugs.Count()} bugs in the system!";
                OnPropertyChanged("BugsCountText");
            }
        }
        private int _selectedBug;
        public int SelectedBug
        {
            get { return _selectedBug; }
            set
            {
                _selectedBug = value;
                OnPropertyChanged("SelectedBug");
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }
        public bool CanExecute
        {
            get
            {
                // TODO Check stuff?
                return true;
            }
        }

        public MainViewModel()
        {
            RegisterCommands();
        }
        protected override void RegisterCommands()
        {
            base.RegisterCommands();
            _saveCommand = new CommandHandler(() => ExecuteSaveCommand(), () => CanExecute);

        }
        #region ExecuteCommands
        private void ExecuteSaveCommand()
        {
            MessageBox.Show("Save");
        }
        #endregion
        public void OnSave(object sender, Bug bug)
        {
            // TODO Check if it is checked off, perhaps play a jingle?            
            Bugs.Add(bug);
            // Reload list of bugs
        }
    }
    
    // TODO Bind to event on save in bugs view

}
