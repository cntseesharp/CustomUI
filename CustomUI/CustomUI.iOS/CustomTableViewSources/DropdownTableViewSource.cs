using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomUI.iOS.CustomCells;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CustomUI.iOS.CustomTableViewSources
{
    public class DropdownTableViewSource : MvxTableViewSource
    {
        public DropdownTableViewSource(IntPtr handle) : base(handle)
        {
        }

        public DropdownTableViewSource(UITableView tableView) : base(tableView)
        {
            tableView.RegisterClassForCellReuse(typeof(DropdownCellView), DropdownCellView.Key);
            tableView.RegisterNibForCellReuse(DropdownCellView.Nib, DropdownCellView.Key);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (DropdownCellView)tableView.DequeueReusableCell(DropdownCellView.Key, indexPath);
            cell.BindingContext.DataContext = item;
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            base.RowSelected(tableView, indexPath);
            tableView.DeselectRow(indexPath, false);
        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            tableView.BackgroundColor = UIColor.Clear;
            cell.BackgroundColor = UIColor.Clear;
        }
    }
}