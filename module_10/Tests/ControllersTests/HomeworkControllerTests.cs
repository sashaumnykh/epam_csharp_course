using AutoMapper;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using module_10.WEB.Models;
using module_10.WEB.Controllers;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Tests.ControllersTests
{
    public class HomeworkControllerTests
    {
        private Mock<IHomeworkService> mockService;
        private IMapper mapper;

        private HomeworkViewModel hwVM;

        [SetUp]
        public void SetUp()
        {
            hwVM = new HomeworkViewModel
            {
                LectureID = 1,
                StudentID = 2,
                Mark = 4
            };

            mockService = new Mock<IHomeworkService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HomeworkDTO, HomeworkViewModel>();
                cfg.CreateMap<HomeworkViewModel, HomeworkDTO>();
            });
            mapper = new Mapper(config);
        }

        [Test]
        public void Create_APIMethod_Invokes_ServiceMethodCreate()
        {
            mockService.Setup(x => x.Create(It.IsAny<HomeworkDTO>())).Verifiable();

            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            HomeworkController.Create(hwVM);

            mockService.Verify(x => x.Create(It.IsAny<HomeworkDTO>()), Times.Once());
        }

        [Test]
        public void Create_AnObjectWasCreated_ReturnesOK()
        {
            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            var result = HomeworkController.Create(hwVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_APIMethodInvokesServiceMethodUpdate()
        {
            mockService.Setup(x => x.Update(It.IsAny<HomeworkDTO>())).Verifiable();

            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            HomeworkController.Update(hwVM);

            mockService.Verify(x => x.Update(It.IsAny<HomeworkDTO>()), Times.Once());
        }

        [Test]
        public void Update_ValidInput_ReturnsdOK()
        {
            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            var result = HomeworkController.Update(hwVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_InvalidInput_ReturnesNotFound()
        {
            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            var result = HomeworkController.Update(null);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void Get_APIMethodInvokesServiceMethodGet()
        {
            mockService.Setup(x => x.Get(It.IsAny<int>())).Verifiable();

            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            HomeworkController.Get(new Random().Next());

            mockService.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsAnObject()
        {
            HomeworkDTO hwDTO = new HomeworkDTO
            {
                ID = hwVM.ID,
                LectureID = hwVM.LectureID,
                StudentID = hwVM.StudentID,
                Mark = hwVM.Mark
            };

            mockService.Setup(x => x.Get(hwVM.ID)).Returns(hwDTO);

            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            var result = HomeworkController.Get(hwVM.ID);

            Assert.AreEqual(hwVM.LectureID, result.LectureID);
            Assert.AreEqual(hwVM.StudentID, result.StudentID);
            Assert.AreEqual(hwVM.Mark, result.Mark);
        }

        [Test]
        public void GetAll_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.GetAll()).Verifiable();

            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            var all = HomeworkController.GetAll();

            mockService.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects()
        {
            List<HomeworkViewModel> hwsVM = new List<HomeworkViewModel>
            {
                new HomeworkViewModel{
                    LectureID = 1,
                    StudentID = 3,
                    Mark = 4
                },
                new HomeworkViewModel
                {
                    LectureID = 1,
                    StudentID = 1,
                    Mark = 5
                }
            };

            List<HomeworkDTO> hwsDTO = new List<HomeworkDTO>
            {
                new HomeworkDTO{
                    LectureID = 1,
                    StudentID = 3,
                    Mark = 4
                },
                new HomeworkDTO
                {
                    LectureID = 1,
                    StudentID = 1,
                    Mark = 5
                }
            };

            mockService.Setup(x => x.GetAll()).Returns(hwsDTO);

            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            var result = new List<HomeworkViewModel>(HomeworkController.GetAll());

            Assert.AreEqual(hwsVM.Count, result.Count);
            for (int i = 0; i < hwsVM.Count; i++)
            {
                Assert.AreEqual(hwsVM[i].LectureID, result[i].LectureID);
                Assert.AreEqual(hwsVM[i].StudentID, result[i].StudentID);
                Assert.AreEqual(hwsVM[i].Mark, result[i].Mark);
            }
        }

        [Test]
        public void Delete_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            HomeworkController.Delete(new Random().Next());

            mockService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Delete_ValidInput_ReturnsOk()
        {
            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            var result = HomeworkController.Delete(7);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Delete_InvalidInput_ReturnsOk()
        {
            var HomeworkController = new HomeworkController(mockService.Object, mapper);

            var result = HomeworkController.Delete(-1);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
