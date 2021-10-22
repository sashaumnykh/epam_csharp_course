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
    public class LectureServiceTests
    {
        private Mock<IUnitOfWork> mockDatabase;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mockDatabase = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Lecture, LectureDTO>();
                cfg.CreateMap<LectureDTO, Lecture>();
            });
            mapper = new Mapper(config);
        }

        [Test]
        public void Create_ServiceMethod_Invokes_RepoMethodCreate_Test()
        {
            LectureDTO lectureDTO = new LectureDTO();
            lectureDTO.HomeworkID = 1;

            mockDatabase.Setup(x => x.Homeworks.Get(It.IsAny<int>())).Returns(new Homework());
            mockDatabase.Setup(x => x.Lectures.Create(It.IsAny<Lecture>())).Verifiable();

            var LectureService = new LectureService(mockDatabase.Object, mapper);

            LectureService.Create(lectureDTO);

            mockDatabase.Verify(x => x.Lectures.Create(It.IsAny<Lecture>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsTheObject_Test()
        {
            LectureDTO lectureDTO = new LectureDTO();
            lectureDTO.HomeworkID = 1;

            Lecture lecture = new Lecture();
            lecture.HomeworkID = 1;

            mockDatabase.Setup(x => x.Lectures.Get(It.IsAny<int>())).Returns(lecture);

            var LectureService = new LectureService(mockDatabase.Object, mapper);
            var result = LectureService.Get(10);
            Assert.AreEqual(lectureDTO.HomeworkID, result.HomeworkID);
        }

        [Test]
        public void Get_ServiceMethod_Invokes_RepoMethodGet_Test()
        {
            mockDatabase.Setup(x => x.Lectures.Get(It.IsAny<int>())).Verifiable();

            var LectureService = new LectureService(mockDatabase.Object, mapper);

            LectureService.Get(new Random().Next());

            mockDatabase.Verify(x => x.Lectures.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects_Test()
        {
            List<Lecture> lectures = new List<Lecture>
            {
                new Lecture { ID = 1 },
                new Lecture { ID = 2},
            };

            List<LectureDTO> lecturesDTO = new List<LectureDTO>
            {
                new LectureDTO { ID = 1 },
                new LectureDTO { ID = 2}
            };

            mockDatabase.Setup(x => x.Lectures.GetAll()).Returns(lectures);

            var LectureService = new LectureService(mockDatabase.Object, mapper);

            List<LectureDTO> gotLectures = new List<LectureDTO>(LectureService.GetAll());

            Assert.AreEqual(lecturesDTO.Count, gotLectures.Count);
            for (int i = 0; i < lecturesDTO.Count; i++)
            {
                Assert.AreEqual(lecturesDTO[i].ID, gotLectures[i].ID);
            }
        }


        [Test]
        public void Update_ServiceMethod_Invokes_RepoMethodUpdate_Test()
        {
            LectureDTO lectureDTO = new LectureDTO();
            lectureDTO.HomeworkID = 1;

            var calls = 0;

            mockDatabase.Setup(x => x.Lectures.Update(It.IsAny<Lecture>())).Callback(() => calls++).Verifiable();

            var LectureService = new LectureService(mockDatabase.Object, mapper);

            LectureService.Update(lectureDTO);

            Assert.That(calls > 0);
        }

        [Test]
        public void Delete_ServiceMethod_Invokes_RepoMethodDelete_Test()
        {
            LectureDTO lectureDTO = new LectureDTO();
            lectureDTO.HomeworkID = 1;

            mockDatabase.Setup(x => x.Lectures.Delete(It.IsAny<Lecture>())).Verifiable();

            var LectureService = new LectureService(mockDatabase.Object, mapper);

            LectureService.Delete(lectureDTO.ID);

            mockDatabase.Verify(x => x.Lectures.Delete(It.IsAny<Lecture>()), Times.Once());
        }
    }
}
