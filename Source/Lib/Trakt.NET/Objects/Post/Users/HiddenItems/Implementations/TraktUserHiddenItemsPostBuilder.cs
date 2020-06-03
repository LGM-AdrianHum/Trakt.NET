﻿namespace TraktNet.Objects.Post.Users.HiddenItems
{
    using Get.Movies;
    using Get.Seasons;
    using Get.Shows;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This is a helper class to build a <see cref="ITraktUserHiddenItemsPost" />.
    /// <para>
    /// It is recommended to use this class to build an user hidden items post.<para /> 
    /// An instance of this class can be obtained with <see cref="TraktUserHiddenItemsPost.Builder()" />.
    /// </para>
    /// </summary>
    public class TraktUserHiddenItemsPostBuilder
    {
        private readonly ITraktUserHiddenItemsPost _hiddenItemsPost;

        /// <summary>Initializes a new instance of the <see cref="TraktUserHiddenItemsPostBuilder" /> class.</summary>
        public TraktUserHiddenItemsPostBuilder()
        {
            _hiddenItemsPost = new TraktUserHiddenItemsPost();
        }

        /// <summary>Adds a <see cref="ITraktMovie" />, which will be added to the user hidden items post.</summary>
        /// <param name="movie">The Trakt movie, which will be added.</param>
        /// <returns>The current <see cref="TraktUserHiddenItemsPostBuilder" /> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown, if the given movie is null.
        /// Thrown, if the given movie ids are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown, if the given movie has no valid ids set.
        /// Thrown, if the given movie has an year set, which has more or less than four digits.
        /// </exception>
        public TraktUserHiddenItemsPostBuilder AddMovie(ITraktMovie movie)
        {
            ValidateMovie(movie);
            EnsureMoviesListExists();

            var existingMovie = _hiddenItemsPost.Movies.FirstOrDefault(m => m.Ids == movie.Ids);

            if (existingMovie != null)
                return this;

            (_hiddenItemsPost.Movies as List<TraktUserHiddenItemsPostMovie>)?.Add(
                new TraktUserHiddenItemsPostMovie
                {
                    Title = movie.Title,
                    Year = movie.Year,
                    Ids = movie.Ids
                });

            return this;
        }

        /// <summary>Adds a collection of <see cref="ITraktMovie" />s, which will be added to the hidden items post.</summary>
        /// <param name="movies">A collection of Trakt movies, which will be added.</param>
        /// <returns>The current <see cref="TraktUserHiddenItemsPostBuilder" /> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown, if the given movies collection is null.
        /// Thrown, if one of the given movies is null.
        /// Thrown, if one of the given movies' ids are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown, if one of the given movies has no valid ids set.
        /// Thrown, if one of the given movies has an year set, which has more or less than four digits.
        /// </exception>
        public TraktUserHiddenItemsPostBuilder AddMovies(IEnumerable<ITraktMovie> movies)
        {
            if (movies == null)
                throw new ArgumentNullException(nameof(movies));

            if (!movies.Any())
                return this;

            foreach (var movie in movies)
                AddMovie(movie);

            return this;
        }

        /// <summary>Adds a <see cref="ITraktShow" />, which will be added to the user hidden items post.</summary>
        /// <param name="show">The Trakt show, which will be added.</param>
        /// <returns>The current <see cref="TraktUserHiddenItemsPostBuilder" /> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown, if the given show is null.
        /// Thrown, if the given show ids are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown, if the given show has no valid ids set.
        /// Thrown, if the given show has an year set, which has more or less than four digits.
        /// </exception>
        public TraktUserHiddenItemsPostBuilder AddShow(ITraktShow show)
        {
            ValidateShow(show);
            EnsureShowsListExists();

            var existingShow = _hiddenItemsPost.Shows.FirstOrDefault(s => s.Ids == show.Ids);

            if (existingShow != null)
                return this;

            (_hiddenItemsPost.Shows as List<TraktUserHiddenItemsPostShow>)?.Add(
                new TraktUserHiddenItemsPostShow
                {
                    Title = show.Title,
                    Year = show.Year,
                    Ids = show.Ids
                });

            return this;
        }

        /// <summary>Adds a collection of <see cref="ITraktShow" />s, which will be added to the hidden items post.</summary>
        /// <param name="shows">A collection of Trakt shows, which will be added.</param>
        /// <returns>The current <see cref="TraktUserHiddenItemsPostBuilder" /> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown, if the given shows collection is null.
        /// Thrown, if one of the given shows is null.
        /// Thrown, if one of the given shows' ids are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown, if one of the given shows has no valid ids set.
        /// Thrown, if one of the given shows has an year set, which has more or less than four digits.
        /// </exception>
        public TraktUserHiddenItemsPostBuilder AddShows(IEnumerable<ITraktShow> shows)
        {
            if (shows == null)
                throw new ArgumentNullException(nameof(shows));

            if (!shows.Any())
                return this;

            foreach (var show in shows)
                AddShow(show);

            return this;
        }

        /// <summary>Adds a <see cref="ITraktShow" />, which will be added to the user hidden items post.</summary>
        /// <param name="show">The Trakt show, which will be added.</param>
        /// <param name="season">
        /// A season number for a season in the given show. The complete season will be added to the hidden items list.
        /// </param>
        /// <param name="seasons">
        /// An optional array of season numbers for seasons in the given show.
        /// The complete seasons will be added to the hidden items list.
        /// </param>
        /// <returns>The current <see cref="TraktUserHiddenItemsPostBuilder" /> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown, if the given show is null.
        /// Thrown, if the given show ids are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown, if the given show has no valid ids set.
        /// Thrown, if the given show has an year set, which has more or less than four digits.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if at least one of the given season numbers is below zero.
        /// </exception>
        public TraktUserHiddenItemsPostBuilder AddShow(ITraktShow show, int season, params int[] seasons)
        {
            ValidateShow(show);
            EnsureShowsListExists();

            var seasonsToAdd = new int[seasons.Length + 1];
            seasonsToAdd[0] = season;
            seasons.CopyTo(seasonsToAdd, 1);

            var showSeasons = new List<TraktUserHiddenItemsPostShowSeason>();

            for (int i = 0; i < seasonsToAdd.Length; i++)
            {
                if (seasonsToAdd[i] < 0)
                    throw new ArgumentOutOfRangeException("at least one season number not valid");

                showSeasons.Add(new TraktUserHiddenItemsPostShowSeason { Number = seasonsToAdd[i] });
            }

            var existingShow = _hiddenItemsPost.Shows.FirstOrDefault(s => s.Ids == show.Ids);

            if (existingShow != null)
            {
                existingShow.Seasons = showSeasons;
            }
            else
            {
                (_hiddenItemsPost.Shows as List<TraktUserHiddenItemsPostShow>)?.Add(
                    new TraktUserHiddenItemsPostShow
                    {
                        Title = show.Title,
                        Year = show.Year,
                        Ids = show.Ids,
                        Seasons = showSeasons
                    });
            }

            return this;
        }

        /// <summary>Adds a <see cref="ITraktShow" />, which will be added to the hidden items post.</summary>
        /// <param name="show">The Trakt show, which will be added.</param>
        /// <param name="seasons">
        /// An array of season numbers for seasons in the given show.
        /// All seasons numbers will be added to the hidden items post.
        /// </param>
        /// <returns>The current <see cref="TraktUserHiddenItemsPostBuilder" /> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown, if the given show is null.
        /// Thrown, if the given show ids are null.
        /// Thrown, if the given seasons array is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown, if the given show has no valid ids set.
        /// Thrown, if the given show has an year set, which has more or less than four digits.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if at least one of the given season numbers is below zero.
        /// </exception>
        public TraktUserHiddenItemsPostBuilder AddShow(ITraktShow show, int[] seasons)
        {
            ValidateShow(show);
            EnsureShowsListExists();

            if (seasons == null)
                throw new ArgumentNullException(nameof(seasons));

            var showSeasons = new List<TraktUserHiddenItemsPostShowSeason>();

            for (int i = 0; i < seasons.Length; i++)
            {
                if (seasons[i] < 0)
                    throw new ArgumentOutOfRangeException("at least one season number not valid");

                showSeasons.Add(new TraktUserHiddenItemsPostShowSeason { Number = seasons[i] });
            }

            var existingShow = _hiddenItemsPost.Shows.FirstOrDefault(s => s.Ids == show.Ids);

            if (existingShow != null)
            {
                existingShow.Seasons = showSeasons;
            }
            else
            {
                (_hiddenItemsPost.Shows as List<TraktUserHiddenItemsPostShow>)?.Add(
                    new TraktUserHiddenItemsPostShow
                    {
                        Title = show.Title,
                        Year = show.Year,
                        Ids = show.Ids,
                        Seasons = showSeasons
                    });
            }

            return this;
        }

        /// <summary>Adds a <see cref="ITraktSeason" />, which will be added to the user hidden items post.</summary>
        /// <param name="season">The Trakt season, which will be added.</param>
        /// <returns>The current <see cref="TraktUserHiddenItemsPostBuilder" /> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown, if the given season is null.
        /// Thrown, if the given season ids are null.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown, if the given season has no valid ids set.</exception>
        public TraktUserHiddenItemsPostBuilder AddSeason(ITraktSeason season)
        {
            ValidateSeason(season);
            EnsureSeasonsListExists();

            var existingSeason = _hiddenItemsPost.Movies.FirstOrDefault(s => s.Ids == season.Ids);

            if (existingSeason != null)
                return this;

            (_hiddenItemsPost.Seasons as List<TraktUserHiddenItemsPostSeason>)?.Add(
                new TraktUserHiddenItemsPostSeason
                {
                    Ids = season.Ids
                });

            return this;
        }

        /// <summary>Adds a collection of <see cref="ITraktSeason" />s, which will be added to the hidden items post.</summary>
        /// <param name="seasons">A collection of Trakt seasons, which will be added.</param>
        /// <returns>The current <see cref="TraktUserHiddenItemsPostBuilder" /> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown, if the given shows collection is null.
        /// Thrown, if one of the given seasons is null.
        /// Thrown, if one of the given seasons' ids are null.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown, if one of the given seasons has no valid ids set.</exception>
        public TraktUserHiddenItemsPostBuilder AddSeasons(IEnumerable<ITraktSeason> seasons)
        {
            if (seasons == null)
                throw new ArgumentNullException(nameof(seasons));

            if (!seasons.Any())
                return this;

            foreach (var season in seasons)
                AddSeason(season);

            return this;
        }

        /// <summary>Removes all already added movies, shows and seasons.</summary>
        public void Reset()
        {
            if (_hiddenItemsPost.Movies != null)
            {
                (_hiddenItemsPost.Movies as List<TraktUserHiddenItemsPostMovie>)?.Clear();
                _hiddenItemsPost.Movies = null;
            }

            if (_hiddenItemsPost.Shows != null)
            {
                (_hiddenItemsPost.Shows as List<TraktUserHiddenItemsPostShow>)?.Clear();
                _hiddenItemsPost.Shows = null;
            }

            if (_hiddenItemsPost.Seasons != null)
            {
                (_hiddenItemsPost.Seasons as List<TraktUserHiddenItemsPostSeason>)?.Clear();
                _hiddenItemsPost.Seasons = null;
            }
        }

        /// <summary>
        /// Returns an <see cref="ITraktUserHiddenItemsPost" /> instance, which contains all
        /// added movies, shows and seasons.
        /// </summary>
        /// <returns>An <see cref="ITraktUserHiddenItemsPost" /> instance.</returns>
        public ITraktUserHiddenItemsPost Build() => _hiddenItemsPost;

        private void ValidateMovie(ITraktMovie movie)
        {
            if (movie.Ids == null)
                throw new ArgumentNullException(nameof(movie.Ids));

            if (!movie.Ids.HasAnyId)
                throw new ArgumentException("no movie ids set or valid", nameof(movie.Ids));
        }

        private void ValidateShow(ITraktShow show)
        {
            if (show.Ids == null)
                throw new ArgumentNullException(nameof(show.Ids));

            if (!show.Ids.HasAnyId)
                throw new ArgumentException("no show ids set or valid", nameof(show.Ids));
        }

        private void ValidateSeason(ITraktSeason season)
        {
            if (season.Ids == null)
                throw new ArgumentNullException(nameof(season.Ids));

            if (!season.Ids.HasAnyId)
                throw new ArgumentException("no season ids set or valid", nameof(season.Ids));
        }

        private void EnsureMoviesListExists()
        {
            if (_hiddenItemsPost.Movies == null)
                _hiddenItemsPost.Movies = new List<TraktUserHiddenItemsPostMovie>();
        }

        private void EnsureShowsListExists()
        {
            if (_hiddenItemsPost.Shows == null)
                _hiddenItemsPost.Shows = new List<TraktUserHiddenItemsPostShow>();
        }

        private void EnsureSeasonsListExists()
        {
            if (_hiddenItemsPost.Seasons == null)
                _hiddenItemsPost.Seasons = new List<TraktUserHiddenItemsPostSeason>();
        }
    }
}
