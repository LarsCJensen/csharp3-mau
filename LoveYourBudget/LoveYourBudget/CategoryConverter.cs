using LoveYourBudget.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LoveYourBudget.Converters
{
    public class CategoryConverter : IValueConverter
    {
        /// <summary>
        /// Convert CategoryId to Category.Name
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(String) && value != null)
            {
                int id = (int)value;
                BaseViewModel vm = parameter as BaseViewModel;
                if (vm != null)
                {
                    Category category = vm.Categories.FirstOrDefault(c => c.Id == id);
                    return category.Name;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
