﻿namespace TraktNet.Objects.Post.Users.CustomListItems
{
    using System.Collections.Generic;

    /// <summary>An user custom list items post season, containing the required season number and optional episodes.</summary>
    public interface ITraktUserCustomListItemsPostShowSeason
    {
        /// <summary>Gets or sets the required season number of the Trakt season.</summary>
        int Number { get; set; }

        /// <summary>
        /// An optional list of <see cref="ITraktUserCustomListItemsPostShowEpisode" />s.
        /// <para>
        /// If no episodes are set, the whole Trakt season will be added to the custom list.
        /// Otherwise, only the specified episodes will be added to the custom list.
        /// </para>
        /// </summary>
        IEnumerable<ITraktUserCustomListItemsPostShowEpisode> Episodes { get; set; }
    }
}
