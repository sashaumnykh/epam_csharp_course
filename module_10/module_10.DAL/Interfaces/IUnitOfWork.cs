using System;
using System.Collections.Generic;
using module_10.DAL.Entities;

namespace module_10.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Attendance> Attendances { get; }
        IRepository<Homework> Homeworks { get; }
        IRepository<Lecture> Lectures { get; }
        IRepository<Lecturer> Lecturers { get; }
        IRepository<Student> Students { get; }
        void Save();
    }
}