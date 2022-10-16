using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// General utility functions which can be useful to have
        /// </summary>
        public static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;
        public static bool IsNull([NotNullWhen(true)] object? obj) => obj == null;
        /// <summary>
        /// Helper funciton to move items in generic list
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">Generic list</param>
        /// <param name="oldIndex">Move from index</param>
        /// <param name="newIndex">To index</param>
        /// <returns></returns>
        public static List<T> Move<T>(List<T> list, int oldIndex, int newIndex)
        {
            T item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
            return list;
        }
        /// <summary>
        /// Get video duration through APICodePack-SHell
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Length of video</returns>
        public static double GetVideoDuration(string filePath)
        {
            using (var shell = ShellObject.FromParsingName(filePath))
            {
                IShellProperty prop = shell.Properties.System.Media.Duration;
                var t = (ulong)prop.ValueAsObject;
                return TimeSpan.FromTicks((long)t).TotalMilliseconds;
            }
        }
    }
}
