using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using CustomUI.Core.Models;
using CustomUI.iOS.CustomTableViewSources;
using Foundation;
using MvvmCross.Binding.Attributes;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CustomUI.iOS.CustomViews
{
    [Register("SimpleDropdownView")]
    public class SimpleDropdownView : UIView
    {
        #region Fields

        public EventHandler SelectedIndexChanged;

        private UILabel _selectedLabel;
        private UIImageView _imageView;
        private UITableView _tableView;
        private DropdownTableViewSource _source;

        private NSLayoutConstraint _heightConstraint;

        private bool _isExpanded;
        private IEnumerable<BaseObjectModel> _itemsSource;
        private int _selectedIndex;

        public IEnumerable<BaseObjectModel> ItemsSource
        {
            get => _itemsSource;
            set
            {
                if (value != null)
                {
                    _itemsSource = value;
                    SelectedIndex = -1;
                    Placeholder = Placeholder;
                    _source.ItemsSource = value;
                    Expand(false);
                }
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (ItemsSource.Count() - 1 < value || value < -1)
                    return;
                _selectedLabel.Text = value == -1 ? Placeholder : (ItemsSource.ElementAt(value) as BaseObjectModel).Title;
                _selectedIndex = value;
                SelectedIndexChanged?.Invoke(this, null);
            }
        }

        public int ExpandedHeight { get; set; } = 400;
        public int CurtailedHeight { get; set; } = 30;

        private string _placeholder;
        public string Placeholder { get => _placeholder; set { if (_selectedLabel != null) _selectedLabel.Text = value; _placeholder = value; } }

        public string EmptyMessage { get; set; }


        #endregion
        #region Constructors

        public SimpleDropdownView()
        {
            ViewInitializer();
        }

        public SimpleDropdownView(NSCoder coder) : base(coder)
        {
            ViewInitializer();
        }

        public SimpleDropdownView(CGRect frame) : base(frame)
        {
            ViewInitializer();
        }

        protected SimpleDropdownView(NSObjectFlag t) : base(t)
        {
            ViewInitializer();
        }

        protected internal SimpleDropdownView(IntPtr handle) : base(handle)
        {
            ViewInitializer();
        }

        #endregion
        #region Methods

        private void ViewInitializer()
        {
            this.TranslatesAutoresizingMaskIntoConstraints = false;
            this.ClipsToBounds = true;
            this.SetContentCompressionResistancePriority(1000, UILayoutConstraintAxis.Horizontal);
            this.SetContentCompressionResistancePriority(1000, UILayoutConstraintAxis.Vertical);

            var toRemove = this.Constraints.Where(x => x.FirstItem == this && x.FirstAttribute == NSLayoutAttribute.Height);
            if (toRemove.FirstOrDefault() != null)
                CurtailedHeight = (int)toRemove.FirstOrDefault().Constant;
            foreach (var v in toRemove)
            {
                this.RemoveConstraint(v);
            }

            _imageView = new UIImageView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Image = UIImage.FromBundle("arrow_down"),
                ContentMode = UIViewContentMode.ScaleAspectFill,
                UserInteractionEnabled = true,
            };

            this.AddSubview(_imageView);

            _imageView.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, -12).Active = true;
            _imageView.TopAnchor.ConstraintEqualTo(this.TopAnchor, (CurtailedHeight - 26) / 2).Active = true;
            _imageView.HeightAnchor.ConstraintEqualTo(26).Active = true;
            _imageView.WidthAnchor.ConstraintEqualTo(_imageView.HeightAnchor).Active = true;
            _imageView.SetContentHuggingPriority(750, UILayoutConstraintAxis.Horizontal);

            _imageView.AddGestureRecognizer(new UITapGestureRecognizer(() => Expand()));

            _selectedLabel = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Text = _placeholder,
                Font = UIFont.SystemFontOfSize(17),
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 1,
            };

            this.AddSubview(_selectedLabel);

            _selectedLabel.HeightAnchor.ConstraintEqualTo(20).Active = true;
            _selectedLabel.TopAnchor.ConstraintEqualTo(this.TopAnchor, (CurtailedHeight - 20) / 2).Active = true;
            _selectedLabel.TrailingAnchor.ConstraintEqualTo(_imageView.LeadingAnchor).Active = true;
            _selectedLabel.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, 32).Active = true;

            _heightConstraint = this.HeightAnchor.ConstraintEqualTo(CurtailedHeight);
            _heightConstraint.Active = true;

            var separator = new UIView();
            separator.TranslatesAutoresizingMaskIntoConstraints = false;
            separator.Layer.BackgroundColor = new CGColor(0.97f, 0.75f);

            this.AddSubview(separator);

            separator.TrailingAnchor.ConstraintEqualTo(_imageView.LeadingAnchor, -10).Active = true;
            separator.TopAnchor.ConstraintEqualTo(this.TopAnchor, 8).Active = true;
            separator.WidthAnchor.ConstraintEqualTo(1).Active = true;
            separator.HeightAnchor.ConstraintEqualTo(CurtailedHeight - 16).Active = true;

            _tableView = new UITableView { TranslatesAutoresizingMaskIntoConstraints = false, RowHeight = 40 };
            _tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            _source = new DropdownTableViewSource(_tableView);
            _tableView.Source = _source;
            _source.SelectedItemChanged += (s, a) => { SelectedIndex = _tableView.IndexPathForSelectedRow.Row; Expand(false); };

            this.AddSubview(_tableView);

            _tableView.TopAnchor.ConstraintEqualTo(this.TopAnchor, CurtailedHeight + 1).Active = true;
            _tableView.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor).Active = true;
            _tableView.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor).Active = true;
            _tableView.BottomAnchor.ConstraintEqualTo(this.BottomAnchor, -8).Active = true;

            var horizontalSeparator = new UIView();
            horizontalSeparator.TranslatesAutoresizingMaskIntoConstraints = false;
            horizontalSeparator.Layer.BackgroundColor = new CGColor(0.97f, 0.75f);

            this.AddSubview(horizontalSeparator);

            horizontalSeparator.TrailingAnchor.ConstraintEqualTo(this.TrailingAnchor, 0).Active = true;
            horizontalSeparator.LeadingAnchor.ConstraintEqualTo(this.LeadingAnchor, 0).Active = true;
            horizontalSeparator.TopAnchor.ConstraintEqualTo(this.TopAnchor, CurtailedHeight).Active = true;
            horizontalSeparator.HeightAnchor.ConstraintEqualTo(1).Active = true;
        }

        public void Expand(bool? force = null)
        {
            if (ItemsSource.Count() == 0)
            {
                if (EmptyMessage != null)
                {
                    var alert = new UIAlertView()
                    {
                        Title = "Warning",
                        Message = EmptyMessage,
                    };
                    alert.AddButton("Ok");
                    alert.Show();
                }

                return;
            }

            _isExpanded = force ?? !_isExpanded;

            Animate(0.3f, 0, UIViewAnimationOptions.CurveEaseOut, (() =>
            {
                var height = _isExpanded ? Math.Min(40 * _itemsSource.Count() + CurtailedHeight + 7, ExpandedHeight) : CurtailedHeight;
                _heightConstraint.Constant = height;

                var corner = _isExpanded ? 2 * (float)Math.PI / 2 : 4 * (float)Math.PI / 2;
                _imageView.Transform = CGAffineTransform.MakeRotation(corner);

                this.Superview.LayoutIfNeeded();
            }), null);
        }

        #endregion
    }
}