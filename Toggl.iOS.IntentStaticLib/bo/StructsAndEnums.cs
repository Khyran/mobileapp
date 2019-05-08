using System;
using ObjCRuntime;

namespace Toggl.iOS.Intents
{
	[Watch (5,0), iOS (12,0)]
	[Native]
	public enum ContinueTimerIntentResponseCode : nint
	{
		Unspecified = 0,
		Ready,
		ContinueInApp,
		InProgress,
		Success,
		Failure,
		FailureRequiringAppLaunch,
		FailureNoApiToken = 100,
		SuccessWithEntryDescription
	}

	[Watch (5,0), iOS (12,0)]
	[Native]
	public enum ShowReportIntentResponseCode : nint
	{
		Unspecified = 0,
		Ready,
		ContinueInApp,
		InProgress,
		Success,
		Failure,
		FailureRequiringAppLaunch
	}

	[Watch (5,0), iOS (12,0)]
	[Native]
	public enum ShowReportPeriodReportPeriod : nint
	{
		Unknown = 0,
		Today = 1,
		Yesterday = 2,
		ThisWeek = 3,
		LastWeek = 4,
		ThisMonth = 5,
		LastMonth = 6,
		ThisYear = 7
	}

	[Watch (5,0), iOS (12,0)]
	[Native]
	public enum ShowReportPeriodIntentResponseCode : nint
	{
		Unspecified = 0,
		Ready,
		ContinueInApp,
		InProgress,
		Success,
		Failure,
		FailureRequiringAppLaunch
	}

	[Watch (5,0), iOS (12,0)]
	[Native]
	public enum StartTimerFromClipboardIntentResponseCode : nint
	{
		Unspecified = 0,
		Ready,
		ContinueInApp,
		InProgress,
		Success,
		Failure,
		FailureRequiringAppLaunch,
		FailureNoApiToken = 100,
		FailureSyncConflict
	}

	[Watch (5,0), iOS (12,0)]
	[Native]
	public enum StartTimerIntentResponseCode : nint
	{
		Unspecified = 0,
		Ready,
		ContinueInApp,
		InProgress,
		Success,
		Failure,
		FailureRequiringAppLaunch,
		FailureNoApiToken = 100,
		FailureSyncConflict
	}

	[Watch (5,0), iOS (12,0)]
	[Native]
	public enum StopTimerIntentResponseCode : nint
	{
		Unspecified = 0,
		Ready,
		ContinueInApp,
		InProgress,
		Success,
		Failure,
		FailureRequiringAppLaunch,
		FailureNoTimerRunning = 100,
		FailureNoApiToken,
		SuccessWithEmptyDescription,
		FailureSyncConflict
	}
}
