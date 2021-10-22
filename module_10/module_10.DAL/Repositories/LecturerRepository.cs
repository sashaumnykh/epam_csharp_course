using System;
using System.Collections.Generic;
using System.Linq;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using module_10.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace module_10.DAL.Repositories
{
    public class LecturerRepository : IRepository<Lecturer>
    {
        private MyContext db;

        public LecturerRepository(MyContext context)
        {
            db = context;
        }
        public void Create(Lecturer item)
        {
            db.Lecturers.Add(item);
            db.SaveChanges();
        }

        public void Delete(Lecturer item)
        {
            var lecturer = db.Lecturers.Find(item.ID);
            if (lecturer != null)
            {
                db.Lecturers.Remove(lecturer);
            }
            db.SaveChanges();
        }

        public Lecturer Get(int id)
        {
            return db.Lecturers.Find(id);
        }

        public IEnumerable<Lecturer> GetAll()
        {
            return db.Lecturers.ToList();
        }

        public void Update(Lecturer item)
        {
            var lecturer = db.Lecturers.Find(item.ID);
            lecturer.Name = item.Name;
            lecturer.PhoneNumber = item.PhoneNumber;
            lecturer.Email = item.Email;
            db.SaveChanges();
        }
    }
}
