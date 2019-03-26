﻿namespace TraktNet.Tests.Objects.Basic.Json.Writer
{
    using FluentAssertions;
    using System;
    using System.Threading.Tasks;
    using Traits;
    using TraktNet.Extensions;
    using TraktNet.Objects.Basic;
    using TraktNet.Objects.Basic.Json.Writer;
    using TraktNet.Objects.Get.Users;
    using Xunit;

    [Category("Objects.Basic.JsonWriter")]
    public partial class CommentObjectJsonWriter_Tests
    {
        [Fact]
        public void Test_CommentObjectJsonWriter_WriteObject_Object_Exceptions()
        {
            var traktJsonWriter = new CommentObjectJsonWriter();
            Func<Task<string>> action = () => traktJsonWriter.WriteObjectAsync(default);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Empty()
        {
            ITraktComment traktComment = new TraktComment();

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":false,""review"":false}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_Id_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                Id = 76957U
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":76957,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":false,""review"":false}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_ParentId_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                ParentId = 1234U
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""parent_id"":1234,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":false,""review"":false}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_CreatedAt_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                CreatedAt = CREATED_UPDATED_AT
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0," +
                             $"\"created_at\":\"{CREATED_UPDATED_AT.ToTraktLongDateTimeString()}\"," +
                             @"""spoiler"":false,""review"":false}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_UpdatedAt_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                UpdatedAt = CREATED_UPDATED_AT
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             $"\"updated_at\":\"{CREATED_UPDATED_AT.ToTraktLongDateTimeString()}\"," +
                             @"""spoiler"":false,""review"":false}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_Comment_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                Comment = "I hate they made The flash a kids show. Could else be much better. And with a better flash offcourse."
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""comment"":""I hate they made The flash a kids show. Could else be much better. And with a better flash offcourse.""," +
                             @"""spoiler"":false,""review"":false}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_Spoiler_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                Spoiler = true
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":true,""review"":false}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_Review_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                Review = true
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":false,""review"":true}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_Replies_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                Replies = 1
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":false,""review"":false,""replies"":1}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_Likes_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                Likes = 2
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":false,""review"":false,""likes"":2}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_UserRating_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                UserRating = 7.3f
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":false,""review"":false,""user_rating"":7.3}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Only_User_Property()
        {
            ITraktComment traktComment = new TraktComment
            {
                User = new TraktUser
                {
                    Username = "sean",
                    IsPrivate = false,
                    Name = "Sean Rudford",
                    IsVIP = true,
                    IsVIP_EP = true,
                    Ids = new TraktUserIds
                    {
                        Slug = "sean"
                    }
                }
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":0,""created_at"":""0001-01-01T00:00:00.000Z""," +
                             @"""spoiler"":false,""review"":false," +
                             @"""user"":{""username"":""sean"",""private"":false,""ids"":{""slug"":""sean""}," +
                             @"""name"":""Sean Rudford"",""vip"":true,""vip_ep"":true}}");
        }

        [Fact]
        public async Task Test_CommentObjectJsonWriter_WriteObject_Object_Complete()
        {
            ITraktComment traktComment = new TraktComment
            {
                Id = 76957U,
                ParentId = 1234U,
                CreatedAt = CREATED_UPDATED_AT,
                UpdatedAt = CREATED_UPDATED_AT,
                Comment = "I hate they made The flash a kids show. Could else be much better. And with a better flash offcourse.",
                Spoiler = true,
                Review = true,
                Replies = 1,
                Likes = 2,
                UserRating = 7.3f,
                User = new TraktUser
                {
                    Username = "sean",
                    IsPrivate = false,
                    Name = "Sean Rudford",
                    IsVIP = true,
                    IsVIP_EP = true,
                    Ids = new TraktUserIds
                    {
                        Slug = "sean"
                    }
                }
            };

            var traktJsonWriter = new CommentObjectJsonWriter();
            string json = await traktJsonWriter.WriteObjectAsync(traktComment);
            json.Should().Be(@"{""id"":76957,""parent_id"":1234," +
                             $"\"created_at\":\"{CREATED_UPDATED_AT.ToTraktLongDateTimeString()}\"," +
                             $"\"updated_at\":\"{CREATED_UPDATED_AT.ToTraktLongDateTimeString()}\"," +
                             @"""comment"":""I hate they made The flash a kids show. Could else be much better. And with a better flash offcourse.""," +
                             @"""spoiler"":true,""review"":true,""replies"":1,""likes"":2,""user_rating"":7.3," +
                             @"""user"":{""username"":""sean"",""private"":false,""ids"":{""slug"":""sean""}," +
                             @"""name"":""Sean Rudford"",""vip"":true,""vip_ep"":true}}");
        }
    }
}
