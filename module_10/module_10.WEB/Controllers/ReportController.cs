using AutoMapper;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using module_10.WEB.Models;
using Microsoft.AspNetCore.Http;

namespace module_10.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        IReportService _service;
        IMapper _mapper;

        public ReportController(IReportService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("~/getreport")]
        public IActionResult GetReport([FromQuery] ReportViewModel report)
        {
            if (report == null)
            {
                return Ok();
            }
            var reportDTO = _mapper.Map<ReportViewModel, ReportDTO>(report);
            _service.GetReport(reportDTO);
            return Ok();
        }
    }
}
