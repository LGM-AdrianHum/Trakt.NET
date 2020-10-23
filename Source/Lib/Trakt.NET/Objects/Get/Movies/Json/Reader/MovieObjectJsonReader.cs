﻿namespace TraktNet.Objects.Get.Movies.Json.Reader
{
    using Enums;
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MovieObjectJsonReader : AObjectJsonReader<ITraktMovie>
    {
        public override async Task<ITraktMovie> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default)
        {
            CheckJsonTextReader(jsonReader);

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var idsObjectReader = new MovieIdsObjectJsonReader();
                ITraktMovie traktMovie = new TraktMovie();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.PROPERTY_NAME_TITLE:
                            traktMovie.Title = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_YEAR:
                            traktMovie.Year = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_IDS:
                            traktMovie.Ids = await idsObjectReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_TAGLINE:
                            traktMovie.Tagline = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_OVERVIEW:
                            traktMovie.Overview = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_RELEASED:
                            {
                                var value = await JsonReaderHelper.ReadDateTimeValueAsync(jsonReader, cancellationToken);

                                if (value.First)
                                    traktMovie.Released = value.Second;

                                break;
                            }
                        case JsonProperties.PROPERTY_NAME_RUNTIME:
                            traktMovie.Runtime = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_TRAILER:
                            traktMovie.Trailer = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_HOMEPAGE:
                            traktMovie.Homepage = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_RATING:
                            traktMovie.Rating = (float?)await jsonReader.ReadAsDoubleAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_VOTES:
                            traktMovie.Votes = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_UPDATED_AT:
                            {
                                var value = await JsonReaderHelper.ReadDateTimeValueAsync(jsonReader, cancellationToken);

                                if (value.First)
                                    traktMovie.UpdatedAt = value.Second;

                                break;
                            }
                        case JsonProperties.PROPERTY_NAME_LANGUAGE:
                            traktMovie.LanguageCode = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_AVAILABLE_TRANSLATIONS:
                            traktMovie.AvailableTranslationLanguageCodes = await JsonReaderHelper.ReadStringArrayAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_GENRES:
                            traktMovie.Genres = await JsonReaderHelper.ReadStringArrayAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_CERTIFICATION:
                            traktMovie.Certification = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_COUNTRY:
                            traktMovie.CountryCode = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_COMMENT_COUNT:
                            traktMovie.CommentCount = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_STATUS:
                            traktMovie.Status = await JsonReaderHelper.ReadEnumerationValueAsync<TraktMovieStatus>(jsonReader, cancellationToken);
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
