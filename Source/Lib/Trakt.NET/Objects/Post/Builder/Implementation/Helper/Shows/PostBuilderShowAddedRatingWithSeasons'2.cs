﻿namespace TraktNet.Objects.Post.Builder.Implementation.Helper
{
    using Get.Shows;
    using Interfaces;
    using Interfaces.Capabilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class PostBuilderShowAddedRatingWithSeasons<TPostBuilderAddShow, TPostObject>
        : ITraktPostBuilderShowAddedRatingWithSeasons<TPostBuilderAddShow, TPostObject>
          where TPostBuilderAddShow : ITraktPostBuilder<TPostObject>, ITraktPostBuilderAddShowWithRatingWithSeasons<TPostBuilderAddShow, TPostObject>
    {
        private readonly TPostBuilderAddShow _postBuilder;
        private ITraktShow _currentShow;
        private readonly List<PostBuilderRatedObjectWithSeasons<ITraktShow, IEnumerable<int>>> _showsAndRatingWithSeasons;

        internal PostBuilderShowAddedRatingWithSeasons(TPostBuilderAddShow postBuilder)
        {
            _postBuilder = postBuilder;
            _currentShow = null;
            _showsAndRatingWithSeasons = new List<PostBuilderRatedObjectWithSeasons<ITraktShow, IEnumerable<int>>>();
        }

        public TPostBuilderAddShow WithRating(int rating, int[] seasons)
        {
            _showsAndRatingWithSeasons.Add(new PostBuilderRatedObjectWithSeasons<ITraktShow, IEnumerable<int>>
            {
                Object = _currentShow,
                Rating = rating,
                Seasons = seasons.ToList()
            });

            return _postBuilder;
        }

        public TPostBuilderAddShow WithRating(int rating, DateTime ratedAt, int[] seasons)
        {
            _showsAndRatingWithSeasons.Add(new PostBuilderRatedObjectWithSeasons<ITraktShow, IEnumerable<int>>
            {
                Object = _currentShow,
                Rating = rating,
                RatedAt = ratedAt,
                Seasons = seasons.ToList()
            });

            return _postBuilder;
        }

        public TPostBuilderAddShow WithRating(int rating, int season, params int[] seasons)
        {
            var newSeasons = new List<int>
            {
               season
            };

            newSeasons.AddRange(seasons);

            _showsAndRatingWithSeasons.Add(new PostBuilderRatedObjectWithSeasons<ITraktShow, IEnumerable<int>>
            {
                Object = _currentShow,
                Rating = rating,
                Seasons = newSeasons
            });

            return _postBuilder;
        }

        public TPostBuilderAddShow WithRating(int rating, DateTime ratedAt, int season, params int[] seasons)
        {
            var newSeasons = new List<int>
            {
                season
            };

            newSeasons.AddRange(seasons);

            _showsAndRatingWithSeasons.Add(new PostBuilderRatedObjectWithSeasons<ITraktShow, IEnumerable<int>>
            {
                Object = _currentShow,
                Rating = rating,
                RatedAt = ratedAt,
                Seasons = newSeasons
            });

            return _postBuilder;
        }

        public void SetCurrentShow(ITraktShow show)
        {
            _currentShow = show;
        }
    }
}
