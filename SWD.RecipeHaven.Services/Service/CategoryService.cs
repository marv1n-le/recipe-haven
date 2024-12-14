using Microsoft.EntityFrameworkCore;
using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.DTOs;
using SWD.RecipeHaven.Repo.requestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{

    public interface ICategoryService
    {
        Task<Paginate<CategoryDTO>> GetAll(int page);
        Task<CategoryDTO> GetById(int id);
        Task Create(CategoryRequest entity);
        Task Update(int id, CategoryRequest entity);
        Task Delete(int id);
        Task<List<Category>> Search(string searchTerm);
    }
    public class CategoryService : ICategoryService
    {
        private readonly GenericRepository<Category> _genericRepository;

        public CategoryService()
        {
            _genericRepository ??= new GenericRepository<Category>();
        }

        public async Task Create(CategoryRequest entity)
        {
            try
            {
                //var check = await _genericRepository.GetByIdAsync(entity.CategoryId);
                //if (check != null)
                //{
                //    throw new Exception("Category already exists.");
                //}

                var result = new Category();
                result.Name = entity.Name;
                result.Description = entity.Description;
                result.ActiveStatus = true;

                // Create
                await _genericRepository.CreateAsync(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating : " + ex.Message, ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var check = await _genericRepository.GetByIdAsync(id);
                if (check == null)
                {
                    throw new Exception("Item not found");
                }

                check.ActiveStatus = false;

                await _genericRepository.UpdateAsync(check);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while delete : " + ex.Message, ex);
            }
        }

        public async Task<Paginate<CategoryDTO>> GetAll(int page)
        {
            var result = await _genericRepository.GetPagingListAsync(
               selector: x => x,
               page: page,
               size: 10,
               predicate: x => x.ActiveStatus == true
               );

            if (result == null)
            {
                throw new Exception("No Result");
            }

            var paginate = new Paginate<CategoryDTO>();
            paginate.Items = MapObjectToDTO(result.Items);
            paginate.Size = result.Size;
            paginate.Page = result.Page;
            paginate.TotalPages = result.TotalPages;
            paginate.Total = result.Total;

            return paginate;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                                           predicate: x => x.ActiveStatus == true && x.CategoryId == id

                            );

            return MapObjectToDTO(category);
        }

        public Task<List<Category>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, CategoryRequest entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.GetByIdAsync(id);
                if (tmpEntity == null)
                {
                    throw new Exception("Item not found");
                }

                tmpEntity.Name = entity.Name;
                tmpEntity.Description = entity.Description;
                //tmpEntity.ActiveStatus = entity.ActiveStatus;


                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update : " + ex.Message, ex);
            }
        }

        private List<CategoryDTO> MapObjectToDTO(IEnumerable<Category> categorys)
        {
            if (categorys == null || !categorys.Any())
            {
                return new List<CategoryDTO>();
            }

            List<CategoryDTO> result = new List<CategoryDTO>();

            foreach (var category in categorys)
            {
                var dto = new CategoryDTO
                {
                    ID =  category.CategoryId,
                    Name = category.Name,
                    Description = category.Description,
                };

                result.Add(dto);
            }
            return result;
        }

        // Overload for single entity
        private CategoryDTO MapObjectToDTO(Category entity)
        {
            if (entity == null)
            {
                return null;
            }

            return MapObjectToDTO(new List<Category> { entity }).FirstOrDefault();
        }

    }
}
