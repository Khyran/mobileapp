using CoreFoundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toggl.Core.UI.Navigation;
using Toggl.Core.UI.ViewModels;
using UIKit;

namespace Toggl.iOS.Presentation
{
    public abstract class IosPresenter : IPresenter
    {
        protected UIWindow Window { get; }
        protected AppDelegate AppDelegate { get; }
        
        protected abstract HashSet<Type> AcceptedViewModels { get; }
        protected abstract void PresentOnMainThread<TInput, TOutput>(ViewModel<TInput, TOutput> viewModel);
        
        public IosPresenter(UIWindow window, AppDelegate appDelegate)
        {
            Window = window;
            AppDelegate = appDelegate;
        }

        public virtual bool CanPresent<TInput, TOutput>(ViewModel<TInput, TOutput> viewModel)
            => AcceptedViewModels.Contains(viewModel.GetType());

        public Task Present<TInput, TOutput>(ViewModel<TInput, TOutput> viewModel)
        {
            var tcs = new TaskCompletionSource<object>();
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                PresentOnMainThread(viewModel);
                tcs.SetResult(true);
            });

            return tcs.Task;
        }

        protected UIViewController FindPresentedViewController()
            => FindPresentedViewController(Window.RootViewController);

        private UIViewController FindPresentedViewController(UIViewController current)
            => current.PresentedViewController == null || current.PresentedViewController.IsBeingDismissed
                ? current
                : FindPresentedViewController(current.PresentedViewController);
    }
}
