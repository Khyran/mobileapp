using System;
using System.Linq;
using System.Collections.Generic;
using System.Reactive.Linq;
using Foundation;
using SiriExtension.Models;
using Toggl.iOS.ExtensionKit;
using Toggl.iOS.ExtensionKit.Analytics;
using Toggl.iOS.ExtensionKit.Extensions;
using Toggl.iOS.Intents;
using Toggl.Networking;

namespace SiriExtension
{
    public class StartTimerIntentHandler : StartTimerIntentHandling
    {
        private ITogglApi togglAPI;
        private const string startTimerActivityType = "StartTimer";

        public StartTimerIntentHandler(ITogglApi togglAPI)
        {
            this.togglAPI = togglAPI;
        }

        public override void ConfirmStartTimer(StartTimerIntent intent, Action<StartTimerIntentResponse> completion)
        {
            if (togglAPI == null)
            {
                var userActivity = new NSUserActivity(startTimerActivityType);
                userActivity.SetResponseText("Log in to use this shortcut.");
                completion(new StartTimerIntentResponse(StartTimerIntentResponseCode.FailureNoApiToken, userActivity));
                return;
            }

            var lastUpdated = SharedStorage.instance.GetLastUpdateDate();
            togglAPI.TimeEntries.GetAllSince(lastUpdated)
                .Subscribe(tes =>
                    {
                        // If there are no changes since last sync, or there are changes in the server but not in the app, we are ok
                        if (tes.Count == 0 || tes.OrderBy(te => te.At).Last().At >= lastUpdated)
                        {
                            completion(new StartTimerIntentResponse(StartTimerIntentResponseCode.Ready, null));
                        }
                        else
                        {
                            var userActivity = new NSUserActivity(startTimerActivityType);
                            userActivity.SetResponseText("Open the app to sync your data, then try again.");
                            completion(new StartTimerIntentResponse(StartTimerIntentResponseCode.FailureSyncConflict, userActivity));
                        }
                    }
                );
        }

        public override void HandleStartTimer(StartTimerIntent intent, Action<StartTimerIntentResponse> completion)
        {
            var timeEntry = createTimeEntry(intent);
            togglAPI.TimeEntries.Create(timeEntry).Subscribe(te =>
            {
                SharedStorage.instance.SetNeedsSync(true);
                SharedStorage.instance.AddSiriTrackingEvent(SiriTrackingEvent.StartTimer(te));

                var response = new StartTimerIntentResponse(StartTimerIntentResponseCode.Success, null);
                completion(response);
            }, exception =>
            {
                SharedStorage.instance.AddSiriTrackingEvent(SiriTrackingEvent.Error(exception.Message));
                var userActivity = new NSUserActivity(startTimerActivityType);
                userActivity.SetResponseText("Something went wrong, please try again.");
                completion(new StartTimerIntentResponse(StartTimerIntentResponseCode.Failure, userActivity));
            });
        }

        private TimeEntry createTimeEntry(StartTimerIntent intent)
        {
            var workspaceId = intent.Workspace == null ? SharedStorage.instance.GetDefaultWorkspaceId() : (long)Convert.ToDouble(intent.Workspace.Identifier);                                                            

            if (string.IsNullOrEmpty(intent.EntryDescription))
            {
                return new TimeEntry(workspaceId, null, null, false, DateTimeOffset.Now, null, "", new long[0], (long)SharedStorage.instance.GetUserId(), 0, null, DateTimeOffset.Now);
            }

            return new TimeEntry(
                workspaceId,
                stringToLong(intent.ProjectId?.Identifier),
                null,
                intent.Billable == null ? false : intent.Billable.Identifier == "True",
                DateTimeOffset.Now,
                null,
                intent.EntryDescription,
                intent.Tags == null ? new long[0] : stringToLongCollection(intent.Tags.Select(tag => tag.Identifier)),
                (long)SharedStorage.instance.GetUserId(),
                0,
                null,
                DateTimeOffset.Now
            );
        }

        private long? stringToLong(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            return (long)Convert.ToDouble(str);
        }

        private IEnumerable<long> stringToLongCollection(IEnumerable<string> strings)
        {
            if (strings.Count() == 0)
                return new long[0];

            return strings.Select(stringToLong).Cast<long>();
        }
    }
}
