using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Academix.Views
{
    /// <summary>
    /// Interaction logic for HamburgerMenu.xaml
    /// </summary>
    public partial class HamburgerMenu : UserControl
    {

       // public event Action<ContentControl> ItemInvoke;
        public HamburgerMenu()
        {
            InitializeComponent();
        }

       

        private void HamburgerMenuControl_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
        {

        }

        //private bool isOpen = true;
        //private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        //{
        //    this.HamburgerMenuControl.Content = e.InvokedItem;

        //    if (!e.IsItemOptions && this.HamburgerMenuControl.IsPaneOpen)
        //    {
        //        // You can close the menu if an item was selected
        //        // this.HamburgerMenuControl.SetCurrentValue(HamburgerMenuControl.IsPaneOpenProperty, false);
        //    }
        //}

        //private void HamburgerMenuControl_HamburgerButtonClick(object sender, RoutedEventArgs e)
        //{
        //    if (HamburgerMenuControl.IsPaneOpen)
        //        HamburgerMenuControl.IsPaneOpen = false;
        //    else
        //        HamburgerMenuControl.IsPaneOpen = true;
        //}
    }
}
