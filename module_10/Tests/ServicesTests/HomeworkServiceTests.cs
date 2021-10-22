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
    public class HomeworkServiceTests
    {
        private IMapper mapper;
        private Mock<IUnitOfWork> mockIUnitOfWork;

        [SetUp]
        public void SetUp()
        {
            mockIUnitOfWork = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Homework, HomeworkDTO>();
                cfg.CreateMap<HomeworkDTO, Homework>();
            });
            mapper = new Mapper(config);
        }

        [Test]
        public void Create_ServiceMethod_Invokes_RepoMethodCreate_Test()
        {
            HomeworkDTO hwDTO = new HomeworkDTO();
            hwDTO.LectureID = 2;
            hwDTO.Mark = 5;

            mockIUnitOfWork.Setup(x => x.Homeworks.Create(It.IsAny<Homework>())).Verifiable();

            var HomeworkService = new HomeworkService(mockIUnitOfWork.Object, mapper);

            HomeworkService.Create(hwDTO);

            mockIUnitOfWork.Verify(x => x.Homeworks.Create(It.IsAny<Homework>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsTheObject_Test()
        {
            HomeworkDTO hwDTO = new HomeworkDTO();
            hwDTO.ID = 1;
            hwDTO.LectureID = 1;
            Homework hw = new Homework();
            hw.ID = hwDTO.ID;
            hw.LectureID = hwDTO.LectureID;

            mockIUnitOfWork.Setup(x => x.Homeworks.Get(It.IsAny<int>())).Returns(hw);

            var HomeworkService = new HomeworkService(mockIUnitOfWork.Object, mapper);
            var result = HomeworkService.Get(10);
            Assert.AreEqual(hwDTO.ID, result.ID);
            Assert.AreEqual(hwDTO.LectureID, result.LectureID);
        }

        [Test]
        public void Get_ServiceMethod_Invokes_RepoMethodGet_Test()
        {
            mockIUnitOfWork.Setup(x => x.Homeworks.Get(It.IsAny<int>())).Verifiable();

            var HomeworkService = new HomeworkService(mockIUnitOfWork.Object, mapper);

            HomeworkService.Get(new Random().Next());

            mockIUnitOfWork.Verify(x => x.Homeworks.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects_Test()
        {
            List<Homework> hws = new List<Homework>
            {
                new Homework { ID = 1 },
                new Homework { ID = 2},
            };

            List<HomeworkDTO> hwsDTO = new List<HomeworkDTO>
            {
                new HomeworkDTO { ID = 1 },
                new HomeworkDTO { ID = 2}
            };

            mockIUnitOfWork.Setup(x => x.Homeworks.GetAll()).Returns(hws);

            var HomeworkService = new HomeworkService(mockIUnitOfWork.Object, mapper);

            List<HomeworkDTO> gotHW = new List<HomeworkDTO>(HomeworkService.GetAll());

            Assert.AreEqual(hwsDTO.Count, gotHW.Count);
            for (int i = 0; i < hwsDTO.Count; i++)
            {
                Assert.AreEqual(hwsDTO[i].ID, gotHW[i].ID);
            }
        }

        [Test]
        public void Update_ServiceMethod_Invokes_RepoMethodUpdate_Test()
        {
            HomeworkDTO hwDTO = new HomeworkDTO();
            hwDTO.LectureID = 2;
            hwDTO.Mark = 4;
            hwDTO.StudentID = 1;

            var calls = 0;

            mockIUnitOfWork.Setup(x => x.Homeworks.Update(It.IsAny<Homework>())).Callback(() => calls++).Verifiable();

            var HomeworkService = new HomeworkService(mockIUnitOfWork.Object, mapper);

            HomeworkService.Update(hwDTO);

            Assert.That(calls > 0);
        }

        [Test]
        public void Delete_ServiceMethod_Invokes_RepoMethodDelete_Test()
        {
            HomeworkDTO hwDTO = new HomeworkDTO();
            hwDTO.LectureID = 2;
            hwDTO.ID = 5;

            mockIUnitOfWork.Setup(x => x.Homeworks.Delete(It.IsAny<Homework>())).Verifiable();

            var HomeworkService = new HomeworkService(mockIUnitOfWork.Object, mapper);

            HomeworkService.Delete(hwDTO.ID);

            mockIUnitOfWork.Verify(x => x.Homeworks.Delete(It.IsAny<Homework>()), Times.Once());
        }
    }
}