﻿using Academix.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels.Main.Report
{
    public class SubjectReportItemViewModel: BaseViewModel
    {
        private Lop _class;

        public string ClassId => _class.Malop;

        public string ClassName
        {
            get => _class.Tenlop;
        }

        public int ClassSize
        {
            get => _class.Siso;
            set 
                {
                _class.Siso = value; 
                OnPropertyChanged(nameof(ClassSize)); 
            }
        }

        private int _count;
        public int Count
        {
            get => _count;
            set 
            {
                _count = value;
                OnPropertyChanged(nameof(Count)); 
            }
        }

        private double _passingRate;
        public double PassingRate
        {
            get => _passingRate;
            set 
            { 
                _passingRate = value; 
                OnPropertyChanged(nameof(PassingRate)); 
            }
        }

        public SubjectReportItemViewModel(Lop cl, int count, double passingRate)
        {
            _class = cl;
            _count = count;
            _passingRate = passingRate;
        }

    }
}
