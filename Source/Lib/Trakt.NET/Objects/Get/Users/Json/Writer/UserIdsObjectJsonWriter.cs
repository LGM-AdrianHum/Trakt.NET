﻿namespace TraktNet.Objects.Get.Users.Json.Writer
{
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UserIdsObjectJsonWriter : AObjectJsonWriter<ITraktUserIds>
    {
        public override async Task WriteObjectAsync(JsonTextWriter jsonWriter, ITraktUserIds obj, CancellationToken cancellationToken = default)
        {
            CheckJsonTextWriter(jsonWriter);
            await jsonWriter.WriteStartObjectAsync(cancellationToken).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(obj.Slug))
            {
                await jsonWriter.WritePropertyNameAsync(JsonProperties.PROPERTY_NAME_SLUG, cancellationToken).ConfigureAwait(false);
                await jsonWriter.WriteValueAsync(obj.Slug, cancellationToken).ConfigureAwait(false);
            }

            if (!string.IsNullOrEmpty(obj.UUID))
            {
                await jsonWriter.WritePropertyNameAsync(JsonProperties.PROPERTY_NAME_UUID, cancellationToken).ConfigureAwait(false);
                await jsonWriter.WriteValueAsync(obj.UUID, cancellationToken).ConfigureAwait(false);
            }

            await jsonWriter.WriteEndObjectAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
