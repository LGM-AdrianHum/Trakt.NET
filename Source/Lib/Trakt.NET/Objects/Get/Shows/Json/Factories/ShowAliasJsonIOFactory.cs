﻿namespace TraktNet.Objects.Get.Shows.Json.Factories
{
    using Get.Shows.Json.Reader;
    using Get.Shows.Json.Writer;
    using Objects.Json;

    internal class ShowAliasJsonIOFactory : IJsonIOFactory<ITraktShowAlias>
    {
        public IObjectJsonReader<ITraktShowAlias> CreateObjectReader() => new ShowAliasObjectJsonReader();

        public IObjectJsonWriter<ITraktShowAlias> CreateObjectWriter() => new ShowAliasObjectJsonWriter();
    }
}
