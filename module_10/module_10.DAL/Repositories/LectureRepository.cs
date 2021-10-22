using System;
using System.Collections.Generic;
using System.Linq;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using module_10.DAL.EF;

namespace module_10.DAL.Repositories
{
    public class LectureRepository : IRepository<Lecture>
    {
        private MyContext db;

        public LectureRepository(MyContext context)
        {
            db = context;
        }
        public void Create(Lecture item)
        {
            db.Lectures.Add(item);
            db.SaveChanges();
        }

        public Lecture Get(int id)
        {
            return db.Lectures.Find(id);
        }

        public IEnumerable<Lecture> GetAll()
        {
            return db.Lectures.ToList();
        }

        public void Update(Lecture item)
        {
            Lecture lecture = db.Lectures.Find(item.ID);
            lecture.LecturerID = item.LecturerID;
            lecture.Name = item.Name;
            db.SaveChanges();
        }

        public void Delete(Lecture item)
        {
            db.Lectures.Remove(item);
            db.SaveChanges();
        }
    }
}
