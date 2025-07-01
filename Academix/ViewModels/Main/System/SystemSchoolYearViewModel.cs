using Academix.DbContexts;
using Academix.Models;
using Academix.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Academix.ViewModels.Main.System
{
    public class SystemSchoolYearViewModel: BaseViewModel
    {
        private ObservableCollection<SchoolYearViewModel> _schoolYears = new();
        public ObservableCollection<SchoolYearViewModel> SchoolYears
        {
            get => _schoolYears;
            set
            {
                _schoolYears = value;
                OnPropertyChanged(nameof(SchoolYears));
            }
        }

        public ObservableCollection<String> SchoolYearList { get; }

        private SchoolYearViewModel _selectedSchoolYear;
        public SchoolYearViewModel SelectedSchoolYear
        {
            get => _selectedSchoolYear;
            set
            {
                _selectedSchoolYear = value;
                if(_selectedSchoolYear != null)
                {
                    ModifiedSchoolYear = _selectedSchoolYear.ToString();
                    OnPropertyChanged(nameof(ModifiedSchoolYear));
                    OnPropertyChanged(nameof(SelectedSchoolYear));
                }
               
            }
        }

        public String ModifiedSchoolYear { get; set; }
        public String NewSchoolYear { get; set; }

        public ICommand AddCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }
        public SystemSchoolYearViewModel()
        {
            SchoolYearList = new ObservableCollection<string>();
            List<int> years = Enumerable.Range(2000, DateTime.Now.Year - 2000 + 1).ToList();
            foreach(int year in years)
            {
                SchoolYearList.Add($"{year}-{year + 1}");
            }
            NewSchoolYear = SchoolYearList.Last();
            ModifiedSchoolYear = SchoolYearList.Last();
            Task.Run(LoadDataAsync).ConfigureAwait(false);
            AddCommand = new AsyncRelayCommand(Add);
            ModifyCommand = new AsyncRelayCommand(Modify);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }
        private async Task LoadDataAsync()
        {
            using (var context = new QuanlyhocsinhContext())
            {
                List<Namhoc> schoolYears = await context.Namhocs.ToListAsync();
                ObservableCollection<SchoolYearViewModel> schoolYearViewModels = new ObservableCollection<SchoolYearViewModel>();
                foreach (Namhoc schoolYear in schoolYears)
                {
                    schoolYearViewModels.Add(new SchoolYearViewModel(schoolYear));
                }
                SchoolYears = schoolYearViewModels;
            }
        }

        public async Task Add()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(NewSchoolYear))
                    throw new Exception("Xin hãy chọn năm học!");
                using (var context = new QuanlyhocsinhContext())
                {
                    String[] years = NewSchoolYear.Split("-");
                    int firstYear = Convert.ToInt32(years[0]);
                    int secondYear = Convert.ToInt32(years[1]);
                    if (context.Namhocs.Any(nh => nh.Nam1 == firstYear && nh.Nam2 == secondYear))
                        throw new Exception("Năm học đã tồn tại. Xin hãy chọn năm học khác!");
                    Namhoc schoolYear = new Namhoc(GenerateIdService.GenerateId(), firstYear, secondYear );
                    context.Namhocs.Add(schoolYear);
                    await context.SaveChangesAsync();
                    SchoolYears.Add(new SchoolYearViewModel(schoolYear));
                    MessageBox.Show("Thêm năm học thành công");
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

        public async Task Modify()
        {
            try
            {
                if (_selectedSchoolYear == null)
                    throw new Exception("Xin hãy chọn năm học muốn sửa!");
                if (String.IsNullOrWhiteSpace(ModifiedSchoolYear))
                    throw new Exception("Xin hãy chọn năm học!");
                using (var context = new QuanlyhocsinhContext())
                {
                    String[] years = ModifiedSchoolYear.Split("-");
                    int firstYear = Convert.ToInt32(years[0]);
                    int secondYear = Convert.ToInt32(years[1]);
                    if (context.Namhocs.Any(nh => nh.Nam1 == firstYear && nh.Nam2 == secondYear && nh.Nam1 != _selectedSchoolYear.FirstYear && nh.Nam2 != _selectedSchoolYear.SecondYear))
                        throw new Exception("Năm đã tồn tại!");
                    Namhoc schoolYear = await context.Namhocs.FirstOrDefaultAsync(nh => nh.Manamhoc == _selectedSchoolYear.Id);
                    schoolYear.Nam1 = firstYear;
                    schoolYear.Nam2 = secondYear;
                    await context.SaveChangesAsync();
                    _selectedSchoolYear.FirstYear = firstYear;
                    _selectedSchoolYear.SecondYear = secondYear;
                    OnPropertyChanged(nameof(SchoolYears));
                    MessageBox.Show("Sửa năm học thành công!");
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

        public async Task Delete()
        {
            try
            {
                if (_selectedSchoolYear == null)
                    throw new Exception("Xin hãy chọn năm học muốn xóa!");
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa năm học?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new QuanlyhocsinhContext())
                    {
                        Namhoc schoolYear = await context.Namhocs.FirstOrDefaultAsync(nh => nh.Manamhoc == _selectedSchoolYear.Id);
                        context.Namhocs.Remove(schoolYear);
                        await context.SaveChangesAsync();
                        SchoolYears.Remove(_selectedSchoolYear);
                        _selectedSchoolYear = null;
                        MessageBox.Show("Xóa năm học thành công");
                    }
                }

            }
            catch (DbUpdateException ex)
            {

                MessageBox.Show("Tồn tại dữ liệu liên quan năm học, không thể xóa!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {

            }
        }
    }
}
