using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyRESTService.BLL.DTOs;
using MyRESTService.BLL.Interfaces;
using MyRESTService.Data.Interfaces;
using MyRESTService.Domain.Models;

namespace MyRESTService.BLL
{
    public class CategoryBLL : ICategoryBLL
    {
        private readonly ICategoryData _categoryData;
        private readonly IMapper _mapper;

        public CategoryBLL(ICategoryData categoryData, IMapper mapper) 
        { 
            _categoryData = categoryData;
            _mapper = mapper;
        }
        public async Task<Task> Delete(int id)
        {
            await _categoryData.Delete(id);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var allCategory = await _categoryData.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(allCategory);
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _categoryData.GetById(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetByName(string name)
        {
            var categories = await _categoryData.GetByName(name);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<int> GetCountCategories(string name)
        {
            var count = await _categoryData.GetCountCategories(name);
            return count;
        }

        public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            var categories = await _categoryData.GetWithPaging(pageNumber, pageSize, name);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return categoriesDto;
        }

        public async Task<Task> Insert(CategoryCreateDTO entity)
        {
            var categoryInsert = _mapper.Map<Category>(entity);
            await _categoryData.Insert(categoryInsert);
            return Task.CompletedTask;
        }

        public async Task<Task> Update(CategoryUpdateDTO entity)
        {
            var category = _mapper.Map<Category>(entity);
            await _categoryData.Update(category.CategoryId, category);
            return Task.CompletedTask;
        }
    }
}
