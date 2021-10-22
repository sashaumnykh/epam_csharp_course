using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using module_10.DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using module_10.DAL.Entities;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace module_10.BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        private IUnitOfWork _database { get; set; }
        private IMapper _mapper;
        private readonly ILogger<AttendanceService> _logger;

        public AttendanceService(IUnitOfWork uow, IMapper mapper, ILogger<AttendanceService> logger)
        {
            _database = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public void Create(AttendanceDTO attendance)
        {
            Attendance att = _mapper.Map<AttendanceDTO, Attendance>(attendance);

            if (_database.Lectures.Get(attendance.LectureID) == null)
            {
                //throw LecturerIsNotCreatedException();
            }
            if (_database.Students.Get(attendance.StudentID) == null)
            {
                // throw StudentIsNotCreatedException();
            }

            if (attendance.isPresent == false || attendance.isHwDone == false)
            {
                var lecture = _database.Lectures.Get(attendance.LectureID);
                var homeworks = from hw in _database.Homeworks.GetAll()
                                where hw.LectureID == attendance.LectureID
                                select hw;
                foreach (var hw in homeworks)
                {
                    hw.Mark = 0;
                }
            }

            _database.Attendances.Create(att);
            _database.Save();
        }

        public AttendanceDTO Get(int id)
        {
            _logger.LogInformation("ServiceGetMethod was invoked. Start getting an object by its ID");
            return _mapper.Map<Attendance, AttendanceDTO>(
                _database.Attendances.Get(id));
        }

        public IEnumerable<AttendanceDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDTO>>(
                _database.Attendances.GetAll());
        }

        public void Update(AttendanceDTO att)
        {
            Attendance attendance = _mapper.Map<AttendanceDTO, Attendance>(att);

            if (_database.Lecturers.Get(attendance.LectureID) == null)
            {
                //throw LecturerIsNotCreatedException();
            }
            if (_database.Students.Get(attendance.StudentID) == null)
            {
                // throw StudentIsNotCreatedException();
            }

            _database.Attendances.Update(attendance);

            if (attendance.isPresent == false || attendance.isHwDone == false)
            {
                var lecture = _database.Lectures.Get(attendance.LectureID);
                var homeworks = from hw in _database.Homeworks.GetAll()
                               where hw.LectureID == attendance.LectureID
                               select hw;
                foreach(var hw in homeworks)
                {
                    hw.Mark = 0;
                }
            }

            _database.Save();
        }
        public void Delete(int id)
        {
            Attendance att = _database.Attendances.Get(id);
            _database.Attendances.Delete(att);
            _database.Save();
        }
    }
}