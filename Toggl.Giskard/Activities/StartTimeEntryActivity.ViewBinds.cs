using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Toggl.Giskard.Views;
using static Toggl.Giskard.Resource.Id;

namespace Toggl.Giskard.Activities
{
    public partial class StartTimeEntryActivity
    {
        private View durationCard;
        private View doneButton;
        private View closeButton;
        private ImageView selectTagToolbarButton;
        private ImageView selectProjectToolbarButton;
        private ImageView selectBillableToolbarButton;
        private ImageView topShadow;

        private TextView durationLabel;

        private RecyclerView recyclerView;

        private AutocompleteEditText descriptionField;

        protected override void InitializeViews()
        {
            durationCard = FindViewById(DurationCard);
            doneButton = FindViewById(DoneButton);
            closeButton = FindViewById(CloseButton);
            selectTagToolbarButton = FindViewById<ImageView>(ToolbarTagButton);
            selectProjectToolbarButton = FindViewById<ImageView>(ToolbarProjectButton);
            selectBillableToolbarButton = FindViewById<ImageView>(ToolbarBillableButton);
            topShadow = FindViewById<ImageView>(TopShadow);

            durationLabel = FindViewById<TextView>(DurationText);

            recyclerView = FindViewById<RecyclerView>(SuggestionsRecyclerView);

            descriptionField = FindViewById<AutocompleteEditText>(DescriptionTextField);
        }
    }
}
