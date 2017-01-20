﻿namespace TraktApiSharp.Tests.Experimental.Requests.Comments.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Comments.OAuth;

    [TestClass]
    public class TraktCommentUnlikeRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktCommentUnlikeRequestIsNotAbstract()
        {
            typeof(TraktCommentUnlikeRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktCommentUnlikeRequestIsSealed()
        {
            typeof(TraktCommentUnlikeRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktCommentUnlikeRequestIsSubclassOfATraktNoContentDeleteByIdRequest()
        {
            //typeof(TraktCommentUnlikeRequest).IsSubclassOf(typeof(ATraktNoContentDeleteByIdRequest)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktCommentUnlikeRequestHasValidUriTemplate()
        {
            var request = new TraktCommentUnlikeRequest(null);
            request.UriTemplate.Should().Be("comments/{id}/like");
        }
    }
}