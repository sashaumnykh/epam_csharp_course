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
    public class LectureController : ControllerBase
    {
        private ILectureService _service;
        private IMapper _mapper;

        public LectureController(ILectureService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/lecture/{id}")]
        public LectureViewModel Get(int id)
        {
            var model = _service.Get(id);
            var viewModel = _mapper.Map<LectureDTO, LectureViewModel>(model);
            return viewModel;
        }

        [HttpGet]
        [Route("/lecture/")]
        public IEnumerable<LectureViewModel> GetAll()
        {
            var models = _service.GetAll();
            return _mapper.Map<IEnumerable<LectureDTO>, IEnumerable<LectureViewModel>>(models);
        }

        [HttpPost]
        public IActionResult Create([FromBody] LectureViewModel viewModel)
        {
            var model = _mapper.Map<LectureViewModel, LectureDTO>(viewModel);
            _service.Create(model);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(LectureViewModel viewModel)
        {
            if (viewModel == null)
            {
                return NotFound();
            }
            var lecture = _mapper.Map<LectureViewModel, LectureDTO>(viewModel);
            _service.Update(lecture);
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
