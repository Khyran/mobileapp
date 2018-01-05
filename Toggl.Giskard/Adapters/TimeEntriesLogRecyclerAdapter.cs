﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Toggl.Foundation.MvvmCross.Collections;

namespace Toggl.Giskard.Adapters
{
    public sealed class TimeEntriesLogRecyclerAdapter : MvxRecyclerAdapter
    {
        private readonly object headerListLock = new object();
        private readonly List<int> headerIndexes = new List<int>();

        public TimeEntriesLogRecyclerAdapter()
        {
        }

        public TimeEntriesLogRecyclerAdapter(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        protected override void SetItemsSource(IEnumerable value)
        {
            base.SetItemsSource(value);

            calculateHeaderIndexes();
        }

        protected override void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsSourceCollectionChanged(sender, e);

            calculateHeaderIndexes();
        }

        private void calculateHeaderIndexes()
        {
            lock (headerListLock)
            {
                headerIndexes.Clear();

                var timeEntryGroups = getTimeEntryGroups();
                if (timeEntryGroups == null) return;

                var index = 0;
                foreach (var timeEntryGroup in timeEntryGroups)
                {
                    headerIndexes.Add(index);
                    index += timeEntryGroup.Count + 1;
                }
            }
        }

        public override object GetItem(int viewPosition)
        {
            try
            {
                var timeEntryGroups = ItemsSource as MvxObservableCollection<TimeEntryViewModelCollection>;
                if (timeEntryGroups == null)
                    return null;

                var groupIndex = headerIndexes.IndexOf(viewPosition);
                if (groupIndex >= 0)
                    return timeEntryGroups[groupIndex];

                var currentGroupIndex = headerIndexes.FindLastIndex(index => index < viewPosition);
                var offset = headerIndexes[currentGroupIndex] + 1;
                var indexInGroup = viewPosition - offset;
                return timeEntryGroups[currentGroupIndex][indexInGroup];
            }
            catch
            {
                calculateHeaderIndexes();
                return GetItem(viewPosition);
            }
        }

        public override int ItemCount
        {
            get
            {
                var timeEntryGroups = getTimeEntryGroups();
                if (timeEntryGroups == null) return 0;

                var itemCount = timeEntryGroups.Aggregate(timeEntryGroups.Count, (acc, g) => acc + g.Count);
                return itemCount;
            }
        }

        private MvxObservableCollection<TimeEntryViewModelCollection> getTimeEntryGroups()
            => ItemsSource as MvxObservableCollection<TimeEntryViewModelCollection>;
    }
}
