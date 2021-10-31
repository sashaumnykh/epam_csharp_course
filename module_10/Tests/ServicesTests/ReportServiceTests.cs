using AutoMapper;
using Microsoft.Extensions.Logging;
using module_10.BLL.DTO;
using module_10.BLL.Services;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ServicesTests
{
    public class ReportServiceTests
    {
        private Mock<IUnitOfWork> mockDatabase { get; set; }
        private IMapper mapper;
        private Mock<ILogger<ReportService>> mockLogger;

        private ReportDTO reportForLectureDTO;
        private IEnumerable<Attendance> attendance;

        [SetUp]
        public void Setup()
        {
            mockDatabase = new Mock<IUnitOfWork>();
            mockLogger = new Mock<ILogger<ReportService>>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Attendance, AttendanceDTO>();
                cfg.CreateMap<AttendanceDTO, Attendance>();
            });
            mapper = new Mapper(config);

            attendance = new List<Attendance>
            {
                new Attendance
                {
                    LectureID = 1,
                    StudentID = 1,
                    isPresent = true
                },
                new Attendance
                {
                    LectureID = 2,
                    StudentID = 2,
                    isPresent = false
                },
                new Attendance
                {
                    LectureID = 3,
                    StudentID = 3,
                    isPresent = true
                }
            };

            reportForLectureDTO = new ReportDTO
            {
                Format = "Json",
                lectureName = "Math"
            };

        }

        [Test]
        public void ValidInput_JSONReportForLecture_ReturnsNotEmptyReport()
        {
            ReportDTO JSONreportForLecture = new ReportDTO
            {
                Format = "Json",
                lectureName = "Math"
            };

            List<Lecture> lectures = new List<Lecture>
            {
                new Lecture
                {
                    Name = JSONreportForLecture.lectureName,
                    ID = 1
                },
                new Lecture
                {
                    Name = JSONreportForLecture.lectureName,
                    ID = 2
                },
                new Lecture
                {
                    Name = JSONreportForLecture.lectureName,
                    ID = 3
                }
            };

            mockDatabase.Setup(x => x.Lectures.GetAll()).Returns(lectures);
            mockDatabase.Setup(x => x.Attendances.GetAll()).Returns(attendance);

            var ReportService = new ReportService(mockDatabase.Object, mapper, mockLogger.Object);
            var res = ReportService.GetReport(JSONreportForLecture);
            Assert.IsNotEmpty(res);
        }

        [Test]
        public void ValidInput_JSONReportForStudent_ReturnsNotEmptyReport()
        {
            ReportDTO JSONreportForStudent = new ReportDTO
            {
                Format = "Json",
                studentName = "Lenny Kravitz"
            };

            List<Student> students = new List<Student>
            {
                new Student
                {
                    Name = JSONreportForStudent.studentName,
                    ID = 1
                },
                new Student
                {
                    Name = JSONreportForStudent.studentName,
                    ID = 2
                },
                new Student
                {
                    Name = JSONreportForStudent.studentName,
                    ID = 3
                }
            };

            mockDatabase.Setup(x => x.Students.GetAll()).Returns(students);
            mockDatabase.Setup(x => x.Attendances.GetAll()).Returns(attendance);

            var ReportService = new ReportService(mockDatabase.Object, mapper, mockLogger.Object);
            var res = ReportService.GetReport(JSONreportForStudent);
            Assert.IsNotEmpty(res);
        }

        [Test]
        public void ValidInput_XMLReportForLecture_ReturnsNotEmptyReport()
        {
            ReportDTO JSONreportForLecture = new ReportDTO
            {
                Format = "xMl",
                lectureName = "Math"
            };

            List<Lecture> lectures = new List<Lecture>
            {
                new Lecture
                {
                    Name = JSONreportForLecture.lectureName,
                    ID = 1
                },
                new Lecture
                {
                    Name = JSONreportForLecture.lectureName,
                    ID = 2
                },
                new Lecture
                {
                    Name = JSONreportForLecture.lectureName,
                    ID = 3
                }
            };

            mockDatabase.Setup(x => x.Lectures.GetAll()).Returns(lectures);
            mockDatabase.Setup(x => x.Attendances.GetAll()).Returns(attendance);

            var ReportService = new ReportService(mockDatabase.Object, mapper, mockLogger.Object);
            var res = ReportService.GetReport(JSONreportForLecture);
            Assert.IsNotEmpty(res);
        }

        [Test]
        public void ValidInput_XMLReportForStudent_ReturnsNotEmptyReport()
        {
            ReportDTO JSONreportForStudent = new ReportDTO
            {
                Format = "xmL",
                studentName = "Lenny Kravitz"
            };

            List<Student> students = new List<Student>
            {
                new Student
                {
                    Name = JSONreportForStudent.studentName,
                    ID = 1
                },
                new Student
                {
                    Name = JSONreportForStudent.studentName,
                    ID = 2
                },
                new Student
                {
                    Name = JSONreportForStudent.studentName,
                    ID = 3
                }
            };

            mockDatabase.Setup(x => x.Students.GetAll()).Returns(students);
            mockDatabase.Setup(x => x.Attendances.GetAll()).Returns(attendance);

            var ReportService = new ReportService(mockDatabase.Object, mapper, mockLogger.Object);
            var res = ReportService.GetReport(JSONreportForStudent);
            Assert.IsNotEmpty(res);
        }

        [Test]
        public void ValidInput_EmptyNameProperties_ReturnsEmptyReport()
        {
            ReportDTO JSONreportForNull = new ReportDTO
            {
                Format = "xmL"
            };


            var ReportService = new ReportService(mockDatabase.Object, mapper, mockLogger.Object);
            var res = ReportService.GetReport(JSONreportForNull);

            Assert.AreEqual("<attendances />", res);
        }
    }
}
