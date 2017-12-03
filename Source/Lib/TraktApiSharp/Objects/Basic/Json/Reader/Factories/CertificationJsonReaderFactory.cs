﻿namespace TraktApiSharp.Objects.Basic.Json.Factories.Reader
{
    using Objects.Basic.Json.Reader;
    using Objects.Json;

    internal class CertificationJsonReaderFactory : IJsonReaderFactory<ITraktCertification>
    {
        public IObjectJsonReader<ITraktCertification> CreateObjectReader() => new CertificationObjectJsonReader();

        public IArrayJsonReader<ITraktCertification> CreateArrayReader() => new CertificationArrayJsonReader();
    }
}