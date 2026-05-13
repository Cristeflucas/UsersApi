using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsersApi.Data.Dtos;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class UserService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        public SignInManager<User> _singInManager;
        private TokenService _tokenService;

        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _singInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Register(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("User registration failed");
            }
        }

        public async Task<string> Login(LoginUserDto dto)
        {
            var result = await _singInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if(!result.Succeeded)
            {
                throw new ApplicationException("User not authenticated");
            }
            var user = _singInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedUserName == dto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }
}
