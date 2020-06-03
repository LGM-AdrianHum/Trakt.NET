﻿namespace TraktNet.Objects.Post.Users.CustomListItems
{
    using Get.Shows;
    using System.Collections.Generic;

    /// <summary>
    /// An user custom list items post show, containing the required show ids.
    /// <para>Can also contain optional seasons.</para>
    /// </summary>
    public interface ITraktUserCustomListItemsPostShow
    {
        /// <summary>Gets or sets the required show ids. See also <seealso cref="ITraktShowIds" />.</summary>
        ITraktShowIds Ids { get; set; }

        /// <summary>
        /// An optional list of <see cref="ITraktUserCustomListItemsPostShowSeason" />s.
        /// <para>
        /// If no seasons are set, the whole Trakt show will be added to the custom list.
        /// Otherwise, only the specified seasons and / or episodes will be added to the custom list.
        /// </para>
        /// </summary>
        IEnumerable<ITraktUserCustomListItemsPostShowSeason> Seasons { get; set; }
    }
}
