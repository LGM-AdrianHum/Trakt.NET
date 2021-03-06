﻿namespace TraktNet.Objects.Get.People.Credits.Json.Reader
{
    using Movies.Json.Reader;
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class PersonMovieCreditsCrewItemObjectJsonReader : AObjectJsonReader<ITraktPersonMovieCreditsCrewItem>
    {
        public override async Task<ITraktPersonMovieCreditsCrewItem> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default)
        {
            CheckJsonTextReader(jsonReader);

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var movieObjectReader = new MovieObjectJsonReader();

                ITraktPersonMovieCreditsCrewItem movieCreditsCrewItem = new TraktPersonMovieCreditsCrewItem();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.PROPERTY_NAME_JOBS:
                            movieCreditsCrewItem.Jobs = await JsonReaderHelper.ReadStringArrayAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_MOVIE:
                            movieCreditsCrewItem.Movie = await movieObjectReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return movieCreditsCrewItem;
            }

            return await Task.FromResult(default(ITraktPersonMovieCreditsCrewItem));
        }
    }
}
