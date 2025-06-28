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
    public class SystemSemesterViewModel:BaseViewModel
    {

        private ObservableCollection<SemesterViewModel> _semesters =  new();
        public ObservableCollection<SemesterViewModel> Semesters
        {
            get => _semesters;
            set
            {
                _semesters = value;
                OnPropertyChanged(nameof(Semesters));
            }
        }

        private SemesterViewModel _selectedSemester;
        public SemesterViewModel SelectedSemester
        {
            get => _selectedSemester;
            set
            {
                _selectedSemester = value;
                ModifiedSemesterName = _selectedSemester.Name;
                OnPropertyChanged(nameof(ModifiedSemesterName));
                OnPropertyChanged(nameof(SelectedSemester));
            }
        }

        public String ModifiedSemesterName { get; set; }
        public String NewSemesterName { get; set; }

        public ICommand AddCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }

        public SystemSemesterViewModel()
        {
            Task.Run(LoadDataAsync).ConfigureAwait(false);
            AddCommand = new AsyncRelayCommand(Add);
            ModifyCommand = new AsyncRelayCommand(Modify);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }
        private async Task LoadDataAsync()
        {
            using(var context = new QuanlyhocsinhContext())
            {
                List<Hocky> semesters = await context.Hockies.ToListAsync();
                ObservableCollection<SemesterViewModel> semesterViewModels = new ObservableCollection<SemesterViewModel>();
                foreach(Hocky semester in semesters)
                {
                    semesterViewModels.Add(new SemesterViewModel(semester));
                }
                Semesters = semesterViewModels;
            }
        }

        public async Task Add()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(NewSemesterName))
                    throw new Exception("Xin hãy nhập tên học kỳ!");
                using(var context = new QuanlyhocsinhContext())
                {
                    if (context.Hockies.Any(hk => hk.Tenhocky == NewSemesterName))
                        throw new Exception("Tên học kỳ đã tồn tại. Xin hãy nhập tên mới!");
                    Hocky semester = new Hocky(GenerateIdService.GenerateId(), NewSemesterName);
                    context.Hockies.Add(semester);
                    await context.SaveChangesAsync();
                    Semesters.Add(new SemesterViewModel(semester));
                    MessageBox.Show("Thêm học kỳ thành công");
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

        public async Task Modify()
        {
            try
            {
                if (_selectedSemester == null)
                    throw new Exception("Xin hãy chọn học kỳ muốn sửa!");
                if (String.IsNullOrWhiteSpace(ModifiedSemesterName))
                    throw new Exception("Xin hãy nhập tên học kỳ!");
                using (var context = new QuanlyhocsinhContext())
                {
                    if ( context.Hockies.Any(hk => hk.Tenhocky == ModifiedSemesterName && hk.Tenhocky != _selectedSemester.Name))
                        throw new Exception("Tên học kỳ đã tồn tại. Xin hãy nhập tên mới!");
                    Hocky semester = await context.Hockies.FirstOrDefaultAsync(hk => hk.Mahocky == _selectedSemester.Id);
                    semester.Tenhocky = ModifiedSemesterName;
                    await context.SaveChangesAsync();
                    _selectedSemester.Name = ModifiedSemesterName;
                    OnPropertyChanged(nameof(Semesters));
                    MessageBox.Show("Sửa tên học kỳ thành công");
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
                if (_selectedSemester == null)
                    throw new Exception("Xin hãy chọn học kỳ muốn xóa!");
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa học kỳ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new QuanlyhocsinhContext())
                    {
                        Hocky semester = await context.Hockies.FirstOrDefaultAsync(hk => hk.Mahocky == _selectedSemester.Id);
                        context.Hockies.Remove(semester);
                        await context.SaveChangesAsync();
                        Semesters.Remove(_selectedSemester);
                        _selectedSemester = null;
                        MessageBox.Show("Xóa học kỳ thành công");
                    }
                }
                  
            }
            catch(DbUpdateException ex)
            {

                MessageBox.Show("Tồn tại dữ liệu liên quan học kỳ không thể xóa");
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
