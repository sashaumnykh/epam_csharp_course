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
    public class LecturerControllerTests
    {
        private Mock<ILecturerService> mockService;
        private IMapper mapper;
        private LecturerViewModel lecturerVM;
        private LecturerDTO lecturerDTO;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<ILecturerService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LecturerDTO, LecturerViewModel>();
                cfg.CreateMap<LecturerViewModel, LecturerDTO>();
            });
            mapper = new Mapper(config);

            lecturerVM = new LecturerViewModel
            {
                Name = "Vikror Pelevin",
                Email = "pelevin@gmail.com"
            };

            lecturerDTO = new LecturerDTO
            {
                Name = lecturerVM.Name,
                Email = lecturerVM.Email
            };
        }

        [Test]
        public void Create_APIMethod_Invokes_ServiceMethodCreate()
        {
            mockService.Setup(x => x.Create(It.IsAny<LecturerDTO>())).Verifiable();

            var LecturerController = new LecturerController(mockService.Object, mapper);

            LecturerController.Create(lecturerVM);

            mockService.Verify(x => x.Create(It.IsAny<LecturerDTO>()), Times.Once());
        }

        [Test]
        public void Create_AnObjectWasCreated_ReturnesOK()
        {
            var LecturerController = new LecturerController(mockService.Object, mapper);

            var result = LecturerController.Create(lecturerVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_APIMethodInvokesServiceMethodUpdate()
        {
            mockService.Setup(x => x.Update(It.IsAny<LecturerDTO>())).Verifiable();

            var LecturerController = new LecturerController(mockService.Object, mapper);

            LecturerController.Update(lecturerVM);

            mockService.Verify(x => x.Update(It.IsAny<LecturerDTO>()), Times.Once());
        }

        [Test]
        public void Update_ValidInput_ReturnsdOK()
        {
            var LecturerController = new LecturerController(mockService.Object, mapper);

            var result = LecturerController.Update(lecturerVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_InvalidInput_ReturnesNotFound()
        {
            var LecturerController = new LecturerController(mockService.Object, mapper);

            var result = LecturerController.Update(null);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void Get_APIMethodInvokesServiceMethodGet()
        {
            mockService.Setup(x => x.Get(It.IsAny<int>())).Verifiable();

            var LecturerController = new LecturerController(mockService.Object, mapper);

            LecturerController.Get(new Random().Next());

            mockService.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsAnObject()
        {
            mockService.Setup(x => x.Get(lecturerVM.ID)).Returns(lecturerDTO);

            var LecturerController = new LecturerController(mockService.Object, mapper);

            var result = LecturerController.Get(lecturerVM.ID);

            Assert.AreEqual(lecturerVM.Name, result.Name);
            Assert.AreEqual(lecturerVM.Email, result.Email);
        }

        [Test]
        public void GetAll_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.GetAll()).Verifiable();

            var LecturerController = new LecturerController(mockService.Object, mapper);

            var all = LecturerController.GetAll();

            mockService.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects()
        {
            List<LecturerViewModel> lecturersVM = new List<LecturerViewModel>
            {
                new LecturerViewModel{
                    Name = "Kurt Vonnegut",
                    Email = "kurt@gmail.com"
                },
                new LecturerViewModel
                {
                    Name = "Ernest Hemingway",
                    Email = "yourfavouritewriter@gmail.com"
                }
            };

            List<LecturerDTO> lecturersDTO = new List<LecturerDTO>
            {
                new LecturerDTO{
                    Name = "Kurt Vonnegut",
                    Email = "kurt@gmail.com"
                },
                new LecturerDTO
                {
                    Name = "Ernest Hemingway",
                    Email = "yourfavouritewriter@gmail.com"
                }
            };

            mockService.Setup(x => x.GetAll()).Returns(lecturersDTO);

            var LecturerController = new LecturerController(mockService.Object, mapper);

            var result = new List<LecturerViewModel>(LecturerController.GetAll());

            Assert.AreEqual(lecturersVM.Count, result.Count);
            for (int i = 0; i < lecturersVM.Count; i++)
            {
                Assert.AreEqual(lecturersVM[i].Name, result[i].Name);
                Assert.AreEqual(lecturersVM[i].Email, result[i].Email);
            }
        }

        [Test]
        public void Delete_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            var LecturerController = new LecturerController(mockService.Object, mapper);

            //AttendanceController.Delete(new Random().Next());
            LecturerController.Delete(0);

            mockService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Delete_ValidInput_ReturnsOk()
        {
            var LecturerController = new LecturerController(mockService.Object, mapper);

            var result = LecturerController.Delete(7);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Delete_InvalidInput_ReturnsOk()
        {
            var LecturerController = new LecturerController(mockService.Object, mapper);

            var result = LecturerController.Delete(-1);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}