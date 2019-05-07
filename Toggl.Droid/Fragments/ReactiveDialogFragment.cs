using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Toggl.Core.UI.ViewModels;
using Toggl.Core.UI.Views;

namespace Toggl.Droid.Fragments
{
    public abstract class ReactiveDialogFragment<TViewModel> : DialogFragment, IView
        where TViewModel : class, IViewModel
    {
        protected CompositeDisposable DisposeBag = new CompositeDisposable();

        protected abstract void InitializeViews(View view);

        public TViewModel ViewModel { get; set; }

        protected ReactiveDialogFragment()
        {
            ViewModel = AndroidDependencyContainer.Instance
                .DialogFragmentPresenter
                .GetCachedViewModel<TViewModel>();
        }

        protected ReactiveDialogFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
        
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            ViewModel?.AttachView(this);
        }

        public override void OnStart()
        {
            base.OnStart();
            ViewModel?.ViewAppearing();
        }

        public override void OnResume()
        {
            base.OnResume();
            ViewModel?.ViewAppeared();
        }

        public override void OnPause()
        {
            base.OnPause();
            ViewModel?.ViewDisappearing();
        }

        public override void OnStop()
        {
            base.OnStop();
            ViewModel?.ViewDisappeared();
        }

        public override void OnDestroyView()
        {
            ViewModel?.DetachView();
            base.OnDestroyView();
            DisposeBag.Dispose();
            DisposeBag = new CompositeDisposable();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing) return;
            DisposeBag?.Dispose();
        }
        
        public IObservable<bool> Confirm(string title, string message, string confirmButtonText, string dismissButtonText)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> Alert(string title, string message, string buttonTitle)
        {
            throw new NotImplementedException();
        }

        public IObservable<bool> ConfirmDestructiveAction(ActionType type, params object[] formatArguments)
        {
            throw new NotImplementedException();
        }

        public IObservable<T> Select<T>(string title, IEnumerable<SelectOption<T>> options, int initialSelectionIndex)
        {
            throw new NotImplementedException();
        }

        public IObservable<bool> RequestCalendarAuthorization(bool force = false)
        {
            throw new NotImplementedException();
        }

        public IObservable<bool> RequestNotificationAuthorization(bool force = false)
        {
            throw new NotImplementedException();
        }

        public void OpenAppSettings()
        {
            throw new NotImplementedException();
        }

        public IObservable<string> GetGoogleToken()
        {
            throw new NotImplementedException();
        }

        public Task Close()
        {
            Dismiss();
            return Task.CompletedTask;
        }
    }
}
