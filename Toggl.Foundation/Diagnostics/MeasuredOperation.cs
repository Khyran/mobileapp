﻿namespace Toggl.Foundation.Diagnostics
{
    public enum MeasuredOperation
    {
        None,
        OpenStartView,
        StartTimeEntrySuggestionsLoadingTime,
        StartTimeEntrySuggestionsRenderingTime,
        OpenSelectTagsView,
        OpenSettingsView,
        EditTimeEntryFromCalendar,
        EditTimeEntryFromMainLog,
        UpdateTimeEntriesGroup,
        OpenReportsViewForTheFirstTime,
        OpenCreateProjectViewFromStartTimeEntryView,
        OpenSelectProjectFromEditView,
        OpenReportsFromGiskard,
        CreateMainLogItemViewHolder,
        CreateMainLogSectionViewHolder,
        BindMainLogItemVH,
        BindMainLogSectionVH,
        BindMainLogSuggestionsVH,
        CreateMainLogSuggestionsViewHolder,
        MainActivityOnCreate,
        BackgroundSync,
        Sync
    }
}
