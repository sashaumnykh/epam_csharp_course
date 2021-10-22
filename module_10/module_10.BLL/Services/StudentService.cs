using System.Collections.Generic;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using AutoMapper;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using System.Linq;
using System;

namespace module_10.BLL.Services
{
    public class StudentService : IStudentService
    {
        private IUnitOfWork _database { get; set; }
        private IMapper _mapper;
        private IEmailSender _emailSender;

        public StudentService(IUnitOfWork uow, IMapper mapper, IEmailSender sender)
        {
            _database = uow;
            _mapper = mapper;
            _emailSender = sender;
        }

        public void Create(StudentDTO student)
        {
            Student s = _mapper.Map<StudentDTO, Student>(student);
            _database.Students.Create(s);
            _database.Save();
        }

        public IEnumerable<StudentDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(
                _database.Students.GetAll());
        }

        public StudentDTO Get(int id)
        {
            return _mapper.Map<Student, StudentDTO>(
                _database.Students.Get(id));
        }

        public void Update(StudentDTO student)
        {
            Student s = _mapper.Map<StudentDTO, Student>(student);
            _database.Students.Update(s);
            _database.Save();
        }
        public void Delete(int id)
        {
            Student s = _database.Students.Get(id);
            _database.Students.Delete(s);
            _database.Save();
        }
        public void CheckAttendance(StudentDTO student)
        {
            var lecturersIDs = from lecturers in _database.Lecturers.GetAll()
                               select lecturers.ID;
            var skippedLectures = from att in _database.Attendances.GetAll()
                                  where att.StudentID == student.ID && att.isPresent == false
                                  select att;
            foreach (int id in lecturersIDs)
            {
                var lectures = from sl in skippedLectures
                               where _database.Lectures.Get(sl.LectureID).LecturerID == id
                               select sl;
                int skippedLecturesCount = lectures.Count();
                if (skippedLecturesCount > 3)
                {
                    string toLecturerText = String.Format("Студентом по имени {0} было пропущено более 3-х лекций, а именно - {1}.", student.Name, skippedLecturesCount);
                    var lecturerEmail = _database.Lecturers.Get(id).Email;
                    _emailSender.SendEmail(lecturerEmail, toLecturerText);
                    string toStudentText = String.Format("Вами было пропущено более 3-х лекций, а именно - {0}.", skippedLecturesCount);
                    _emailSender.SendEmail(student.Email, toStudentText);
                }
            }
            
            
        }

        public void CheckAverageScore(StudentDTO student)
        {
            var marks = from hw in _database.Homeworks.GetAll()
                        where hw.StudentID == student.ID && hw.Mark != null
                        select hw.Mark;
            double? averageScore = marks.Average();
            if (averageScore < 4)
            {
                string message = String.Format("Ваш средний бал {0} меньше 4х", averageScore);
                // SendSMS(student.PhoneNumber, message);
            }
        }
    }
}