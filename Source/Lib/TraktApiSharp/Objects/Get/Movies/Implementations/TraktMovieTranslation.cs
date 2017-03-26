﻿namespace TraktApiSharp.Objects.Get.Movies.Implementations
{
    using Basic;
    using Newtonsoft.Json;
    using TraktApiSharp.Objects.Basic.Implementations;

    /// <summary>A translation for a Trakt movie.</summary>
    public class TraktMovieTranslation : TraktTranslation, ITraktMovieTranslation
    {
        /// <summary>Gets or sets the tagline for the release.<para>Nullable</para></summary>
        [JsonProperty(PropertyName = "tagline")]
        public string Tagline { get; set; }
    }
}
