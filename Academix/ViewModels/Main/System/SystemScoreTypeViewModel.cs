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
using Academix.DbContexts;


namespace Academix.ViewModels.Main.System
{
    class SystemScoreTypeViewModel:BaseViewModel
    {
        private bool _isProcessing;
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

        public string NewScoreTypeName { get; set; }
        public int NewScoreTypeMultiplier { get; set; }

        public string SelectedScoreTypeName { get; set; } = "";
        public int SelectedScoreTypeMultiplier { get; set; } = 1;

        private ScoreTypeViewModel _selectedScoreType;
        public ScoreTypeViewModel SelectedScoreType
        {
            get
            {
                return _selectedScoreType;
            }
            set
            {
                _selectedScoreType = value;
                if(_selectedScoreType != null)
                {
                    SelectedScoreTypeName = value.Name;
                    SelectedScoreTypeMultiplier = value.Multiplier;
                    OnPropertyChanged(nameof(SelectedScoreTypeName));
                    OnPropertyChanged(nameof(SelectedScoreTypeMultiplier));
                }
                
            }
        }

        public ICommand AddScoreTypeCommand { get; }
        public ICommand ModifyScoreTypeCommand { get; }
        public ICommand DeleteScoreTypeCommand { get; }

        public SystemScoreTypeViewModel()
        {
            _scoreTypes = new ObservableCollection<ScoreTypeViewModel>();
            NewScoreTypeMultiplier = 1;

            AddScoreTypeCommand = new AsyncRelayCommand(AddNewScoreType);
            ModifyScoreTypeCommand = new AsyncRelayCommand(ModifyScoreType);
            DeleteScoreTypeCommand = new AsyncRelayCommand(DeleteScoreType);

            Task.Run(LoadDataAsynce).ConfigureAwait(false);
        }

        private async Task LoadDataAsynce()
        {
            using(var context = new QuanlyhocsinhContext())
            {
                List<Loaidiem> scoreTypes = await context.Loaidiems.ToListAsync();
                ObservableCollection<ScoreTypeViewModel> scoreTypeViewModels = new ObservableCollection<ScoreTypeViewModel>();
                foreach(Loaidiem scoreType in scoreTypes)
                {
                    scoreTypeViewModels.Add(new ScoreTypeViewModel(scoreType));
                }
                _scoreTypes = scoreTypeViewModels;
            }
        }

        private async Task AddNewScoreType()
        {
            using(var context = new QuanlyhocsinhContext())
            {
                try
                {
                    
                    if (string.IsNullOrWhiteSpace(NewScoreTypeName))
                        throw new Exception("Tên không được trống!");
                    foreach(ScoreTypeViewModel scoreTypeViewModel in _scoreTypes)
                    {
                        if(scoreTypeViewModel.Name == NewScoreTypeName)
                            throw new Exception("Tên loại điểm đã tồn tại. Hãy đặt tên khác!");

                    }
                    Loaidiem scoreType = new Loaidiem();
                    scoreType.Maloaidiem = GenerateIdService.GenerateId();
                    scoreType.Tenloaidiem = NewScoreTypeName;
                    scoreType.Hesold = NewScoreTypeMultiplier;
                    await context.Loaidiems.AddAsync(scoreType);
                    await context.SaveChangesAsync();
                    _scoreTypes.Add(new ScoreTypeViewModel(scoreType));
                    OnPropertyChanged(nameof(ScoreTypes));
                    MessageBox.Show("Thêm loại điểm mới thành công!");
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
            }
        }

        private async Task ModifyScoreType()
        {
            using (var context = new QuanlyhocsinhContext())
            {
                try
                {
                    if (_selectedScoreType == null)
                        throw new Exception("Xin hãy chọn môn học muốn sửa!");

                    if (string.IsNullOrWhiteSpace(SelectedScoreTypeName))
                        throw new Exception("Tên không được trống!");

                    if(SelectedScoreTypeName != _selectedScoreType.Name)
                        foreach (ScoreTypeViewModel scoreTypeViewModel in _scoreTypes)
                        {
                            if (scoreTypeViewModel.Name == SelectedScoreTypeName)
                                throw new Exception("Tên loại điểm đã tồn tại. Hãy đặt tên khác!");

                        }

                    Loaidiem scoreType = await context.Loaidiems.FirstOrDefaultAsync(st => st.Maloaidiem == _selectedScoreType.Id);
                    scoreType.Tenloaidiem = SelectedScoreTypeName;
                    scoreType.Hesold = SelectedScoreTypeMultiplier;
                    await context.SaveChangesAsync();
                    _selectedScoreType.Name = scoreType.Tenloaidiem;
                    _selectedScoreType.Multiplier = scoreType.Hesold;
                    OnPropertyChanged(nameof(ScoreTypes));
                    MessageBox.Show("Thay đổi loại điểm thành công!");
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

        private async Task DeleteScoreType()
        {
            using (var context = new QuanlyhocsinhContext())
            {
                try
                {

                    if (_selectedScoreType == null)
                        throw new Exception("Xin hãy chọn môn học muốn xóa");

                    if (string.IsNullOrWhiteSpace(SelectedScoreTypeName))
                        throw new Exception("Tên không được trống!");

                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa học sinh?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Loaidiem scoreType = await context.Loaidiems.FirstOrDefaultAsync(st => st.Maloaidiem == _selectedScoreType.Id);
                        context.Loaidiems.Remove(scoreType);
                        await context.SaveChangesAsync();
                        _scoreTypes.Remove(_selectedScoreType);
                        _selectedScoreType = null;
                        SelectedScoreTypeName = "";
                        SelectedScoreTypeMultiplier = 1;
                        OnPropertyChanged(nameof(SelectedScoreTypeName));
                        OnPropertyChanged(nameof(SelectedScoreTypeMultiplier));
                        OnPropertyChanged(nameof(ScoreTypes));
                        MessageBox.Show("Xóa loại điểm thành công!");
                    }
                        

                }
                catch (DbUpdateException ex)
                {

                    MessageBox.Show("Tồn tại dữ liệu liên quan loại điểm không thể xóa");
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
}
