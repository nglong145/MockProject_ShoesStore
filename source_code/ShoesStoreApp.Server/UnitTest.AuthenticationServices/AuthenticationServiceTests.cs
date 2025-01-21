using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.AuthenticationService;
using ShoesStoreApp.BLL.ViewModels.Auth;
using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Models;
using System.Data;

namespace UnitTest.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<UserManager<User>> _userManagerMock;
        private Mock<RoleManager<Role>> _roleManagerMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<ShoesStoreAppDbContext> _contextMock;
        private AuthenticationService _authenticationService;
        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<UserManager<User>>(
            new Mock<IUserStore<User>>().Object,
            null, null, null, null, null, null, null, null);

            _roleManagerMock = new Mock<RoleManager<Role>>(
                new Mock<IRoleStore<Role>>().Object,
                null, null, null, null);

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(config => config["JWT:Secret"])
                     .Returns("this-is-a-our-project-secret-key");
            _configurationMock.Setup(config => config["JWT:Issuer"])
                              .Returns("https://localhost:7158");
            _configurationMock.Setup(config => config["JWT:Audience"])
                              .Returns("User");
            _contextMock = new Mock<ShoesStoreAppDbContext>(new DbContextOptions<ShoesStoreAppDbContext>());


            _authenticationService = new AuthenticationService(
                _userManagerMock.Object,
                _roleManagerMock.Object,
                _configurationMock.Object,
                _contextMock.Object
            );
        }

        [Test]
        public async Task RegisterUserAsync_ShouldReturnSuccessMessage_WhenUserIsRegistered()
        {
            // Arrange
            var registerVm = new RegisterVm
            {
                FullName = "Test User",
                Email = "test@example.com",
                Password = "Password@123"
            };

            // Email not already exsist.
            _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                            .ReturnsAsync((User)null); 
            _roleManagerMock.Setup(r => r.FindByNameAsync(It.IsAny<string>()))
                            .ReturnsAsync(new Role { Id = Guid.NewGuid(), Name = "User" });
            _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authenticationService.RegisterUserAsync(registerVm);

            // Assert
            Assert.AreEqual($"User {registerVm.Email} created!", result);
        }

        [Test]
        public void RegisterUserAsync_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var registerVm = new RegisterVm
            {
                FullName = "Test User",
                Email = "test@example.com",
                Password = "Password@123"
            };

            // Email already exist.
            _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                            .ReturnsAsync(new User()); 

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _authenticationService.RegisterUserAsync(registerVm));
        }

        [Test]
        public void LoginUserAsync_ShouldThrowException_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginVm = new LoginVm
            {
                Email = "test@example.com",
                Password = "Password@123"
            };

            // User not found.
            _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                            .ReturnsAsync((User)null); 

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _authenticationService.LoginUserAsync(loginVm));
        }


        [Test]
        public async Task ChangePasswordAsync_ShouldReturnTrue_WhenPasswordIsChanged()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var passwordVm = new ChangePasswordVm
            {
                OldPassword = "OldPassword@123",
                NewPassword = "NewPassword@123"
            };

            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                            .ReturnsAsync(new User { Id = userId });
            // Old password is correct
            _userManagerMock.Setup(u => u.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(true); 
            _userManagerMock.Setup(u => u.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authenticationService.ChangePasswordAsync(userId, passwordVm);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ChangePasswordAsync_ShouldThrowException_WhenOldPasswordIsIncorrect()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var passwordVm = new ChangePasswordVm
            {
                OldPassword = "IncorrectOldPassword",
                NewPassword = "NewPassword@123"
            };

            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                            .ReturnsAsync(new User { Id = userId });
            // Old password is incorrect
            _userManagerMock.Setup(u => u.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _authenticationService.ChangePasswordAsync(userId, passwordVm));
        }

        [Test]
        public async Task RefreshTokenAsync_ShouldReturnNewAuthResult_WhenTokenIsValid()
        {
            var userRole = new Role { Id = Guid.NewGuid(), Name = "User" };
            _contextMock.Setup(c => c.Roles)
                        .ReturnsDbSet(new List<Role> { userRole });

            var refreshToken = "valid-refresh-token";
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                RoleId = userRole.Id
            };
            var storedToken = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                DateExpire = DateTime.UtcNow.AddDays(1),
                IsRevoked = false
            };

            _contextMock.Setup(c => c.RefreshToken)
                        .ReturnsDbSet(new List<RefreshToken> { storedToken });

            _userManagerMock.Setup(u => u.FindByIdAsync(user.Id.ToString()))
                            .ReturnsAsync(user);

            _configurationMock.Setup(config => config["JWT:Secret"])
                              .Returns("this-is-a-strong-test-secret-key!");
            _configurationMock.Setup(config => config["JWT:Issuer"])
                              .Returns("https://localhost");
            _configurationMock.Setup(config => config["JWT:Audience"])
                              .Returns("User");

            // Act
            var result = await _authenticationService.RefreshTokenAsync(refreshToken);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<AuthResultVm>(result);
        }

        [Test]
        public void RefreshTokenAsync_ShouldThrowException_WhenTokenIsExpired()
        {
            // Arrange
            var refreshToken = "expired-token";
            var storedToken = new RefreshToken
            {
                Token = refreshToken,
                DateExpire = DateTime.UtcNow.AddDays(-1),
                IsRevoked = false
            };

            _contextMock.Setup(c => c.RefreshToken)
                        .ReturnsDbSet(new List<RefreshToken> { storedToken });

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authenticationService.RefreshTokenAsync(refreshToken));
        }

        [Test]
        public async Task GetUserInfoAsync_ShouldReturnUserVm_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FullName = "Test User",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Address = "Test Address"
            };

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync(user);

            // Act
            var result = await _authenticationService.GetUserInfoAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(user.FullName, result.FullName);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(user.Address, result.Address);
        }

        [Test]
        public void GetUserInfoAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _authenticationService.GetUserInfoAsync(userId));
        }


        [Test]
        public async Task UpdateUserInfoAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUserVm = new UpdateUserVm
            {
                FullName = "Updated User",
                PhoneNumber = "987654321",
                Address = "Updated Address",
                Avatar = "avatar.png"
            };

            var user = new User { Id = userId };

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync(user);
            _userManagerMock.Setup(u => u.UpdateAsync(It.IsAny<User>()))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authenticationService.UpdateUserInfoAsync(userId, updateUserVm);

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void ChangePasswordAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var passwordVm = new ChangePasswordVm
            {
                OldPassword = "OldPassword@123",
                NewPassword = "NewPassword@123"
            };

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync((User)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _authenticationService.ChangePasswordAsync(userId, passwordVm));
        }

        [Test]
        public void RegisterUserAsync_ShouldThrowException_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var registerVm = new RegisterVm
            {
                FullName = "Test User",
                Email = "test@example.com",
                Password = "Password@123"
            };

            _userManagerMock.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                            .ReturnsAsync((User)null);
            _roleManagerMock.Setup(r => r.FindByNameAsync(It.IsAny<string>()))
                            .ReturnsAsync(new Role { Id = Guid.NewGuid(), Name = "User" });
            _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ThrowsAsync(new Exception("Unexpected error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _authenticationService.RegisterUserAsync(registerVm));
            Assert.That(ex.Message, Is.EqualTo("Unexpected error"));
        }


    }
}