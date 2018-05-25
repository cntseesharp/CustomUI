// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CustomUI.iOS
{
    [Register ("FirstViewController")]
    partial class FirstViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        CustomUI.iOS.CustomViews.SimpleDropdownView cities { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        CustomUI.iOS.CustomViews.SimpleDropdownView dropdownView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel selectedIndexLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (cities != null) {
                cities.Dispose ();
                cities = null;
            }

            if (dropdownView != null) {
                dropdownView.Dispose ();
                dropdownView = null;
            }

            if (selectedIndexLabel != null) {
                selectedIndexLabel.Dispose ();
                selectedIndexLabel = null;
            }
        }
    }
}