﻿using Academix.Models;
using Academix.Models.Main;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Academix.DbContexts;


namespace Academix.ViewModels.Main.System
{
    public class SystemParametersViewModel: BaseViewModel
    {
        private static string MINIMUM_AGE = "TuoiToiThieu";
        private static string MAXIMUM_AGE = "TuoiToiDa";
        private static string MAXIMUM_CLASS_SIZE = "SiSoToiDa";
        private static string MINIMUM_SCORE = "DiemToiThieu";
        private static string MAXIMUM_SCORE = "DiemToiDa";
        private static string PASSING_GRADE = "DiemDat";
        private static string SUBJECT_PASSING_GRADE = "DiemDatMon";

        private bool _isProcessing = true;
        public bool IsProcessing
        {
            get
            {
                return _isProcessing;
            }
            set
            {
                _isProcessing = value;
                OnPropertyChanged(nameof(IsProcessing));
            }
        }
        private int _minimumAge;
        public int MinimumAge
        {
            get
            {
                return _minimumAge;
            }
            set
            {
                _minimumAge = value;
                OnPropertyChanged(nameof(MinimumAge));
            }
        }

        private int _maximumAge;
        public int MaximumAge
        {
            get
            {
                return _maximumAge;
            }
            set
            {
                _maximumAge = value;
                OnPropertyChanged(nameof(MaximumAge));
            }
        }

        private int _maximumClasssize;
        public int MaximumClassize
        {
            get
            {
                return _maximumClasssize;
            }
            set
            {
                _maximumClasssize = value;
                OnPropertyChanged(nameof(MaximumClassize));
            }
        }

        private double _minimumScore;
        public double MinimumScore
        {
            get
            {
                return _minimumScore;
            }
            set
            {
                _minimumScore = value;
                OnPropertyChanged(nameof(MinimumScore));
            }
        }

        private double _maximumScore;
        public double MaximumScore
        {
            get
            {
                return _maximumScore;
            }
            set
            {
                _maximumScore = value;
                OnPropertyChanged(nameof(MaximumScore));
            }
        }

        private double _passingGrade;
        public double PassingGrade
        {
            get
            {
                return _passingGrade;
            }
            set
            {
                _passingGrade = value;
                OnPropertyChanged(nameof(PassingGrade));
            }
        }

        private double _subjectPassingGrade;
        public double SubjectPassingGrade
        {
            get
            {
                return _subjectPassingGrade;
            }
            set
            {
                _subjectPassingGrade = value;
                OnPropertyChanged(nameof(SubjectPassingGrade));
            }
        }

        //public ICommand RestoreCommand { get; }
        public ICommand SaveCommand { get; }

        public SystemParametersViewModel()
        {
            SaveCommand = new AsyncRelayCommand(SaveData);
            Task.Run(LoadingData).ConfigureAwait(false);
      
        }

       
        private async Task LoadingData()
        {
            using (var context = new QuanlyhocsinhContext())
            {
                List<Thamso> parameters = await context.Thamsos.ToListAsync();
                MinimumAge = Convert.ToInt32(parameters.FirstOrDefault(p => p.Tenthamso == MINIMUM_AGE).Giatri);
                MaximumAge = Convert.ToInt32(parameters.FirstOrDefault(p => p.Tenthamso == MAXIMUM_AGE).Giatri);
                MinimumScore = parameters.FirstOrDefault(p => p.Tenthamso == MINIMUM_SCORE).Giatri ?? 0d;
                MaximumScore = parameters.FirstOrDefault(p => p.Tenthamso == MAXIMUM_SCORE).Giatri ?? 0d;
                MaximumClassize = Convert.ToInt32(parameters.FirstOrDefault(p => p.Tenthamso == MAXIMUM_CLASS_SIZE).Giatri);
                PassingGrade = parameters.FirstOrDefault(p => p.Tenthamso == PASSING_GRADE).Giatri ?? 0d;
                SubjectPassingGrade = parameters.FirstOrDefault(p => p.Tenthamso == SUBJECT_PASSING_GRADE).Giatri ?? 0d;

            }

            IsProcessing = false;

        }

        private async Task SaveData()
        {
            try
            {
                
                if (MinimumAge >= MaximumAge)
                    throw new Exception("Tuổi tối thiểu không được lớn hơn hoặc bằng tuổi tối đa");
                if (MinimumScore >= MaximumScore)
                    throw new Exception("Điểm tối thiểu không được lớn hơn hoặc bằng điểm tối đa");
                if(MinimumAge < 0 || 
                    MaximumAge < 0 ||
                    MinimumScore < 0 ||
                    MaximumScore < 0 ||
                    MaximumClassize < 0 ||
                    PassingGrade < 0 ||
                    SubjectPassingGrade < 0)
                    throw new Exception("Điểm thông số không thể mang giá trị âm");

                using (var context = new QuanlyhocsinhContext())
                {
                    List<Thamso> parameters = await context.Thamsos.ToListAsync();
                    parameters.FirstOrDefault(p => p.Tenthamso == MINIMUM_AGE).Giatri = MinimumAge;
                    parameters.FirstOrDefault(p => p.Tenthamso == MAXIMUM_AGE).Giatri = MaximumAge;
                    parameters.FirstOrDefault(p => p.Tenthamso == MINIMUM_SCORE).Giatri = MinimumScore;
                    parameters.FirstOrDefault(p => p.Tenthamso == MAXIMUM_SCORE).Giatri = MaximumScore;
                    parameters.FirstOrDefault(p => p.Tenthamso == MAXIMUM_CLASS_SIZE).Giatri = MaximumClassize;
                    parameters.FirstOrDefault(p => p.Tenthamso == PASSING_GRADE).Giatri = PassingGrade;
                    parameters.FirstOrDefault(p => p.Tenthamso == SUBJECT_PASSING_GRADE).Giatri = SubjectPassingGrade;
                    IsProcessing = true;
                    await context.SaveChangesAsync();
                }

                IsProcessing = false;
                MessageBox.Show("Thay đổi lưu thành công");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
           
        }


    }
}
