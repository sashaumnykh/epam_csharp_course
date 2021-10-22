using AutoMapper;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using module_10.WEB.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;

namespace module_10.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceController : ControllerBase
    {
        private IAttendanceService _service;
        private IMapper _mapper;
        private readonly ILogger<AttendanceController> _logger;

        public AttendanceController(IAttendanceService service, IMapper mapper, ILogger<AttendanceController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("/attendance/{id}")]
        public AttendanceViewModel Get(int id)
        {
            _logger.LogInformation("ControllerGetMethod was invoked. Start getting an object by its ID");
            var model = _service.Get(id);
            var viewModel = _mapper.Map<AttendanceDTO, AttendanceViewModel>(model);
            return viewModel;
        }

        [HttpGet]
        [Route("/attendance/")]
        public IEnumerable<AttendanceViewModel> GetAll()
        {
            var models = _service.GetAll();
            var viewModels = _mapper.Map<IEnumerable<AttendanceDTO>, IEnumerable<AttendanceViewModel>>(models);
            return viewModels;
        }

        [HttpPost]
        [Route("/attendance/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody] AttendanceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<AttendanceViewModel, AttendanceDTO>(viewModel);
                _service.Create(model);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(AttendanceViewModel viewModel)
        {
            if (viewModel == null)
            {
                return NotFound();
            }
            var s = _mapper.Map<AttendanceViewModel, AttendanceDTO>(viewModel);
            _service.Update(s);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            _service.Delete(id);
            return Ok();
        }
    }
}
