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
    public class AttendanceControllerTests
    {
        private Mock<IAttendanceService> mockService;
        private IMapper mapper;
        private AttendanceViewModel attVM;

        [SetUp]
        public void SetUp()
        {
            attVM = new AttendanceViewModel
            {
                LectureID = 1,
                StudentID = 2,
                isPresent = false
            };

            mockService = new Mock<IAttendanceService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AttendanceDTO, AttendanceViewModel>();
                cfg.CreateMap<AttendanceViewModel, AttendanceDTO>();
            });
            mapper = new Mapper(config);
        }

        [Test]
        public void Create_APIMethod_Invokes_ServiceMethodCreate()
        {
            mockService.Setup(x => x.Create(It.IsAny<AttendanceDTO>())).Verifiable();

            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            AttendanceController.Create(attVM);

            mockService.Verify(x => x.Create(It.IsAny<AttendanceDTO>()), Times.Once());
        }

        [Test]
        public void Create_AnObjectWasCreated_ReturnesOK()
        {
            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            var result = AttendanceController.Create(attVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_APIMethodInvokesServiceMethodUpdate()
        {
            mockService.Setup(x => x.Update(It.IsAny<AttendanceDTO>())).Verifiable();

            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            AttendanceController.Update(attVM);

            mockService.Verify(x => x.Update(It.IsAny<AttendanceDTO>()), Times.Once());
        }

        [Test]
        public void Update_ValidInput_ReturnsdOK()
        {
            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            var result = AttendanceController.Update(attVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_InvalidInput_ReturnesNotFound()
        {
            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            var result = AttendanceController.Update(null);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void Get_APIMethodInvokesServiceMethodGet()
        {
            mockService.Setup(x => x.Get(It.IsAny<int>())).Verifiable();

            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            AttendanceController.Get(new Random().Next());

            mockService.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsAnObject()
        {
            AttendanceDTO hwDTO = new AttendanceDTO
            {
                ID = attVM.ID,
                LectureID = attVM.LectureID,
                StudentID = attVM.StudentID,
                isPresent = attVM.isPresent
            };

            mockService.Setup(x => x.Get(attVM.ID)).Returns(hwDTO);

            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            var result = AttendanceController.Get(attVM.ID);

            Assert.AreEqual(attVM.LectureID, result.LectureID);
            Assert.AreEqual(attVM.StudentID, result.StudentID);
            Assert.AreEqual(attVM.isPresent, result.isPresent);
        }

        [Test]
        public void GetAll_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.GetAll()).Verifiable();

            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            var all = AttendanceController.GetAll();

            mockService.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects()
        {
            List<AttendanceViewModel> attsVM = new List<AttendanceViewModel>
            {
                new AttendanceViewModel{
                    LectureID = 1,
                    StudentID = 2,
                    isPresent = false
                },
                new AttendanceViewModel
                {
                    LectureID = 1,
                    StudentID = 3,
                    isPresent = true
                }
            };

            List<AttendanceDTO> attsDTO = new List<AttendanceDTO>
            {
                new AttendanceDTO{
                    LectureID = 1,
                    StudentID = 2,
                    isPresent = false
                },
                new AttendanceDTO
                {
                    LectureID = 1,
                    StudentID = 3,
                    isPresent = true
                }
            };

            mockService.Setup(x => x.GetAll()).Returns(attsDTO);

            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            var result = new List<AttendanceViewModel>(AttendanceController.GetAll());

            Assert.AreEqual(attsVM.Count, result.Count);
            for (int i = 0; i < attsVM.Count; i++)
            {
                Assert.AreEqual(attsVM[i].LectureID, result[i].LectureID);
                Assert.AreEqual(attsVM[i].StudentID, result[i].StudentID);
                Assert.AreEqual(attsVM[i].isPresent, result[i].isPresent);
            }
        }

        [Test]
        public void Delete_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            //AttendanceController.Delete(new Random().Next());
            AttendanceController.Delete(0);

            mockService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Delete_ValidInput_ReturnsOk()
        {
            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            var result = AttendanceController.Delete(7);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Delete_InvalidInput_ReturnsOk()
        {
            var AttendanceController = new AttendanceController(mockService.Object, mapper);

            var result = AttendanceController.Delete(-1);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}

