﻿namespace TraktApiSharp.Tests.Requests.Scrobbles.OAuth
{
    using FluentAssertions;
    using Traits;
    using TraktApiSharp.Requests.Base;
    using TraktApiSharp.Requests.Scrobbles.OAuth;
    using Xunit;

    [Category("Requests.Scrobbles.OAuth")]
    public class ScrobbleStopRequest_2_Tests
    {
        [Fact]
        public void Test_ScrobbleStopRequest_2_Has_AuthorizationRequirement_Required()
        {
            var request = new ScrobbleStopRequest<int, float>();
            request.AuthorizationRequirement.Should().Be(AuthorizationRequirement.Required);
        }

        [Fact]
        public void Test_ScrobbleStopRequest_2_Has_Valid_UriTemplate()
        {
            var request = new ScrobbleStopRequest<int, float>();
            request.UriTemplate.Should().Be("scrobble/stop");
        }

        [Fact]
        public void Test_ScrobbleStopRequest_2_Returns_Valid_UriPathParameters()
        {
            var request = new ScrobbleStopRequest<int, float>();
            request.GetUriPathParameters().Should().NotBeNull().And.HaveCount(0);
        }
    }
}