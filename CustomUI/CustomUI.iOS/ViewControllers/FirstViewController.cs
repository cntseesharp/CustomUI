using CustomUI.Core.Models;
using CustomUI.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.Platform.Converters;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using UIKit;

namespace CustomUI.iOS
{
    public partial class FirstViewController : MvxViewController<FirstViewModel>
    {
        public FirstViewController() : base("FirstViewController", null)
        {
        }

        public FirstViewController(IntPtr handle) : base("FirstViewController", null) { Handle = handle; }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstViewController, FirstViewModel>();
            set.Bind(selectedIndexLabel).To(vm => vm.SelectedCountryIndex);
            set.Bind(dropdownView).For(x => x.SelectedIndex).To(vm => vm.SelectedCountryIndex);
            set.Bind(dropdownView).For(x => x.ItemsSource).To(vm => vm.Countries);
            set.Bind(cities).For(x => x.ItemsSource).To(vm => vm.Cities);
            set.Apply();

            dropdownView.Placeholder = "Select country";
            NavigationController.NavigationBarHidden = true;

            cities.Placeholder = "Select city";
            cities.EmptyMessage = "You have to select country first";
        }
    }
}