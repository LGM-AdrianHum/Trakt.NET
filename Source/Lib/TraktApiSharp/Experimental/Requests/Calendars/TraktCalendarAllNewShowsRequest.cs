﻿namespace TraktApiSharp.Experimental.Requests.Calendars
{
    using Objects.Get.Calendars;

    internal sealed class TraktCalendarAllNewShowsRequest : ATraktCalendarAllRequest<TraktCalendarShow>
    {
        internal TraktCalendarAllNewShowsRequest(TraktClient client) : base(client) { }

        public string UriTemplate => "calendars/all/shows/new{/start_date}{/days}{?extended,query,years,genres,languages,countries,runtimes,ratings}";
    }
}
