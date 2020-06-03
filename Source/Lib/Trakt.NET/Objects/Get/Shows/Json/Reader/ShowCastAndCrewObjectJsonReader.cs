﻿namespace TraktNet.Objects.Get.Shows.Json.Reader
{
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ShowCastAndCrewObjectJsonReader : AObjectJsonReader<ITraktShowCastAndCrew>
    {
        public override async Task<ITraktShowCastAndCrew> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default)
        {
            if (jsonReader == null)
                return await Task.FromResult(default(ITraktShowCastAndCrew));

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var showCastReader = new ShowCastMemberArrayJsonReader();
                var showCrewReader = new ShowCrewObjectJsonReader();
                ITraktShowCastAndCrew traktShowCastAndCrew = new TraktShowCastAndCrew();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.CAST_AND_CREW_PROPERTY_NAME_CAST:
                            traktShowCastAndCrew.Cast = await showCastReader.ReadArrayAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.CAST_AND_CREW_PROPERTY_NAME_CREW:
                            traktShowCastAndCrew.Crew = await showCrewReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return traktShowCastAndCrew;
            }

            return await Task.FromResult(default(ITraktShowCastAndCrew));
        }
    }
}
