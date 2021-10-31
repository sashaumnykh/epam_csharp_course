using AutoMapper;
using Common;
using Common.Exceptions;
using Microsoft.Extensions.Logging;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using module_10.BLL.Services;
using module_10.DAL.Entities;
using module_10.WEB.Controllers;
using module_10.WEB.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ControllersTests
{
    public class ReportControllerTests
    {
        private Mock<IReportService> mockService;
        private IMapper mapper;
        private Mock<ILogger<ReportController>> mockLogger;

        private ReportViewModel onlyFormatReport;
        private ReportViewModel validReport;

        [SetUp]
        public void Setup()
        {
            mockService = new Mock<IReportService>();
            mockLogger = new Mock<ILogger<ReportController>>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ReportViewModel, ReportDTO>();
                cfg.CreateMap<ReportDTO, ReportViewModel>();
            });
            mapper = new Mapper(config);

            onlyFormatReport = new ReportViewModel
            {
                Format = "json"
            };

            validReport = new ReportViewModel
            {
                Format = "json",
                lectureName = "Math"
            };
        }

        [Test]
        public void APIMethodInvokesServiceMethodGetReport()
        {
            mockService.Setup(x => x.GetReport(It.IsAny<ReportDTO>())).Verifiable();

            var ReportController = new ReportController(mockService.Object, mapper, mockLogger.Object);

            var res = ReportController.GetReport(onlyFormatReport);

            mockService.Verify(x => x.GetReport(It.IsAny<ReportDTO>()), Times.Once());
        }

        [Test]
        public void GetReport_InvalidInput_InvalidReportFormatException()
        {
            var ReportController = new ReportController(mockService.Object, mapper, mockLogger.Object);


            ReportViewModel report = new ReportViewModel
            {
                Format = ""
            };
            var ex = Assert.Throws<InvalidReportFormatException> (() => ReportController.GetReport(report));
            Assert.AreEqual(LogHelper.reportInvalidFormat, ex.Message);
        }

        [Test]
        public void GetReport_ValidInput_ReturnsNotNull()
        {
            ReportDTO validDTO = new ReportDTO
            {
                Format = validReport.Format,
                lectureName = validReport.lectureName,
                studentName = validReport.studentName
            };
            
            mockService.Setup(x => x.GetReport(validDTO)).Returns("not null");
            var ReportController = new ReportController(mockService.Object, mapper, mockLogger.Object);
            var result = ReportController.GetReport(validReport);

            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetReport_ValidInput_JSONFormat_EmptyNameProperties_ReturnsNull()
        {
            var ReportController = new ReportController(mockService.Object, mapper, mockLogger.Object);
            ReportViewModel report = new ReportViewModel
            {
                Format = "json"
            };
            var result = ReportController.GetReport(onlyFormatReport);

            Assert.IsNull(result);
        }

        [Test]
        public void GetReport_ValidInput_XMLFormat_EmptyNameProperties_ReturnsNull()
        {
            var ReportController = new ReportController(mockService.Object, mapper, mockLogger.Object);
            ReportViewModel report = new ReportViewModel
            {
                Format = "XmL"
            };
            var result = ReportController.GetReport(onlyFormatReport);

            Assert.IsNull(result);
        }
    }
}
