﻿namespace TraktNet.Objects.Get.Collections.Json.Reader
{
    using Basic.Json.Reader;
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CollectionShowEpisodeObjectJsonReader : AObjectJsonReader<ITraktCollectionShowEpisode>
    {
        public override async Task<ITraktCollectionShowEpisode> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default)
        {
            CheckJsonTextReader(jsonReader);

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var metadataObjectReader = new MetadataObjectJsonReader();

                ITraktCollectionShowEpisode traktCollectionShowEpisode = new TraktCollectionShowEpisode();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.PROPERTY_NAME_NUMBER:
                            traktCollectionShowEpisode.Number = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_COLLECTED_AT:
                            {
                                var value = await JsonReaderHelper.ReadDateTimeValueAsync(jsonReader, cancellationToken);

                                if (value.First)
                                    traktCollectionShowEpisode.CollectedAt = value.Second;

                                break;
                            }
                        case JsonProperties.PROPERTY_NAME_METADATA:
                            traktCollectionShowEpisode.Metadata = await metadataObjectReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return traktCollectionShowEpisode;
            }

            return await Task.FromResult(default(ITraktCollectionShowEpisode));
        }
    }
}
