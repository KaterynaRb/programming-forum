using Microsoft.AspNetCore.SignalR;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.Hosting;
using BLL;

namespace ProgrammingForum_ASPNETCore.Hubs
{
    public class PostHub : Hub
    {
        private readonly ILikeService _likeService;
        private readonly IDislikeService _dislikeService;
        private readonly IPostService _postService;
        private readonly IPostReplyService _postReplyService;

        public PostHub(ILikeService likeService, IDislikeService dislikeService, IPostService postService, IPostReplyService postReplyService)
        {
            _likeService = likeService;
            _dislikeService = dislikeService;
            _postService = postService;
            _postReplyService= postReplyService;
        }

        public async Task UpdateLikes(int postId, string userId)
        {
            bool liked;
            if (_likeService.GetByPostAndUser(userId, postId) != null)
            {
                await _likeService.DeleteOnPost(userId, postId);
                liked = false;
            }
            else
            {
                if (_dislikeService.GetByPostAndUser(userId, postId) != null)
                {
                    await UpdateDislikes(postId, userId);
                }
                Like like = new Like { UserId = userId, PostId = postId, Date = DateTime.Now };

                await _likeService.Add(like);
                liked = true;
            }

            int totalLikes = _postService.GetLikesCount(postId);
            await _postService.UpdateLikesCount(postId, totalLikes);
            await Clients.All.SendAsync("UpdateLikesInPage", totalLikes, postId, liked, userId);
        }


        public async Task UpdateDislikes(int postId, string userId)
        {
            bool disliked;
            if (_dislikeService.GetByPostAndUser(userId, postId) != null)
            {
                await _dislikeService.DeleteOnPost(userId, postId);
                disliked = false;
            }
            else
            {
                if (_likeService.GetByPostAndUser(userId, postId) != null)
                {
                    await UpdateLikes(postId, userId);
                }
                Dislike dislike = new Dislike { UserId = userId, PostId = postId, Date = DateTime.Now };

                await _dislikeService.Add(dislike);
                disliked = true;
            }

            int totalDislikes = _postService.GetDislikesCount(postId);
            await _postService.UpdateDislikesCount(postId, totalDislikes);

            await Clients.All.SendAsync("UpdateDislikesInPage", totalDislikes, postId, disliked, userId);
        }


        public async Task UpdateLikesOnReply(int postId, int postreplyId, string userId)
        {
            bool liked;
            if (_likeService.GetByPostReplyAndUser(userId, postreplyId) != null)
            {
                await _likeService.DeleteOnPostReply(userId, postreplyId);
                liked = false;
            }
            else
            {
                if (_dislikeService.GetByPostReplyAndUser(userId, postreplyId) != null)
                {
                    await UpdateDislikesOnReply(postId, postreplyId, userId);
                }
                Like like = new Like { UserId = userId, PostId = postId, PostReplyId = postreplyId, Date = DateTime.Now };

                await _likeService.Add(like);
                liked = true;
            }

            int totalLikes = _postReplyService.GetLikesCount(postreplyId);
            await _postReplyService.UpdateLikesCount(postreplyId, totalLikes);

            await Clients.All.SendAsync("UpdateLikesOnReplyInPage", totalLikes, postId, postreplyId, liked, userId);
        }

        public async Task UpdateDislikesOnReply(int postId, int postreplyId, string userId)
        {
            bool disliked;
            if (_dislikeService.GetByPostReplyAndUser(userId, postreplyId) != null)
            {
                await _dislikeService.DeleteOnPostReply(userId, postreplyId);
                disliked = false;
            }
            else
            {
                if (_likeService.GetByPostReplyAndUser(userId, postreplyId) != null)
                {
                    await UpdateLikesOnReply(postId, postreplyId, userId);
                }
                Dislike dislike = new Dislike { UserId = userId, PostId = postId, PostReplyId = postreplyId, Date = DateTime.Now };

                await _dislikeService.Add(dislike);
                disliked = true;
            }

            int totalDislikes = _postReplyService.GetDislikesCount(postreplyId);
            await _postReplyService.UpdateDislikesCount(postreplyId, totalDislikes);

            await Clients.All.SendAsync("UpdateDislikesOnReplyInPage", totalDislikes, postId, postreplyId, disliked, userId);
        }
    }
}
