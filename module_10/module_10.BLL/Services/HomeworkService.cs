using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using module_10.DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using module_10.DAL.Entities;

namespace module_10.BLL.Services
{
    public class HomeworkService : IHomeworkService
    {
        private IUnitOfWork _database { get; set; }
        private IMapper _mapper;

        public HomeworkService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public void Create(HomeworkDTO hw)
        {
            Homework homework = _mapper.Map<HomeworkDTO, Homework>(hw);
            if (_database.Lecturers.Get(hw.LectureID) == null)
            {
                //throw LecturerIsNotCreatedException();
            }
            if (hw.StudentID != null && _database.Students.Get((int)hw.StudentID) == null)
            {
                // throw StudentIsNotCreatedException();
            }
            _database.Homeworks.Create(homework);

            _database.Save();
        }

        public HomeworkDTO Get(int id)
        {
            Homework hw = _database.Homeworks.Get(id);
            return _mapper.Map<Homework, HomeworkDTO>(hw);
        }

        public IEnumerable<HomeworkDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Homework>, IEnumerable<HomeworkDTO>>(
                _database.Homeworks.GetAll());
        }

        public void Update(HomeworkDTO hw)
        {
            Homework homework = _mapper.Map<HomeworkDTO, Homework>(hw);
            if (_database.Lecturers.Get(hw.LectureID) == null)
            {
                //throw LecturerIsNotCreatedException();
            }
            if (hw.StudentID != null && _database.Students.Get((int)hw.StudentID) == null)
            {
                // throw StudentIsNotCreatedException();
            }
            _database.Homeworks.Update(homework);
            _database.Save();
        }
        public void Delete(int id)
        {
            Homework hw = _database.Homeworks.Get(id);
            _database.Homeworks.Delete(hw);
            _database.Save();
        }
    }
}
