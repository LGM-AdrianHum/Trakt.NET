﻿namespace TraktNet.Objects.Post.Tests.Users.Json.Reader
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Trakt.NET.Tests.Utility.Traits;
    using TraktNet.Objects.Post.Users;
    using TraktNet.Objects.Post.Users.Json.Reader;
    using Xunit;

    [Category("Objects.Post.Users.JsonReader")]
    public partial class UserCustomListsReorderPostObjectJsonReader_Tests
    {
        [Fact]
        public async Task Test_UserCustomListsReorderPostObjectJsonReader_ReadObject_From_JsonReader_Complete()
        {
            var traktJsonReader = new UserCustomListsReorderPostObjectJsonReader();

            using (var reader = new StringReader(JSON_COMPLETE))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktUserCustomListsReorderPost = await traktJsonReader.ReadObjectAsync(jsonReader);

                traktUserCustomListsReorderPost.Should().NotBeNull();
                traktUserCustomListsReorderPost.Rank.Should().NotBeNull().And.HaveCount(7);
                traktUserCustomListsReorderPost.Rank.Should().BeEquivalentTo(new List<uint> { 823, 224, 88768, 356456, 245, 2, 890 });
            }
        }

        [Fact]
        public async Task Test_UserCustomListsReorderPostObjectJsonReader_ReadObject_From_JsonReader_Not_Valid()
        {
            var traktJsonReader = new UserCustomListsReorderPostObjectJsonReader();

            using (var reader = new StringReader(JSON_NOT_VALID))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktUserCustomListsReorderPost = await traktJsonReader.ReadObjectAsync(jsonReader);

                traktUserCustomListsReorderPost.Should().NotBeNull();
                traktUserCustomListsReorderPost.Rank.Should().BeNull();
            }
        }

        [Fact]
        public void Test_UserCustomListsReorderPostObjectJsonReader_ReadObject_From_JsonReader_Null()
        {
            var traktJsonReader = new UserCustomListsReorderPostObjectJsonReader();
            Func<Task<ITraktUserCustomListsReorderPost>> traktUserCustomListsReorderPost = () => traktJsonReader.ReadObjectAsync(default(JsonTextReader));
            traktUserCustomListsReorderPost.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Test_UserCustomListsReorderPostObjectJsonReader_ReadObject_From_JsonReader_Empty()
        {
            var traktJsonReader = new UserCustomListsReorderPostObjectJsonReader();

            using (var reader = new StringReader(string.Empty))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var traktUserCustomListsReorderPost = await traktJsonReader.ReadObjectAsync(jsonReader);
                traktUserCustomListsReorderPost.Should().BeNull();
            }
        }
    }
}
