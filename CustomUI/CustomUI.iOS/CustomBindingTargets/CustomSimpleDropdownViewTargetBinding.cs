using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomUI.iOS.CustomViews;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using UIKit;

namespace CustomUI.iOS.CustomBindingTargets
{
    public class CustomSimpleDropdownViewTargetBinding : MvxTargetBinding<SimpleDropdownView, int>
    {
        private IDisposable _subscription;

        public CustomSimpleDropdownViewTargetBinding(SimpleDropdownView target) : base(target)
        {
        }

        protected override void SetValue(int value)
        {
            Target.SelectedIndex = value;
        }

        public override void SubscribeToEvents()
        {
            if (Target == null)
                return;

            Target.SelectedIndexChanged += HandleValueChanged;
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (!isDisposing) return;

            Target.SelectedIndexChanged -= HandleValueChanged;

            _subscription?.Dispose();
            _subscription = null;
        }

        private void HandleValueChanged(object sender, EventArgs e)
        {
            FireValueChanged((sender as SimpleDropdownView).SelectedIndex);
        }
    }
}