using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Matrix_Calculator
{
    class Matrix
    {
        public int rows;
        public int columns;
        public List<int> matrixValues;

        public Matrix()
        {
            matrixValues = new List<int>();
            rows = 1;
            columns = 1;
        }
    }
}