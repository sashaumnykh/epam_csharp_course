using System;
using System.Collections.Generic;
using System.Linq;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using module_10.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace module_10.DAL.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private MyContext db;

        public StudentRepository(MyContext context)
        {
            db = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students.ToList();
        }

        public Student Get(int id)
        {
            return db.Students.Find(id);
        }

        public void Create(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void Update(Student s)
        {
            var student = db.Students.Find(s.ID);
            student.Name = s.Name;
            student.Email = s.Email;
            student.PhoneNumber = s.PhoneNumber;
            db.SaveChanges();
        }

        public void Delete(Student s)
        {
            Student student = db.Students.Find(s.ID);
            if (student != null)
            {
                db.Students.Remove(student);
            }
            db.SaveChanges();
        }
    }
}