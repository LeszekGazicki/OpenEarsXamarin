// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace OpenEarsTest.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView messageBox { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton recognizeDigitsButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton recognizeStartStopButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton recognizeYesNoButton { get; set; }

		[Action ("RecognizeDigitsButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void RecognizeDigitsButton_TouchUpInside (UIButton sender);

		[Action ("RecognizeStartStopButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void RecognizeStartStopButton_TouchUpInside (UIButton sender);

		[Action ("RecognizeYesNoButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void RecognizeYesNoButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (messageBox != null) {
				messageBox.Dispose ();
				messageBox = null;
			}
			if (recognizeDigitsButton != null) {
				recognizeDigitsButton.Dispose ();
				recognizeDigitsButton = null;
			}
			if (recognizeStartStopButton != null) {
				recognizeStartStopButton.Dispose ();
				recognizeStartStopButton = null;
			}
			if (recognizeYesNoButton != null) {
				recognizeYesNoButton.Dispose ();
				recognizeYesNoButton = null;
			}
		}
	}
}
