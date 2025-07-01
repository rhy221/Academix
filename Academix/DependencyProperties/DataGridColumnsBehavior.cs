using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Academix.DependencyProperties
{
    public class DataGridColumnsBehavior
    {
        public static readonly DependencyProperty BindableColumnsProperty =
        DependencyProperty.RegisterAttached("BindableColumns",
            typeof(ObservableCollection<DataGridColumn>),
            typeof(DataGridColumnsBehavior),
            new UIPropertyMetadata(null, OnBindableColumnsChanged));

        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
            => element.SetValue(BindableColumnsProperty, value);

        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
            => (ObservableCollection<DataGridColumn>)element.GetValue(BindableColumnsProperty);

        private static void OnBindableColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid && e.NewValue is ObservableCollection<DataGridColumn> columns)
            {
                foreach (var column in columns)
                    dataGrid.Columns.Add(column);

                columns.CollectionChanged += (s, args) =>
                {
                    if (args.NewItems != null)
                        foreach (DataGridColumn col in args.NewItems)
                            dataGrid.Columns.Add(col);
                    if (args.OldItems != null)
                        foreach (DataGridColumn col in args.OldItems)
                            dataGrid.Columns.Remove(col);
                };
            }
        }
    }
}
