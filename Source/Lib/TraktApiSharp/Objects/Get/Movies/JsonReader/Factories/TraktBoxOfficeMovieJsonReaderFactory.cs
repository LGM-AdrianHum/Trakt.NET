﻿namespace TraktApiSharp.Objects.Get.Movies.JsonReader.Factories
{
    using Objects.JsonReader;

    internal class TraktBoxOfficeMovieJsonReaderFactory : IJsonReaderFactory<ITraktBoxOfficeMovie>
    {
        public ITraktObjectJsonReader<ITraktBoxOfficeMovie> CreateObjectReader() => new TraktBoxOfficeMovieObjectJsonReader();

        public IArrayJsonReader<ITraktBoxOfficeMovie> CreateArrayReader() => new TraktBoxOfficeMovieArrayJsonReader();
    }
}
