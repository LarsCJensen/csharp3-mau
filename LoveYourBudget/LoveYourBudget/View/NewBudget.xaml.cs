﻿using LoveYourBudget.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoveYourBudget.View
{
    /// <summary>
    /// Interaction logic for NewBudget.xaml
    /// </summary>
    public partial class NewBudget : Window
    {
        //NewBudgetViewModel vm = new NewBudgetViewModel();
        public NewBudget()
        {
            InitializeComponent();
            //this.DataContext = vm;
        }
    }
}
