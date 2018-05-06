using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Web.Http.Results;
using YogaApi.Controllers;
using YogaApi.Core.Models;
using YogaApi.Interfaces;
using YogaApi.Models;

namespace YogaApi.Tests.Controllers
{
    [TestFixture]
    public class SequencesControllerTests
    {
        private Mock<ISequencesService> _sequencesServiceMock;
        private SequencesController _sut;

        [SetUp]
        public void SetUp()
        {
            _sequencesServiceMock = new Mock<ISequencesService>();
            _sut = new SequencesController(_sequencesServiceMock.Object);
        }

        [Test]
        public async Task SaveSequence_calls_service()
        {
            await _sut.SaveSequence(It.IsAny<SequencePostModel>());

            _sequencesServiceMock.Verify(r => r.SaveSequence(It.IsAny<SequencePostModel>()), Times.Once);
        }

        [Test]
        public async Task SaveSequece_returns_200()
        {
            var result = await _sut.SaveSequence(It.IsAny<SequencePostModel>());

            Assert.IsInstanceOf<OkNegotiatedContentResult<ApiResponse<string>>>(result);
        }
    }
}
