using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using Academix.Models;
using Microsoft.Win32;
using Newtonsoft.Json;

using static Academix.Models.Address;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows.Input;

namespace Academix.ViewModels
{
    class AddFreshmanViewModel:BaseViewModel
    {

        private ContentControl _mainView;
        private ContentControl _container;
        public ICommand BackCommand { get; }
        public ICommand UploadImageCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }
        public event Action<Student> StudentSaved;


        public AddFreshmanViewModel()
        {
            BackCommand = new RelayCommand(Back);
            UploadImageCommand = new RelayCommand(UploadImage);
            SaveCommand = new RelayCommand(SaveStudent);
            ResetCommand = new RelayCommand(ResetFields);

            LoadProvinces();
        }


        public AddFreshmanViewModel(ContentControl mainView, ContentControl container)
        {
            _container = container;
            _mainView = mainView;
            BackCommand = new RelayCommand(Back);
            UploadImageCommand = new RelayCommand(UploadImage);
            SaveCommand = new RelayCommand(SaveStudent);
            ResetCommand = new RelayCommand(ResetFields);

            LoadProvinces();
        }

        private void Back()
        {
            if (_container == null || _mainView == null)
                return;
            _container.Visibility = Visibility.Collapsed;
            _container.Content = null;
            _mainView.Visibility = Visibility.Visible;
        }

        private BitmapImage profileImage;
        public BitmapImage ProfileImage
        {
            get => profileImage;
            set
            {
                if (profileImage != value)
                {
                    profileImage = value;
                    OnPropertyChanged(nameof(ProfileImage));
                }
            }
        }

        private void UploadImage()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh hồ sơ";
            openFileDialog.Filter = "Ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    using (var stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                        bitmap.Freeze(); // Để ảnh có thể được dùng trên nhiều thread
                    }
                    ProfileImage = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            if (bitmapImage == null) return null;

            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        private void SaveStudent()
        {
            if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(StudentID))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin học sinh.");
                return;
            }

            var genderBool = SelectedGender == "Nam";
            string address = $"{SelectedWard?.name}, {SelectedDistrict?.name}, {SelectedProvince?.name}";
            byte[] avatarBytes = BitmapImageToByteArray(ProfileImage);

            var newStudent = new Student(StudentID, FullName, genderBool, DateOfBirth ?? DateTime.Now, address, "", avatarBytes);
            StudentSaved?.Invoke(newStudent);
            MessageBox.Show("Đã lưu.");
        }

        private void ResetFields()
        {
            FullName = string.Empty;
            StudentID = string.Empty;
            SelectedGrade = null;
            SelectedClass = null;
            SelectedStatus = null;
            SelectedGender = null;
            DateOfBirth = null;
            SelectedProvince = null;
            SelectedDistrict = null;
            SelectedWard = null;
            ProfileImage = null;
        }

        private string selectedGrade;
        public List<string> GradeList { get; } = new List<string> { "10", "11", "12" };
        public string SelectedGrade
        {
            get => selectedGrade;
            set
            {
                selectedGrade = value;
                OnPropertyChanged(nameof(SelectedGrade));
                FilterClassByGrade();
            }
        }

        private string selectedClass;
        public List<string> ClassList { get; } = new List<string> { "10/1", "10/2", "10/3",
                                                                    "11/1", "11/2", "11/3",
                                                                    "12/1", "12/2", "12/3"};
        public string SelectedClass
        {
            get => selectedClass;
            set
            {
                selectedClass = value;
                OnPropertyChanged(nameof(SelectedClass));
            }
        }
        private List<string> filteredClassList;
        public List<string> FilteredClassList
        {
            get => filteredClassList;
            set
            {
                filteredClassList = value;
                OnPropertyChanged(nameof(FilteredClassList));
            }
        }
        private void FilterClassByGrade()
        {
            if (string.IsNullOrEmpty(SelectedGrade))
            {
                FilteredClassList = new List<string>();
            }
            else
            {
                FilteredClassList = ClassList
                    .Where(cls => cls.StartsWith(SelectedGrade))
                    .ToList();
            }

            SelectedClass = null;
        }

        private string selectedStatus;
        public List<string> StatusList { get; } = new List<string> { "Đang học", "Nghỉ học" };

        public string SelectedStatus
        {
            get => selectedStatus;
            set
            {
                if (selectedStatus != value)
                {
                    selectedStatus = value;
                    OnPropertyChanged(nameof(SelectedStatus));
                }
            }
        }

        private string selectedGender;
        public List<string> GenderList { get; } = new List<string> { "Nam", "Nữ" };
        public string SelectedGender
        {
            get => selectedGender;
            set
            {
                selectedGender = value;
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        private string selectedEthnicity;
        public List<string> EthnicityList { get; } = new List<string>
        {
            "Ba Na", "Bố Y", "Brâu", "Bru - Vân Kiều", "Chăm",
            "Chơ Ro", "Chứt", "Co", "Cơ Tu", "Cống",
            "Cờ Lao", "Dao", "Ê Đê", "Giáy", "Gia Rai",
            "Hà Nhì", "Hrê", "H'Mông", "Kháng", "Khơ Mú",
            "Khmer", "Kinh", "La Chí", "La Ha", "La Hủ",
            "Lào", "Lô Lô", "Lự", "Mạ", "Mảng",
            "Mnông", "Mường", "Ngái", "Nùng", "Ô Đu",
            "Pà Thẻn", "Phù Lá", "Pu Péo", "Ra Glai", "Rơ Măm",
            "Sán Chay", "Sán Dìu", "Si La", "Stiêng", "Tà Ôi",
            "Tày", "Thái", "Thổ", "Xê Đăng", "Xinh Mun",
            "Xơ Đăng", "Yến", "Dao Đỏ", "Khác"
        };
        public string SelectedEthnicity
        {
            get => selectedEthnicity;
            set
            {
                selectedEthnicity = value;
                OnPropertyChanged(nameof(SelectedEthnicity));
            }
        }

        private string studentID;
        public string StudentID
        {
            get => studentID;
            set
            {
                if (studentID != value)
                {
                    studentID = value;
                    OnPropertyChanged(nameof(StudentID));
                }
            }
        }
        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
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
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DonViHanhChinhVietNam.json");
            string json = System.IO.File.ReadAllText(path);
            var provinceDict = JsonConvert.DeserializeObject<Dictionary<string, Province>>(json);

            Provinces.Clear();
            foreach (var p in provinceDict.Values.OrderBy(p => p.name)) 
            {
                Provinces.Add(p);
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

        private DateTime? dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (dateOfBirth != value)
                {
                    dateOfBirth = value;
                    OnPropertyChanged(nameof(DateOfBirth));
                }
            }
        }
    }
}
