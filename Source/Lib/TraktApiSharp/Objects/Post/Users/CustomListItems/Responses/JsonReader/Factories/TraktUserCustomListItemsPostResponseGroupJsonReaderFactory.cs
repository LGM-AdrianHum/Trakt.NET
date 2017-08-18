﻿namespace TraktApiSharp.Objects.Post.Users.CustomListItems.Responses.JsonReader.Factories
{
    using Objects.JsonReader;
    using System;

    internal class TraktUserCustomListItemsPostResponseGroupJsonReaderFactory : IJsonReaderFactory<ITraktUserCustomListItemsPostResponseGroup>
    {
        public ITraktObjectJsonReader<ITraktUserCustomListItemsPostResponseGroup> CreateObjectReader() => new TraktUserCustomListItemsPostResponseGroupObjectJsonReader();

        public IArrayJsonReader<ITraktUserCustomListItemsPostResponseGroup> CreateArrayReader()
        {
            throw new NotSupportedException($"A array json reader for {nameof(ITraktUserCustomListItemsPostResponseGroup)} is not supported.");
        }
    }
}
