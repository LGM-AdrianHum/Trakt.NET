﻿namespace TraktNet.Objects.Post.Users.Json.Writer
{
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UserCustomListsReorderPostObjectJsonWriter : AObjectJsonWriter<ITraktUserCustomListsReorderPost>
    {
        public override async Task WriteObjectAsync(JsonTextWriter jsonWriter, ITraktUserCustomListsReorderPost obj, CancellationToken cancellationToken = default)
        {
            CheckJsonTextWriter(jsonWriter);
            await jsonWriter.WriteStartObjectAsync(cancellationToken).ConfigureAwait(false);

            if (obj.Rank != null)
            {
                await jsonWriter.WritePropertyNameAsync(JsonProperties.PROPERTY_NAME_RANK, cancellationToken).ConfigureAwait(false);
                await JsonWriterHelper.WriteUnsignedIntegerArrayAsync(jsonWriter, obj.Rank, cancellationToken).ConfigureAwait(false);
            }

            await jsonWriter.WriteEndObjectAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
