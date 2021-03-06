﻿namespace TraktNet.Objects.Post.Tests.Users.Json.Writer
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Trakt.NET.Tests.Utility.Traits;
    using TraktNet.Objects.Json;
    using TraktNet.Objects.Post.Users;
    using Xunit;

    [Category("Objects.Post.Users.JsonWriter")]
    public partial class UserCustomListsReorderPostArrayJsonWriter_Tests
    {
        [Fact]
        public void Test_UserCustomListsReorderPostArrayJsonWriter_WriteArray_StringWriter_Exceptions()
        {
            var traktJsonWriter = new ArrayJsonWriter<ITraktUserCustomListsReorderPost>();
            IEnumerable<ITraktUserCustomListsReorderPost> traktUserCustomListsReorderPosts = new List<TraktUserCustomListsReorderPost>();
            Func<Task<string>> action = () => traktJsonWriter.WriteArrayAsync(default(StringWriter), traktUserCustomListsReorderPosts);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Test_UserCustomListsReorderPostArrayJsonWriter_WriteArray_StringWriter_Empty()
        {
            IEnumerable<ITraktUserCustomListsReorderPost> traktUserCustomListsReorderPosts = new List<TraktUserCustomListsReorderPost>();

            using (var stringWriter = new StringWriter())
            {
                var traktJsonWriter = new ArrayJsonWriter<ITraktUserCustomListsReorderPost>();
                string json = await traktJsonWriter.WriteArrayAsync(stringWriter, traktUserCustomListsReorderPosts);
                json.Should().Be("[]");
            }
        }

        [Fact]
        public async Task Test_UserCustomListsReorderPostArrayJsonWriter_WriteArray_StringWriter_SingleObject()
        {
            IEnumerable<ITraktUserCustomListsReorderPost> traktUserCustomListsReorderPosts = new List<ITraktUserCustomListsReorderPost>
            {
                new TraktUserCustomListsReorderPost
                {
                    Rank = new List<uint> { 823, 224, 88768, 356456, 245, 2, 890 }
                }
            };

            using (var stringWriter = new StringWriter())
            {
                var traktJsonWriter = new ArrayJsonWriter<ITraktUserCustomListsReorderPost>();
                string json = await traktJsonWriter.WriteArrayAsync(stringWriter, traktUserCustomListsReorderPosts);
                json.Should().Be(@"[{""rank"":[823,224,88768,356456,245,2,890]}]");
            }
        }

        [Fact]
        public async Task Test_UserCustomListsReorderPostArrayJsonWriter_WriteArray_StringWriter_Complete()
        {
            IEnumerable<ITraktUserCustomListsReorderPost> traktUserCustomListsReorderPosts = new List<ITraktUserCustomListsReorderPost>
            {
                new TraktUserCustomListsReorderPost
                {
                    Rank = new List<uint> { 823, 224, 88768, 356456, 245, 2, 890 }
                },
                new TraktUserCustomListsReorderPost
                {
                    Rank = new List<uint> { 823, 224, 88768, 356456, 245, 2, 890 }
                }
            };

            using (var stringWriter = new StringWriter())
            {
                var traktJsonWriter = new ArrayJsonWriter<ITraktUserCustomListsReorderPost>();
                string json = await traktJsonWriter.WriteArrayAsync(stringWriter, traktUserCustomListsReorderPosts);
                json.Should().Be(@"[{""rank"":[823,224,88768,356456,245,2,890]}," +
                                 @"{""rank"":[823,224,88768,356456,245,2,890]}]");
            }
        }
    }
}
