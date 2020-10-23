﻿namespace TraktNet.Objects.Get.People.Credits.Json.Writer
{
    using Movies.Json.Writer;
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class PersonMovieCreditsCrewItemObjectJsonWriter : AObjectJsonWriter<ITraktPersonMovieCreditsCrewItem>
    {
        public override async Task WriteObjectAsync(JsonTextWriter jsonWriter, ITraktPersonMovieCreditsCrewItem obj, CancellationToken cancellationToken = default)
        {
            CheckJsonTextWriter(jsonWriter);
            await jsonWriter.WriteStartObjectAsync(cancellationToken).ConfigureAwait(false);

            if (obj.Jobs != null)
            {
                await jsonWriter.WritePropertyNameAsync(JsonProperties.PROPERTY_NAME_JOBS, cancellationToken).ConfigureAwait(false);
                await JsonWriterHelper.WriteStringArrayAsync(jsonWriter, obj.Jobs, cancellationToken).ConfigureAwait(false);
            }

            if (obj.Movie != null)
            {
                var movieObjectJsonWriter = new MovieObjectJsonWriter();
                await jsonWriter.WritePropertyNameAsync(JsonProperties.PROPERTY_NAME_MOVIE, cancellationToken).ConfigureAwait(false);
                await movieObjectJsonWriter.WriteObjectAsync(jsonWriter, obj.Movie, cancellationToken).ConfigureAwait(false);
            }

            await jsonWriter.WriteEndObjectAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
