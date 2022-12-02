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

        public PostHub(ILikeService likeService, IDislikeService dislikeService, IPostService postService)
        {
            _likeService = likeService;
            _dislikeService = dislikeService;
            _postService = postService;
        }

        public async Task UpdateLikes(int postId, string userId)
        {
            bool liked;
            //add or remove like to db (remove if exists)
            if (_likeService.GetById(userId, postId) != null)
            {
                await _likeService.Delete(userId, postId);
                await _postService.UpdateLikesCount(postId, -1);
                liked = false;
            }
            else
            {
                if (_dislikeService.GetById(userId, postId) != null)
                {
                    await UpdateDislikes(postId, userId);
                }
                Like like = new Like { UserId = userId, PostId = postId, Date = DateTime.Now };

                await _likeService.Add(like);
                await _postService.UpdateLikesCount(postId, 1);
                liked = true;
            }

            int totalLikes = _postService.GetLikesCount(postId);
            await Clients.All.SendAsync("UpdateLikesInPage", totalLikes, postId, liked, userId);
        }


        public async Task UpdateDislikes(int postId, string userId)
        {
            bool disliked;
            //add or remove dislike to db (remove if exists)
            if (_dislikeService.GetById(userId, postId) != null)
            {
                await _dislikeService.Delete(userId, postId);
                await _postService.UpdateDislikesCount(postId, -1);
                disliked = false;
            }
            else
            {
                if (_likeService.GetById(userId, postId) != null)
                {
                    await UpdateLikes(postId, userId);
                }
                Dislike dislike = new Dislike { UserId = userId, PostId = postId, Date = DateTime.Now };

                await _dislikeService.Add(dislike);
                await _postService.UpdateDislikesCount(postId, 1);
                disliked = true;
            }

            int totalDislikes = _postService.GetDislikesCount(postId);
            await Clients.All.SendAsync("UpdateDislikesInPage", totalDislikes, postId, disliked, userId);
        }

    }
}
