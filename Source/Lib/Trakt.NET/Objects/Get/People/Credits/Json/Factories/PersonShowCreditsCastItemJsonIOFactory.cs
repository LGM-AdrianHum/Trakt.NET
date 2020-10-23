﻿namespace TraktNet.Objects.Get.People.Credits.Json.Factories
{
    using Get.People.Credits.Json.Reader;
    using Get.People.Credits.Json.Writer;
    using Objects.Json;

    internal class PersonShowCreditsCastItemJsonIOFactory : IJsonIOFactory<ITraktPersonShowCreditsCastItem>
    {
        public IObjectJsonReader<ITraktPersonShowCreditsCastItem> CreateObjectReader() => new PersonShowCreditsCastItemObjectJsonReader();

        public IObjectJsonWriter<ITraktPersonShowCreditsCastItem> CreateObjectWriter() => new PersonShowCreditsCastItemObjectJsonWriter();
    }
}
