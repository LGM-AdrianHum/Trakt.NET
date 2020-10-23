﻿namespace TraktNet.Objects.Get.Shows.Json.Factories
{
    using Get.Shows.Json.Reader;
    using Get.Shows.Json.Writer;
    using Objects.Json;

    internal class ShowTranslationJsonIOFactory : IJsonIOFactory<ITraktShowTranslation>
    {
        public IObjectJsonReader<ITraktShowTranslation> CreateObjectReader() => new ShowTranslationObjectJsonReader();

        public IObjectJsonWriter<ITraktShowTranslation> CreateObjectWriter() => new ShowTranslationObjectJsonWriter();
    }
}
