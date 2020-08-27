using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CarePortal.API.Entities;
using CarePortal.Data.Models;
using CarePortal.Data.ViewModels;

namespace CarePortal.API.Repositories
{
    public class FeedRepository : IFeedRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Feed AddFeed(string userId, string data, bool isDelete, DateTimeOffset timestamp, int departmentId)
        {
            Feed feed = new Feed();

            var query = $"EXEC AddFeed '{userId}','{data}','{isDelete}','{timestamp}','{departmentId}'; ";

            feed = _context.Feed.FromSql(query).FirstOrDefault();

            return feed;
        }

        public List<FeedViewModel> GetFeedById(string userId, int pageNumber, int pageSize)
        {
            int startPage = pageNumber * pageSize;

            List<FeedViewModel> feedViewModel = new List<FeedViewModel>();

            var query = $"EXEC GetFeedById '{userId}','{startPage}','{pageSize}'; ";
            
            feedViewModel = _context.FeedViewModel.FromSql(query).ToList();

            return feedViewModel;
        }

        public List<CommentsViewModel> GetCommentsByFeedId(int feedId)
        {
            try
            {
                List<CommentsViewModel> commentsViewModel = new List<CommentsViewModel>();

                var query = $"EXEC GetCommentsByFeedId '{feedId}'; ";

                commentsViewModel = _context.CommentsViewModel.FromSql(query).ToList();

                return commentsViewModel;
            }
            catch(Exception ex)
            {
               throw ex;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IFeedRepository : IDisposable
    {
        Feed AddFeed(string userId, string data, bool isDelete, DateTimeOffset timestamp, int departmentId);
        List<FeedViewModel> GetFeedById(string userId, int pageNumber, int pageSize);
        List<CommentsViewModel> GetCommentsByFeedId(int feedId);
    }
}
