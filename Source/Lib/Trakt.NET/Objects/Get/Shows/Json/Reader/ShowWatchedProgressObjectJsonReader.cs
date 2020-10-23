﻿namespace TraktNet.Objects.Get.Shows.Json.Reader
{
    using Episodes.Json.Reader;
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using TraktNet.Objects.Get.Seasons;

    internal class ShowWatchedProgressObjectJsonReader : AObjectJsonReader<ITraktShowWatchedProgress>
    {
        public override async Task<ITraktShowWatchedProgress> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default)
        {
            CheckJsonTextReader(jsonReader);

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var seasonsArrayReader = new ArrayJsonReader<ITraktSeason>();
                var seasonWatchedProgressArrayReader = new ArrayJsonReader<ITraktSeasonWatchedProgress>();
                var episodeObjectReader = new EpisodeObjectJsonReader();

                ITraktShowWatchedProgress traktShowWatchedProgress = new TraktShowWatchedProgress();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.PROPERTY_NAME_AIRED:
                            traktShowWatchedProgress.Aired = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_COMPLETED:
                            traktShowWatchedProgress.Completed = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_LAST_WATCHED_AT:
                            {
                                var value = await JsonReaderHelper.ReadDateTimeValueAsync(jsonReader, cancellationToken);

                                if (value.First)
                                    traktShowWatchedProgress.LastWatchedAt = value.Second;

                                break;
                            }
                        case JsonProperties.PROPERTY_NAME_SEASONS:
                            traktShowWatchedProgress.Seasons = await seasonWatchedProgressArrayReader.ReadArrayAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_HIDDEN_SEASONS:
                            traktShowWatchedProgress.HiddenSeasons = await seasonsArrayReader.ReadArrayAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_NEXT_EPISODE:
                            traktShowWatchedProgress.NextEpisode = await episodeObjectReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_LAST_EPISODE:
                            traktShowWatchedProgress.LastEpisode = await episodeObjectReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_RESET_AT:
                            {
                                var value = await JsonReaderHelper.ReadDateTimeValueAsync(jsonReader, cancellationToken);

                                if (value.First)
                                    traktShowWatchedProgress.ResetAt = value.Second;

                                break;
                            }
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return traktShowWatchedProgress;
            }

            return await Task.FromResult(default(ITraktShowWatchedProgress));
        }
    }
}
