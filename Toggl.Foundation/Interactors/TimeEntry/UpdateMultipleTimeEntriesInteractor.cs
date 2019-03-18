﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Toggl.Foundation.DataSources;
using Toggl.Foundation.DTOs;
using Toggl.Foundation.Models.Interfaces;
using Toggl.Multivac;
using Toggl.Multivac.Extensions;

namespace Toggl.Foundation.Interactors
{
    internal class UpdateMultipleTimeEntriesInteractor : IInteractor<IObservable<IEnumerable<IThreadSafeTimeEntry>>>
    {
        private readonly IInteractorFactory interactorFactory;
        private readonly EditTimeEntryDto[] timeEntriesDtos;

        public UpdateMultipleTimeEntriesInteractor(
            IInteractorFactory interactorFactory,
            EditTimeEntryDto[] timeEntriesDtos)
        {
            Ensure.Argument.IsNotNull(interactorFactory, nameof(interactorFactory));
            Ensure.Argument.IsNotNull(timeEntriesDtos, nameof(timeEntriesDtos));

            this.interactorFactory = interactorFactory;
            this.timeEntriesDtos = timeEntriesDtos;
        }

        public IObservable<IEnumerable<IThreadSafeTimeEntry>> Execute()
        {
            return timeEntriesDtos
                .ToObservable()
                .SelectMany(dto => interactorFactory.UpdateTimeEntry(dto).Execute())
                .ToList();
        }
    }
}