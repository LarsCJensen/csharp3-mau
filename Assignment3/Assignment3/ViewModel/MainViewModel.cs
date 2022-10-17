using Assignment3.BLL;
using Assignment3.BLL.Model;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
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
        private bool _dataDirty;
        private string _dataFileName;
        public ObservableCollection<Bug> _bugs = new ObservableCollection<Bug>();
        public ObservableCollection<Bug> Bugs
        {
            get { return _bugs; }   
            set 
            { 
                _bugs = value;
                OnPropertyChanged("Bugs");
                OnPropertyChanged("BugsCountText");
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
        private Bug _selectedBug;
        public Bug SelectedBug
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
        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get { return _loadCommand; }
        }
        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
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
            // Lambda Statement 1
            _saveCommand = new CommandHandler(() => ExecuteSaveCommand(), () => CanExecute);
            _loadCommand = new CommandHandler(() => ExecuteOpenCommand(), () => CanExecute);
            _deleteCommand = new CommandHandler(() => ExecuteDeleteCommand(), () => CanExecute);

        }
        #region ExecuteCommands
        private void ExecuteSaveCommand()
        {
            if(_dataFileName == null)
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = "XML|*.xml|All files (*.*)|*.*";
                bool? result = dialog.ShowDialog();
                if (result == true)
                {
                    _dataFileName = dialog.FileName;
                }
            }
            try
            {
                Serializer.XmlFileSerialize<ObservableCollection<Bug>>(_dataFileName, Bugs);                    
                _dataDirty = false;
            }
            catch (SerializerException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        public void ExecuteOpenCommand()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML|*.xml|All files (*.*)|*.*";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    Bugs = new ObservableCollection<Bug>(Serializer.XmlFileDeSerialize<ObservableCollection<Bug>>(dialog.FileName));
                    _dataFileName = dialog.FileName;
                    _dataDirty = false;
                }
                catch (SerializerException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ExecuteDeleteCommand()
        {
            MessageBox.Show("Delete bug");
        }
        #endregion
        public void OnSave(object sender, Bug bug)
        {
            // TODO Check if it is checked off, perhaps play a jingle?            
            Bugs.Add(bug);
            _dataDirty = true;
            MessageBox.Show("Saved!");
            // Reload list of bugs
        }        
    }
    
    // TODO Bind to event on save in bugs view

}
