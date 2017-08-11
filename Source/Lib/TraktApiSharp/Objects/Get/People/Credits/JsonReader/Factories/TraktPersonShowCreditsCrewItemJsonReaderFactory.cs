﻿namespace TraktApiSharp.Objects.Get.People.Credits.JsonReader.Factories
{
    using Objects.JsonReader;

    internal class TraktPersonShowCreditsCrewItemJsonReaderFactory : ITraktJsonReaderFactory<ITraktPersonShowCreditsCrewItem>
    {
        public ITraktObjectJsonReader<ITraktPersonShowCreditsCrewItem> CreateObjectReader() => new TraktPersonShowCreditsCrewItemObjectJsonReader();

        public ITraktArrayJsonReader<ITraktPersonShowCreditsCrewItem> CreateArrayReader() => new TraktPersonShowCreditsCrewItemArrayJsonReader();
    }
}