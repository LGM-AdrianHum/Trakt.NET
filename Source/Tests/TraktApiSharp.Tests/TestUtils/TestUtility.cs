﻿namespace TraktApiSharp.Tests.TestUtils
{
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using TraktApiSharp.Authentication;
    using TraktApiSharp.Core;
    using TraktApiSharp.Requests.Handler;

    internal static class TestUtility
    {
        private static MockHttpMessageHandler MOCK_HTTP;
        private static string BASE_URL;

        private const string TRAKT_CLIENT_ID = "traktClientId";
        private const string TRAKT_CLIENT_SECRET = "traktClientSecret";
        private const string DEFAULT_REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

        private const string HEADER_STARTDATE_KEY = "X-Start-Date";
        private const string HEADER_ENDDATE_KEY = "X-End-Date";

        private static readonly TraktAuthorization MOCK_AUTHORIZATION = new TraktAuthorization { AccessToken = "mock_access_token", ExpiresInSeconds = 3600 };

        internal static TraktClient MOCK_TEST_CLIENT = new TraktClient(TRAKT_CLIENT_ID, TRAKT_CLIENT_SECRET);

        internal static void SetupMockHttpClient()
        {
            BASE_URL = MOCK_TEST_CLIENT.Configuration.BaseUrl;
            MOCK_HTTP = new MockHttpMessageHandler();

            RequestHandler.s_httpClient = new HttpClient(MOCK_HTTP);
            RequestHandler.s_httpClient.DefaultRequestHeaders.Add("trakt-api-key", TRAKT_CLIENT_ID);
            RequestHandler.s_httpClient.DefaultRequestHeaders.Add("trakt-api-version", "2");
            RequestHandler.s_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void SetupMockAuthenticationHttpClient()
        {
            BASE_URL = MOCK_TEST_CLIENT.Configuration.BaseUrl;
            MOCK_HTTP = new MockHttpMessageHandler();
            TraktConfiguration.HTTP_CLIENT = new HttpClient(MOCK_HTTP);
            TraktConfiguration.HTTP_CLIENT.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // ------------------------------------------------------

            RequestHandler.s_httpClient = new HttpClient(MOCK_HTTP);
            RequestHandler.s_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal static void ResetMockHttpClient()
        {
            MOCK_TEST_CLIENT.Configuration.ForceAuthorization = false;
            RequestHandler.s_httpClient = null;
        }

        internal static void ClearMockHttpClient()
        {
            MOCK_TEST_CLIENT.Configuration.ForceAuthorization = false;
            MOCK_HTTP.Clear();
        }

        public static void SetDefaultClientValues()
        {
            MOCK_TEST_CLIENT.Should().NotBeNull();

            MOCK_TEST_CLIENT.ClientId = TRAKT_CLIENT_ID;
            MOCK_TEST_CLIENT.ClientSecret = TRAKT_CLIENT_SECRET;
            MOCK_TEST_CLIENT.Authentication.RedirectUri = DEFAULT_REDIRECT_URI;
        }

        public static void SetupMockAuthenticationResponse(string url, string requestContent, string responseContent)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();

            url.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When($"{BASE_URL}{url}")
                     .WithContent(requestContent)
                     .Respond("application/json", responseContent);
        }

        internal static void SetupMockResponseWithoutOAuth(string uri, string responseContent)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();

            uri.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When($"{BASE_URL}{uri}")
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" }
                     })
                     .Respond("application/json", responseContent);
        }

        internal static void SetupMockResponseWithoutOAuth(string uri, HttpStatusCode httpStatusCode)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();

            uri.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When($"{BASE_URL}{uri}")
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" }
                     })
                     .Respond(httpStatusCode);
        }

        internal static void SetupMockResponseWithOAuth(string uri, string responseContent)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();
            MOCK_AUTHORIZATION.Should().NotBeNull();
            MOCK_AUTHORIZATION.AccessToken.Should().NotBeNullOrEmpty();

            MOCK_TEST_CLIENT.Authorization = MOCK_AUTHORIZATION;

            uri.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When($"{BASE_URL}{uri}")
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {MOCK_AUTHORIZATION.AccessToken}" }
                     })
                     .Respond("application/json", responseContent);
        }

        public static void SetupMockResponseWithOAuthWithHeaders(string uri, string responseContent,
                                                                 string startDate = null, string endDate = null)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();
            MOCK_AUTHORIZATION.Should().NotBeNull();
            MOCK_AUTHORIZATION.AccessToken.Should().NotBeNullOrEmpty();

            MOCK_TEST_CLIENT.Authorization = MOCK_AUTHORIZATION;

            uri.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            var response = new HttpResponseMessage();

            if (!string.IsNullOrEmpty(startDate))
                response.Headers.Add(HEADER_STARTDATE_KEY, startDate);

            if (!string.IsNullOrEmpty(endDate))
                response.Headers.Add(HEADER_ENDDATE_KEY, endDate);

            response.Headers.Add("Accept", "application/json");
            response.Content = new StringContent(responseContent);

            MOCK_HTTP.When(BASE_URL + uri)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {MOCK_AUTHORIZATION.AccessToken}" }
                     })
                     .Respond(response);
        }

        public static void SetupMockResponseWithOAuth(string uri, string responseContent, TraktAuthorization authorization)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();

            MOCK_TEST_CLIENT.Authorization = authorization;

            uri.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + uri)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {authorization.AccessToken}" }
                     })
                     .Respond("application/json", responseContent);
        }

        public static void SetupMockResponseWithOAuthWithToken(string uri, string responseContent, string accessToken)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();

            uri.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + uri)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {accessToken}" }
                     })
                     .Respond("application/json", responseContent);
        }

        public static void SetupMockResponseWithOAuth(string uri, string requestContent, string responseContent)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();
            MOCK_AUTHORIZATION.Should().NotBeNull();
            MOCK_AUTHORIZATION.AccessToken.Should().NotBeNullOrEmpty();

            MOCK_TEST_CLIENT.Authorization = MOCK_AUTHORIZATION;

            uri.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + uri)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {MOCK_AUTHORIZATION.AccessToken}" }
                     })
                     .WithContent(requestContent)
                     .Respond("application/json", responseContent);
        }

        public static void SetupMockResponseWithOAuth(string uri, HttpStatusCode httpStatusCode)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();
            MOCK_AUTHORIZATION.Should().NotBeNull();
            MOCK_AUTHORIZATION.AccessToken.Should().NotBeNullOrEmpty();

            MOCK_TEST_CLIENT.Authorization = MOCK_AUTHORIZATION;

            uri.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + uri)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {MOCK_AUTHORIZATION.AccessToken}" }
                     })
                     .Respond(httpStatusCode);
        }

        public static void SetupMockResponseWithOAuth(string uri, HttpStatusCode httpStatusCode, TraktAuthorization authorization)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();

            MOCK_TEST_CLIENT.Authorization = authorization;

            uri.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + uri)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {authorization.AccessToken}" }
                     })
                     .Respond(httpStatusCode);
        }

        public static void SetupMockResponseWithOAuthWithToken(string uri, HttpStatusCode httpStatusCode, string accessToken)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();

            uri.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + uri)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {accessToken}" }
                     })
                     .Respond(httpStatusCode);
        }

        public static void AddMockExpectationResponse(string url, string requestContent, string responseContent)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();

            url.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.Expect(BASE_URL + url)
                     .WithContent(requestContent)
                     .Respond("application/json", responseContent);
        }

        public static void AddMockExpectationResponse(string uri, HttpStatusCode httpStatusCode, TraktAuthorization authorization)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();
            MOCK_TEST_CLIENT.Should().NotBeNull();

            MOCK_TEST_CLIENT.Authorization = authorization;

            uri.Should().NotBeNullOrEmpty();

            MOCK_HTTP.Expect(BASE_URL + uri)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {authorization.AccessToken}" }
                     })
                     .Respond(httpStatusCode);
        }

        public static void VerifyNoOutstandingExceptations()
        {
            MOCK_HTTP.Should().NotBeNull();
            MOCK_HTTP.VerifyNoOutstandingExpectation();
        }

        public static void SetupMockAuthenticationErrorResponse(string url, string requestContent, string responseContent, HttpStatusCode httpStatusCode)
        {
            MOCK_HTTP.Clear();

            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();

            url.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + url)
                     .WithContent(requestContent)
                     .Respond(httpStatusCode, "application/json", responseContent);
        }

        public static void SetupMockAuthenticationErrorResponse(string url, HttpStatusCode httpStatusCode)
        {
            MOCK_HTTP.Clear();

            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();

            url.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + url).Respond(httpStatusCode);
        }

        public static void SetupMockAuthenticationTokenRevokeResponse(string url, string requestContent)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();

            url.Should().NotBeNullOrEmpty();
            requestContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + url)
                     .WithFormData(requestContent)
                     .WithHeaders(new Dictionary<string, string>
                     {
                         { "trakt-api-key", MOCK_TEST_CLIENT.ClientId },
                         { "trakt-api-version", "2" },
                         { "Authorization", $"Bearer {MOCK_TEST_CLIENT.Authorization.AccessToken}" }
                     })
                     .Respond(HttpStatusCode.OK);
        }

        public static void SetupMockAuthenticationResponse(string url, string requestContent, string responseContent, HttpStatusCode httpStatusCode)
        {
            MOCK_HTTP.Should().NotBeNull();
            BASE_URL.Should().NotBeNullOrEmpty();

            url.Should().NotBeNullOrEmpty();
            responseContent.Should().NotBeNullOrEmpty();

            MOCK_HTTP.When(BASE_URL + url)
                     .WithContent(requestContent)
                     .Respond(httpStatusCode, "application/json", responseContent);
        }

        internal static Stream ToStream(this string str)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);

            streamWriter.Write(str);
            streamWriter.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
