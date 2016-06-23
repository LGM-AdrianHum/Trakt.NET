﻿namespace TraktApiSharp.Tests.Modules
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using TraktApiSharp.Enums;
    using TraktApiSharp.Exceptions;
    using TraktApiSharp.Extensions;
    using TraktApiSharp.Modules;
    using TraktApiSharp.Objects.Basic;
    using TraktApiSharp.Objects.Get.Movies;
    using TraktApiSharp.Objects.Get.Movies.Common;
    using TraktApiSharp.Requests;
    using Utils;

    [TestClass]
    public class TraktMoviesModuleTests
    {
        [TestMethod]
        public void TestTraktMoviesModuleIsModule()
        {
            typeof(TraktBaseModule).IsAssignableFrom(typeof(TraktMoviesModule)).Should().BeTrue();
        }

        [ClassInitialize]
        public static void InitializeTests(TestContext context)
        {
            TestUtility.SetupMockHttpClient();
        }

        [ClassCleanup]
        public static void CleanupTests()
        {
            TestUtility.ResetMockHttpClient();
        }

        [TestCleanup]
        public void CleanupSingleTest()
        {
            TestUtility.ClearMockHttpClient();
        }

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region SingleMovie

        [TestMethod]
        public void TestTraktMoviesModuleGetSingleMovie()
        {
            var movie = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieSummaryFullAndImages.json");
            movie.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}", movie);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Title.Should().Be("Star Wars: The Force Awakens");
            response.Year.Should().Be(2015);
            response.Ids.Should().NotBeNull();
            response.Ids.Trakt.Should().Be(94024);
            response.Ids.Slug.Should().Be("star-wars-the-force-awakens-2015");
            response.Ids.Imdb.Should().Be("tt2488496");
            response.Ids.Tmdb.Should().Be(140607);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetSingleMovieWithExtendedOption()
        {
            var movie = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieSummaryFullAndImages.json");
            movie.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}?extended={extendedOption.ToString()}", movie);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAsync(movieId, extendedOption).Result;

            response.Should().NotBeNull();
            response.Title.Should().Be("Star Wars: The Force Awakens");
            response.Year.Should().Be(2015);
            response.Ids.Should().NotBeNull();
            response.Ids.Trakt.Should().Be(94024);
            response.Ids.Slug.Should().Be("star-wars-the-force-awakens-2015");
            response.Ids.Imdb.Should().Be("tt2488496");
            response.Ids.Tmdb.Should().Be(140607);
            response.Images.Should().NotBeNull();
            response.Images.FanArt.Full.Should().Be("https://walter.trakt.us/images/movies/000/094/024/fanarts/original/707a0ae2ab.jpg");
            response.Images.FanArt.Medium.Should().Be("https://walter.trakt.us/images/movies/000/094/024/fanarts/medium/707a0ae2ab.jpg");
            response.Images.FanArt.Thumb.Should().Be("https://walter.trakt.us/images/movies/000/094/024/fanarts/thumb/707a0ae2ab.jpg");
            response.Images.Poster.Full.Should().Be("https://walter.trakt.us/images/movies/000/094/024/posters/original/45feef2558.jpg");
            response.Images.Poster.Medium.Should().Be("https://walter.trakt.us/images/movies/000/094/024/posters/medium/45feef2558.jpg");
            response.Images.Poster.Thumb.Should().Be("https://walter.trakt.us/images/movies/000/094/024/posters/thumb/45feef2558.jpg");
            response.Images.Logo.Full.Should().Be("https://walter.trakt.us/images/movies/000/094/024/logos/original/077cc27594.png");
            response.Images.ClearArt.Full.Should().Be("https://walter.trakt.us/images/movies/000/094/024/cleararts/original/a31ab70d60.png");
            response.Images.Banner.Full.Should().Be("https://walter.trakt.us/images/movies/000/094/024/banners/original/b20b70cbf5.jpg");
            response.Images.Thumb.Full.Should().Be("https://walter.trakt.us/images/movies/000/094/024/thumbs/original/627810fb39.jpg");
            response.Tagline.Should().Be("Every generation has a story.");
            response.Overview.Should().Be("Thirty years after defeating the Galactic Empire, Han Solo and his allies face a new threat from the evil Kylo Ren and his army of Stormtroopers.");
            response.Released.Should().Be(DateTime.Parse("2015-12-18"));
            response.Runtime.Should().Be(136);
            response.UpdatedAt.Should().Be(DateTime.Parse("2016-03-31T09:01:59Z").ToUniversalTime());
            response.Trailer.Should().Be("http://youtube.com/watch?v=uwa7N0ShN2U");
            response.TrailerUri.Should().NotBeNull();
            response.Homepage.Should().Be("http://www.starwars.com/films/star-wars-episode-vii");
            response.HomepageUri.Should().NotBeNull();
            response.Rating.Should().Be(8.31988f);
            response.Votes.Should().Be(9338);
            response.LanguageCode.Should().Be("en");
            response.AvailableTranslationLanguageCodes.Should().NotBeNull().And.HaveCount(4).And.Contain("en", "de", "en", "it");
            response.Genres.Should().NotBeNull().And.HaveCount(4).And.Contain("action", "adventure", "fantasy", "science-fiction");
            response.Certification.Should().Be("PG-13");
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetSingleMovieExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktMovie>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetSingleMovieArgumentExceptions()
        {
            var movie = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieSummaryFullAndImages.json");
            movie.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}", movie);

            Func<Task<TraktMovie>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MultipleMovies

        [TestMethod]
        public void TestTraktMoviesModuleGetMultipleMoviesArgumentExceptions()
        {
            Func<Task<TraktListResult<TraktMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviesAsync(new string[] { null });
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviesAsync(new string[] { string.Empty });
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviesAsync(new string[] { });
            act.ShouldNotThrow();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviesAsync(null);
            act.ShouldNotThrow();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieAliases

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieAliases()
        {
            var movieAliases = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieAliases.json");
            movieAliases.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/aliases", movieAliases);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAliasesAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(4);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieAliasesExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/aliases";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktListResult<TraktMovieAlias>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAliasesAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieAliasesArgumentExceptions()
        {
            var movieAliases = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieAliases.json");
            movieAliases.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/aliases", movieAliases);

            Func<Task<TraktListResult<TraktMovieAlias>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAliasesAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieAliasesAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieReleases

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieReleases()
        {
            var movieReleases = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieReleases.json");
            movieReleases.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/releases", movieReleases);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieReleasesAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(3);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieReleasesExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/releases";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktListResult<TraktMovieRelease>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieReleasesAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieReleasesArgumentExceptions()
        {
            var movieReleases = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieReleases.json");
            movieReleases.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/releases", movieReleases);

            Func<Task<TraktListResult<TraktMovieRelease>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieReleasesAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieReleasesAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieSingleRelease

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieSingleRelease()
        {
            var movieRelease = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieSingleRelease.json");
            movieRelease.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var countryCode = "us";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/releases/{countryCode}", movieRelease);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleReleaseAsync(movieId, countryCode).Result;

            response.Should().NotBeNull();
            response.CountryCode.Should().Be(countryCode);
            response.Certification.Should().Be("PG-13");
            response.ReleaseDate.Should().Be(DateTime.Parse("2015-12-14"));
            response.ReleaseType.Should().Be(TraktReleaseType.Premiere);
            response.Note.Should().Be("Los Angeles, California");
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieSingleReleaseExceptions()
        {
            var movieId = "94024";
            var countryCode = "us";
            var uri = $"movies/{movieId}/releases/{countryCode}";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktMovieRelease>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleReleaseAsync(movieId, countryCode);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieSingleReleaseArgumentExceptions()
        {
            var movieRelease = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieSingleRelease.json");
            movieRelease.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var countryCode = "us";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/releases/{countryCode}", movieRelease);

            Func<Task<TraktMovieRelease>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleReleaseAsync(null, countryCode);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleReleaseAsync(string.Empty, countryCode);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleReleaseAsync(movieId, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleReleaseAsync(movieId, string.Empty);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleReleaseAsync(movieId, "u");
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleReleaseAsync(movieId, "usa");
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieTranslations

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieTranslations()
        {
            var movieTranslations = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieTranslations.json");
            movieTranslations.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/translations", movieTranslations);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieTranslationsAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(3);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieTranslationsExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/translations";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktListResult<TraktMovieTranslation>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieTranslationsAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieTranslationsArgumentExceptions()
        {
            var movieTranslations = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieTranslations.json");
            movieTranslations.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/translations", movieTranslations);

            Func<Task<TraktListResult<TraktMovieTranslation>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieTranslationsAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieTranslationsAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieSingleTranslation

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieSingleTranslation()
        {
            var movieTranslation = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieSingleTranslation.json");
            movieTranslation.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var languageCode = "en";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/translations/{languageCode}", movieTranslation);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleTranslationAsync(movieId, languageCode).Result;

            response.Should().NotBeNull();
            response.Title.Should().Be("Star Wars: Episode VII - The Force Awakens");
            response.Overview.Should().Be("A continuation of the saga created by George Lucas, set thirty years after Star Wars: Episode VI – Return of the Jedi.");
            response.Tagline.Should().Be("The Force Lives On...");
            response.LanguageCode.Should().Be(languageCode);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieSingleTranslationExceptions()
        {
            var movieId = "94024";
            var languageCode = "us";
            var uri = $"movies/{movieId}/translations/{languageCode}";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktMovieTranslation>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleTranslationAsync(movieId, languageCode);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieSingleTranslationArgumentExceptions()
        {
            var movieTranslation = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieSingleTranslation.json");
            movieTranslation.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var languageCode = "en";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/translations/{languageCode}", movieTranslation);

            Func<Task<TraktMovieTranslation>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleTranslationAsync(null, languageCode);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleTranslationAsync(string.Empty, languageCode);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleTranslationAsync(movieId, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleTranslationAsync(movieId, string.Empty);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleTranslationAsync(movieId, "u");
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieSingleTranslationAsync(movieId, "usa");
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieComments

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieComments()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments",
                                                                movieComments, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsWithSortOrder()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var sortOrder = TraktCommentSortOrder.Likes;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments/{sortOrder.AsString()}",
                                                                movieComments, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId, sortOrder).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsWithPage()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments?page={page}",
                                                                movieComments, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsWithSortOrderAndPage()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var sortOrder = TraktCommentSortOrder.Likes;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments/{sortOrder.AsString()}?page={page}",
                                                                movieComments, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId, sortOrder, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsWithLimit()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments?limit={limit}",
                                                                movieComments, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsWithSortOrderAndLimit()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var sortOrder = TraktCommentSortOrder.Likes;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments/{sortOrder.AsString()}?limit={limit}",
                                                                movieComments, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId, sortOrder, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsWithPageAndLimit()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments?page={page}&limit={limit}",
                                                                movieComments, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsComplete()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var sortOrder = TraktCommentSortOrder.Likes;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments/{sortOrder.AsString()}?page={page}&limit={limit}",
                                                                movieComments, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId, sortOrder, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/comments";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktPaginationListResult<TraktComment>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieCommentsArgumentExceptions()
        {
            var movieComments = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieComments.json");
            movieComments.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/comments", movieComments);

            Func<Task<TraktPaginationListResult<TraktComment>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieCommentsAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviePeople

        [TestMethod]
        public void TestTraktMoviesModuleGetMoviePeople()
        {
            var moviePeople = TestUtility.ReadFileContents(@"Objects\Get\Movies\MoviePeople.json");
            moviePeople.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/people", moviePeople);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviePeopleAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Cast.Should().NotBeNull().And.HaveCount(3);
            response.Crew.Should().NotBeNull();
            response.Crew.Production.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Art.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Crew.Should().NotBeNull().And.HaveCount(2);
            response.Crew.CostumeAndMakeup.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Directing.Should().NotBeNull().And.HaveCount(1);
            response.Crew.Writing.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Sound.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Camera.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Lighting.Should().NotBeNull().And.HaveCount(2);
            response.Crew.VisualEffects.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Editing.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMoviePeopleWithExtendedOption()
        {
            var moviePeople = TestUtility.ReadFileContents(@"Objects\Get\Movies\MoviePeople.json");
            moviePeople.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/people?extended={extendedOption.ToString()}", moviePeople);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviePeopleAsync(movieId, extendedOption).Result;

            response.Should().NotBeNull();
            response.Cast.Should().NotBeNull().And.HaveCount(3);
            response.Crew.Should().NotBeNull();
            response.Crew.Production.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Art.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Crew.Should().NotBeNull().And.HaveCount(2);
            response.Crew.CostumeAndMakeup.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Directing.Should().NotBeNull().And.HaveCount(1);
            response.Crew.Writing.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Sound.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Camera.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Lighting.Should().NotBeNull().And.HaveCount(2);
            response.Crew.VisualEffects.Should().NotBeNull().And.HaveCount(2);
            response.Crew.Editing.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMoviePeopleExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/people";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktMoviePeople>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviePeopleAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMoviePeopleArgumentExceptions()
        {
            var moviePeople = TestUtility.ReadFileContents(@"Objects\Get\Movies\MoviePeople.json");
            moviePeople.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/people", moviePeople);

            Func<Task<TraktMoviePeople>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviePeopleAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMoviePeopleAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieRatings

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRatings()
        {
            var movieRatings = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRatings.json");
            movieRatings.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/ratings", movieRatings);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRatingsAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Rating.Should().Be(8.31325f);
            response.Votes.Should().Be(10359);

            var distribution = new Dictionary<string, int>()
            {
                { "1",  185 }, { "2", 28 }, { "3", 34 }, { "4", 89 }, { "5", 184 },
                { "6",  630 }, { "7", 1244 }, { "8", 2509 }, { "9", 2622 }, { "10", 2834 }
            };

            response.Distribution.Should().HaveCount(10).And.Contain(distribution);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRatingsExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/ratings";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktMovieRating>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRatingsAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRatingsArgumentExceptions()
        {
            var movieRatings = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRatings.json");
            movieRatings.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/ratings", movieRatings);

            Func<Task<TraktMovieRating>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRatingsAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRatingsAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieRelatedMovies

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMovies()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related", movieRelatedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesWithExtendedOption()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related?extended={extendedOption.ToString()}",
                                                                movieRelatedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesWithPage()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related?page={page}",
                                                                movieRelatedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesWithExtendedOptionAndPage()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var page = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related?extended={extendedOption.ToString()}&page={page}",
                                                                movieRelatedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId, extendedOption, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesWithLimit()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related?limit={limit}",
                                                                movieRelatedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesWithExtendedOptionAndLimit()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related?extended={extendedOption.ToString()}&limit={limit}",
                                                                movieRelatedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId, extendedOption, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesWithPageAndLimit()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related?page={page}&limit={limit}",
                                                                movieRelatedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesComplete()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";
            var itemCount = 2;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                                                                movieRelatedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/related";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktPaginationListResult<TraktMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieRelatedMoviesArgumentExceptions()
        {
            var movieRelatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieRelatedMoviesFullAndImages.json");
            movieRelatedMovies.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/{movieId}/related", movieRelatedMovies);

            Func<Task<TraktPaginationListResult<TraktMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieRelatedMoviesAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieStatistics

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieStatistics()
        {
            var movieStatistics = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieStatistics.json");
            movieStatistics.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/stats", movieStatistics);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieStatisticsAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Watchers.Should().Be(40619);
            response.Plays.Should().Be(64620);
            response.Collectors.Should().Be(17519);
            response.CollectedEpisodes.Should().NotHaveValue();
            response.Comments.Should().Be(99);
            response.Lists.Should().Be(17089);
            response.Votes.Should().Be(10359);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieStatisticsExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/stats";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktMovieStatistics>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieStatisticsAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieStatisticsArgumentExceptions()
        {
            var movieStatistics = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieStatistics.json");
            movieStatistics.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/stats", movieStatistics);

            Func<Task<TraktMovieStatistics>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieStatisticsAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieStatisticsAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MovieWatchingUsers

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieWatchingUsers()
        {
            var movieWatchingUsers = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieWatchingUsers.json");
            movieWatchingUsers.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/watching", movieWatchingUsers);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieWatchingUsersAsync(movieId).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(3);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieWatchingUsersWithExtendedOption()
        {
            var movieWatchingUsers = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieWatchingUsers.json");
            movieWatchingUsers.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/watching?extended={extendedOption.ToString()}", movieWatchingUsers);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieWatchingUsersAsync(movieId, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(3);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieWatchingUsersExceptions()
        {
            var movieId = "94024";
            var uri = $"movies/{movieId}/watching";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktListResult<TraktMovieWatchingUser>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieWatchingUsersAsync(movieId);
            act.ShouldThrow<TraktMovieNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMovieWatchingUsersArgumentExceptions()
        {
            var movieWatchingUsers = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieWatchingUsers.json");
            movieWatchingUsers.Should().NotBeNullOrEmpty();

            var movieId = "94024";

            TestUtility.SetupMockResponseWithoutOAuth($"movies/{movieId}/watching", movieWatchingUsers);

            Func<Task<TraktListResult<TraktMovieWatchingUser>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieWatchingUsersAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMovieWatchingUsersAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviesTrending

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMovies()
        {
            var moviesTrending = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesTrending.json");
            moviesTrending.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var userCount = 300;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/trending", moviesTrending, 1, 10, 1, itemCount, userCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
            response.UserCount.Should().HaveValue().And.Be(userCount);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMoviesWithExtendedOption()
        {
            var moviesTrending = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesTrending.json");
            moviesTrending.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var userCount = 300;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/trending?extended={extendedOption.ToString()}",
                                                                moviesTrending, 1, 10, 1, itemCount, userCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync(extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
            response.UserCount.Should().HaveValue().And.Be(userCount);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMoviesWithPage()
        {
            var moviesTrending = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesTrending.json");
            moviesTrending.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var userCount = 300;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/trending?page={page}", moviesTrending, page, 10, 1, itemCount, userCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync(null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
            response.UserCount.Should().HaveValue().And.Be(userCount);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMoviesWithExtendedOptionAndPage()
        {
            var moviesTrending = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesTrending.json");
            moviesTrending.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var userCount = 300;
            var page = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/trending?extended={extendedOption.ToString()}&page={page}",
                                                                moviesTrending, page, 10, 1, itemCount, userCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync(extendedOption, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
            response.UserCount.Should().HaveValue().And.Be(userCount);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMoviesWithLimit()
        {
            var moviesTrending = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesTrending.json");
            moviesTrending.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var userCount = 300;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/trending?limit={limit}", moviesTrending, 1, limit, 1, itemCount, userCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync(null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
            response.UserCount.Should().HaveValue().And.Be(userCount);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMoviesWithExtendedOptionAndLimit()
        {
            var moviesTrending = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesTrending.json");
            moviesTrending.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var userCount = 300;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/trending?extended={extendedOption.ToString()}&limit={limit}",
                                                                moviesTrending, 1, limit, 1, itemCount, userCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync(extendedOption, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
            response.UserCount.Should().HaveValue().And.Be(userCount);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMoviesWithPageAndLimit()
        {
            var moviesTrending = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesTrending.json");
            moviesTrending.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var userCount = 300;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/trending?page={page}&limit={limit}",
                                                                moviesTrending, page, limit, 1, itemCount, userCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync(null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
            response.UserCount.Should().HaveValue().And.Be(userCount);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMoviesComplete()
        {
            var moviesTrending = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesTrending.json");
            moviesTrending.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var userCount = 300;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/trending?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                                                                moviesTrending, page, limit, 1, itemCount, userCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync(extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
            response.UserCount.Should().HaveValue().And.Be(userCount);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetTrendingMoviesExceptions()
        {
            var uri = $"movies/trending";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);

            Func<Task<TraktPaginationListResult<TraktTrendingMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetTrendingMoviesAsync();
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviesPopular

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMovies()
        {
            var popularMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesPopular.json");
            popularMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/popular", popularMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMoviesWithExtendedOption()
        {
            var popularMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesPopular.json");
            popularMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/popular?extended={extendedOption.ToString()}",
                                                                popularMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync(extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMoviesWithPage()
        {
            var popularMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesPopular.json");
            popularMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/popular?page={page}", popularMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync(null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMoviesWithExtendedOptionAndPage()
        {
            var popularMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesPopular.json");
            popularMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/popular?extended={extendedOption.ToString()}&page={page}",
                                                                popularMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync(extendedOption, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMoviesWithLimit()
        {
            var popularMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesPopular.json");
            popularMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/popular?limit={limit}", popularMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync(null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMoviesWithExtendedOptionAndLimit()
        {
            var popularMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesPopular.json");
            popularMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/popular?extended={extendedOption.ToString()}&limit={limit}",
                                                                popularMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync(extendedOption, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMoviesWithPageAndLimit()
        {
            var popularMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesPopular.json");
            popularMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/popular?page={page}&limit={limit}",
                                                                popularMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync(null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMoviesComplete()
        {
            var popularMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesPopular.json");
            popularMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/popular?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                                                                popularMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync(extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetPopularMoviesExceptions()
        {
            var uri = $"movies/popular";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);

            Func<Task<TraktPaginationListResult<TraktPopularMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetPopularMoviesAsync();
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviesMostPlayed

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMovies()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played", mostPlayedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithPeriod()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played/{period.AsString()}",
                                                                mostPlayedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(period).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithExtendedOption()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played?extended={extendedOption.ToString()}",
                                                                mostPlayedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(null, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithPage()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played?page={page}", mostPlayedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(null, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithLimit()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played?limit={limit}", mostPlayedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(null, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithPeriodAndExtendedOption()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played/{period.AsString()}?extended={extendedOption.ToString()}",
                                                                mostPlayedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(period, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithPeriodAndPage()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played/{period.AsString()}?page={page}",
                                                                mostPlayedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(period, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithPeriodAndLimit()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played/{period.AsString()}?limit={limit}",
                                                                mostPlayedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(period, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithExtendedOptionAndPage()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played?extended={extendedOption.ToString()}&page={page}",
                                                                mostPlayedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(null, extendedOption, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithExtendedOptionAndLimit()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played?extended={extendedOption.ToString()}&limit={limit}",
                                                                mostPlayedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(null, extendedOption, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithPageAndLimit()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played?page={page}&limit={limit}",
                                                                mostPlayedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(null, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithExtendedOptionAndPageAndLimit()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                                                                mostPlayedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(null, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesWithPeriodAndPageAndLimit()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/played/{period.AsString()}?page={page}&limit={limit}",
                                                                mostPlayedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(period, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesComplete()
        {
            var mostPlayedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostPlayed.json");
            mostPlayedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth(
                $"movies/played/{period.AsString()}?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                mostPlayedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync(period, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostPlayedMoviesExceptions()
        {
            var uri = $"movies/played";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);

            Func<Task<TraktPaginationListResult<TraktMostPlayedMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMostPlayedMoviesAsync();
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviesMostWatched

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMovies()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched", mostWatchedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithPeriod()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched/{period.AsString()}",
                                                                mostWatchedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(period).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithExtendedOption()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched?extended={extendedOption.ToString()}",
                                                                mostWatchedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(null, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithPage()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched?page={page}", mostWatchedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(null, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithLimit()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched?limit={limit}", mostWatchedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(null, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithPeriodAndExtendedOption()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched/{period.AsString()}?extended={extendedOption.ToString()}",
                                                                mostWatchedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(period, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithPeriodAndPage()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched/{period.AsString()}?page={page}",
                                                                mostWatchedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(period, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithPeriodAndLimit()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched/{period.AsString()}?limit={limit}",
                                                                mostWatchedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(period, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithExtendedOptionAndPage()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched?extended={extendedOption.ToString()}&page={page}",
                                                                mostWatchedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(null, extendedOption, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithExtendedOptionAndLimit()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched?extended={extendedOption.ToString()}&limit={limit}",
                                                                mostWatchedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(null, extendedOption, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithPageAndLimit()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched?page={page}&limit={limit}",
                                                                mostWatchedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(null, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithExtendedOptionAndPageAndLimit()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                                                                mostWatchedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(null, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesWithPeriodAndPageAndLimit()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/watched/{period.AsString()}?page={page}&limit={limit}",
                                                                mostWatchedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(period, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesComplete()
        {
            var mostWatchedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostWatched.json");
            mostWatchedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth(
                $"movies/watched/{period.AsString()}?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                mostWatchedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync(period, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostWatchedMoviesExceptions()
        {
            var uri = $"movies/watched";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);

            Func<Task<TraktPaginationListResult<TraktMostWatchedMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMostWatchedMoviesAsync();
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviesMostCollected

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMovies()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected", mostCollectedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithPeriod()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected/{period.AsString()}",
                                                                mostCollectedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(period).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithExtendedOption()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected?extended={extendedOption.ToString()}",
                                                                mostCollectedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(null, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithPage()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected?page={page}", mostCollectedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(null, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithLimit()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected?limit={limit}", mostCollectedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(null, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithPeriodAndExtendedOption()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected/{period.AsString()}?extended={extendedOption.ToString()}",
                                                                mostCollectedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(period, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithPeriodAndPage()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected/{period.AsString()}?page={page}",
                                                                mostCollectedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(period, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithPeriodAndLimit()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected/{period.AsString()}?limit={limit}",
                                                                mostCollectedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(period, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithExtendedOptionAndPage()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected?extended={extendedOption.ToString()}&page={page}",
                                                                mostCollectedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(null, extendedOption, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithExtendedOptionAndLimit()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected?extended={extendedOption.ToString()}&limit={limit}",
                                                                mostCollectedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(null, extendedOption, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithPageAndLimit()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected?page={page}&limit={limit}",
                                                                mostCollectedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(null, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithExtendedOptionAndPageAndLimit()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                                                                mostCollectedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(null, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesWithPeriodAndPageAndLimit()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/collected/{period.AsString()}?page={page}&limit={limit}",
                                                                mostCollectedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(period, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesComplete()
        {
            var mostCollectedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostCollected.json");
            mostCollectedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var period = TraktPeriod.Monthly;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth(
                $"movies/collected/{period.AsString()}?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                mostCollectedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync(period, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostCollectedMoviesExceptions()
        {
            var uri = $"movies/collected";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);

            Func<Task<TraktPaginationListResult<TraktMostCollectedMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMostCollectedMoviesAsync();
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviesMostAnticipated

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMovies()
        {
            var mostAnticipatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostAnticipated.json");
            mostAnticipatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/anticipated", mostAnticipatedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMoviesWithExtendedOption()
        {
            var mostAnticipatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostAnticipated.json");
            mostAnticipatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/anticipated?extended={extendedOption.ToString()}",
                                                                mostAnticipatedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync(extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMoviesWithPage()
        {
            var mostAnticipatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostAnticipated.json");
            mostAnticipatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/anticipated?page={page}", mostAnticipatedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync(null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMoviesWithExtendedOptionAndPage()
        {
            var mostAnticipatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostAnticipated.json");
            mostAnticipatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/anticipated?extended={extendedOption.ToString()}&page={page}",
                                                                mostAnticipatedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync(extendedOption, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMoviesWithLimit()
        {
            var mostAnticipatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostAnticipated.json");
            mostAnticipatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/anticipated?limit={limit}", mostAnticipatedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync(null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMoviesWithExtendedOptionAndLimit()
        {
            var mostAnticipatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostAnticipated.json");
            mostAnticipatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/anticipated?extended={extendedOption.ToString()}&limit={limit}",
                                                                mostAnticipatedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync(extendedOption, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMoviesWithPageAndLimit()
        {
            var mostAnticipatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostAnticipated.json");
            mostAnticipatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/anticipated?page={page}&limit={limit}",
                                                                mostAnticipatedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync(null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMoviesComplete()
        {
            var mostAnticipatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesMostAnticipated.json");
            mostAnticipatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/anticipated?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                                                                mostAnticipatedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync(extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetMostAnticipatedMoviesExceptions()
        {
            var uri = $"movies/anticipated";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);

            Func<Task<TraktPaginationListResult<TraktMostAnticipatedMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetMostAnticipatedMoviesAsync();
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviesBoxOffice

        [TestMethod]
        public void TestTraktMoviesModuleGetBoxOfficeMovies()
        {
            var boxOfficeMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesBoxOffice.json");
            boxOfficeMovies.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithoutOAuth($"movies/boxoffice", boxOfficeMovies);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetBoxOfficeMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetBoxOfficeMoviesWithExtendedOption()
        {
            var boxOfficeMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesBoxOffice.json");
            boxOfficeMovies.Should().NotBeNullOrEmpty();

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithoutOAuth($"movies/boxoffice?extended={extendedOption.ToString()}", boxOfficeMovies);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetBoxOfficeMoviesAsync(extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetBoxOfficeMoviesExceptions()
        {
            var uri = $"movies/boxoffice";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);

            Func<Task<TraktListResult<TraktBoxOfficeMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetBoxOfficeMoviesAsync();
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region MoviesRecentlyUpdated

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMovies()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates", recentlyUpdatedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithStartDate()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var today = DateTime.UtcNow;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates/{today.ToTraktDateString()}",
                                                                recentlyUpdatedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(today).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithExtendedOption()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates?extended={extendedOption.ToString()}",
                                                                recentlyUpdatedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(null, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithPage()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates?page={page}", recentlyUpdatedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(null, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithLimit()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates?limit={limit}", recentlyUpdatedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(null, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithStartDateAndExtendedOption()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var today = DateTime.UtcNow;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth(
                $"movies/updates/{today.ToTraktDateString()}?extended={extendedOption.ToString()}",
                recentlyUpdatedMovies, 1, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(today, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithStartDateAndPage()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var today = DateTime.UtcNow;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates/{today.ToTraktDateString()}?page={page}",
                                                                recentlyUpdatedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(today, null, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithStartDateAndLimit()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;
            var today = DateTime.UtcNow;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates/{today.ToTraktDateString()}?limit={limit}",
                                                                recentlyUpdatedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(today, null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithExtendedOptionAndPage()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates?extended={extendedOption.ToString()}&page={page}",
                                                                recentlyUpdatedMovies, page, 10, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(null, extendedOption, page).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(10);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithExtendedOptionAndLimit()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates?extended={extendedOption.ToString()}&limit={limit}",
                                                                recentlyUpdatedMovies, 1, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(null, extendedOption, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(1);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithPageAndLimit()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates?page={page}&limit={limit}",
                                                                recentlyUpdatedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(null, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithExtendedOptionAndPageAndLimit()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                                                                recentlyUpdatedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(null, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesWithStartDateAndPageAndLimit()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;
            var today = DateTime.UtcNow;

            TestUtility.SetupMockPaginationResponseWithoutOAuth($"movies/updates/{today.ToTraktDateString()}?page={page}&limit={limit}",
                                                                recentlyUpdatedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(today, null, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesComplete()
        {
            var recentlyUpdatedMovies = TestUtility.ReadFileContents(@"Objects\Get\Movies\Common\MoviesRecentlyUpdated.json");
            recentlyUpdatedMovies.Should().NotBeNullOrEmpty();

            var itemCount = 2;
            var page = 2;
            var limit = 4;
            var today = DateTime.UtcNow;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockPaginationResponseWithoutOAuth(
                $"movies/updates/{today.ToTraktDateString()}?extended={extendedOption.ToString()}&page={page}&limit={limit}",
                recentlyUpdatedMovies, page, limit, 1, itemCount);

            var response = TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync(today, extendedOption, page, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(itemCount);
            response.ItemCount.Should().HaveValue().And.Be(itemCount);
            response.Limit.Should().HaveValue().And.Be(limit);
            response.Page.Should().HaveValue().And.Be(page);
            response.PageCount.Should().HaveValue().And.Be(1);
        }

        [TestMethod]
        public void TestTraktMoviesModuleGetRecentlyUpdatedMoviesExceptions()
        {
            var uri = $"movies/updates";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);

            Func<Task<TraktPaginationListResult<TraktRecentlyUpdatedMovie>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Movies.GetRecentlyUpdatedMoviesAsync();
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion
    }
}