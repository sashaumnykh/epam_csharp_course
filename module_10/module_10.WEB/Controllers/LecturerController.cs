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
    public class LecturerController : ControllerBase
    {
        private ILecturerService _service;
        private IMapper _mapper;

        public LecturerController(ILecturerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/lecturer/{id}")]
        public LecturerViewModel Get(int id)
        {
            var model = _service.Get(id);
            var viewModel = _mapper.Map<LecturerDTO, LecturerViewModel>(model);
            return viewModel;
        }

        [HttpGet]
        [Route("/lecturer/")]
        public IEnumerable<LecturerViewModel> GetAll()
        {
            var models = _service.GetAll();
            var viewModels = _mapper.Map<IEnumerable<LecturerDTO>, IEnumerable<LecturerViewModel>>(models);
            return viewModels;
        }


        [HttpPost]
        [Route("/lecturer/")]
        public IActionResult Create([FromBody] LecturerViewModel viewModel)
        {
            var model = _mapper.Map<LecturerViewModel, LecturerDTO>(viewModel);
            _service.Create(model);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(LecturerViewModel viewModel)
        {
            if (viewModel == null)
            {
                return NotFound();
            }
            var lecturer = _mapper.Map<LecturerViewModel, LecturerDTO>(viewModel);
            _service.Update(lecturer);
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

