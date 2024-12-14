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

    public interface IOriginService
    {
        Task<Paginate<OriginDTO>> GetAll(int page);
        Task<OriginDTO> GetById(int id);
        Task Create(OriginRequest entity);
        Task Update(int id, OriginRequest entity);
        Task Delete(int id);
        Task<List<OriginDTO>> Search(string searchTerm);
    }
    public class OriginService : IOriginService
    {
        private readonly GenericRepository<Origin> _genericRepository;

        public OriginService() { _genericRepository ??= new GenericRepository<Origin>(); }

        public async Task Create(OriginRequest entity)
        {
            try
            {
                //var check = await _genericRepository.GetByIdAsync(entity.OriginId);
                //if (check != null)
                //{
                //    throw new Exception("Origin already exists.");
                //}
                var result = new Origin();
                result.Country = entity.Country;
                result.Region = entity.Region;
                result.ActiveStatus = true;
                result.CultureDescription = entity.CultureDescription;

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

        public async Task<Paginate<OriginDTO>> GetAll(int page)
        {
            var result = await _genericRepository.GetPagingListAsync(
               predicate: x => x.ActiveStatus == true,
               page: page,
               size: 10,
               selector: x => x);

            if (result == null)
            {
                throw new Exception("No Result");
            }

            var paginate = new Paginate<OriginDTO>();
            paginate.Items = MapObjectToDTO(result.Items);
            paginate.Size = result.Size;
            paginate.Page = result.Page;
            paginate.TotalPages = result.TotalPages;
            paginate.Total = result.Total;

            return paginate;
        }

        public async Task<OriginDTO> GetById(int id)
        {
            var result = await _genericRepository.SingleOrDefaultAsync(
                            selector: x => x,
                            predicate: x => x.OriginId.Equals(id));
            return MapObjectToDTO(result);

        }

        public Task<List<OriginDTO>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, OriginRequest entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.GetByIdAsync(id);
                if (tmpEntity == null)
                {
                    throw new Exception("Item not found");
                }

                tmpEntity.Region = entity.Region;
                tmpEntity.Country = entity.Country;
                tmpEntity.CultureDescription = entity.CultureDescription;

                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while update : " + ex.Message, ex);
            }
        }

        private List<OriginDTO> MapObjectToDTO(IEnumerable<Origin> entityList)
        {
            if (entityList == null || !entityList.Any())
            {
                return new List<OriginDTO>();
            }

            var result = new List<OriginDTO>();

            foreach (var entity in entityList)
            {
                var dto = new OriginDTO
                {
                    Id = entity.OriginId,
                    Country = entity.Country,
                    CultureDescription = entity.CultureDescription,
                    Region = entity.Region
                };

                result.Add(dto);
            }
            return result;
        }

        // Overload for single entity
        private OriginDTO MapObjectToDTO(Origin entity)
        {
            if (entity == null)
            {
                return null;
            }

            return MapObjectToDTO(new List<Origin> { entity }).FirstOrDefault();
        }
    }
}
