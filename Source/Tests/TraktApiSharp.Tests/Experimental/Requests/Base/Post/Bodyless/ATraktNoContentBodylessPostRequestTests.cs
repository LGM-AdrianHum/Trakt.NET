﻿namespace TraktApiSharp.Tests.Experimental.Requests.Base.Post.Bodyless
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base;
    using TraktApiSharp.Experimental.Requests.Base.Post.Bodyless;
    using TraktApiSharp.Experimental.Requests.Interfaces;

    [TestClass]
    public class ATraktNoContentBodylessPostRequestTests
    {
        [TestMethod]
        public void TestATraktNoContentBodylessPostRequestIsAbstract()
        {
            typeof(ATraktNoContentBodylessPostRequest).IsAbstract.Should().BeTrue();
        }

        [TestMethod]
        public void TestATraktNoContentBodylessPostRequestIsSubclassOfATraktNoContentRequest()
        {
            typeof(ATraktNoContentBodylessPostRequest).IsSubclassOf(typeof(ATraktNoContentRequest)).Should().BeTrue();
        }

        [TestMethod]
        public void TestATraktNoContentBodylessPostRequestImplementsITraktRequestInterface()
        {
            typeof(ATraktNoContentBodylessPostRequest).GetInterfaces().Should().Contain(typeof(ITraktRequest));
        }
    }
}
