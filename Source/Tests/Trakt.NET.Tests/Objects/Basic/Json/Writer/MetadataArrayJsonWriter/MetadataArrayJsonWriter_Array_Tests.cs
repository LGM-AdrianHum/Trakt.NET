﻿namespace TraktNet.Tests.Objects.Basic.Json.Writer
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Traits;
    using TraktNet.Enums;
    using TraktNet.Objects.Basic;
    using TraktNet.Objects.Json;
    using Xunit;

    [Category("Objects.Basic.JsonWriter")]
    public partial class MetadataArrayJsonWriter_Tests
    {
        [Fact]
        public void Test_MetadataArrayJsonWriter_WriteArray_Array_Exceptions()
        {
            ArrayJsonWriter<ITraktMetadata> traktJsonWriter = new ArrayJsonWriter<ITraktMetadata>();
            Func<Task<string>> action = () => traktJsonWriter.WriteArrayAsync(default);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Test_MetadataArrayJsonWriter_WriteArray_Array_Empty()
        {
            IEnumerable<ITraktMetadata> traktMetadata = new List<TraktMetadata>();
            var traktJsonWriter = new ArrayJsonWriter<ITraktMetadata>();
            string json = await traktJsonWriter.WriteArrayAsync(traktMetadata);
            json.Should().Be("[]");
        }

        [Fact]
        public async Task Test_MetadataArrayJsonWriter_WriteArray_Array_SingleObject()
        {
            IEnumerable<ITraktMetadata> traktMetadata = new List<ITraktMetadata>
            {
                new TraktMetadata
                {
                    MediaType = TraktMediaType.Digital,
                    MediaResolution = TraktMediaResolution.UHD_4k,
                    Audio = TraktMediaAudio.DolbyAtmos,
                    AudioChannels = TraktMediaAudioChannel.Channels_7_1,
                    ThreeDimensional = true
                }
            };

            var traktJsonWriter = new ArrayJsonWriter<ITraktMetadata>();
            string json = await traktJsonWriter.WriteArrayAsync(traktMetadata);
            json.Should().Be(@"[{""media_type"":""digital"",""resolution"":""uhd_4k"",""audio"":""dolby_atmos""," +
                             @"""audio_channels"":""7.1"",""3d"":true}]");
        }

        [Fact]
        public async Task Test_MetadataArrayJsonWriter_WriteArray_Array_Complete()
        {
            IEnumerable<ITraktMetadata> traktMetadata = new List<ITraktMetadata>
            {
                new TraktMetadata
                {
                    MediaType = TraktMediaType.Digital,
                    MediaResolution = TraktMediaResolution.UHD_4k,
                    Audio = TraktMediaAudio.DolbyAtmos,
                    AudioChannels = TraktMediaAudioChannel.Channels_7_1,
                    ThreeDimensional = true
                },
                new TraktMetadata
                {
                    MediaType = TraktMediaType.Digital,
                    MediaResolution = TraktMediaResolution.UHD_4k,
                    Audio = TraktMediaAudio.DolbyAtmos,
                    AudioChannels = TraktMediaAudioChannel.Channels_7_1,
                    ThreeDimensional = true
                }
            };

            var traktJsonWriter = new ArrayJsonWriter<ITraktMetadata>();
            string json = await traktJsonWriter.WriteArrayAsync(traktMetadata);
            json.Should().Be(@"[{""media_type"":""digital"",""resolution"":""uhd_4k"",""audio"":""dolby_atmos""," +
                             @"""audio_channels"":""7.1"",""3d"":true}," +
                             @"{""media_type"":""digital"",""resolution"":""uhd_4k"",""audio"":""dolby_atmos""," +
                             @"""audio_channels"":""7.1"",""3d"":true}]");
        }
    }
}