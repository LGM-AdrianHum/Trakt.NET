﻿namespace TraktApiSharp.Tests.Objects.Get.Syncs.Playback.JsonReader
{
    using FluentAssertions;
    using System.IO;
    using System.Threading.Tasks;
    using TestUtils;
    using Traits;
    using TraktApiSharp.Objects.Get.Syncs.Playback.JsonReader;
    using Xunit;

    [Category("Objects.Get.Syncs.Playback.JsonReader")]
    public partial class TraktSyncPlaybackProgressItemObjectJsonReader_Tests
    {
        [Fact]
        public async Task Test_TraktSyncPlaybackProgressItemObjectJsonReader_ReadObject_From_Stream_Null()
        {
            var jsonReader = new TraktSyncPlaybackProgressItemObjectJsonReader();

            var traktPlaybackProgressItem = await jsonReader.ReadObjectAsync(default(Stream));
            traktPlaybackProgressItem.Should().BeNull();
        }

        [Fact]
        public async Task Test_TraktSyncPlaybackProgressItemObjectJsonReader_ReadObject_From_Stream_Empty()
        {
            var jsonReader = new TraktSyncPlaybackProgressItemObjectJsonReader();

            using (var stream = string.Empty.ToStream())
            {
                var traktPlaybackProgressItem = await jsonReader.ReadObjectAsync(stream);
                traktPlaybackProgressItem.Should().BeNull();
            }
        }
    }
}