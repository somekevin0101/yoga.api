using AutoFixture;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using YogaApi.Controllers;
using YogaApi.Core.Models;
using YogaApi.Interfaces;
using YogaApi.Models;

namespace YogaApi.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUsersService> _usersServiceMock;
        private Fixture _fixure;
        private UsersController _sut;

        [SetUp]
        public void SetUp()
        {
            _usersServiceMock = new Mock<IUsersService>();
            _fixure = new Fixture();
            _sut = new UsersController(_usersServiceMock.Object);
        }

        [Test]
        public async Task GetUser_calls_service()
        {
            var response = _fixure.Create<ApiResponse<UserGetModel>>();
            _usersServiceMock.Setup(r => r.GetUser(1)).ReturnsAsync(response);

            await _sut.GetUser(1);

            _usersServiceMock.Verify(r => r.GetUser(1), Times.Once);    
        }

        [Test]
        public async Task GetUser_returns_404_when_service_returns_404()
        {
            var response = _fixure.Create<ApiResponse<UserGetModel>>();
            response.httpStatusCode = 404;
            _usersServiceMock.Setup(r => r.GetUser(1)).ReturnsAsync(response);

            var result = await _sut.GetUser(1);

            var actual = ((NegotiatedContentResult<ApiResponse<UserGetModel>>)result);

            Assert.AreEqual(actual.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetUser_returns_200_when_service_returns_200()
        {
            var response = _fixure.Create<ApiResponse<UserGetModel>>();
            response.httpStatusCode = 200;
            _usersServiceMock.Setup(r => r.GetUser(1)).ReturnsAsync(response);

            var result = await _sut.GetUser(1);

            var actual = ((OkNegotiatedContentResult<ApiResponse<UserGetModel>>)result);

            Assert.IsInstanceOf<OkNegotiatedContentResult<ApiResponse<UserGetModel>>>(actual);
        }

        [Test]
        public async Task CreateUser_calls_service()
        {
            await _sut.CreateUser(It.IsAny<UserPostModel>());

            _usersServiceMock.Verify(r => r.CreateUser(It.IsAny<UserPostModel>()), Times.Once);
        }

        [Test]
        public async Task CreateUser_returns_200()
        {
            var result = await _sut.CreateUser(It.IsAny<UserPostModel>());
            var actual = ((OkNegotiatedContentResult<ApiResponse<int>>)result);

            Assert.IsInstanceOf<OkNegotiatedContentResult<ApiResponse<int>>>(actual);
        }

        [Test]
        public async Task GetShallowSequences_calls_service()
        {
            await _sut.GetShallowSequences(1);

            _usersServiceMock.Verify(r => r.GetShallowSequences(1), Times.Once);
        }

        [Test]
        public async Task GetShallowSequences_returns_200()
        {
            var result = await _sut.GetShallowSequences(1);
            var actual = ((OkNegotiatedContentResult<ApiResponse<IEnumerable<SequenceGetModel>>>)result);

            Assert.IsInstanceOf<OkNegotiatedContentResult<ApiResponse<IEnumerable<SequenceGetModel>>>>(actual);
        }
    }
}
