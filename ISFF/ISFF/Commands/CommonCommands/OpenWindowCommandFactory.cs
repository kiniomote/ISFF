﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;

namespace ISFF
{
    public class OpenWindowCommandFactory : CommonRelayCommandFactory
    {
        #region Windows for open

        public const int WINDOW_EMPLOYEES = 0;
        public const int WINDOW_INGREDIENTs = 1;
        public const int WINDOW_PRODUCTS = 2;
        public const int WINDOW_ORDERS = 3;

        #endregion

        public OpenWindowCommandFactory(int open_window)
        {
            OpenWindow = open_window;
        }

        public int OpenWindow { get; set; }

        public override Action<object> Execute()
        {
            return param =>
            {

                Window window = null;
                switch (OpenWindow)
                {
                    case WINDOW_EMPLOYEES:
                        window = new WindowEmployees();
                        break;
                    case WINDOW_INGREDIENTs:
                        window = new WindowIngredients();
                        break;
                    case WINDOW_PRODUCTS:
                        window = new WindowProducts();
                        break;
                    case WINDOW_ORDERS:
                        window = new WindowOrders();
                        break;
                }
                window.ShowDialog();
            };
        }

        public override Func<object, bool> CanExecute()
        {
            return null;
        }
    }
}
