using Academix.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Academix.Services;
using Academix.DbContexts;

namespace Academix.ViewModels.Main.System
{
    public class SubjectSystemViewModel: BaseViewModel
    {
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
        private ObservableCollection<SubjectViewModel> _subjects;
        public ObservableCollection<SubjectViewModel> Subjects
        {
            get
            {
                return _subjects;
            }
            set
            {
                _subjects = value;
                OnPropertyChanged(nameof(Subjects));
            }
        }
        private ObservableCollection<ScoreTypeViewModel> _scoreTypes;
        public ObservableCollection<ScoreTypeViewModel> ScoreTypes
        {
            get
            {
                return _scoreTypes;
            }
            set
            {
                _scoreTypes = value;
                OnPropertyChanged(nameof(ScoreTypes));
            }
        }

        public int Size => _subjects.Count;

        private SubjectViewModel _selectedSubject;
        public SubjectViewModel  SelectedSubject
        {
            get
            {
                return _selectedSubject;
            }
            set
            {
                _selectedSubject = value;
                SelectedSubjectName = value.Name;
                SelectedSubjectMultiplier = value.Multiplier;
                OnPropertyChanged(nameof(SelectedSubject));
                OnPropertyChanged(nameof(SelectedSubjectName));
                OnPropertyChanged(nameof(SelectedSubjectMultiplier));
                foreach(ScoreTypeViewModel scoreTypeViewModel in _selectedSubjectScoreTypes)
                {
                    bool isFound = false;
                    foreach(Loaidiem scoreType in value.ScoreTypes)
                    {
                        if(scoreType.Maloaidiem == scoreTypeViewModel.Id)
                        {
                            isFound = true;
                            break;
                        }

                    }
                    scoreTypeViewModel.IsChecked = isFound;
                }
                OnPropertyChanged(nameof(SelectedSubjectScoreTypes));
            }
        }
        public string SelectedSubjectName { get; set; }
        public int SelectedSubjectMultiplier { get; set; }
        private ObservableCollection<ScoreTypeViewModel> _selectedSubjectScoreTypes;
        public ObservableCollection<ScoreTypeViewModel> SelectedSubjectScoreTypes
        {
            get
            {
                if(_selectedSubjectScoreTypes != null)
                    return _selectedSubjectScoreTypes;
                else
                    return new ObservableCollection<ScoreTypeViewModel>();
                 
            }
            set
            {
                _selectedSubjectScoreTypes = value;
                OnPropertyChanged(nameof(SelectedSubjectScoreTypes));
            }
        }

      
        public string NewSubjectName { get; set; }
        public int NewSubjectMultiplier { get; set; }
   

        public ICommand ModifyCommand { get; }
        public ICommand AddNewCommand { get; }
        public ICommand DeleteCommand { get; }

        public SubjectSystemViewModel()
        {
            ModifyCommand = new AsyncRelayCommand(ModifySubject);
            AddNewCommand = new AsyncRelayCommand(AddNewSubject);
            DeleteCommand = new AsyncRelayCommand(DeleteSubject);
            _subjects = new ObservableCollection<SubjectViewModel>();
            NewSubjectMultiplier = 1;
            Task.Run(LoadDataAsync).ConfigureAwait(false);
        }

        private async Task AddNewSubject()
        {
            IsProcessing = true;
            using(var context = new QuanlyhocsinhContext())
            {
                try
                {                  

                    if (string.IsNullOrWhiteSpace(NewSubjectName))
                        throw new Exception("Tên không được trống!");

                    foreach (SubjectViewModel subjectVM in _subjects)
                    {
                        if (subjectVM.Name == NewSubjectName)
                            throw new Exception("Tên môn học đã tồn tại. Hãy đặt tên khác!");
                    }

                    Monhoc subject = new Monhoc();
                    bool isAllUnchecked = true;
                    foreach (ScoreTypeViewModel scoreTypeViewModel in ScoreTypes)
                    {
                        if (scoreTypeViewModel.IsChecked)
                        {
                            isAllUnchecked = false;
                            Loaidiem scoreType = await context.Loaidiems.FirstOrDefaultAsync(st => st.Maloaidiem == scoreTypeViewModel.Id);
                            subject.Maloaidiems.Add(scoreType);
                        }
                    }
                    if (isAllUnchecked)
                        throw new Exception("Phải chọn ít nhât một cột điểm!");

                    subject.Mamh = GenerateIdService.GenerateId();
                    subject.Tenmh = NewSubjectName;
                    subject.Heso = NewSubjectMultiplier;
                    
                    await context.Monhocs.AddAsync(subject);
                    await context.SaveChangesAsync();
                    SubjectViewModel subjectViewModel = new SubjectViewModel(subject);
                    subjectViewModel.ScoreTypes = new ObservableCollection<Loaidiem>(subject.Maloaidiems);
                    _subjects.Add(subjectViewModel);
                    
                    MessageBox.Show("Thêm môn học thành công!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    IsProcessing = false;

                }

            }
        }

        private async Task ModifySubject()
        {
            IsProcessing = true;
            using(var context = new QuanlyhocsinhContext())
            {
                try
                {
                    if (_selectedSubject == null)
                        throw new Exception("Xin hãy chọn môn học muốn sửa!");

                    if (string.IsNullOrWhiteSpace(SelectedSubjectName))
                        throw new Exception("Tên không được trống!");

                    if(SelectedSubjectName != _selectedSubject.Name)
                        foreach(SubjectViewModel subjectViewModel in _subjects)
                        {
                            if (subjectViewModel.Name == SelectedSubjectName)
                                throw new Exception("Tên môn học đã tồn tại. Hãy đặt tên khác!");
                        }
                    Monhoc subject = await context.Monhocs
                        .Include(s => s.Maloaidiems)
                        .FirstOrDefaultAsync(s => s.Mamh == _selectedSubject.Id);
                    
                    bool isAllUnchecked = true;
                    foreach (ScoreTypeViewModel scoreTypeViewModel in SelectedSubjectScoreTypes)
                    {
                        if (scoreTypeViewModel.IsChecked)
                        {
                            isAllUnchecked = false;
                            if (!subject.Maloaidiems.Any(st => st.Maloaidiem == scoreTypeViewModel.Id))
                            {
                                Loaidiem scoreType = await context.Loaidiems.FirstOrDefaultAsync(st => st.Maloaidiem == scoreTypeViewModel.Id);
                                subject.Maloaidiems.Add(scoreType);
                            }
                           
                        }
                        else
                        {
                            if (subject.Maloaidiems.Any(st => st.Maloaidiem == scoreTypeViewModel.Id))
                            {
                                Loaidiem scoreType = await context.Loaidiems.FirstOrDefaultAsync(st => st.Maloaidiem == scoreTypeViewModel.Id);
                                subject.Maloaidiems.Remove(scoreType);
                            }
                                
                        }
                    }
                    if (isAllUnchecked)
                        throw new Exception("Phải chọn ít nhất một cột điểm!");

                    subject.Tenmh = SelectedSubjectName;
                    subject.Heso = SelectedSubjectMultiplier;

                    await context.SaveChangesAsync();

                    _selectedSubject.Name = subject.Tenmh;
                    _selectedSubject.Multiplier = subject.Heso;
                    _selectedSubject.ScoreTypes = new ObservableCollection<Loaidiem>(subject.Maloaidiems);

                    OnPropertyChanged(nameof(Subjects));
                    MessageBox.Show("Sửa môn học đổi thành công!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    IsProcessing = false;

                }


            }
            

        }
        private async Task DeleteSubject()
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    IsProcessing = true;
                    if (_selectedSubject == null)
                        throw new Exception("Xin hãy chọn môn học muốn xóa!");
                    using (var context = new QuanlyhocsinhContext())
                    {
                        Monhoc subject = await context.Monhocs
                                                        .Include(s => s.Maloaidiems)
                                                        .FirstOrDefaultAsync(s => s.Mamh == _selectedSubject.Id);
                        subject.Maloaidiems.Clear();
                        context.Monhocs.Remove(subject);
                        await context.SaveChangesAsync();
                    }
                    _subjects.Remove(_selectedSubject);
                    _selectedSubject = null;
                    SelectedSubjectName = "";
                    SelectedSubjectMultiplier = 1;
                    OnPropertyChanged(nameof(SelectedSubjectName));
                    OnPropertyChanged(nameof(SelectedSubjectMultiplier));
                    MessageBox.Show("Xóa môn học thành công!");
                }
            }
            catch (DbUpdateException ex)
            {

                MessageBox.Show("Tồn tại dữ liệu liên quan môn học không thể xóa");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsProcessing = false;
            }
           
        }

        private async Task LoadDataAsync()
        {
            IsProcessing = true;
            using(var context = new QuanlyhocsinhContext())
            {
                List<Monhoc> subjects = await context.Monhocs
                                            .Include(s => s.Maloaidiems)
                                            .ToListAsync();
                List<SubjectViewModel> subjectViewModels = new List<SubjectViewModel>();
                foreach (Monhoc subject in subjects)
                {
                    SubjectViewModel subjectViewModel = new SubjectViewModel(subject);
                    subjectViewModel.ScoreTypes = new ObservableCollection<Loaidiem>(subject.Maloaidiems);
                    subjectViewModels.Add(subjectViewModel);
                }

                Subjects = new ObservableCollection<SubjectViewModel>(subjectViewModels);

                List<Loaidiem> scoreTypes = await context.Loaidiems.ToListAsync();
                List<ScoreTypeViewModel> scoreTypeViewModels = new List<ScoreTypeViewModel>();
                List<ScoreTypeViewModel> selectedScoreTypeViewModels = new List<ScoreTypeViewModel>();
                List<ScoreTypeViewModel> newScoreTypeViewModels = new List<ScoreTypeViewModel>();

                foreach (Loaidiem scoreType in scoreTypes)
                {
                    scoreTypeViewModels.Add(new ScoreTypeViewModel(scoreType));
                    selectedScoreTypeViewModels.Add(new ScoreTypeViewModel(scoreType));
                    newScoreTypeViewModels.Add(new ScoreTypeViewModel(scoreType));
                }
                ScoreTypes = new ObservableCollection<ScoreTypeViewModel>(scoreTypeViewModels);
                SelectedSubjectScoreTypes = new ObservableCollection<ScoreTypeViewModel>(selectedScoreTypeViewModels);
                
            }
            IsProcessing = false;
        }

       

    }
}
