﻿namespace TraktApiSharp.Objects.Get.Shows.Json.Reader
{
    using Implementations;
    using Newtonsoft.Json;
    using Objects.Json;
    using Shows;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MostAnticipatedShowObjectJsonReader : IObjectJsonReader<ITraktMostAnticipatedShow>
    {
        public Task<ITraktMostAnticipatedShow> ReadObjectAsync(string json, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(json))
                return Task.FromResult(default(ITraktMostAnticipatedShow));

            using (var reader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return ReadObjectAsync(jsonReader, cancellationToken);
            }
        }

        public Task<ITraktMostAnticipatedShow> ReadObjectAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (stream == null)
                return Task.FromResult(default(ITraktMostAnticipatedShow));

            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return ReadObjectAsync(jsonReader, cancellationToken);
            }
        }

        public async Task<ITraktMostAnticipatedShow> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jsonReader == null)
                return await Task.FromResult(default(ITraktMostAnticipatedShow));

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var showObjectReader = new ShowObjectJsonReader();
                ITraktMostAnticipatedShow traktMostAnticipatedShow = new TraktMostAnticipatedShow();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.MOST_ANTICIPATED_SHOW_PROPERTY_NAME_LIST_COUNT:
                            traktMostAnticipatedShow.ListCount = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.MOST_ANTICIPATED_SHOW_PROPERTY_NAME_SHOW:
                            traktMostAnticipatedShow.Show = await showObjectReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return traktMostAnticipatedShow;
            }

            return await Task.FromResult(default(ITraktMostAnticipatedShow));
        }
    }
}