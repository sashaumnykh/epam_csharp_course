using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using module_10.DAL.EF;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;

namespace module_10.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private MyContext db;
        private ILogger _logger;
        private IRepository<Attendance> _attendanceRepo;
        private AttendanceRepository _attendanceRepository;
        private HomeworkRepository _homeworkRepository;
        private LectureRepository _lectureRepository;
        private LecturerRepository _lecturerRepository;
        private StudentRepository _studentRepository;
        
        public EFUnitOfWork(MyContext context)
        {
            db = context;
            //_logger = logger;
        }
        

        public IRepository<Attendance> Attendances
        {
            get
            {
                if (_attendanceRepository == null)
                {
                    _attendanceRepository = new AttendanceRepository(db);
                }

                return _attendanceRepository;
            }
        }

        public IRepository<Homework> Homeworks
        {
            get
            {
                if (_homeworkRepository == null)
                {
                    _homeworkRepository = new HomeworkRepository(db);
                }

                return _homeworkRepository;
            }
        }

        public IRepository<Lecture> Lectures
        {
            get
            {
                if (_lectureRepository == null)
                {
                    _lectureRepository = new LectureRepository(db);
                }

                return _lectureRepository;
            }
        }

        public IRepository<Lecturer> Lecturers
        {
            get
            {
                if (_lecturerRepository == null)
                {
                    _lecturerRepository = new LecturerRepository(db);
                }

                return _lecturerRepository;
            }
        }

        public IRepository<Student> Students
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(db);
                }

                return _studentRepository;
            }
        }
        

        public void Save()
        {
            db.SaveChanges();
        }

        bool disposed = false;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                db.Dispose();
            }

            disposed = true;
        }
    }
}