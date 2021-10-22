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
    public class StudentControllerTests
    {
        private Mock<IStudentService> mockService;
        private IMapper mapper;
        private StudentViewModel studentVM;
        private StudentDTO studentDTO;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<IStudentService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StudentDTO, StudentViewModel>();
                cfg.CreateMap<StudentViewModel, StudentDTO>();
            });
            mapper = new Mapper(config);

            studentVM = new StudentViewModel
            {
                Name = "Irina Lapteva",
                Email = "irina_lapteva@mail.ru"
            };

            studentDTO = new StudentDTO
            {
                Name = studentVM.Name,
                Email = studentVM.Email
            };
        }

        [Test]
        public void Create_APIMethod_Invokes_ServiceMethodCreate()
        {
            mockService.Setup(x => x.Create(It.IsAny<StudentDTO>())).Verifiable();

            var StudentController = new StudentController(mockService.Object, mapper);

            StudentController.Create(studentVM);

            mockService.Verify(x => x.Create(It.IsAny<StudentDTO>()), Times.Once());
        }

        [Test]
        public void Create_AnObjectWasCreated_ReturnesOK()
        {
            var StudentController = new StudentController(mockService.Object, mapper);

            var result = StudentController.Create(studentVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_APIMethodInvokesServiceMethodUpdate()
        {
            mockService.Setup(x => x.Update(It.IsAny<StudentDTO>())).Verifiable();

            var StudentController = new StudentController(mockService.Object, mapper);

            StudentController.Update(studentVM);

            mockService.Verify(x => x.Update(It.IsAny<StudentDTO>()), Times.Once());
        }

        [Test]
        public void Update_ValidInput_ReturnsdOK()
        {
            var StudentController = new StudentController(mockService.Object, mapper);

            var result = StudentController.Update(studentVM);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_InvalidInput_ReturnesNotFound()
        {
            var StudentController = new StudentController(mockService.Object, mapper);

            var result = StudentController.Update(null);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void Get_APIMethodInvokesServiceMethodGet()
        {
            mockService.Setup(x => x.Get(It.IsAny<int>())).Verifiable();

            var StudentController = new StudentController(mockService.Object, mapper);

            StudentController.Get(new Random().Next());

            mockService.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Get_ReturnsAnObject()
        {
            mockService.Setup(x => x.Get(studentVM.ID)).Returns(studentDTO);

            var StudentController = new StudentController(mockService.Object, mapper);

            var result = StudentController.Get(studentVM.ID);

            Assert.AreEqual(studentVM.Name, result.Name);
            Assert.AreEqual(studentVM.Email, result.Email);
        }

        [Test]
        public void GetAll_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.GetAll()).Verifiable();

            var StudentController = new StudentController(mockService.Object, mapper);

            var all = StudentController.GetAll();

            mockService.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void GetAll_ReturnsObjects()
        {
            List<StudentViewModel> studentsVM = new List<StudentViewModel>
            {
                new StudentViewModel{
                    Name = "Ivan Ivanov",
                    Email = "ivanov@gmail.com"
                },
                new StudentViewModel
                {
                    Name = "Konstantin Dmitriev",
                    Email = "kostyadmi@gmail.com"
                }
            };

            List<StudentDTO> studentsDTO = new List<StudentDTO>
            {
                new StudentDTO{
                    Name = "Ivan Ivanov",
                    Email = "ivanov@gmail.com"
                },
                new StudentDTO
                {
                    Name = "Konstantin Dmitriev",
                    Email = "kostyadmi@gmail.com"
                }
            };

            mockService.Setup(x => x.GetAll()).Returns(studentsDTO);

            var StudentController = new StudentController(mockService.Object, mapper);

            var result = new List<StudentViewModel>(StudentController.GetAll());

            Assert.AreEqual(studentsVM.Count, result.Count);
            for (int i = 0; i < studentsVM.Count; i++)
            {
                Assert.AreEqual(studentsVM[i].Name, result[i].Name);
                Assert.AreEqual(studentsVM[i].Email, result[i].Email);
            }
        }

        [Test]
        public void Delete_APIMethodInvokesServiceMethod()
        {
            mockService.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            var StudentController = new StudentController(mockService.Object, mapper);

            //AttendanceController.Delete(new Random().Next());
            StudentController.Delete(0);

            mockService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void Delete_ValidInput_ReturnsOk()
        {
            var StudentController = new StudentController(mockService.Object, mapper);

            var result = StudentController.Delete(7);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Delete_InvalidInput_ReturnsOk()
        {
            var StudentController = new StudentController(mockService.Object, mapper);

            var result = StudentController.Delete(-1);
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
