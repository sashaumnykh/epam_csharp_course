using System;
using System.Collections.Generic;
using System.Linq;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using module_10.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace module_10.DAL.Repositories
{
    public class AttendanceRepository : IRepository<Attendance>
    {
        private MyContext db;
        private readonly ILogger _logger;

        public AttendanceRepository(MyContext context)
        //public AttendanceRepository(MyContext context, ILogger logger)
        {
            db = context;
            //_logger = logger;
        }

        public IEnumerable<Attendance> GetAll()
        {
            return db.Attendances.ToList();
        }

        public Attendance Get(int id)
        {
            //_logger.LogInformation("RepoGetMethod was invoked. Start getting an object by its ID");
            return db.Attendances.Find(id);
        }

        public void Create(Attendance att)
        {
            db.Attendances.Add(att);
            db.SaveChanges();
        }

        public void Update(Attendance att)
        {
            var attendance = db.Attendances.Find(att.ID);
            attendance.isPresent = att.isPresent;
            attendance.StudentID = att.StudentID;
            attendance.LectureID = att.LectureID;
            attendance.isHwDone = att.isHwDone;
            db.SaveChanges();
        }

        public void Delete(Attendance att)
        {
            Attendance attendance = db.Attendances.Find(att.ID);
            if (attendance != null)
            {
                db.Attendances.Remove(attendance);
            }
            db.SaveChanges();
        }
    }
}