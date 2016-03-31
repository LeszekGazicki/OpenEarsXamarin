using ObjCRuntime;
using System;
using Foundation;

[assembly: LinkWith ("libOpenEars.a", LinkTarget.Simulator | LinkTarget.Arm64 | LinkTarget.ArmV6 | LinkTarget.ArmV7,  ForceLoad = true, Frameworks="AudioToolbox AVFoundation", IsCxx=true)]
