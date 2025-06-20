using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using static Academix.Models.Address;
using static Academix.Models.Student;
using Microsoft.Win32; 
using System.Windows.Media.Imaging; 
using System.IO;
using Academix.Models;
using Academix.Services;
using Microsoft.EntityFrameworkCore;
using Academix.Stores;
using System.Configuration;


namespace Academix.ViewModels
{
    public class AddStudentViewModel:BaseViewModel
    {
        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;
       
        public ICommand BackCommand { get; }
        public ICommand UploadImageCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }
        public event Action<Student> StudentSaved;



        public AddStudentViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;

            BackCommand = new RelayCommand(Back);
            SaveCommand = new AsyncRelayCommand(SaveStudent);
            ResetCommand = new RelayCommand(ResetFields);
            Task.Run(LoadDataAsync).ConfigureAwait(false);
            LoadProvinces();

        }

        private async Task LoadDataAsync()
        {
            using(var context = new QuanlyhocsinhContext())
            {
                List<Khoi> grades = await context.Khois.ToListAsync();
                List<Lop> classes = await context.Lops.Where(l => l.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc).ToListAsync();
                GradeList = new ObservableCollection<Khoi>(grades);
                _classList = classes;

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
                    string.IsNullOrWhiteSpace(_selectedGender)||
                    _selectedProvince == null ||
                    _selectedDistrict == null ||
                    _selectedWard == null)
                {
                    throw new Exception("Vui lòng điền đầy đủ thông tin học sinh.");
                }

                
                

                using (var context = new QuanlyhocsinhContext())
                {
                    List<Thamso> thamsos = await context.Thamsos.ToListAsync();
                    DateTime dob = _dateOfBirth ?? new DateTime();
                    int age = DateTime.Now.Year - dob.Year;
                    Thamso minimumAge = thamsos.FirstOrDefault(t => t.Tenthamso == "TuoiToiThieu");
                    Thamso maximumAge = thamsos.FirstOrDefault(t => t.Tenthamso == "TuoiToiDa");

                    if (age < minimumAge.Giatri || age > maximumAge.Giatri)
                        throw new Exception($"Tuổi học sinh phải từ {minimumAge.Giatri} đến {maximumAge.Giatri}");

                    Thamso maximumClassSize = thamsos.FirstOrDefault(t => t.Tenthamso == "SiSoToiDa");

                    if (_selectedClass.Siso == maximumClassSize.Giatri)
                        throw new Exception($"Không thể thêm học sinh vì vượt qua sĩ số tối đa là {maximumClassSize.Giatri}");

                    Hocsinh student = new Hocsinh(GenerateIdService.GenerateId(), _fullName, _selectedGender, _dateOfBirth ?? new DateTime(), _selectedProvince.name + "_" + _selectedDistrict.name + "_" + _selectedWard.name, _email);
                    student.CtLops.Add(new CtLop(_selectedClass.Malop, _studentID, "HK1", 0d));
                    student.CtLops.Add(new CtLop(_selectedClass.Malop, _studentID, "HK2", 0d));

                    context.Hocsinhs.Add(student);
                    await context.SaveChangesAsync();

                    MessageBox.Show("Thêm học sinh thành công!");
                }
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
           
        }

        private void ResetFields()
        {
            FullName = string.Empty;
            StudentID = string.Empty;
            SelectedGrade = null;
            SelectedClass = null;
            SelectedGender = null;
            DateOfBirth = null;
            SelectedProvince = null;
            SelectedDistrict = null;
            SelectedWard = null;
            Email = string.Empty;
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
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\DonViHanhChinhVietNam.json");
                string json = System.IO.File.ReadAllText(path);
                var provinceDict = JsonConvert.DeserializeObject<Dictionary<string, Province>>(json);

                Provinces.Clear();
                foreach (var p in provinceDict.Values.OrderBy(p => p.name))
                {
                    Provinces.Add(p);
                }
            }
            catch(Exception ex)
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
            return "Học sinh >> Thêm học sinh ";
        }
    }
}

