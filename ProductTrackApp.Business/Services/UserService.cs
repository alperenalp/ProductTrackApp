using AutoMapper;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.DTOs.Responses;
using ProductTrackApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDisplayResponse> GetUserByIdAsync(int userId)
        {
            var user = await _repository.GetUserByIdAsync(userId);
            return _mapper.Map<UserDisplayResponse>(user);
        }

        public async Task<UserValidateResponse> ValidateUserAsync(ValidateUserLoginRequest request)
        {
            var users = await _repository.GetAllUserAsync();
            var response = users.SingleOrDefault(x => x.Username == request.Username && x.Password == request.Password);
            return _mapper.Map<UserValidateResponse>(response);
        }
    }
}
