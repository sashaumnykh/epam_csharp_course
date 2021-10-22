using NUnit.Framework;
using Moq;
using module_10.BLL.Services;
using module_10.DAL.Interfaces;
using AutoMapper;
using module_10.BLL.DTO;
using System;
using module_10.DAL.Entities;
using System.Collections.Generic;

namespace Tests.ServicesTests
{
    public class AttendanceServiceTests
    {
        private Mock<IUnitOfWork> mockDatabase;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mockDatabase = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Attendance, AttendanceDTO>();
                cfg.CreateMap<AttendanceDTO, Attendance>();
            });
            mapper = new Mapper(config);
        }

        [Test]
        public void Create_ServiceMethod_Invokes_RepoMethodCreate_Test()
        {
            AttendanceDTO attendanceDTO = new AttendanceDTO();
            attendanceDTO.LectureID = 1;
            attendanceDTO.StudentID = 2;

            mockDatabase.Setup(x => x.Attendances.Create(It.IsAny<Attendance>())).Verifiable();
            mockDatabase.Setup(x => x.Lectures.Get(It.IsAny<int>())).Returns(new Lecture());
            mockDatabase.Setup(x => x.Homeworks.Get(It.IsAny<int>())).Returns(new Homework());

            var AttendanceService = new AttendanceService(mockDatabase.Object, mapper);

            AttendanceService.Create(attendanceDTO);

            mockDatabase.Verify(x => x.Attendances.Create(It.IsAny<Attendance>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsTheObject_Test()
        {
            AttendanceDTO attendanceDTO = new AttendanceDTO();
            attendanceDTO.LectureID = 1;
            attendanceDTO.StudentID = 2;

            Attendance attendance = new Attendance();
            attendance.LectureID = attendanceDTO.LectureID;
            attendance.StudentID = attendanceDTO.StudentID;

            mockDatabase.Setup(x => x.Attendances.Get(It.IsAny<int>())).Returns(attendance);

            var AttendanceService = new AttendanceService(mockDatabase.Object, mapper);

            var result = AttendanceService.Get(10);
            Assert.AreEqual(attendanceDTO.LectureID, result.LectureID);
            Assert.AreEqual(attendanceDTO.StudentID, result.StudentID);
        }

        [Test]
        public void Get_ServiceMethod_Invokes_RepoMethodGet_Test()
        {
            mockDatabase.Setup(x => x.Attendances.Get(It.IsAny<int>())).Verifiable();

            var AttendanceService = new AttendanceService(mockDatabase.Object, mapper);

            AttendanceService.Get(new Random().Next());

            mockDatabase.Verify(x => x.Attendances.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects_Test()
        {
            List<Attendance> attendances = new List<Attendance>
            {
                new Attendance { LectureID = 3, StudentID = 5, isPresent = true},
                new Attendance { LectureID = 3, StudentID = 4, isPresent = false},
            };

            List<AttendanceDTO> attendancesDTO = new List<AttendanceDTO>
            {
                new AttendanceDTO { LectureID = 3, StudentID = 5, isPresent = true},
                new AttendanceDTO { LectureID = 3, StudentID = 4, isPresent = false},
            };

            mockDatabase.Setup(x => x.Attendances.GetAll()).Returns(attendances);

            var AttendanceService = new AttendanceService(mockDatabase.Object, mapper);

            List<AttendanceDTO> gotAttendances = new List<AttendanceDTO>(AttendanceService.GetAll());

            Assert.AreEqual(attendancesDTO.Count, gotAttendances.Count);
            for (int i = 0; i < attendancesDTO.Count; i++)
            {
                Assert.AreEqual(attendancesDTO[i].LectureID, gotAttendances[i].LectureID);
                Assert.AreEqual(attendancesDTO[i].StudentID, gotAttendances[i].StudentID);
                Assert.AreEqual(attendancesDTO[i].isPresent, gotAttendances[i].isPresent);
            }
        }


        [Test]
        public void Update_ServiceMethod_Invokes_RepoMethodUpdate_Test()
        {
            AttendanceDTO attendanceDTO = new AttendanceDTO();
            attendanceDTO.LectureID = 7;
            attendanceDTO.StudentID = 1;
            attendanceDTO.isPresent = true;
            attendanceDTO.isHwDone = false;

            var calls = 0;

            mockDatabase.Setup(x => x.Attendances.Update(It.IsAny<Attendance>())).Callback(() => calls++).Verifiable();

            var AttendanceService = new AttendanceService(mockDatabase.Object, mapper);

            AttendanceService.Update(attendanceDTO);

            Assert.That(calls > 0);
        }

        [Test]
        public void Delete_ServiceMethod_Invokes_RepoMethodDelete_Test()
        {
            AttendanceDTO attendanceDTO = new AttendanceDTO();
            attendanceDTO.LectureID = 4;
            attendanceDTO.StudentID = 8;
            attendanceDTO.isPresent = false;
            attendanceDTO.isHwDone = false;

            mockDatabase.Setup(x => x.Attendances.Delete(It.IsAny<Attendance>())).Verifiable();

            var AttendanceService = new AttendanceService(mockDatabase.Object, mapper);

            AttendanceService.Delete(attendanceDTO.ID);

            mockDatabase.Verify(x => x.Attendances.Delete(It.IsAny<Attendance>()), Times.Once());
        }
    }
}

