﻿namespace TraktNet.Objects.Json
{
    internal interface IJsonIOFactory<TObjectType> : IJsonReaderFactory<TObjectType>, IJsonWriterFactory<TObjectType>
    {
    }
}
