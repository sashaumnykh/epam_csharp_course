using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;

namespace module_10.BLL.Services
{
    public class LectureService : ILectureService
    {
        private IUnitOfWork _database { get; set; }
        private IMapper _mapper;

        public LectureService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public void Create(LectureDTO lecture)
        {
            Lecture l = _mapper.Map<LectureDTO, Lecture>(lecture);
            _database.Lectures.Create(l);
            if (lecture.LecturerID != null && _database.Lecturers.Get((int)lecture.LecturerID) == null)
            {
                //throw LecturerNotCreatedException();
            }
            _database.Save();
        }

        public LectureDTO Get(int id)
        {
            return _mapper.Map<Lecture, LectureDTO>(
                _database.Lectures.Get(id));
        }

        public IEnumerable<LectureDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Lecture>, IEnumerable<LectureDTO>>(
                _database.Lectures.GetAll());

        }

        public void Update(LectureDTO lecture)
        {
            Lecture l = _mapper.Map<LectureDTO, Lecture>(lecture);
            if (lecture.LecturerID != null && _database.Lecturers.Get((int)lecture.LecturerID) == null)
            {
                //throw LecturerIsNotCreatedException();
            }
            _database.Lectures.Update(l);
            _database.Save();
        }

        public void Delete(int id)
        {
            Lecture l = _database.Lectures.Get(id);
            _database.Lectures.Delete(l);
            _database.Save();
        }
    }
}
