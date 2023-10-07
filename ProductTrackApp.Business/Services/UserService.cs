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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserValidateResponse> ValidateUserAsync(ValidateUserLoginRequest request)
        {
            var users = await _userRepository.GetAllUserAsync();
            var response = users.SingleOrDefault(x => x.Username == request.Username && x.Password == request.Password);
            return _mapper.Map<UserValidateResponse>(response);
        }
    }
}
