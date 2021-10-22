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
    public class LecturerServiceTests
    {
        private Mock<IUnitOfWork> mockDatabase;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mockDatabase = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Lecturer, LecturerDTO>();
                cfg.CreateMap<LecturerDTO, Lecturer>();
            });
            mapper = new Mapper(config);
        }

        [Test]
        public void Create_ServiceMethod_Invokes_RepoMethodCreate_Test()
        {
            LecturerDTO lecturerDTO = new LecturerDTO();
            lecturerDTO.Name = "Tom";
            lecturerDTO.Surname = "York";

            mockDatabase.Setup(x => x.Lecturers.Create(It.IsAny<Lecturer>())).Verifiable();

            var LecturerService = new LecturerService(mockDatabase.Object, mapper);

            LecturerService.Create(lecturerDTO);

            mockDatabase.Verify(x => x.Lecturers.Create(It.IsAny<Lecturer>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsTheObject_Test()
        {
            LecturerDTO lecturerDTO = new LecturerDTO();
            lecturerDTO.Name = "Tom";
            lecturerDTO.Surname = "York";

            Lecturer lecturer = new Lecturer();
            lecturer.Name = lecturerDTO.Name;
            lecturer.Surname = lecturerDTO.Surname;

            mockDatabase.Setup(x => x.Lecturers.Get(It.IsAny<int>())).Returns(lecturer);

            var LecturerService = new LecturerService(mockDatabase.Object, mapper);

            var result = LecturerService.Get(10);
            Assert.AreEqual(lecturerDTO.Name, result.Name);
            Assert.AreEqual(lecturerDTO.Surname, result.Surname);
        }

        [Test]
        public void Get_ServiceMethod_Invokes_RepoMethodGet_Test()
        {
            mockDatabase.Setup(x => x.Lecturers.Get(It.IsAny<int>())).Verifiable();

            var LecturerService = new LecturerService(mockDatabase.Object, mapper);

            LecturerService.Get(new Random().Next());

            mockDatabase.Verify(x => x.Lecturers.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects_Test()
        {
            List<Lecturer> lecturers = new List<Lecturer>
            {
                new Lecturer { Name = "Sam", Surname = "Smith" },
                new Lecturer { Name = "Alex", Surname = "Turner"},
            };

            List<LecturerDTO> lecturersDTO = new List<LecturerDTO>
            {
                new LecturerDTO { Name = "Sam", Surname = "Smith" },
                new LecturerDTO { Name = "Alex", Surname = "Turner"},
            };

            mockDatabase.Setup(x => x.Lecturers.GetAll()).Returns(lecturers);

            var LecturerService = new LecturerService(mockDatabase.Object, mapper);

            List<LecturerDTO> gotLecturers = new List<LecturerDTO>(LecturerService.GetAll());

            Assert.AreEqual(lecturersDTO.Count, gotLecturers.Count);
            for (int i = 0; i < lecturersDTO.Count; i++)
            {
                Assert.AreEqual(lecturersDTO[i].Name, gotLecturers[i].Name);
                Assert.AreEqual(lecturersDTO[i].Surname, gotLecturers[i].Surname);
            }
        }


        [Test]
        public void Update_ServiceMethod_Invokes_RepoMethodUpdate_Test()
        {
            LecturerDTO lecturerDTO = new LecturerDTO();
            lecturerDTO.Name = "Ed";
            lecturerDTO.Surname = "Sheeran";

            var calls = 0;

            mockDatabase.Setup(x => x.Lecturers.Update(It.IsAny<Lecturer>())).Callback(() => calls++).Verifiable();

            var LecturerService = new LecturerService(mockDatabase.Object, mapper);

            LecturerService.Update(lecturerDTO);

            Assert.That(calls > 0);
        }

        [Test]
        public void Delete_ServiceMethod_Invokes_RepoMethodDelete_Test()
        {
            LecturerDTO lecturerDTO = new LecturerDTO();
            lecturerDTO.Name = "Amy";
            lecturerDTO.Surname = "Winehouse";

            mockDatabase.Setup(x => x.Lecturers.Delete(It.IsAny<Lecturer>())).Verifiable();

            var LecturerService = new LecturerService(mockDatabase.Object, mapper);

            LecturerService.Delete(lecturerDTO.ID);

            mockDatabase.Verify(x => x.Lecturers.Delete(It.IsAny<Lecturer>()), Times.Once());
        }
    }
}
