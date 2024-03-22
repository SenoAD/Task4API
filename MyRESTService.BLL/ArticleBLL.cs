using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyRESTService.BLL.DTOs;
using MyRESTService.BLL.Interfaces;
using MyRESTService.Data;
using MyRESTService.Data.Interfaces;
using MyRESTService.Domain.Models;

namespace MyRESTService.BLL
{
    public class ArticleBLL : IArticleBLL
    {
        private readonly IArticleData _articleData;
        private readonly IMapper _mapper;
        public ArticleBLL(IArticleData articleData, IMapper mapper)
        {
            _articleData = articleData;
            _mapper = mapper;   
        }
        public async Task<Task> Delete(int id)
        {
            await _articleData.Delete(id);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<ArticleDTO>> GetAll()
        {
            var allArticle = await _articleData.GetAll();
            return _mapper.Map<IEnumerable<ArticleDTO>>(allArticle);
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
        {
            var articlebyCategory = await _articleData.GetArticleByCategory(categoryId);
            return _mapper.Map<IEnumerable<ArticleDTO>>(articlebyCategory);
        }

        public async Task<ArticleDTO> GetArticleById(int id)
        {
            var article = await _articleData.GetById(id);
            return _mapper.Map<ArticleDTO>(article);
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticleWithCategory()
        {
            var articlewithCategories = await _articleData.GetArticleWithCategory();
            return _mapper.Map<IEnumerable<ArticleDTO>>(articlewithCategories);
        }

        public Task<int> GetCountArticles()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Task> Insert(ArticleCreateDTO article)
        {
            var articleInsert = _mapper.Map<Article>(article);
            await _articleData.Insert(articleInsert);
            return Task.CompletedTask;
        }

        public Task<int> InsertWithIdentity(ArticleCreateDTO article)
        {
            throw new NotImplementedException();
        }

        public async Task<Task> Update(ArticleUpdateDTO article)
        {
            var articleUpdate = _mapper.Map<Article>(article);
            await _articleData.Update(articleUpdate.ArticleId, articleUpdate);
            return Task.CompletedTask;
        }
    }
}
