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
    public class HomeworkController : ControllerBase
    {
        private IHomeworkService _service;
        private IMapper _mapper;

        public HomeworkController(IHomeworkService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/homework/{id}")]
        public HomeworkViewModel Get(int id)
        {
            var model = _service.Get(id);
            var viewModel = _mapper.Map<HomeworkDTO, HomeworkViewModel>(model);
            return viewModel;
        }

        [HttpGet]
        [Route("/homework/")]
        public IEnumerable<HomeworkViewModel> GetAll()
        {
            var models = _service.GetAll();
            var viewModels = _mapper.Map<IEnumerable<HomeworkDTO>, IEnumerable<HomeworkViewModel>>(models);
            return viewModels;
        }

        
        [HttpPost]
        [Route("/homework/")]
        public IActionResult Create([FromBody] HomeworkViewModel viewModel)
        {
            var model = _mapper.Map<HomeworkViewModel, HomeworkDTO>(viewModel);
            _service.Create(model);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(HomeworkViewModel viewModel)
        {
            if (viewModel == null)
            {
                return NotFound();
            }
            var homework = _mapper.Map<HomeworkViewModel, HomeworkDTO>(viewModel);
            _service.Update(homework);
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
