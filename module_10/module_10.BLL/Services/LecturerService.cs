using System.Collections.Generic;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using AutoMapper;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;

namespace module_10.BLL.Services
{
    public class LecturerService : ILecturerService
    {
        private IUnitOfWork _database { get; set; }
        private IMapper _mapper;

        public LecturerService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public void Create(LecturerDTO person)
        {
            Lecturer l = _mapper.Map<LecturerDTO, Lecturer>(person);
            _database.Lecturers.Create(l);
            _database.Save();
        }

        public IEnumerable<LecturerDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Lecturer>, IEnumerable<LecturerDTO>>(_database.Lecturers.GetAll());
        }

        public LecturerDTO Get(int id)
        {
            return _mapper.Map<Lecturer, LecturerDTO>(
                _database.Lecturers.Get(id));
        }

        public void Update(LecturerDTO lecturer)
        {
            Lecturer l = _mapper.Map<LecturerDTO, Lecturer>(lecturer);
            _database.Lecturers.Update(l);
            _database.Save();
        }

        public void Delete(int id)
        {
            Lecturer l = _database.Lecturers.Get(id);
            _database.Lecturers.Delete(l);
            _database.Save();
        }
    }
}