using System.Threading.Tasks;
using Toggl.Core.Models.Interfaces;
using Toggl.Shared.Models;

namespace Toggl.Core.Services
{
    public enum ReportPeriod
    {
        Unknown,
        LastMonth,
        LastWeek,
        Yesterday,
        Today,
        ThisWeek,
        ThisMonth,
        ThisYear,
        LastYear
    }

    public interface IIntentDonationService
    {
        void SetDefaultShortcutSuggestions();
        void DonateStartTimeEntry(IThreadSafeTimeEntry timeEntry);
        void DonateStopCurrentTimeEntry();
        void DonateShowReport(ReportPeriod period);
        void DonateShowReport();
        void ClearAll();
    }
}
