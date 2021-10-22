using System;
using System.Collections.Generic;
using System.Linq;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using module_10.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace module_10.DAL.Repositories
{
    public class HomeworkRepository : IRepository<Homework>
    {
        private MyContext db;

        public HomeworkRepository(MyContext context)
        {
            db = context;
        }
        public void Create(Homework item)
        {
            db.Homeworks.Add(item);
            db.SaveChanges();
        }

        public Homework Get(int id)
        {
            return db.Homeworks.Find(id);
        }

        public IEnumerable<Homework> GetAll()
        {
            return db.Homeworks.ToList();
        }

        public void Update(Homework item)
        {
            var hw = db.Homeworks.Find(item.ID);
            hw.LectureID = item.LectureID;
            hw.Mark = item.Mark;
            hw.StudentID = item.StudentID;
            db.SaveChanges();
        }
        public void Delete(Homework item)
        {
            Homework hw = db.Homeworks.Find(item.ID);
            if (hw != null)
            {
                db.Homeworks.Remove(hw);
            }
            db.SaveChanges();
        }
    }
}
