using System;

namespace Toggl.Core.Services
{
    public interface IPrivateSharedStorageService
    {
        void SaveApiToken(string apiToken);

        void SaveUserId(long userId);

        void SaveLastUpdateDate(DateTimeOffset date);

        void SaveDefaultWorkspaceId(long workspaceId);

        void ClearAll();
    }
}
