using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using CarePortal.API.Entities;
using CarePortal.Data.Models;
using CarePortal.Data.ViewModels;

namespace CarePortal.API.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly ApplicationDbContext _context;

        public CalendarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CalendarEventModel> GetEvents(string userId)
        {
            var query = $"EXEC GetEvents '{userId}'; ";

            List<CalendarEventModel> events = _context.CalendarEventModel.FromSql(query).ToList();

            return events;
        }

        public Calendar AddEvent(string doctorId, string patientId, DateTimeOffset startTime, DateTimeOffset endTime, string title, 
            string notes, int duration, int status, int type, bool success, bool isDelete, DateTimeOffset timestamp)
        {
            Calendar calendar = new Calendar();

            var query = $"EXEC AddCalendarEvent '{doctorId}','{patientId}','{startTime}','{endTime}','{title}','{notes}','{duration}','{status}','{type}','{success}','{isDelete}','{timestamp}'; ";

            calendar = _context.Calendar.FromSql(query).FirstOrDefault();

            return calendar;
        }

        public Calendar UpdateEvent(int calendarId, DateTimeOffset startTime, DateTimeOffset endTime, string title,
            string note, int duration, int status, int type, bool success, bool isDelete, DateTimeOffset timestamp)
        {
            Calendar calendar = new Calendar();

            var query = $"EXEC UpdateCalendarEvent '{calendarId}','{startTime}','{endTime}','{note}','{duration}','{status}','{type}','{success}','{isDelete}','{timestamp}'; ";

            calendar = _context.Calendar.FromSql(query).FirstOrDefault();

            return calendar;
        }

        public Calendar DeleteEvent(int calendarId, bool isDelete, DateTimeOffset timestamp)
        {
            Calendar calendar = new Calendar();

            var query = $"EXEC DeleteCalendarEvent '{calendarId}','{isDelete}','{timestamp}'; ";

            calendar = _context.Calendar.FromSql(query).FirstOrDefault();

            return calendar;
        }

        //public Calendar UpdateCalendarEvent(int eventId, string doctorId, string patientId, DateTimeOffset appointmentTime, string note,
        //   int duration, int status, int type, bool success, bool isDelete, DateTimeOffset timestamp)
        //{
        //    Calendar calendar = new Calendar();

        //    var query = $"EXEC UpdateCalendarEvent '{eventId}','{doctorId}','{patientId}','{appointmentTime}','{note}','{duration}','{status}','{type}','{success}','{isDelete}','{timestamp}'; ";

        //    calendar = _context.Calendar.FromSql(query).FirstOrDefault();

        //    return calendar;
        //}

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface ICalendarRepository : IDisposable
    {
        List<CalendarEventModel> GetEvents(string userId);

        Calendar AddEvent(string doctorId, string patientId, DateTimeOffset startTime, DateTimeOffset endTime, string title, string notes, int duration,
               int status, int type, bool success, bool isDelete, DateTimeOffset timestamp);

        Calendar UpdateEvent(int calendarId, DateTimeOffset startTime, DateTimeOffset endTime, string title,
            string note, int duration, int status, int type, bool success, bool isDelete, DateTimeOffset timestamp);

        Calendar DeleteEvent(int calendarId, bool isDelete, DateTimeOffset timestamp);

        //Calendar UpdateCalendarEvent(int eventId, string doctorId, string patientId, DateTimeOffset appointmentTime, string note, int duration,
        //       int status, int type, bool success, bool isDelete, DateTimeOffset timestamp);
    }
}
