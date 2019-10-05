﻿namespace TraktNet.Objects.Authentication.Tests.Json.Writer
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Trakt.NET.Tests.Utility.Traits;
    using TraktNet.Objects.Authentication;
    using TraktNet.Objects.Json;
    using Xunit;

    [Category("Objects.Authentication.JsonWriter")]
    public partial class DeviceArrayJsonWriter_Tests
    {
        [Fact]
        public void Test_DeviceArrayJsonWriter_WriteArray_StringWriter_Exceptions()
        {
            var traktJsonWriter = new ArrayJsonWriter<ITraktDevice>();
            IEnumerable<ITraktDevice> traktDevices = new List<TraktDevice>();
            Func<Task<string>> action = () => traktJsonWriter.WriteArrayAsync(default(StringWriter), traktDevices);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Test_DeviceArrayJsonWriter_WriteArray_StringWriter_Empty()
        {
            IEnumerable<ITraktDevice> traktDevices = new List<TraktDevice>();

            using (var stringWriter = new StringWriter())
            {
                var traktJsonWriter = new ArrayJsonWriter<ITraktDevice>();
                string json = await traktJsonWriter.WriteArrayAsync(stringWriter, traktDevices);
                json.Should().Be("[]");
            }
        }

        [Fact]
        public async Task Test_DeviceArrayJsonWriter_WriteArray_StringWriter_SingleObject()
        {
            IEnumerable<ITraktDevice> traktDevices = new List<ITraktDevice>
            {
                new TraktDevice
                {
                    DeviceCode = "mockDeviceCode1",
                    UserCode = "mockUserCode1",
                    VerificationUrl = "mockUrl1",
                    ExpiresInSeconds = 7200,
                    IntervalInSeconds = 600
                }
            };

            using (var stringWriter = new StringWriter())
            {
                var traktJsonWriter = new ArrayJsonWriter<ITraktDevice>();
                string json = await traktJsonWriter.WriteArrayAsync(stringWriter, traktDevices);
                json.Should().Be(@"[{""device_code"":""mockDeviceCode1"",""user_code"":""mockUserCode1""," +
                                 @"""verification_url"":""mockUrl1"",""expires_in"":7200,""interval"":600}]");
            }
        }

        [Fact]
        public async Task Test_DeviceArrayJsonWriter_WriteArray_StringWriter_Complete()
        {
            IEnumerable<ITraktDevice> traktDevices = new List<ITraktDevice>
            {
                new TraktDevice
                {
                    DeviceCode = "mockDeviceCode1",
                    UserCode = "mockUserCode1",
                    VerificationUrl = "mockUrl1",
                    ExpiresInSeconds = 7200,
                    IntervalInSeconds = 600
                },
                new TraktDevice
                {
                    DeviceCode = "mockDeviceCode2",
                    UserCode = "mockUserCode2",
                    VerificationUrl = "mockUrl2",
                    ExpiresInSeconds = 7200,
                    IntervalInSeconds = 600
                },
                new TraktDevice
                {
                    DeviceCode = "mockDeviceCode3",
                    UserCode = "mockUserCode3",
                    VerificationUrl = "mockUrl3",
                    ExpiresInSeconds = 7200,
                    IntervalInSeconds = 600
                }
            };

            using (var stringWriter = new StringWriter())
            {
                var traktJsonWriter = new ArrayJsonWriter<ITraktDevice>();
                string json = await traktJsonWriter.WriteArrayAsync(stringWriter, traktDevices);
                json.Should().Be(@"[{""device_code"":""mockDeviceCode1"",""user_code"":""mockUserCode1""," +
                                 @"""verification_url"":""mockUrl1"",""expires_in"":7200,""interval"":600}," +
                                 @"{""device_code"":""mockDeviceCode2"",""user_code"":""mockUserCode2""," +
                                 @"""verification_url"":""mockUrl2"",""expires_in"":7200,""interval"":600}," +
                                 @"{""device_code"":""mockDeviceCode3"",""user_code"":""mockUserCode3""," +
                                 @"""verification_url"":""mockUrl3"",""expires_in"":7200,""interval"":600}]");
            }
        }
    }
}