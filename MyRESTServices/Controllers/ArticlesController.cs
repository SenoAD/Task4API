using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRESTService.BLL.DTOs;
using MyRESTService.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleBLL _articleBLL;
        //private readonly IValidator<CategoryCreateDTO> _validatorCategoryCreate;
        //private readonly IValidator<CategoryUpdateDTO> _validatorCategoryUpdate;
        public ArticlesController(IArticleBLL articleBLL)
        {
            _articleBLL = articleBLL;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> Get()
        {
            var results = await _articleBLL.GetAll();
            return results;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _articleBLL.GetArticleById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Category/{id}")]
        public async Task<IActionResult> GetbyCategoryID(int id)
        {
            var result = await _articleBLL.GetArticleByCategory(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("with-Category")]
        public async Task<IActionResult> GetwithCategory()
        {
            var result = await _articleBLL.GetArticleWithCategory();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ArticleCreateDTO articleCreateDTO)
        {
            if (articleCreateDTO == null)
            {
                return BadRequest();
            }

            try
            {
                await _articleBLL.Insert(articleCreateDTO);
                return Ok("Insert data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ArticleUpdateDTO articleUpdateDTO)
        {
            if (await _articleBLL.GetArticleById(id) == null)
            {
                return NotFound();
            }

            try
            {
                //var validatorResult = await _validatorCategoryUpdate.ValidateAsync(categoryUpdateDTO);
                //if (!validatorResult.IsValid)
                //{
                //    Helpers.Extensions.AddToModelState(validatorResult, ModelState);
                //    return BadRequest(ModelState);
                //}
                await _articleBLL.Update(articleUpdateDTO);
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
            if (await _articleBLL.GetArticleById(id) == null)
            {
                return NotFound();
            }

            try
            {
                await _articleBLL.Delete(id);
                return Ok("Delete data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

