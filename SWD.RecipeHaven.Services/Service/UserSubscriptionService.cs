using SWD.RecipeHaven.Data.Models;
using SWD.RecipeHaven.Repo.responseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.RecipeHaven.Services.Service
{
    public interface IUserSubscriptionService
    {
        Task<Paginate<UserSubscriptionDTO>> GetAll(int page);
        Task<UserSubscriptionDTO> GetById(int id);
        Task Create(CreateUserSubscriptionDTO entity);
        Task Update(int id, UpdateUserSubscriptionDTO entity);
        Task Delete(int id);
        Task<int> GetAmoutView(int? id);
    }
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly GenericRepository<UserSubscription> _genericRepository;

        public UserSubscriptionService()
        {
            _genericRepository ??= new GenericRepository<UserSubscription>();
        }

        public async Task Create(CreateUserSubscriptionDTO entity)
        {
            try
            {
                var result = new UserSubscription
                {
                    UserId = entity.UserId,
                    SubscriptionId = entity.SubscriptionId,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
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

        public async Task<Paginate<UserSubscriptionDTO>> GetAll(int page)
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

            return new Paginate<UserSubscriptionDTO>
            {
                Items = result.Items.Select(x => new UserSubscriptionDTO
                {
                    Id = x.UserSubscriptionId,
                    UserId = x.UserId,
                    SubscriptionId = x.SubscriptionId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList(),
                Size = result.Size,
                Page = result.Page,
                TotalPages = result.TotalPages,
                Total = result.Total
            };
        }

        public async Task<UserSubscriptionDTO> GetById(int id)
        {
            var entity = await _genericRepository.SingleOrDefaultAsync(
                selector: x => x,
                predicate: x => x.UserSubscriptionId == id && x.ActiveStatus == true
            );

            if (entity == null)
                throw new Exception("Item not found");

            return new UserSubscriptionDTO
            {
                Id = entity.UserSubscriptionId,
                UserId = entity.UserId,
                SubscriptionId = entity.SubscriptionId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }
        
        public async Task<int> GetAmoutView(int? id)
        {
            var amout = await _genericRepository.SingleOrDefaultAsync(
                selector: x => x.Subscription.Duration,
                predicate: x => x.User.UserId == id 
            );

            if (amout == null)
                return 0;

            return amout;
        }

        public async Task Update(int id, UpdateUserSubscriptionDTO entity)
        {
            try
            {
                var tmpEntity = await _genericRepository.GetByIdAsync(id);
                if (tmpEntity == null)
                {
                    throw new Exception("Item not found");
                }

                tmpEntity.SubscriptionId = entity.Id;
                tmpEntity.StartDate = entity.StartDate;
                tmpEntity.EndDate = entity.EndDate;

                await _genericRepository.UpdateAsync(tmpEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating: " + ex.Message, ex);
            }
        }
    }
}
