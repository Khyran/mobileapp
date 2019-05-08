using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Toggl.Core.DataSources;
using Toggl.Core.Extensions;
using Toggl.Core.Models.Interfaces;
using Toggl.Shared;

namespace Toggl.Core.Interactors
{
    public class ObserveDefaultWorkspaceIdInteractor : IInteractor<IObservable<long?>>
    {
        private readonly ITogglDataSource dataSource;

        public ObserveDefaultWorkspaceIdInteractor(ITogglDataSource dataSource)
        {
            Ensure.Argument.IsNotNull(dataSource, nameof(dataSource));

            this.dataSource = dataSource;
        }

        public IObservable<long?> Execute()
            => dataSource.User.Current
                .Select(user => user.DefaultWorkspaceId)
                .DistinctUntilChanged();
    }
}
