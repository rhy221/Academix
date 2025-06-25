using Academix.Models;
using Academix.Services;
using Academix.Stores;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Academix.Models.Main.Address;
using Academix.DbContexts;
using System.IO;

namespace Academix.ViewModels.Main.Student
{
   public class ModifyStudentViewModel:BaseViewModel
    {
        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;
        private Hocsinh _student;

        public ICommand BackCommand { get; }
        public ICommand SaveCommand { get; }



        public ModifyStudentViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore, Hocsinh student)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _student = student;
            BackCommand = new RelayCommand(Back);
            SaveCommand = new AsyncRelayCommand(SaveStudent);
            GradeList = new ObservableCollection<Khoi>();
            FilteredClassList = new ObservableCollection<Lop>();
            _classList = new List<Lop>();
            Task.Run(LoadDataAsync).ConfigureAwait(false);
            LoadProvinces();
            SetUpInformation();

        }

        private async Task LoadDataAsync()
        {
            using (var context = new QuanlyhocsinhContext())
            {
               
                List<Khoi> grades = await context.Khois.ToListAsync();
                List<Lop> classes = await context.Lops.Where(l => l.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc).ToListAsync();
               

                GradeList = new ObservableCollection<Khoi>(grades);
                _classList = classes;

                if (_student.CtLops.Count > 0)
                {

                    SelectedGrade = GradeList.FirstOrDefault(k => k.Makhoi == _student.CtLops.LastOrDefault().MalopNavigation.MakhoiNavigation.Makhoi);
                    SelectedClass = FilteredClassList.FirstOrDefault(c => c.Malop == _student.CtLops.LastOrDefault().MalopNavigation.Malop);
                }

            }
        }
        private void SetUpInformation()
        {
            StudentID = _student.Mahs;
            FullName = _student.Hoten;
            DateOfBirth = _student.Ngaysinh;
            SelectedGender = _student.Gioitinh;
            Email = _student.Email;
            string[] address = _student.Diachi.Split("_");
            if (address.Count() > 0)
                SelectedProvince = Provinces.FirstOrDefault(p => p.name == address[0]);
            if (address.Count() > 1)
            {
                LoadDistricts();
                SelectedDistrict = Districts.FirstOrDefault(d => d.name == address[1]);

            }
            if (address.Count() > 2)
            {
                LoadWards();
                SelectedWard = Wards.FirstOrDefault(w => w.name == address[2]);

            }

        }
        private async Task SaveStudent()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullName) ||
                    _selectedGrade == null ||
                    _selectedClass == null ||
                    _dateOfBirth == null ||
                    string.IsNullOrWhiteSpace(_selectedGender) ||
                    _selectedProvince == null ||
                    _selectedDistrict == null ||
                    _selectedWard == null)
                {
                    throw new Exception("Vui lòng điền đầy đủ thông tin học sinh.");
                    
                }

                using (var context = new QuanlyhocsinhContext())
                {
                    Hocsinh student = await context.Hocsinhs.FirstOrDefaultAsync(hs => hs.Mahs == _student.Mahs);

                    student.Hoten = _fullName;
                    student.Gioitinh = _selectedGender;
                    student.Diachi = _selectedProvince.name + "_" + _selectedDistrict.name + "_" + _selectedWard.name;
                    student.Ngaysinh = _dateOfBirth ?? new DateTime();
                    student.Email = _email;

                    
                    if(student.CtLops.Count > 0)
                    {
                        CtLop classDetail = student.CtLops.LastOrDefault();
                        if(_selectedClass.Malop != classDetail.Malop)
                        {
                            student.CtLops.Add(new CtLop(_selectedClass.Malop, classDetail.Mahs, classDetail.Mahocky, classDetail.Dtbhk));
                            context.CtLops.Remove(classDetail);
                        }
                       
                    }
                    await context.SaveChangesAsync();
                  

                    MessageBox.Show("Sửa học sinh thành công!");
                    StudentsViewModel viewModel = (StudentsViewModel)_navigationService.PopStack();
                    if (viewModel != null)
                    {
                        SearchStudentViewModel searchStudentViewModel = (SearchStudentViewModel)viewModel.TabItems[0].ViewModel;
                        searchStudentViewModel.Search();
                        _navigationService.Navigate(viewModel);

                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        



        private Khoi _selectedGrade;
        private ObservableCollection<Khoi> _gradeList;
        public ObservableCollection<Khoi> GradeList
        {
            get => _gradeList;
            set
            {
                _gradeList = value;
                OnPropertyChanged(nameof(GradeList));
            }
        }

        public Khoi SelectedGrade
        {
            get => _selectedGrade;
            set
            {
                _selectedGrade = value;
                OnPropertyChanged(nameof(SelectedGrade));
                FilterClassByGrade();
            }
        }


        private List<Lop> _classList;

        private Lop _selectedClass;

        public Lop SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                OnPropertyChanged(nameof(SelectedClass));
            }
        }

        private ObservableCollection<Lop> _filteredClassList;
        public ObservableCollection<Lop> FilteredClassList
        {
            get => _filteredClassList;
            set
            {
                _filteredClassList = value;
                OnPropertyChanged(nameof(FilteredClassList));
            }
        }

        private void FilterClassByGrade()
        {
            if (SelectedGrade == null)
            {
                FilteredClassList = new ObservableCollection<Lop>();
            }
            else
            {
                FilteredClassList = new ObservableCollection<Lop>(_classList
                                                                    .Where(l => l.Makhoi == _selectedGrade.Makhoi));

            }

            SelectedClass = null;
        }



        private string _selectedGender;
        public List<string> GenderList { get; } = new List<string> { "Nam", "Nữ" };
        public string SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        public ObservableCollection<Province> Provinces { get; set; } = new ObservableCollection<Province>();
        public ObservableCollection<District> Districts { get; set; } = new ObservableCollection<District>();
        public ObservableCollection<Ward> Wards { get; set; } = new ObservableCollection<Ward>();

        private Province _selectedProvince;
        public Province SelectedProvince
        {
            get => _selectedProvince;
            set
            {
                if (_selectedProvince != value)
                {
                    _selectedProvince = value;
                    OnPropertyChanged(nameof(SelectedProvince));
                    LoadDistricts();
                }
            }
        }

        private District _selectedDistrict;
        public District SelectedDistrict
        {
            get => _selectedDistrict;
            set
            {
                if (_selectedDistrict != value)
                {
                    _selectedDistrict = value;
                    OnPropertyChanged(nameof(SelectedDistrict));
                    LoadWards();
                }
            }
        }

        private Ward _selectedWard;
        public Ward SelectedWard
        {
            get => _selectedWard;
            set
            {
                if (_selectedWard != value)
                {
                    _selectedWard = value;
                    OnPropertyChanged(nameof(SelectedWard));
                }
            }
        }

        public void LoadProvinces()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\DonViHanhChinhVietNam.json");
                string json = File.ReadAllText(path);
                var provinceDict = JsonConvert.DeserializeObject<Dictionary<string, Province>>(json);

                Provinces.Clear();
                foreach (var p in provinceDict.Values.OrderBy(p => p.name))
                {
                    Provinces.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadDistricts()
        {
            Districts.Clear();
            Wards.Clear();

            if (SelectedProvince != null && SelectedProvince.quan_huyen != null)
            {
                foreach (var d in SelectedProvince.quan_huyen.Values.OrderBy(d => d.name))
                {
                    Districts.Add(d);
                }
            }
        }

        private void LoadWards()
        {
            Wards.Clear();

            if (SelectedDistrict != null && SelectedDistrict.xa_phuong != null)
            {
                foreach (var w in SelectedDistrict.xa_phuong.Values.OrderBy(w => w.name))
                {
                    Wards.Add(w);
                }
            }
        }

        private DateTime? _dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged(nameof(DateOfBirth));
                }
            }
        }

        private string _studentID;
        public string StudentID
        {
            get => _studentID;
            set
            {
                if (_studentID != value)
                {
                    _studentID = value;
                    OnPropertyChanged(nameof(StudentID));
                }
            }
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private void Back()
        {
            BaseViewModel viewModel = _navigationService.PopStack();
            if (viewModel != null)
                _navigationService.Navigate(viewModel);

        }

        public override string ToString()
        {
            return "Học sinh >> Sửa thông tin học sinh ";
        }
    }
}
