using Android.Support.V7.Widget;
using Android.Views;

namespace Toggl.Droid.Fragments
{
    public partial class SelectWorkspaceFragment
    {
        private RecyclerView recyclerView;

        public void InitializeViews(View rootView)
        {
            recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.SelectWorkspaceRecyclerView);
        }
    }
}