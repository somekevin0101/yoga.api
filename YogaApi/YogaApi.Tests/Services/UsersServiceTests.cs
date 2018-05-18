using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YogaApi.Core.Interfaces;
using YogaApi.Core.Models;
using YogaApi.Models;
using YogaApi.Services.LevelOne;

namespace YogaApi.Tests.Services
{
    [TestFixture]
    public class UsersServiceTests
    {
        private Mock<IUserRepository> _usersRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Fixture _fixture;
        private UserService _sut;

        [SetUp]
        public void SetUp()
        {
            _usersRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _fixture = new Fixture();
            _sut = new UserService(_usersRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task CreateUser_calls_repository()
        {
            var model = _fixture.Create<UserPostModel>();
            await _sut.CreateUser(model);

            _usersRepositoryMock.Verify(r => r.CreateUser(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task CreateUser_calls_mapper()
        {
            var model = _fixture.Create<UserPostModel>();
            await _sut.CreateUser(model);

            _mapperMock.Verify(r => r.Map<User>(model), Times.Once);
        }

        [Test]
        public async Task GetShallowSequences_calls_repository()
        {
            await _sut.GetShallowSequences(1);

            _usersRepositoryMock.Verify(r => r.GetShallowSequences(1), Times.Once);
        }

        [Test]
        public async Task GetShallowSequences_calls_mapper()
        {
            var model = _fixture.Create<IEnumerable<ShallowSequence>>();
            _usersRepositoryMock.Setup(r => r.GetShallowSequences(1)).ReturnsAsync(model);

            await _sut.GetShallowSequences(1);

            _mapperMock.Verify(r => r.Map<IEnumerable<SequenceGetModel>>(model), Times.Once);
        }

        [Test]
        public async Task GetUser_calls_repository()
        {
            await _sut.GetUser(1);

            _usersRepositoryMock.Verify(r => r.GetUser(1), Times.Once);
        }

        [Test]
        public async Task GetUser_returns_api_repsponse_404_when_repository_returns_null()
        {
            var result = await _sut.GetUser(1);

            Assert.AreEqual(404, result.httpStatusCode);
        }

        [Test]
        public async Task GetUser_returns_api_response_200_when_repository_returns_model()
        {
            var user = _fixture.Create<User>();
            _usersRepositoryMock.Setup(r => r.GetUser(1)).ReturnsAsync(user);

            var result = await _sut.GetUser(1);

            Assert.AreEqual(200, result.httpStatusCode);
        }

        [Test]
        public async Task GetUser_calls_mapper()
        {
            var user = _fixture.Create<User>();
            _usersRepositoryMock.Setup(r => r.GetUser(1)).ReturnsAsync(user);

            await _sut.GetUser(1);

            _mapperMock.Verify(r => r.Map<UserGetModel>(user), Times.Once);
        }
    }
}
