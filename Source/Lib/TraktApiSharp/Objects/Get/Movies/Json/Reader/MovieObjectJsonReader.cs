﻿namespace TraktApiSharp.Objects.Get.Movies.Json.Reader
{
    using Implementations;
    using Newtonsoft.Json;
    using Objects.Get.Movies;
    using Objects.Json;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MovieObjectJsonReader : IObjectJsonReader<ITraktMovie>
    {
        public Task<ITraktMovie> ReadObjectAsync(string json, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(json))
                return Task.FromResult(default(ITraktMovie));

            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return ReadObjectAsync(jsonReader, cancellationToken);
            }
        }

        public Task<ITraktMovie> ReadObjectAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (stream == null)
                return Task.FromResult(default(ITraktMovie));

            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return ReadObjectAsync(jsonReader, cancellationToken);
            }
        }

        public async Task<ITraktMovie> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jsonReader == null)
                return await Task.FromResult(default(ITraktMovie));

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var idsObjectReader = new MovieIdsObjectJsonReader();
                ITraktMovie traktMovie = new TraktMovie();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.MOVIE_PROPERTY_NAME_TITLE:
                            traktMovie.Title = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_YEAR:
                            traktMovie.Year = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_IDS:
                            traktMovie.Ids = await idsObjectReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_TAGLINE:
                            traktMovie.Tagline = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_OVERVIEW:
                            traktMovie.Overview = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_RELEASED:
                            {
                                var value = await JsonReaderHelper.ReadDateTimeValueAsync(jsonReader, cancellationToken);

                                if (value.First)
                                    traktMovie.Released = value.Second;

                                break;
                            }
                        case JsonProperties.MOVIE_PROPERTY_NAME_RUNTIME:
                            traktMovie.Runtime = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_TRAILER:
                            traktMovie.Trailer = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_HOMEPAGE:
                            traktMovie.Homepage = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_RATING:
                            traktMovie.Rating = (float?)await jsonReader.ReadAsDoubleAsync(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_VOTES:
                            traktMovie.Votes = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_UPDATED_AT:
                            {
                                var value = await JsonReaderHelper.ReadDateTimeValueAsync(jsonReader, cancellationToken);

                                if (value.First)
                                    traktMovie.UpdatedAt = value.Second;

                                break;
                            }
                        case JsonProperties.MOVIE_PROPERTY_NAME_LANGUAGE:
                            traktMovie.LanguageCode = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_AVAILABLE_TRANSLATIONS:
                            traktMovie.AvailableTranslationLanguageCodes = await JsonReaderHelper.ReadStringArrayAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_GENRES:
                            traktMovie.Genres = await JsonReaderHelper.ReadStringArrayAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.MOVIE_PROPERTY_NAME_CERTIFICATION:
                            traktMovie.Certification = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return traktMovie;
            }

            return await Task.FromResult(default(ITraktMovie));
        }
    }
}