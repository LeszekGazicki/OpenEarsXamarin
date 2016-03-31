using System;		
using UIKit;
using Foundation;
using OpenEarsBinding;

namespace OpenEarsTest.iOS
{
	public partial class ViewController : UIViewController
	{
		internal class EventsObserverDelegate : OEEventsObserverDelegate
		{
			private UITextView messageBox;

			public EventsObserverDelegate(UITextView messageBox) : base()
			{
				this.messageBox = messageBox;
			}

			public override void PocketsphinxDidStartListening ()
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

			public override void PocketsphinxDidStopListening ()
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

			public override void PocketsphinxDidSuspendRecognition ()
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

			public override void PocketsphinxDidResumeRecognition ()
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

			public override void PocketsphinxRecognitionLoopDidStart ()
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

			public override void PocketsphinxDidDetectFinishedSpeech ()
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

			public override void PocketsphinxDidDetectSpeech ()
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

			public override void PocketsphinxDidReceiveHypothesis (string hypothesis, string recognitionScore, string utteranceID)
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name + "hypothesis " + hypothesis);
			}

			public override void PocketsphinxDidReceiveNBestHypothesisArray (string[] hypothesisArray)
			{
				LogMessage (System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

			private void LogMessage(string msg) 
			{
				Console.WriteLine("===== {0} ===== ", msg);
				messageBox.Text = msg + "\n" + messageBox.Text;
			}
		}

		const string digitLanguageModelName = "DigitLanguageModel";
		string pathToDigitLanguageModel;
		string pathToDigitDictionary;

		const string yesNoLanguageModelName = "YesNoLanguageModel";
		string pathToYesNoLanguageModel;
		string pathToYesNoDictionary;

		const string startStopLanguageModelName = "StartStopLanguageModel";
		string pathToStartStopLanguageModel;
		string pathToStartStopDictionary;

		OEEventsObserver eventObserver;
		EventsObserverDelegate eventsObserverDelegate;


		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.eventsObserverDelegate = new EventsObserverDelegate (this.messageBox);
			this.eventObserver = new OEEventsObserver();
			this.eventObserver.Delegate = eventsObserverDelegate;

			OELanguageModelGenerator gen = new OELanguageModelGenerator();
			generateDigitLanguageModel (gen);
			generateYesNoLanguageModel (gen);
			generateStartStopLanguageModel (gen);
		}

		private void generateDigitLanguageModel(OELanguageModelGenerator gen)
		{
			string[] langArray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
				 					"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
				 					"20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" };
			string acousticModelPath = OEAcousticModel.PathToModel ("AcousticModelEnglish");

			NSError error = gen.GenerateLanguageModelFromArray (langArray, digitLanguageModelName, acousticModelPath);
			if(error != null) {
				Console.WriteLine ("Dynamic language generator reported error {0}", error.Description);
			} else {
				pathToDigitLanguageModel = gen.PathToSuccessfullyGeneratedLanguageModelWithRequestedName (digitLanguageModelName);
				pathToDigitDictionary = gen.PathToSuccessfullyGeneratedDictionaryWithRequestedName (digitLanguageModelName);
			}
		}

		private void generateYesNoLanguageModel(OELanguageModelGenerator gen)
		{
			string[] langArray = { "YES", "NO" };
			string acousticModelPath = OEAcousticModel.PathToModel ("AcousticModelEnglish");

			NSError error = gen.GenerateLanguageModelFromArray (langArray, yesNoLanguageModelName, acousticModelPath);
			if(error != null) {
				Console.WriteLine ("Dynamic language generator reported error {0}", error.Description);
			} else {
				pathToYesNoLanguageModel = gen.PathToSuccessfullyGeneratedLanguageModelWithRequestedName (yesNoLanguageModelName);
				pathToYesNoDictionary = gen.PathToSuccessfullyGeneratedDictionaryWithRequestedName (yesNoLanguageModelName);
			}
		}

		private void generateStartStopLanguageModel(OELanguageModelGenerator gen)
		{
			string[] langArray = { "GO", "STOP" };
			string acousticModelPath = OEAcousticModel.PathToModel ("AcousticModelEnglish");

			NSError error = gen.GenerateLanguageModelFromArray (langArray, startStopLanguageModelName, acousticModelPath);
			if(error != null) {
				Console.WriteLine ("Dynamic language generator reported error {0}", error.Description);
			} else {
				pathToStartStopLanguageModel = gen.PathToSuccessfullyGeneratedLanguageModelWithRequestedName (startStopLanguageModelName);
				pathToStartStopDictionary = gen.PathToSuccessfullyGeneratedDictionaryWithRequestedName (startStopLanguageModelName);
			}
		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}

		partial void RecognizeDigitsButton_TouchUpInside (UIButton sender)
		{
			startListening(pathToDigitLanguageModel, pathToDigitDictionary);
		}

		partial void RecognizeYesNoButton_TouchUpInside (UIButton sender)
		{
			startListening(pathToYesNoLanguageModel, pathToYesNoDictionary);
		}

		partial void RecognizeStartStopButton_TouchUpInside (UIButton sender)
		{
			startListening(pathToStartStopLanguageModel, pathToStartStopDictionary);
		}

		private void startListening(string pathLangModel, string pathDictionary)
		{
			if (OEPocketsphinxController.SharedInstance ().isListening)
			{
				OEPocketsphinxController.SharedInstance ().ChangeLanguageModelToFile(
					pathLangModel,
					pathDictionary);
			}
			else
			{
				string acousticModelPath = OEAcousticModel.PathToModel ("AcousticModelEnglish");			
				OEPocketsphinxController.SharedInstance ().StartListeningWithLanguageModelAtPath (
					pathLangModel,
					pathDictionary,
					acousticModelPath,
					false);
			}
		}
	}
}
