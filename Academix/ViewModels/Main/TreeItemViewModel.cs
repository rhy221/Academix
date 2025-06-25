using Academix.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels.Main
{
    public class TreeItemViewModel:BaseViewModel
    {
        public object Item;
        public TreeItemViewModel(object item)
        {
            Item = item;
        }
        public string Title { 
            get
            {

                if (Item is Khoi grade)
                {
                    return grade.Tenkhoi;
                }
                else if (Item is Lop @class)
                {
                    return @class.Tenlop + $"({@class.Siso})";
                }

                return "";
            }
                }
        public ObservableCollection<TreeItemViewModel> Children { get; set; } = new();

        public override string ToString()
        {
            if(Item is Khoi grade)
            {
                return grade.Tenkhoi;
            }
            else if(Item is Lop @class)
            {
                return @class.Tenlop + $"({@class.Siso})";
            }

            return "";
        }
    }
}
