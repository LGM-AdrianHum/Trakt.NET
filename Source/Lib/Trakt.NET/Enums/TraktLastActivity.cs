﻿namespace TraktNet.Enums
{
    /// <summary>Determines the last activity type of a collection or watched progress.</summary>
    public sealed class TraktLastActivity : TraktEnumeration
    {
        /// <summary>An invalid last activity type.</summary>
        public static TraktLastActivity Unspecified { get; } = new TraktLastActivity();

        /// <summary>Use last collected episodes while calculating collection or watched progress.</summary>
        public static TraktLastActivity Collected { get; } = new TraktLastActivity(1, "collected", "collected", "Collected");

        /// <summary>
        /// Initializes a new instance of the <see cref="TraktLastActivity" /> class.<para />
        /// The initialized <see cref="TraktLastActivity" /> is invalid.
        /// </summary>
        public TraktLastActivity()
        {
        }

        private TraktLastActivity(int value, string objectName, string uriName, string displayName) : base(value, objectName, uriName, displayName)
        {
        }
    }
}
