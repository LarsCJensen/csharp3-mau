using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO REMOVE

namespace Assignment1_Utilities
{
    public class TreeViewCommandEventArgs: EventArgs
    {
        private string _buttonCommand;
        public string ButtonCommand
        {
            get { return _buttonCommand; }
        }
        public TreeViewCommandEventArgs(string command)
        {
            _buttonCommand = command;
        }
    }
}
