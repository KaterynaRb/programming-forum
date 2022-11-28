using Microsoft.AspNetCore.SignalR;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.Hosting;

namespace ProgrammingForum_ASPNETCore.Hubs
{
    public class PostHub : Hub
    {
        private readonly AppDbContext _context;

        public PostHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task UpdateLikes(int postId, string userId)
        {
            bool liked;
            //add or remove like to db (remove if exists)
            if (_context.Likes.Find(userId, postId) != null)
            {
                Like like = await _context.Likes.FindAsync(userId, postId);
                _context.Likes.Remove(like);
                _context.SaveChanges();

                //update likesCount in Posts table
                Post post = await _context.Posts.FindAsync(postId);
                post.LikesCount -= 1;
                await _context.SaveChangesAsync();

                liked = false;
            }
            else
            {
                if (_context.Dislikes.Find(userId, postId) != null)
                {
                    await UpdateDislikes(postId, userId);
                }
                Like like = new Like { UserId = userId, PostId = postId, Date = DateTime.Now };
                await _context.Likes.AddAsync(like);
                await _context.SaveChangesAsync();

                //update likesCount in Posts table
                Post post = await _context.Posts.FindAsync(postId);
                post.LikesCount += 1;
                await _context.SaveChangesAsync();
                liked = true;
            }

            int totalLikes = _context.Likes.Where(p => p.PostId == postId).Count();

            await Clients.All.SendAsync("UpdateLikesInPage", totalLikes, postId, liked, userId);
        }


        public async Task UpdateDislikes(int postId, string userId)
        {
            bool disliked;
            //add or remove like to db (remove if exists)
            if (_context.Dislikes.Find(userId, postId) != null)
            {
                Dislike dislike = await _context.Dislikes.FindAsync(userId, postId);
                _context.Dislikes.Remove(dislike);
                _context.SaveChanges();

                //update likesCount in Posts table
                Post post = await _context.Posts.FindAsync(postId);
                post.DislikesCount -= 1;
                await _context.SaveChangesAsync();

                disliked = false;
            }
            else
            {
                if (_context.Likes.Find(userId, postId) != null)
                {
                    await UpdateLikes(postId, userId);
                }
                Dislike dislike = new Dislike { UserId = userId, PostId = postId, Date = DateTime.Now };
                await _context.Dislikes.AddAsync(dislike);
                await _context.SaveChangesAsync();

                //update likesCount in Posts table
                Post post = await _context.Posts.FindAsync(postId);
                post.DislikesCount += 1;
                await _context.SaveChangesAsync();
                disliked = true;
            }

            int totalDislikes = _context.Dislikes.Where(p => p.PostId == postId).Count();

            await Clients.All.SendAsync("UpdateDislikesInPage", totalDislikes, postId, disliked, userId);
        }

    }
}
