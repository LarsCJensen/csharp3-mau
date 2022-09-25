﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.BLL.Model
{
    /// <summary>
    /// DTO for File, used for get
    /// </summary>
    public class FileDto
    {
        public string Extension { get; set; }
        public string FullName { get; set; }
        public int Position { get; set; }
    }
}
