using System;
using System.Drawing;

using Foundation;
using ObjCRuntime;
using CoreAnimation;
using UIKit;
using AVFoundation;

namespace OpenEarsBinding
{
	[BaseType (typeof (NSObject))]
	interface OEContinuousModel {
		[Export ("exitListeningLoop")]
		bool ExitListeningLoop { get; set;  }

		[Export ("inMainRecognitionLoop")]
		bool InMainRecognitionLoop { get; set;  }

		[Export ("thereIsALanguageModelChangeRequest")]
		bool ThereIsALanguageModelChangeRequest { get; set;  }

		[Export ("languageModelFileToChangeTo")]
		string LanguageModelFileToChangeTo { get; set;  }

		[Export ("dictionaryFileToChangeTo")]
		string DictionaryFileToChangeTo { get; set;  }

		[Export ("secondsOfSilenceToDetect")]
		float SecondsOfSilenceToDetect { get; set;  }

		[Export ("listeningLoopWithLanguageModelAtPath:dictionaryAtPath:languageModelIsJSGF:")]
		void ListeningLoopWithLanguageModelAtPathdictionaryAtPathlanguageModelIsJSGF (string languageModelPath, string dictionaryPath, bool languageModelIsJSGF);

		[Export ("changeLanguageModelToFile:withDictionary:")]
		void ChangeLanguageModelToFilewithDictionary (string languageModelPathAsString, string dictionaryPathAsString);

		[Export ("getCurrentRoute")]
		String GetCurrentRoute ();

		[Export ("setCurrentRouteTo:")]
		void SetCurrentRouteTo (string newRoute);

		[Export ("getRecognitionIsInProgress")]
		int GetRecognitionIsInProgress ();

		[Export ("setRecognitionIsInProgressTo:")]
		void SetRecognitionIsInProgressTo (int recognitionIsInProgress);

		[Export ("getRecordData")]
		int GetRecordData ();

		[Export ("setRecordDataTo:")]
		void SetRecordDataTo (int recordData);

		[Export ("getMeteringLevel")]
		float GetMeteringLevel ();
	}

	[BaseType (typeof (NSObject))]
	interface OEFliteController {

		[Export ("speechInProgress")]
		bool SpeechInProgress { get; set;  }

		[Export ("audioPlayer")]
		AVAudioPlayer AudioPlayer { get; set;  }

		[Export ("eventsObserver")]
		OEEventsObserver OEEventsObserver { get; set;  }

		[Export ("speechData")]
		NSData SpeechData { get; set;  }

		[Export ("duration_stretch")]
		float Duration_stretch { get; set;  }

		[Export ("target_mean")]
		float Target_mean { get; set;  }

		[Export ("target_stddev")]
		float Target_stddev { get; set;  }

		[Export ("say:withVoice:")]
		void SaywithVoice (string statement, string voice);

		[Export ("fliteOutputLevel")]
		float FliteOutputLevel ();

		[Export ("interruptionRoutine:")]
		void InterruptionRoutine (AVAudioPlayer player);

		[Export ("interruptionOverRoutine:")]
		void InterruptionOverRoutine (AVAudioPlayer player);

		[Export ("sendResumeNotificationOnMainThread")]
		void SendResumeNotificationOnMainThread ();

		[Export ("sendSuspendNotificationOnMainThread")]
		void SendSuspendNotificationOnMainThread ();

		[Export ("interruptTalking")]
		void InterruptTalking ();

	}

	[BaseType (typeof (NSObject))]
	interface OEEventsObserver {
		[Export ("delegate")]
		OEEventsObserverDelegate Delegate { get; set;  }
	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface OEEventsObserverDelegate {
		
		// Audio Session Status Methods.

		[Export ("audioSessionInterruptionDidBegin")]
		void AudioSessionInterruptionDidBegin ();

		[Export ("audioSessionInterruptionDidEnd")]
		void AudioSessionInterruptionDidEnd ();

		[Export ("audioInputDidBecomeUnavailable")]
		void AudioInputDidBecomeUnavailable ();

		[Export ("audioInputDidBecomeAvailable")]
		void AudioInputDidBecomeAvailable ();

		[Export ("audioRouteDidChangeToRoute:")]
		void AudioRouteDidChangeToRoute (string newRoute);

		// Pocketsphinx Status Methods.

		// Pocketsphinx isn't listening yet but it has entered the main recognition loop.
		[Export ("pocketsphinxRecognitionLoopDidStart")]
		void PocketsphinxRecognitionLoopDidStart ();

		// Pocketsphinx is now listening
		[Export ("pocketsphinxDidStartListening")]
		void PocketsphinxDidStartListening ();

		// Pocketsphinx heard speech and is about to process it.
		[Export ("pocketsphinxDidDetectSpeech")]
		void PocketsphinxDidDetectSpeech ();

		// Pocketsphinx detected a second of silence indicating the end of an utterance.
		[Export ("pocketsphinxDidDetectFinishedSpeech")]
		void PocketsphinxDidDetectFinishedSpeech ();

		// Pocketsphinx has a hypothesis.
		[Export ("pocketsphinxDidReceiveHypothesis:recognitionScore:utteranceID:")]
		void PocketsphinxDidReceiveHypothesis (string hypothesis, string recognitionScore, string utteranceID);

		// Pocketsphinx has an n-best hypothesis dictionary.
		[Export ("pocketsphinxDidReceiveNBestHypothesisArray:")]
		void PocketsphinxDidReceiveNBestHypothesisArray(string[] hypothesisArray);

		// Pocketsphinx has exited the continuous listening loop.
		[Export ("pocketsphinxDidStopListening")]
		void PocketsphinxDidStopListening ();

		// Pocketsphinx has not exited the continuous listening loop but it will not attempt recognition.
		[Export ("pocketsphinxDidSuspendRecognition")]
		void PocketsphinxDidSuspendRecognition ();

		// Pocketsphinx has not existed the continuous listening loop and it will now start attempting recognition again.
		[Export ("pocketsphinxDidResumeRecognition")]
		void PocketsphinxDidResumeRecognition ();

		// Pocketsphinx switched language models inline.
		[Export ("pocketsphinxDidChangeLanguageModelToFile:andDictionary:")]
		void PocketsphinxDidChangeLanguageModelToFile (string newLanguageModelPathAsString, string newDictionaryPathAsString);

		// Some aspect of setting up the continuous loop failed, turn on OELogging for more info.
		[Export ("pocketSphinxContinuousSetupDidFailWithReason:")]
		void PocketSphinxContinuousSetupDidFailWithReason (string reasonForFailure);

		// Some aspect of tearing down the continuous loop failed, turn on OELogging for more info.
		[Export ("pocketSphinxContinuousTeardownDidFailWithReason:")]
		void PocketSphinxContinuousTeardownDidFailWithReason (string reasonForFailure);

		// Your test recognition run has completed.
		[Export ("pocketsphinxTestRecognitionCompleted")]
		void PocketsphinxTestRecognitionCompleted ();

		// Pocketsphinx couldn't start because it has no mic permissions (will only be returned on iOS7 or later).
		[Export ("pocketsphinxFailedNoMicPermissions")]
		void PocketsphinxFailedNoMicPermissions ();

		// The user prompt to get mic permissions, or a check of the mic permissions, has completed with a TRUE or a FALSE result  (will only be returned on iOS7 or later).
		[Export ("micPermissionCheckCompleted:")]
		void MicPermissionCheckCompleted (out bool result);

		// Flite Status Methods.

		// Flite started speaking. You probably don't have to do anything about this.
		[Export ("fliteDidStartSpeaking")]
		void FliteDidStartSpeaking ();

		// Flite finished speaking. You probably don't have to do anything about this.
		[Export ("fliteDidFinishSpeaking")]
		void FliteDidFinishSpeaking ();


	}

	[BaseType (typeof (NSObject))]
	interface OEPocketsphinxController {

		[Export ("continuousModel")]
		OEContinuousModel ContinuousModel { get; set;  }

		[Export ("pocketsphinxControllerQueue")]
		NSOperationQueue PocketsphinxControllerQueue { get; set;  }

		[Export ("openEarsEventsObserver")]
		OEEventsObserver OpenEarsEventsObserver { get; set;  }

		[Export ("secondsOfSilenceToDetect")]
		float SecondsOfSilenceToDetect { get; set; }

		[Export ("returnNbest")]
		bool ReturnNbest { get; set; }

		[Export ("nBestNumber")]
		int NBestNumber { get; set; }

		[Export ("verbosePocketSphinx")]
		bool VerbosePocketSphinx { get; set; }

		[Export ("returnNullHypotheses")]
		bool ReturnNullHypotheses { get; set; }

		[Export ("isSuspended")]
		bool isSuspended { get; set; }

		[Export ("isListening")]
		bool isListening { get; set; }

		[Export ("legacy3rdPassMode")]
		bool legacy3rdPassMode { get; set; }

		[Export ("removingNoise")]
		bool removingNoise { get; set; }

		[Export ("removingSilence")]
		bool removingSilence { get; set; }

		[Export ("vadThreshold")]
		float vadThreshold { get; set; }

		[Export ("disableBluetooth")]
		bool DisableBluetooth { get; set; }

		[Export ("disableMixing")]
		bool DisableMixing { get; set; }

		[Export ("disableSessionResetsWhileStopped")]
		bool DisableSessionResetsWhileStopped { get; set; }

		[Export ("stopListening")]
		void StopListening ();

		[Export ("startListeningWithLanguageModelAtPath:dictionaryAtPath:acousticModelAtPath:languageModelIsJSGF:")]
		void StartListeningWithLanguageModelAtPath (string languageModelPath, string dictionaryPath, string acousticModelAtPath, bool languageModelIsJSGF);

		[Export ("suspendRecognition")]
		void SuspendRecognition ();

		[Export ("resumeRecognition")]
		void ResumeRecognition ();

		[Export ("changeLanguageModelToFile:withDictionary:")]
		void ChangeLanguageModelToFile (string languageModelPathAsString, string dictionaryPathAsString);

		[Export ("removeCmnPlist")]
		void RemoveCmnPlist ();

		[Export ("runRecognitionOnWavFileAtPath:usingLanguageModelAtPath:dictionaryAtPath:acousticModelAtPath:languageModelIsJSGF:")]
		void runRecognitionOnWavFileAtPath (string wavPath, string languageModelPath, string dictionaryPath, string acousticModelPath, bool languageModelIsJSGF);

//		[Export ("suspendRecognitionForFliteSpeech")]
//		void SuspendRecognitionForFliteSpeech ();
//
//		[Export ("resumeRecognitionForFliteSpeech")]
//		void ResumeRecognitionForFliteSpeech ();
//
//		[Export ("setSecondsOfSilence")]
//		void SetSecondsOfSilence ();
//
//		[Export ("validateNBestSettings")]
//		void ValidateNBestSettings ();

		[Export ("requestMicPermission")]
		void RequestMicPermission ();

		[Export ("setActive:error:")]
		bool SetActive (bool active, ref NSError outError);

		[Static, Export ("sharedInstance")]
		OEPocketsphinxController SharedInstance ();

	}

	[BaseType (typeof (NSObject)) ]
	interface OELanguageModelGenerator {
		[Export ("generateLanguageModelFromArray:withFilesNamed:forAcousticModelAtPath:")]
		NSError GenerateLanguageModelFromArray (string[] languageModelArray, string fileName, string acousticModelPath);

		[Export ("pathToSuccessfullyGeneratedDictionaryWithRequestedName:")]
		string PathToSuccessfullyGeneratedDictionaryWithRequestedName (string name);

		[Export ("pathToSuccessfullyGeneratedLanguageModelWithRequestedName:")]
		string PathToSuccessfullyGeneratedLanguageModelWithRequestedName (string name);

		[Export ("PathToSuccessfullyGeneratedGrammarWithRequestedName:")]
		string PathToSuccessfullyGeneratedGrammarWithRequestedName (string name);

		[Export ("generateGrammarFromDictionary:withFilesNamed:forAcousticModelAtPath:")]
		NSError GenerateGrammarFromDictionary (NSDictionary grammarDictionary, string fileName, string acousticModelPath);

		[Export ("generateLanguageModelFromTextFile:withFilesNamed:forAcousticModelAtPath:")]
		NSError GenerateLanguageModelFromTextFile (string pathToTextFile, string fileName, string acousticModelPath);

		[Export ("createLanguageModelFromFilename:")]
		void CreateLanguageModelFromFilename (string fileName);

		[Export ("writeOutCorpusForArray:toFilename:")]
		NSError WriteOutCorpusForArray (string[] normalizedLanguageModelArray, string fileName);
	}

	[BaseType (typeof (NSObject))]
	interface OEAcousticModel {
		[Static, Export ("pathToModel:")]
		 string PathToModel (string acousticModelBundleName);
	}

}
