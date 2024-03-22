using MyWebFormApp.BLL.DTOs;

namespace SampleMVC.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);
        Task<Task> Insert(CategoryCreateDTO categoryCreateDTO);
        Task<Task> Update(int id, CategoryUpdateDTO categoryUpdateDTO);
        Task<Task> Delete(int id);
        Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name);
        Task<int> GetCountCategories(string name);

    }
}
