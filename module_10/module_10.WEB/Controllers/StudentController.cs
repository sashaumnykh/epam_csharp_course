using AutoMapper;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using module_10.WEB.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;

namespace module_10.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private IStudentService _service;
        private IMapper _mapper;

        public StudentController(IStudentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/student/{id}")]
        public StudentViewModel Get(int id)
        {
            var model = _service.Get(id);
            var viewModel = _mapper.Map<StudentDTO, StudentViewModel>(model);
            return viewModel;
        }
        
        [HttpGet]
        [Route("/student/")]
        public IEnumerable<StudentViewModel> GetAll()
        {
            var models = _service.GetAll();
            var viewModels = _mapper.Map<IEnumerable<StudentDTO>, IEnumerable<StudentViewModel>>(models);
            return viewModels;
        }
        

        [HttpPost]
        [Route("/student/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody] StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<StudentViewModel, StudentDTO>(viewModel);
                _service.Create(model);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(StudentViewModel student)
        {
            if (student == null)
            {
                return NotFound();
            }
            var s = _mapper.Map<StudentViewModel, StudentDTO>(student);
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

        [HttpGet("~/checkattendance")]
        public IActionResult CheckAttendance(int studentID)
        {
            var student = _service.Get(studentID);
            _service.CheckAttendance(student);
            return Ok();
        }

        [HttpGet("~/checkaveragescore")]
        public IActionResult CheckAverageScore(int studentID)
        {
            var student = _service.Get(studentID);
            _service.CheckAverageScore(student);
            return Ok();
        }

    }
}
