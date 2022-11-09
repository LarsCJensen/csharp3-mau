using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4B.BLL.Model
{
    public static class ValidExtensions
    {
        /// <summary>
        /// A static class of valid extensions. 
        /// Might have the user to add them in the future, but since extensions depend on WPF control it is better to have full control.
        /// </summary>
        public static List<string> ImageExtensions
        {
            get
            {
                return new List<string> { ".jpg", ".bmp", ".png"};
            }
        }
        public static List<string> VideoExtensions
        {
            get
            {
                return new List<string> { ".mov", ".avi", ".mpg", ".mp4" };
            }
        }
        public static List<string> AllValidExtensions
        {
            get
            {
                List<string> all = new List<string>();
                all.AddRange(ImageExtensions);
                all.AddRange(VideoExtensions);
                return all;
            }
        }
    }
}
