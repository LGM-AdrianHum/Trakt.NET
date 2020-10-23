﻿namespace TraktNet.Objects.Post.Users.HiddenItems.Json.Reader
{
    using Get.Shows.Json.Reader;
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UserHiddenItemsPostShowObjectJsonReader : AObjectJsonReader<ITraktUserHiddenItemsPostShow>
    {
        public override async Task<ITraktUserHiddenItemsPostShow> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default)
        {
            CheckJsonTextReader(jsonReader);

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var showIdsObjectJsonReader = new ShowIdsObjectJsonReader();
                var seasonArrayJsonReader = new ArrayJsonReader<ITraktUserHiddenItemsPostShowSeason>();
                ITraktUserHiddenItemsPostShow hiddenItemsPostShow = new TraktUserHiddenItemsPostShow();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.PROPERTY_NAME_TITLE:
                            hiddenItemsPostShow.Title = await jsonReader.ReadAsStringAsync(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_YEAR:
                            hiddenItemsPostShow.Year = await jsonReader.ReadAsInt32Async(cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_IDS:
                            hiddenItemsPostShow.Ids = await showIdsObjectJsonReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_SEASONS:
                            hiddenItemsPostShow.Seasons = await seasonArrayJsonReader.ReadArrayAsync(jsonReader, cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return hiddenItemsPostShow;
            }

            return await Task.FromResult(default(ITraktUserHiddenItemsPostShow));
        }
    }
}
