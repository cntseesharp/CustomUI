using System;
using CustomUI.Core.Models;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CustomUI.iOS.CustomCells
{
    public partial class DropdownCellView : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("DropdownCellView");
        public static readonly UINib Nib;

        static DropdownCellView()
        {
            Nib = UINib.FromName("DropdownCellView", NSBundle.MainBundle);
        }

        protected DropdownCellView(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.CreateBindingSet<DropdownCellView, BaseObjectModel>().Bind(titleLabel).For(x => x.Text).To(m => m.Title).Apply();
        }
    }
}
