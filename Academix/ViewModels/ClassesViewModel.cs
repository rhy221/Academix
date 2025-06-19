using Academix.Models;
using Academix.Services;
using Academix.Stores;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Academix.ViewModels
{
    public class ClassesViewModel : BaseViewModel
    {
        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;


        public ClassesViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _schoolYearStore = schoolYearStore;
            _navigationService = navigationService;
            Grades = new ObservableCollection<Khoi>();
            Classes = new ObservableCollection<ClassViewModel>();
            SelectedClasses = new List<ClassViewModel>();

            SearchCommand = new AsyncRelayCommand(Search);
            AddCommand = new AsyncRelayCommand(AddClass);
            EditCommand = new AsyncRelayCommand(EditClass);
            DeleteCommand = new AsyncRelayCommand(DeleteSelected);
            ViewClassCommand = new RelayCommand(ViewClass);
            Task.Run(LoadDataAsync).ConfigureAwait(false);
        }

        private async Task LoadDataAsync()
        {
            using(var context = new QuanlyhocsinhContext())
            {
                List<Lop> classes = await context.Lops
                                                 .Include(l => l.MakhoiNavigation)
                                                 .Include(l => l.ManamhocNavigation)
                                                .ToListAsync();
                List<Khoi> grades = await context.Khois.ToListAsync();

                Khoi AllGrade = new Khoi();
                AllGrade.IsAll = true;
                
                ObservableCollection<Khoi> allGrades = new ObservableCollection<Khoi>(grades);
                allGrades.Insert(0, AllGrade);
                SearchSelectedGrade = AllGrade;

                AllGrades = allGrades;
                Grades = new ObservableCollection<Khoi>(grades);

                SchoolYears = new ObservableCollection<Namhoc>(_schoolYearStore.SchoolYears);
                SchoolYears.RemoveAt(0);
                
                ObservableCollection<ClassViewModel> classViewModels = new ObservableCollection<ClassViewModel>();
                foreach(Lop @class in classes)
                {
                    classViewModels.Add(new ClassViewModel(@class, @class.MakhoiNavigation.Tenkhoi, @class.ManamhocNavigation.ToString()));
                }
                Classes = classViewModels;
            }
        }

        private ObservableCollection<Khoi> _allGrade;
        public ObservableCollection<Khoi> AllGrades
        {
            get => _allGrade;
            set
            {
                _allGrade = value;
                OnPropertyChanged(nameof(AllGrades));
                   
            }
        }

        private ObservableCollection<Khoi> _grade;
        public ObservableCollection<Khoi> Grades
        {
            get => _grade;
            set
            {
                _grade = value;
                OnPropertyChanged(nameof(Grades));

            }
        }

        private ObservableCollection<Namhoc> _schoolYears;
        public ObservableCollection<Namhoc> SchoolYears
        {
            get => _schoolYears;
            set
            {
                _schoolYears = value;
                OnPropertyChanged(nameof(SchoolYears));

            }
        }

        private ObservableCollection<ClassViewModel> _classes;
        public ObservableCollection<ClassViewModel> Classes 
        {
            get => _classes;
            set
            {
                _classes = value;
                OnPropertyChanged(nameof(Classes));
            }

        }

        private Khoi _searchSelectedGrade;
        public Khoi SearchSelectedGrade {
            get => _searchSelectedGrade; 
            set
            {
                _searchSelectedGrade = value;
                OnPropertyChanged(nameof(SearchSelectedGrade));
            }
        }
        public string SearchClassName { get; set; }


        private ClassViewModel _selectedClass;
        public ClassViewModel SelectedClass
        {
            get
            {

                return _selectedClass;
            }
            set
            {

                _selectedClass = value;
                EditClassName = _selectedClass.ClassName;
                EditClassGrade = _grade.FirstOrDefault(g => g.Makhoi == _selectedClass.Class.Makhoi);
                EditClassSchoolYear = _schoolYears.FirstOrDefault(sy => sy.Manamhoc == _selectedClass.Class.Manamhoc);
                OnPropertyChanged(nameof(SelectedClass));
            }
        }

        private IList _selectedClasses;
        public IList SelectedClasses {
            get
            {

                return _selectedClasses;
            }
                set
            {
               
                _selectedClasses = value;
                OnPropertyChanged(nameof(SelectedClasses));
            } }

        private string _newClassName;
        public string NewClassName
        {
            get => _newClassName;
            set
            {
                _newClassName = value;
                OnPropertyChanged(nameof(NewClassName));
            }
        }

        private Khoi _newClassGrade;
        public Khoi NewClassGrade {
            get => _newClassGrade;
                
            set 
            {
                _newClassGrade = value;
                OnPropertyChanged(nameof(NewClassGrade));
            } 
        }

        private Namhoc _newClassSchoolYear;
        public Namhoc NewClassSchoolYear
        {
            get => _newClassSchoolYear;

            set
            {
                _newClassSchoolYear = value;
                OnPropertyChanged(nameof(NewClassSchoolYear));
            }
        }

        private string _editClassName;
        public string EditClassName {
            get => _editClassName; 
            set
            {
                _editClassName = value;
                OnPropertyChanged(nameof(EditClassName));
            }
        }

        private Khoi _editClassGrade;
        public Khoi EditClassGrade 
        {
            get => _editClassGrade; 
            set
            {
                _editClassGrade = value;
                OnPropertyChanged(nameof(EditClassGrade));
            }

        }

        private Namhoc _editClassSchoolYear;
        public Namhoc EditClassSchoolYear
        {
            get => _editClassSchoolYear;

            set
            {
                _editClassSchoolYear = value;
                OnPropertyChanged(nameof(EditClassSchoolYear));
            }
        }





        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ViewClassCommand { get; }
       
       

        private async Task Search()
        {
            try
            {
                using(var context = new QuanlyhocsinhContext())
                {

                    IQueryable<Lop> query = context.Lops
                .Include(l => l.MakhoiNavigation)
                .Include(l => l.ManamhocNavigation)
                .Include(l => l.CtLops);

                    if (!_schoolYearStore.SelectedSchoolYear.IsAll)
                        query = query.Where(l => l.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc);

                    if (!SearchSelectedGrade.IsAll)
                        query = query.Where(l => l.Makhoi == SearchSelectedGrade.Makhoi);

                    if (!string.IsNullOrWhiteSpace(SearchClassName))
                        query = query.Where(l => l.Tenlop.Contains(SearchClassName));

                    List<Lop> classes = await query.ToListAsync();
                    //MessageBox.Show("" + classes.Count + _selectedSchoolYear.Manamhoc);

                    ObservableCollection<ClassViewModel> classViewModels = new ObservableCollection<ClassViewModel>();

                    foreach (Lop @class in classes)
                    {
                        classViewModels.Add(new ClassViewModel(@class, @class.MakhoiNavigation.Tenkhoi, @class.ManamhocNavigation.ToString()));
                    }
                    Classes = classViewModels;
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

        private async Task AddClass()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(NewClassName) || NewClassGrade == null || NewClassSchoolYear == null)
                    throw new Exception("Xin hãy nhập thông tin đầy đủ!");

               

                using (var context = new QuanlyhocsinhContext())
                {
                    bool isExist = await context.Lops.AnyAsync(l => l.Tenlop == NewClassName && l.Makhoi == NewClassGrade.Makhoi && l.Manamhoc == NewClassSchoolYear.Manamhoc);
                    if (isExist)
                        throw new Exception("Tên lớp đã tồn tại. Xin hãy chọn tên khác");

                    string id = GenerateIdService.GenerateId();
                    Lop @class = new Lop(id, NewClassName, 0, NewClassGrade.Makhoi, NewClassSchoolYear.Manamhoc);
                    await context.Lops.AddAsync(@class);
                    await context.SaveChangesAsync();
                    @class = await context.Lops
                                    .Include(l => l.MakhoiNavigation)
                                    .Include(l => l.ManamhocNavigation)
                                    .Include(l => l.CtLops)
                                    .FirstOrDefaultAsync(l => l.Malop == id);
                    Classes.Add(new ClassViewModel(@class, NewClassGrade.Tenkhoi, NewClassSchoolYear.ToString()));

                    MessageBox.Show("Thêm lớp thành công");
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

        private async Task EditClass()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(EditClassName) || EditClassGrade == null || EditClassSchoolYear == null)
                    throw new Exception("Xin hãy nhập thông tin đầy đủ!");

                    

                using (var context = new QuanlyhocsinhContext())
                {

                    if (_selectedClass.ClassName != EditClassName)
                    {
                        bool isExist = await context.Lops.AnyAsync(l => l.Tenlop == EditClassName && l.Makhoi == EditClassGrade.Makhoi && l.Manamhoc == EditClassSchoolYear.Manamhoc);
                        if(isExist)
                            throw new Exception("Tên lớp đã tồn tại. Xin hãy chọn tên khác");

                    }

                    Lop @class = await context.Lops.FirstOrDefaultAsync(l => l.Malop == _selectedClass.Id);
                    @class.Tenlop = EditClassName;
                    @class.Makhoi = EditClassGrade.Makhoi;
                    @class.Manamhoc = EditClassSchoolYear.Manamhoc;
                    await context.SaveChangesAsync();
                    @class = await context.Lops
                                   .Include(l => l.MakhoiNavigation)
                                   .Include(l => l.ManamhocNavigation)
                                   .Include(l => l.CtLops)
                                   .FirstOrDefaultAsync(l => l.Malop == _selectedClass.Id);
                    
                    int position = Classes.IndexOf(_selectedClass);
                    _selectedClass = new ClassViewModel(@class, @class.MakhoiNavigation.Tenkhoi, @class.ManamhocNavigation.ToString());
                    Classes[position] = _selectedClass;
                    OnPropertyChanged(nameof(Classes)); 
                    MessageBox.Show("Thay đổi lớp thành công");
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

        private async Task DeleteSelected()
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new QuanlyhocsinhContext())
                    {
                        List<ClassViewModel> classViewModels = _selectedClasses.Cast<ClassViewModel>().ToList();
                        foreach (ClassViewModel classViewModel in classViewModels)
                        {
                            if (classViewModel.Class.CtLops.Count > 0)
                                throw new Exception("Không thể xóa do lớp đã có học sinh");
                            context.Lops.Remove(classViewModel.Class);
                        }

                        await context.SaveChangesAsync();
                        foreach (ClassViewModel classViewModel in classViewModels)
                        {

                            _classes.Remove(classViewModel);
                        }
                        OnPropertyChanged(nameof(Classes));

                        MessageBox.Show("Xoá lớp thành công!");
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

       

        private void ViewClass()
        {
            if(_selectedClass != null)
            {
                _navigationService.PushStack(this);
                _navigationService.Navigate(new ViewClassViewModel(_navigationService, _selectedClass.Class));

            }
        }

       

        public override string ToString()
        {
            return "Lớp";
        }

    }
}
