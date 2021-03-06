﻿using System;
using Toggl.Core.DTOs;
using Toggl.Core.Models.Interfaces;
using Toggl.Shared;

namespace Toggl.Core.Interactors
{
    public sealed partial class InteractorFactory : IInteractorFactory
    {
        public IInteractor<IObservable<byte[]>> GetUserAvatar(string url)
            => new GetUserAvatarInteractor(url);

        public IInteractor<IObservable<IThreadSafeUser>> GetCurrentUser()
           => new GetCurrentUserInteractor(dataSource.User);

        public IInteractor<IObservable<IThreadSafeUser>> UpdateUser(EditUserDTO dto)
            => new UpdateUserInteractor(timeService, dataSource.User, dto);

        public IInteractor<IObservable<IThreadSafeUser>> UpdateDefaultWorkspace(long selectedWorkspaceId)
            => new UpdateWorkspaceInteractor(dataSource.User, selectedWorkspaceId);
    }
}
