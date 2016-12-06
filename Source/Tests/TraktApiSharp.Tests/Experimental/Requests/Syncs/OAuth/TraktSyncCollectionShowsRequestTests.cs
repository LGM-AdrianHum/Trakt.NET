﻿namespace TraktApiSharp.Tests.Experimental.Requests.Syncs.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Interfaces;
    using TraktApiSharp.Experimental.Requests.Syncs.OAuth;
    using TraktApiSharp.Objects.Get.Collection;

    [TestClass]
    public class TraktSyncCollectionShowsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Syncs")]
        public void TestTraktSyncCollectionShowsRequestIsNotAbstract()
        {
            typeof(TraktSyncCollectionShowsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Syncs")]
        public void TestTraktSyncCollectionShowsRequestIsSealed()
        {
            typeof(TraktSyncCollectionShowsRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Syncs")]
        public void TestTraktSyncCollectionShowsRequestIsSubclassOfATraktSyncListRequest()
        {
            typeof(TraktSyncCollectionShowsRequest).IsSubclassOf(typeof(ATraktSyncListRequest<TraktCollectionShow>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Syncs")]
        public void TestTraktSyncCollectionShowsRequestImplementsITraktExtendedInfoInterface()
        {
            typeof(TraktSyncCollectionShowsRequest).GetInterfaces().Should().Contain(typeof(ITraktExtendedInfo));
        }
    }
}
