﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ISFF
{
    public class KitParametrsEmployees : INotifyPropertyChanged
    {
        public KitParametrsEmployees()
        {
            IsReadOnly = true;
            Employees = new ObservableCollection<Employee>
            {
                new Employee{ Id=1, Fio="Попадин Дмитрий Владимирович", IdNumber=156712, Post="Директор", Salary=60000, Exp=7},
                new Employee{ Id=2, Fio="Василич Вася Владимирович", IdNumber=24497, Post="Повар", Salary=20000, Exp=2},
                new Employee{ Id=3, Fio="Иваныч Иван Владимирович", IdNumber=74942, Post="Повар", Salary=20000, Exp=5},
                new Employee{ Id=4, Fio="Петров Петя Владимирович", IdNumber=67149, Post="Админ", Salary=40000, Exp=1},
                new Employee{ Id=5, Fio="Сидоров Сережа Владимирович", IdNumber=99547, Post="Охрана", Salary=15000, Exp=15},
            };
        }

        private bool isReadOnly;
        private Employee selectedEmployee;
        public ObservableCollection<Employee> Employees { get; set; }

        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
