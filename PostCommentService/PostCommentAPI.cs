using Grpc.Core;
using Microsoft.Extensions.Logging;
using PostCommentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCommentService
{
    public class PostCommentAPI: PostCommentService.PostCommentServiceBase
    {

        private readonly ILogger<PostCommentAPI> _logger;
        public PostCommentAPI (ILogger<PostCommentAPI> logger)
        {
            _logger = logger;
        }

        public override Task AddPost(Post post, ServerCallContext context)
        {
            using (var ctx = new PostCommentEntities())
            {
                ctx.Posts.Add(post);
                return Task(new Empty());
            }
        }

        public override Task<Post> UpdatePost(Post post, ServerCallContext context)
        {
            using (var ctx = new PostCommentEntities())
            {
                ctx.ChangeTracker.DetectChanges();
                var postToUpdate = ctx.Posts.Find(post);
                postToUpdate = post;
                return Task.FromResult(postToUpdate);
            }
        }

        public Task DeletePost(Post post, ServerCallContext context)
        {
            using (var ctx = new PostCommentEntities())
            {
                ctx.Posts.Remove(post);
                return Task.CompletedTask;
            }
        }

        public Task<Post> GetPostById(int id, ServerCallContext context)
        {
            using (var ctx = new PostCommentEntities())
            {
                return Task.FromResult(ctx.Posts.Find(id));
            }
        }

        public Task<List<Post>> GetPosts(ServerCallContext context)
        {
            using (var ctx = new PostCommentEntities())
            {
                return Task.FromResult(ctx.Posts.ToList());
            }
        }

        public Task AddComment(Comment comment, ServerCallContext context)
        {
            using (var context = new PostCommentEntities())
            {
                context.Comments.Add(comment);
                return Task.CompletedTask;
            }
        }

        public Task<Comment> UpdateComment(Comment comment, ServerCallContext context)
        {
            using (var ctx = new PostCommentEntities())
            {
                ctx.ChangeTracker.DetectChanges();
                var commentToUpdate = ctx.Comments.Find(comment);
                commentToUpdate = comment;
                return Task.FromResult(commentToUpdate);
            }
        }

        public  Task<Comment> GetCommentById(int id, ServerCallContext context)
        {
            using (var ctx = new PostCommentEntities())
            {
                return Task.FromResult(ctx.Comments.Find(id));
            }
        }
    }
}
