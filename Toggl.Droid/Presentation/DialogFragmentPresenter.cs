using System;
using System.Collections.Generic;
using Android.Support.V7.App;
using Toggl.Core.UI.ViewModels;
using Toggl.Core.UI.ViewModels.Calendar;
using Toggl.Core.UI.ViewModels.Settings;
using Toggl.Core.UI.Views;
using Toggl.Droid.Fragments;
using DialogFragment = Android.Support.V4.App.DialogFragment;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace Toggl.Droid.Presentation
{
    public class DialogFragmentPresenter : AndroidPresenter
    {
        
        private readonly Dictionary<Type, IViewModel> viewModelCache = new Dictionary<Type, IViewModel>();
        
        protected override HashSet<Type> AcceptedViewModels { get; } = new HashSet<Type>
        {
            typeof(CalendarPermissionDeniedViewModel),
            typeof(NoWorkspaceViewModel),
            typeof(SelectBeginningOfWeekViewModel),
            typeof(SelectColorViewModel),
            typeof(SelectDateFormatViewModel),
            typeof(SelectDefaultWorkspaceViewModel),
            typeof(SelectDurationFormatViewModel),
            typeof(SelectUserCalendarsViewModel),
            typeof(TermsOfServiceViewModel),
            typeof(UpcomingEventsNotificationSettingsViewModel)
        };

        private readonly Dictionary<Type, Func<DialogFragment>> presentableDialogFragments = new Dictionary<Type, Func<DialogFragment>>
        {
            [typeof(CalendarPermissionDeniedViewModel)] = () => new CalendarPermissionDeniedFragment(), 
            [typeof(NoWorkspaceViewModel)] = () => new NoWorkspaceFragment(), 
            [typeof(SelectBeginningOfWeekViewModel)] = () => new SelectBeginningOfWeekFragment(), 
            [typeof(SelectColorViewModel)] = () => new SelectColorFragment(), 
            [typeof(SelectDateFormatViewModel)] = () => new SelectDateFormatFragment(), 
            [typeof(SelectDefaultWorkspaceViewModel)] = () => new SelectDefaultWorkspaceFragment(), 
            [typeof(SelectDurationFormatViewModel)] = () => new SelectDurationFormatFragment(), 
            [typeof(SelectUserCalendarsViewModel)] = () => new SelectUserCalendarsFragment(), 
            [typeof(TermsOfServiceViewModel)] = () => new TermsOfServiceFragment(), 
            [typeof(UpcomingEventsNotificationSettingsViewModel)] = () => new UpcomingEventsNotificationSettingsFragment(), 
        };

        protected override void PresentOnMainThread<TInput, TOutput>(ViewModel<TInput, TOutput> viewModel, IView sourceView)
        {
            var viewModelType = viewModel.GetType();
            FragmentManager fragmentManager = null;
            if (sourceView is AppCompatActivity activity)
            {
                fragmentManager = activity.SupportFragmentManager;
            }

            if (sourceView is Fragment fragment)
            {
                fragmentManager = fragment.FragmentManager;
            }

            if (fragmentManager == null)
            {
                throw new Exception($"Parent ViewModel's view trying to present {viewModelType.Name} doesn't have a FragmentManager");
            }
            
            if (!presentableDialogFragments.TryGetValue(viewModelType, out var dialogFactory))
            {
                throw new Exception($"Failed to start Activity for viewModel with type {viewModelType.Name}");
            }

            viewModelCache[viewModelType] = viewModel;
            
            var dialog = dialogFactory();
            dialog.Show(fragmentManager, dialog.GetType().Name);
        }
        
        public TViewModel GetCachedViewModel<TViewModel>()
            where TViewModel : IViewModel
        {
            var cachedViewModel = (TViewModel)viewModelCache[typeof(TViewModel)];
            viewModelCache[typeof(TViewModel)] = null;
            return cachedViewModel;
        }
    }
}