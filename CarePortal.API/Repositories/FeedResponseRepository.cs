using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CarePortal.API.Entities;
using CarePortal.Data.Models;

namespace CarePortal.API.Repositories
{
    public class FeedResponseRepository : IFeedResponseRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedResponseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public FeedResponse AddFeedResponse(string userId, int feedId, int response, DateTimeOffset timestamp)
        {
            FeedResponse feedResponse = new FeedResponse();

            var query = $"EXEC AddFeedResponse '{userId}','{feedId}','{response}','{timestamp}'; ";

            feedResponse = _context.FeedResponse.FromSql(query).FirstOrDefault();

            return feedResponse;
        }
        public Comment AddComment(string id, int feedId, string newComment, DateTimeOffset timestamp) {
            try
            {
                Comment comment = new Comment();
                var query = $"EXEC AddComment '{id}','{feedId}','{newComment}','{timestamp}'; ";

                comment = _context.Comment.FromSql(query).FirstOrDefault();

                return comment;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IFeedResponseRepository : IDisposable
    {
        FeedResponse AddFeedResponse(string userId, int feedId, int response, DateTimeOffset timestamp);
        Comment AddComment(string id, int feedId, string newComment, DateTimeOffset timestamp);
    }
}
