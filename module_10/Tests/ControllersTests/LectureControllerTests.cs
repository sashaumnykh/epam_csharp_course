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
    public class LectureControllerTests
    {
        private Mock<ILectureService> mockService;
        private IMapper mapper;
        private LectureViewModel lectureVM;
        private LectureDTO lectureDTO;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<ILectureService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LectureDTO, LectureViewModel>();
                cfg.CreateMap<LectureViewModel, LectureDTO>();
            });
            mapper = new Mapper(config);

            lectureVM = new LectureViewModel
            {
                LecturerID = 1,
                HomeworkID = 2
            };

            lectureDTO = new LectureDTO
            {
                LecturerID = lectureVM.LecturerID,
                HomeworkID = lectureVM.HomeworkID
            };
        }

        [Test]
        public void Create_APIMethod_Invokes_ServiceMethodCreate()
        {
            mockService.Setup(x => x.Create(It.IsAny<LectureDTO>())).Verifiable();

            var LectureController = new LectureController(mockService.Object, mapper);

            LectureController.Create(lectureVM);

            mockService.Verify(x => x.Create(It.IsAny<LectureDTO>()), Times.Once());
        }

        [Test]
        public void Create_AnObjectWasCreated_ReturnesOK()
        {
            var LectureController = new LectureController(mockService.Object, mapper);

            var result = LectureController.Create(lectureVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_APIMethodInvokesServiceMethodUpdate()
        {
            mockService.Setup(x => x.Update(It.IsAny<LectureDTO>())).Verifiable();

            var LectureController = new LectureController(mockService.Object, mapper);

            LectureController.Update(lectureVM);

            mockService.Verify(x => x.Update(It.IsAny<LectureDTO>()), Times.Once());
        }

        [Test]
        public void Update_ValidInput_ReturnsdOK()
        {
            var LectureController = new LectureController(mockService.Object, mapper);

            var result = LectureController.Update(lectureVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_InvalidInput_ReturnesNotFound()
        {
            var LectureController = new LectureController(mockService.Object, mapper);

            var result = LectureController.Update(null);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void Get_APIMethodInvokesServiceMethodGet()
        {
            mockService.Setup(x => x.Get(It.IsAny<int>())).Verifiable();

            var LectureController = new LectureController(mockService.Object, mapper);

            LectureController.Get(new Random().Next());

            mockService.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsAnObject()
        {
            mockService.Setup(x => x.Get(lectureVM.ID)).Returns(lectureDTO);

            var LectureController = new LectureController(mockService.Object, mapper);

            var result = LectureController.Get(lectureVM.ID);

            Assert.AreEqual(lectureVM.LecturerID, result.LecturerID);
            Assert.AreEqual(lectureVM.HomeworkID, result.HomeworkID);
        }

        [Test]
        public void GetAll_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.GetAll()).Verifiable();

            var LectureController = new LectureController(mockService.Object, mapper);

            var all = LectureController.GetAll();

            mockService.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects()
        {
            List<LectureViewModel> lecturesVM = new List<LectureViewModel>
            {
                new LectureViewModel{
                    LecturerID = 1,
                    HomeworkID = 2
                },
                new LectureViewModel
                {
                    LecturerID = 1,
                    HomeworkID = 3
                }
            };

            List<LectureDTO> lecturesDTO = new List<LectureDTO>
            {
                new LectureDTO{
                    LecturerID = 1,
                    HomeworkID = 2
                },
                new LectureDTO
                {
                    LecturerID = 1,
                    HomeworkID = 3
                }
            };

            mockService.Setup(x => x.GetAll()).Returns(lecturesDTO);

            var LectureController = new LectureController(mockService.Object, mapper);

            var result = new List<LectureViewModel>(LectureController.GetAll());

            Assert.AreEqual(lecturesVM.Count, result.Count);
            for (int i = 0; i < lecturesVM.Count; i++)
            {
                Assert.AreEqual(lecturesVM[i].LecturerID, result[i].LecturerID);
                Assert.AreEqual(lecturesVM[i].HomeworkID, result[i].HomeworkID);
            }
        }

        [Test]
        public void Delete_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            var LectureController = new LectureController(mockService.Object, mapper);

            //AttendanceController.Delete(new Random().Next());
            LectureController.Delete(0);

            mockService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Delete_ValidInput_ReturnsOk()
        {
            var LectureController = new LectureController(mockService.Object, mapper);

            var result = LectureController.Delete(7);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Delete_InvalidInput_ReturnsOk()
        {
            var LectureController = new LectureController(mockService.Object, mapper);

            var result = LectureController.Delete(-1);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}

