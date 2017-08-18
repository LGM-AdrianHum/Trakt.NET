﻿namespace TraktApiSharp.Tests.Objects.Get.People.Credits.JsonReader
{
    using FluentAssertions;
    using Traits;
    using TraktApiSharp.Objects.Get.People.Credits;
    using TraktApiSharp.Objects.Get.People.Credits.JsonReader;
    using TraktApiSharp.Objects.JsonReader;
    using Xunit;

    [Category("Objects.Get.People.Credits.JsonReader")]
    public partial class TraktPersonMovieCreditsCastItemArrayJsonReader_Tests
    {
        [Fact]
        public void Test_TraktPersonMovieCreditsCastItemArrayJsonReader_Implements_ITraktArrayJsonReader_Interface()
        {
            typeof(TraktPersonMovieCreditsCastItemArrayJsonReader).GetInterfaces().Should().Contain(typeof(IArrayJsonReader<ITraktPersonMovieCreditsCastItem>));
        }
    }
}
