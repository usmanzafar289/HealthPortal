using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CarePortal.API.Entities;
using CarePortal.Data.Models;
using CarePortal.Data.ViewModels;

namespace CarePortal.API.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Conversation AddQuestion(string doctorId, string patientId, string title, string message, int messageType, int category, bool isDelete, DateTimeOffset timestamp)
        {
            Conversation conversation = new Conversation();

            var query = $"EXEC AddQuestion '{doctorId}','{patientId}','{title}','{message}','{messageType}','{category}','{isDelete}','{timestamp}'; ";

            conversation = _context.Conversation.FromSql(query).FirstOrDefault();

            return conversation;
        }

        public List<ConversationViewModel> GetQuestionsByUserId(string userId, bool isDoctor)
        {
            List<ConversationViewModel> listConversationViewModel = new List<ConversationViewModel>();

            var query = $"EXEC GetQuestionsByUserId '{userId}','{isDoctor}'; ";

            listConversationViewModel = _context.ConversationViewModel.FromSql(query).ToList();

            return listConversationViewModel;
        }

        public ConversationViewModel AddConversation(int conversationId, string doctorId, string patientId, string title, string message,
            int messageType, int category, bool isDelete, DateTimeOffset timestamp)
        {
            ConversationViewModel conversation = new ConversationViewModel();

            var query = $"EXEC AddConversation '{conversationId}','{doctorId}','{patientId}','{title}','{message}','{messageType}','{category}','{isDelete}','{timestamp}'; ";

            conversation = _context.ConversationViewModel.FromSql(query).FirstOrDefault();

            return conversation;
        }


        public List<ConversationViewModel> GetConversation(int questionId)
        {
            List<ConversationViewModel> listConversationViewModel = new List<ConversationViewModel>();

            var query = $"EXEC GetConversation '{questionId}'; ";

            listConversationViewModel = _context.ConversationViewModel.FromSql(query).ToList();

            return listConversationViewModel;
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IConversationRepository : IDisposable
    {
        Conversation AddQuestion(string doctorId, string patientId, string title, string message, int messageType, int category, bool isDelete, DateTimeOffset timestamp);
        List<ConversationViewModel> GetQuestionsByUserId(string userId, bool isDoctor);
        ConversationViewModel AddConversation(int conversationId, string doctorId, string patientId, string title, string message,
            int messageType, int category, bool isDelete, DateTimeOffset timestamp);
        List<ConversationViewModel> GetConversation(int questionId);
    }
}
