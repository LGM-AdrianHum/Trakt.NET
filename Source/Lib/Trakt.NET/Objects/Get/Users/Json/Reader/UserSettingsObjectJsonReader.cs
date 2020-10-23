﻿namespace TraktNet.Objects.Get.Users.Json.Reader
{
    using Basic.Json.Reader;
    using Newtonsoft.Json;
    using Objects.Json;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UserSettingsObjectJsonReader : AObjectJsonReader<ITraktUserSettings>
    {
        public override async Task<ITraktUserSettings> ReadObjectAsync(JsonTextReader jsonReader, CancellationToken cancellationToken = default)
        {
            CheckJsonTextReader(jsonReader);

            if (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.StartObject)
            {
                var userReader = new UserObjectJsonReader();
                var accountSettingsReader = new AccountSettingsObjectJsonReader();
                var sharingReader = new SharingObjectJsonReader();
                var sharingTextReader = new SharingTextObjectJsonReader();

                ITraktUserSettings traktUserSettings = new TraktUserSettings();

                while (await jsonReader.ReadAsync(cancellationToken) && jsonReader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = jsonReader.Value.ToString();

                    switch (propertyName)
                    {
                        case JsonProperties.PROPERTY_NAME_USER:
                            traktUserSettings.User = await userReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_ACCOUNT:
                            traktUserSettings.Account = await accountSettingsReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_CONNECTIONS:
                            traktUserSettings.Connections = await sharingReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        case JsonProperties.PROPERTY_NAME_SHARING_TEXT:
                            traktUserSettings.SharingText = await sharingTextReader.ReadObjectAsync(jsonReader, cancellationToken);
                            break;
                        default:
                            await JsonReaderHelper.ReadAndIgnoreInvalidContentAsync(jsonReader, cancellationToken);
                            break;
                    }
                }

                return traktUserSettings;
            }

            return await Task.FromResult(default(ITraktUserSettings));
        }
    }
}
