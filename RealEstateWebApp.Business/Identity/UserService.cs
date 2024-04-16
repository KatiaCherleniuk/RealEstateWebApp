using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.DataAccess.Repositories.Identity;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.User;
using Microsoft.AspNetCore.Identity;

namespace RealEstateWebApp.Business.Identity
{
    public class UserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        #region identity

        public async Task<SignInResult> CheckPassword(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return SignInResult.Failed;
            
            var user = await GetUserByLogin(login);
            if (user == null)
                return SignInResult.Failed;
            return await _signInManager.CheckPasswordSignInAsync(user, password, true);
        }

        public async Task<ApplicationUser> GetUserByLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;
            
            ApplicationUser user;
            
            user = await _userManager.FindByEmailAsync(login);
            if (user == null)
                return null;

            if (user.IsBlocked)
                return null;
            
            return user;
        }

        public async Task<SignInResult> Login(string login, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await GetUserByLogin(login);
            if (user == null)
                return SignInResult.Failed;
            
            var res = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
            return res;
        }

        public Task Logout()
        {
            return _signInManager.SignOutAsync();
        }

        #endregion


        public async Task<IEnumerable<UserCreateModel>> GetAllUsersForEdit()
        {
            var a = await _userRepository.GetByRoleId(1);

            return await _userRepository.GetAllUsersForEdit();
        }
        public async Task<(IEnumerable<UserCreateModel>, int)> GetAllManagers(string filter, int pageSize, int currentStep)
        {
            var result = await GetAllUsersForEdit();
            var filteredResult = result.Where(user => user.RoleId == 3 &&
            (
                user.GetType().GetProperties().Any(property =>
                {
                    var value = property.GetValue(user);
                    return value != null && value.ToString().Contains(filter);
                })
            ));
            var totalCount = filteredResult.Count();

            var pageIdList = filteredResult.Skip((currentStep - 1) * pageSize).Take(pageSize).ToArray();

            return (pageIdList, totalCount);
        }

        public async Task<(IEnumerable<UserCreateModel>, int)> GetAllGuests(string filter, int pageSize, int currentStep)
        {
            var result = await GetAllUsersForEdit();
            var filteredResult = result.Where(user => user.RoleId == 2 &&
            (
                user.GetType().GetProperties().Any(property =>
                {
                    var value = property.GetValue(user);
                    return value != null && value.ToString().Contains(filter);
                })
            ));
            var totalCount = filteredResult.Count();

            var pageIdList = filteredResult.Skip((currentStep - 1) * pageSize).Take(pageSize).ToArray();

            return (pageIdList, totalCount);
        }

        public Task<IEnumerable<TitleAndIdModel>> GetAllRoles()
        {
            return _roleRepository.GetAll();
        }

        public Task<bool> SetBlockForUser(int userId, bool isBlocked)
        {
            return _userRepository.SetBlockForUser(userId, isBlocked);
        }

        public Task<bool> Delete(UserCreateModel user)
        {
            return _userRepository.Remove(user.Id);
        }

        public Task<IdentityResult> Create(UserCreateModel user, string password)
        {
            user.EmailConfirmed = true;
            user.CreatedAt = DateTime.UtcNow.Date;
            //todo change this
            return _userManager.CreateAsync(user, password);
        }
    }
}