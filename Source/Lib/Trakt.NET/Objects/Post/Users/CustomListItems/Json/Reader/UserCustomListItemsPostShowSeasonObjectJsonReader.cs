﻿namespace TraktNet.Objects.Post.Users.CustomListItems.Json.Reader
{
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Utils;

    internal class UserCustomListItemsPostShowSeasonObjectJsonReader : AObjectJsonReader<ITraktUserCustomListItemsPostShowSeason>
    {
        public override async Task<ITraktUserCustomListItemsPostShowSeason> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default)
        {
            CheckJsonTextReader(jsonReader);

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var episodeArrayJsonReader = new ArrayJsonReader<ITraktUserCustomListItemsPostShowEpisode>();
                ITraktUserCustomListItemsPostShowSeason customListItemsPostShowSeason = new TraktUserCustomListItemsPostShowSeason();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.PROPERTY_NAME_NUMBER:
                            {
                                Pair<bool, int> value = await JsonReaderHelper.ReadIntegerValueAsync(jsonReader, cancellationToken);

                                if (value.First)
                                    customListItemsPostShowSeason.Number = value.Second;

                                break;
                            }
                        case JsonProperties.PROPERTY_NAME_EPISODES:
                            customListItemsPostShowSeason.Episodes = await episodeArrayJsonReader.ReadArrayAsync(jsonReader, cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return customListItemsPostShowSeason;
            }

            return await Task.FromResult(default(ITraktUserCustomListItemsPostShowSeason));
        }
    }
}
