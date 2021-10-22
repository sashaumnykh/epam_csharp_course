using NUnit.Framework;
using Moq;
using module_10.BLL.Services;
using module_10.DAL.Interfaces;
using AutoMapper;
using module_10.BLL.DTO;
using System;
using module_10.DAL.Entities;
using System.Collections.Generic;
using module_10.BLL.Interfaces;

namespace Tests.ServicesTests
{
    public class StudentServiceTests
    {
        private Mock<IUnitOfWork> mockDatabase;
        private IMapper mapper;
        private Mock<IEmailSender> mockEmailSender;

        [SetUp]
        public void SetUp()
        {
            mockDatabase = new Mock<IUnitOfWork>();
            mockEmailSender = new Mock<IEmailSender>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Student, StudentDTO>();
                cfg.CreateMap<StudentDTO, Student>();
                cfg.CreateMap<List<Student>, List<StudentDTO>>();
            });
            mapper = new Mapper(config);
        }

        [Test]
        public void Create_ServiceMethod_Invokes_RepoMethodCreate_Test()
        {
            StudentDTO studentDTO = new StudentDTO();
            studentDTO.Name = "Tasya Lunina";
            studentDTO.Email = "tlunina@gmail.com";

            mockDatabase.Setup(x => x.Students.Create(It.IsAny<Student>())).Verifiable();

            var StudentService = new StudentService(mockDatabase.Object, mapper, mockEmailSender.Object);

            StudentService.Create(studentDTO);

            mockDatabase.Verify(x => x.Students.Create(It.IsAny<Student>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsTheObject_Test()
        {
            StudentDTO studentDTO = new StudentDTO();
            studentDTO.Name = "Polina Kropotina";
            studentDTO.Email = "kropotina@mail.ru";

            Student student = new Student();
            student.Name = studentDTO.Name;
            student.Email = studentDTO.Email;

            mockDatabase.Setup(x => x.Students.Get(It.IsAny<int>())).Returns(student);

            var StudentService = new StudentService(mockDatabase.Object, mapper, mockEmailSender.Object);

            var result = StudentService.Get(10);
            Assert.AreEqual(studentDTO.Name, result.Name);
            Assert.AreEqual(studentDTO.Email, result.Email);
        }

        [Test]
        public void Get_ServiceMethod_Invokes_RepoMethodGet_Test()
        {
            mockDatabase.Setup(x => x.Students.Get(It.IsAny<int>())).Verifiable();

            var StudentService = new StudentService(mockDatabase.Object, mapper, mockEmailSender.Object);

            StudentService.Get(new Random().Next());

            mockDatabase.Verify(x => x.Students.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects_Test()
        {
            List<Student> students = new List<Student>
            {
                new Student { Name = "Olya Semennikova"},
                new Student {Name = "Andrey Belyakov"}
            };

            List<StudentDTO> studentsDTO = new List<StudentDTO>
            {
                new StudentDTO { Name = "Olya Semennikova"},
                new StudentDTO {Name = "Andrey Belyakov"}
            };
            
            mockDatabase.Setup(x => x.Students.GetAll()).Returns(students);

            var StudentService = new StudentService(mockDatabase.Object, mapper, mockEmailSender.Object);

            List<StudentDTO> gotStudents = new List<StudentDTO>(StudentService.GetAll());

            Assert.AreEqual(studentsDTO.Count, gotStudents.Count);
            for (int i = 0; i < studentsDTO.Count; i++)
            {
                Assert.AreEqual(studentsDTO[i].Name, gotStudents[i].Name);
            }
        }


        [Test]
        public void Update_ServiceMethod_Invokes_RepoMethodUpdate_Test()
        {
            StudentDTO studentDTO = new StudentDTO
            {
                Name = "Sasha Umnykh",
                Email = "alexandraumnykh@gmail.com"
            };

            var calls = 0;

            mockDatabase.Setup(x => x.Students.Update(It.IsAny<Student>())).Callback(() => calls++).Verifiable();

            var StudentService = new StudentService(mockDatabase.Object, mapper, mockEmailSender.Object);

            StudentService.Update(studentDTO);

            Assert.That(calls > 0);
        }

        [Test]
        public void Delete_ServiceMethod_Invokes_RepoMethodDelete_Test()
        {
            StudentDTO studentDTO = new StudentDTO
            {
                ID = 5,
                Name = "Dmitry Yudin",
                Email = "yudin@yandex.ru"
            };

            mockDatabase.Setup(x => x.Students.Delete(It.IsAny<Student>())).Verifiable();

            var StudentService = new StudentService(mockDatabase.Object, mapper, mockEmailSender.Object);

            StudentService.Delete(studentDTO.ID);

            mockDatabase.Verify(x => x.Students.Delete(It.IsAny<Student>()), Times.Once());
        }
    }
}


