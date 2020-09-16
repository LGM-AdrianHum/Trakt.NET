﻿namespace TraktNet.Objects.Get.Tests.Users.Lists.Json.Reader
{
    using FluentAssertions;
    using System;
    using System.Threading.Tasks;
    using Trakt.NET.Tests.Utility;
    using Trakt.NET.Tests.Utility.Traits;
    using TraktNet.Enums;
    using TraktNet.Objects.Get.Users.Lists.Json.Reader;
    using Xunit;

    [Category("Objects.Get.Users.Lists.JsonReader")]
    public partial class ListItemObjectJsonReader_Tests
    {
        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Complete()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_COMPLETE.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Id.Should().Be(101U);
                traktListItem.Rank.Should().Be("1");
                traktListItem.ListedAt.Should().Be(DateTime.Parse("2014-09-01T09:10:11.000Z").ToUniversalTime());
                traktListItem.Type.Should().Be(TraktListItemType.Season);
                traktListItem.Season.Should().NotBeNull();
                traktListItem.Season.Number.Should().Be(1);
                traktListItem.Season.Ids.Should().NotBeNull();
                traktListItem.Season.Ids.Trakt.Should().Be(61430U);
                traktListItem.Season.Ids.Tvdb.Should().Be(279121U);
                traktListItem.Season.Ids.Tmdb.Should().Be(60523U);
                traktListItem.Season.Ids.TvRage.Should().Be(36939U);

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Incomplete_1()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_INCOMPLETE_1.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().BeNull();
                traktListItem.ListedAt.Should().Be(DateTime.Parse("2014-09-01T09:10:11.000Z").ToUniversalTime());
                traktListItem.Type.Should().Be(TraktListItemType.Season);
                traktListItem.Season.Should().NotBeNull();
                traktListItem.Season.Number.Should().Be(1);
                traktListItem.Season.Ids.Should().NotBeNull();
                traktListItem.Season.Ids.Trakt.Should().Be(61430U);
                traktListItem.Season.Ids.Tvdb.Should().Be(279121U);
                traktListItem.Season.Ids.Tmdb.Should().Be(60523U);
                traktListItem.Season.Ids.TvRage.Should().Be(36939U);

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Incomplete_2()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_INCOMPLETE_2.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().Be("1");
                traktListItem.ListedAt.Should().BeNull();
                traktListItem.Type.Should().Be(TraktListItemType.Season);
                traktListItem.Season.Should().NotBeNull();
                traktListItem.Season.Number.Should().Be(1);
                traktListItem.Season.Ids.Should().NotBeNull();
                traktListItem.Season.Ids.Trakt.Should().Be(61430U);
                traktListItem.Season.Ids.Tvdb.Should().Be(279121U);
                traktListItem.Season.Ids.Tmdb.Should().Be(60523U);
                traktListItem.Season.Ids.TvRage.Should().Be(36939U);

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Incomplete_3()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_INCOMPLETE_3.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().Be("1");
                traktListItem.ListedAt.Should().Be(DateTime.Parse("2014-09-01T09:10:11.000Z").ToUniversalTime());
                traktListItem.Type.Should().BeNull();
                traktListItem.Season.Should().NotBeNull();
                traktListItem.Season.Number.Should().Be(1);
                traktListItem.Season.Ids.Should().NotBeNull();
                traktListItem.Season.Ids.Trakt.Should().Be(61430U);
                traktListItem.Season.Ids.Tvdb.Should().Be(279121U);
                traktListItem.Season.Ids.Tmdb.Should().Be(60523U);
                traktListItem.Season.Ids.TvRage.Should().Be(36939U);

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Incomplete_4()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_INCOMPLETE_4.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().Be("1");
                traktListItem.ListedAt.Should().Be(DateTime.Parse("2014-09-01T09:10:11.000Z").ToUniversalTime());
                traktListItem.Type.Should().Be(TraktListItemType.Season);
                traktListItem.Season.Should().BeNull();

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Incomplete_5()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_INCOMPLETE_5.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().Be("1");
                traktListItem.ListedAt.Should().BeNull();
                traktListItem.Type.Should().BeNull();
                traktListItem.Season.Should().BeNull();

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Incomplete_6()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_INCOMPLETE_6.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().BeNull();
                traktListItem.ListedAt.Should().Be(DateTime.Parse("2014-09-01T09:10:11.000Z").ToUniversalTime());
                traktListItem.Type.Should().BeNull();
                traktListItem.Season.Should().BeNull();

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Incomplete_7()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_INCOMPLETE_7.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().BeNull();
                traktListItem.ListedAt.Should().BeNull();
                traktListItem.Type.Should().Be(TraktListItemType.Season);
                traktListItem.Season.Should().BeNull();

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Incomplete_8()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_INCOMPLETE_8.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().BeNull();
                traktListItem.ListedAt.Should().BeNull();
                traktListItem.Type.Should().BeNull();
                traktListItem.Season.Should().NotBeNull();
                traktListItem.Season.Number.Should().Be(1);
                traktListItem.Season.Ids.Should().NotBeNull();
                traktListItem.Season.Ids.Trakt.Should().Be(61430U);
                traktListItem.Season.Ids.Tvdb.Should().Be(279121U);
                traktListItem.Season.Ids.Tmdb.Should().Be(60523U);
                traktListItem.Season.Ids.TvRage.Should().Be(36939U);

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Not_Valid_1()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_NOT_VALID_1.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().BeNull();
                traktListItem.ListedAt.Should().Be(DateTime.Parse("2014-09-01T09:10:11.000Z").ToUniversalTime());
                traktListItem.Type.Should().Be(TraktListItemType.Season);
                traktListItem.Season.Should().NotBeNull();
                traktListItem.Season.Number.Should().Be(1);
                traktListItem.Season.Ids.Should().NotBeNull();
                traktListItem.Season.Ids.Trakt.Should().Be(61430U);
                traktListItem.Season.Ids.Tvdb.Should().Be(279121U);
                traktListItem.Season.Ids.Tmdb.Should().Be(60523U);
                traktListItem.Season.Ids.TvRage.Should().Be(36939U);

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Not_Valid_2()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_NOT_VALID_2.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().Be("1");
                traktListItem.ListedAt.Should().BeNull();
                traktListItem.Type.Should().Be(TraktListItemType.Season);
                traktListItem.Season.Should().NotBeNull();
                traktListItem.Season.Number.Should().Be(1);
                traktListItem.Season.Ids.Should().NotBeNull();
                traktListItem.Season.Ids.Trakt.Should().Be(61430U);
                traktListItem.Season.Ids.Tvdb.Should().Be(279121U);
                traktListItem.Season.Ids.Tmdb.Should().Be(60523U);
                traktListItem.Season.Ids.TvRage.Should().Be(36939U);

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Not_Valid_3()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_NOT_VALID_3.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().Be("1");
                traktListItem.ListedAt.Should().Be(DateTime.Parse("2014-09-01T09:10:11.000Z").ToUniversalTime());
                traktListItem.Type.Should().BeNull();
                traktListItem.Season.Should().NotBeNull();
                traktListItem.Season.Number.Should().Be(1);
                traktListItem.Season.Ids.Should().NotBeNull();
                traktListItem.Season.Ids.Trakt.Should().Be(61430U);
                traktListItem.Season.Ids.Tvdb.Should().Be(279121U);
                traktListItem.Season.Ids.Tmdb.Should().Be(60523U);
                traktListItem.Season.Ids.TvRage.Should().Be(36939U);

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Not_Valid_4()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_NOT_VALID_4.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().Be("1");
                traktListItem.ListedAt.Should().Be(DateTime.Parse("2014-09-01T09:10:11.000Z").ToUniversalTime());
                traktListItem.Type.Should().Be(TraktListItemType.Season);
                traktListItem.Season.Should().BeNull();

                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_ListItemObjectJsonReader_Season_ReadObject_From_Stream_Not_Valid_5()
        {
            var jsonReader = new ListItemObjectJsonReader();

            using (var stream = TYPE_SEASON_JSON_NOT_VALID_5.ToStream())
            {
                var traktListItem = await jsonReader.ReadObjectAsync(stream);

                traktListItem.Should().NotBeNull();
                traktListItem.Rank.Should().BeNull();
                traktListItem.ListedAt.Should().BeNull();
                traktListItem.Type.Should().BeNull();
                traktListItem.Season.Should().BeNull();
                traktListItem.Movie.Should().BeNull();
                traktListItem.Show.Should().BeNull();
                traktListItem.Episode.Should().BeNull();
                traktListItem.Person.Should().BeNull();
            }
        }
    }
}
