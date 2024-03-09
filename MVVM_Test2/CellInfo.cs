﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_Test2
{
    public class CellInfo
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Size { get; set; }
        public Thickness Margin => new(Left, Top, 0, 0);
    }
}