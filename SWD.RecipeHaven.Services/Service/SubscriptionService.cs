using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.responseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{
    public interface ISubscriptionService
    {
        Task<Paginate<SubscriptionDTO>> GetAll(int page);
        Task<Subscription> GetById(int id);
        Task<SubscriptionDTO> GetByIdd(int id);
        Task Create(CreateSubscriptionDTO subscription);
        Task Update(int id, UpdateSubscriptionDTO subscription);
        Task Delete(int id);
    }

    public class SubscriptionService : ISubscriptionService
    {
        private readonly GenericRepository<Subscription> _genericRepository;

        public SubscriptionService()
        {
            _genericRepository ??= new GenericRepository<Subscription>();
        }

        public async Task Create(CreateSubscriptionDTO entity)
        {
            try
            {
                var result = new Subscription
                {
                    Name = entity.Name,
                    Price = entity.Price,
                    Duration = entity.Duration,
                    ActiveStatus = true
                };

                await _genericRepository.CreateAsync(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating: " + ex.Message, ex);
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
                throw new Exception("Error while deleting: " + ex.Message, ex);
            }
        }

        public async Task<Paginate<SubscriptionDTO>> GetAll(int page)
        {
            var result = await _genericRepository.GetPagingListAsync(
                selector: x => x,
                page: page,
                size: 10,
                predicate: x => x.ActiveStatus == true
            );

            if (result == null)
            {
                throw new Exception("No results found");
            }

            return new Paginate<SubscriptionDTO>
            {
                Items = result.Items.Select(x => new SubscriptionDTO
                {
                    Id = x.SubscriptionId,
                    Name = x.Name,
                    Price = x.Price,
                    Duration = x.Duration
                }).ToList(),
                Size = result.Size,
                Page = result.Page,
                TotalPages = result.TotalPages,
                Total = result.Total
            };
        }

        public async Task<SubscriptionDTO> GetByIdd(int id)
        {
            var entity = await _genericRepository.SingleOrDefaultAsync(
                selector: x => x,
                predicate: x => x.SubscriptionId == id && x.ActiveStatus == true
            );

            if (entity == null)
                throw new Exception("Item not found");

            return new SubscriptionDTO
            {
                Id = entity.SubscriptionId,
                Name = entity.Name,
                Price = entity.Price,
                Duration = entity.Duration
            };
        }

        public async Task<Subscription> GetById(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Item not found");
            }

            return entity;
        }   

        public async Task Update(int id, UpdateSubscriptionDTO entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.GetByIdAsync(id);
                if (tmpEntity == null)
                {
                    throw new Exception("Item not found");
                }

                tmpEntity.Name = entity.Name;
                tmpEntity.Price = entity.Price;
                tmpEntity.Duration = entity.Duration;

                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating: " + ex.Message, ex);
            }
        }
    }
}
