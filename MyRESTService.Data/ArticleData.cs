using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyRESTService.Data.Interfaces;
using MyRESTService.Domain.Models;

namespace MyRESTService.Data
{
    public class ArticleData : IArticleData
    {
        private readonly AppDbContext _appDbContext;
        public ArticleData(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }
        public async Task Delete(int id)
        {
            var article = await _appDbContext.Articles.FindAsync(id);
            _appDbContext.Articles.Remove(article);

            // Step 3: Save changes to persist the deletion
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _appDbContext.Articles.ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticleByCategory(int categoryId)
        {
            return await _appDbContext.Articles.Where(c => c.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticleWithCategory()
        {
            return await _appDbContext.Articles.Include(a => a.Category).ToListAsync();
        }

        public async Task<Article> GetById(int id)
        {
            return await _appDbContext.Articles.SingleOrDefaultAsync(c => c.ArticleId == id);
        }

        public Task<int> GetCountArticles()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> Insert(Article entity)
        {
            await _appDbContext.Articles.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public Task<Task> InsertArticleWithCategory(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertWithIdentity(Article article)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> Update(int id, Article entity)
        {
            var article = await _appDbContext.Articles.FindAsync(id);
            if (article == null)
            {
                return null;
            }
            article.Title = entity.Title;
            article.Details = entity.Details;
            article.IsApproved = entity.IsApproved;
            article.Pic = entity.Pic;
            await _appDbContext.SaveChangesAsync();
            return article;
        }
    }
}
