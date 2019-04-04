﻿using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.Widget;
using Toggl.Foundation.MvvmCross.ViewModels;
using static Toggl.Giskard.Resource.Id;
using Toggl.Giskard.Views.Pomodoro;

namespace Toggl.Giskard.Activities
{
    public sealed partial class PomodoroEditWorkflowActivity : ReactiveActivity<PomodoroEditWorkflowViewModel>
    {
        private SeekBar durationSeekBar;
        private PomodoroWorkflowView workflowView;

        protected override void InitializeViews()
        {
            durationSeekBar = FindViewById<SeekBar>(DurationSeekBar);
            workflowView = FindViewById<PomodoroWorkflowView>(WorkflowView);
        }
    }
}
