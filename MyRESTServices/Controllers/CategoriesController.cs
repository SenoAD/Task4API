using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyRESTService.BLL.DTOs;
using MyRESTService.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBLL _categoryBLL;
        private readonly IValidator<CategoryCreateDTO> _validatorCategoryCreate;
        private readonly IValidator<CategoryUpdateDTO> _validatorCategoryUpdate;
        public CategoriesController(ICategoryBLL categoryBLL, IValidator<CategoryCreateDTO> categoryCreateDTO,
            IValidator<CategoryUpdateDTO> categoryUpdateDTO)
        {
            _categoryBLL = categoryBLL;
            _validatorCategoryCreate = categoryCreateDTO;
            _validatorCategoryUpdate = categoryUpdateDTO;   
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            var results = await _categoryBLL.GetAll();
            return results;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryBLL.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _categoryBLL.GetByName(name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("forPaging/{name}")]
        public async Task<IActionResult> GetCount(string name = "")
        {
            var result = await _categoryBLL.GetCountCategories(name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("withPaging/{name}/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> Get(string name = "", int pageNumber= 1, int pageSize=5)
        {
            var result = await _categoryBLL.GetWithPaging(pageNumber, pageSize, name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CategoryCreateDTO categoryCreateDTO)
        {
            if (categoryCreateDTO == null)
            {
                return BadRequest();
            }

            try
            {
                _categoryBLL.Insert(categoryCreateDTO);
                return Ok("Insert data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            if (await _categoryBLL.GetById(id) == null)
            {
                return NotFound();
            }

            try
            {
                var validatorResult = await _validatorCategoryUpdate.ValidateAsync(categoryUpdateDTO);
                if (!validatorResult.IsValid)
                {
                    Helpers.Extensions.AddToModelState(validatorResult, ModelState);
                    return BadRequest(ModelState);
                }
                await _categoryBLL.Update(categoryUpdateDTO);
                return Ok("Update data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _categoryBLL.GetById(id) == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryBLL.Delete(id);
                return Ok("Delete data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
