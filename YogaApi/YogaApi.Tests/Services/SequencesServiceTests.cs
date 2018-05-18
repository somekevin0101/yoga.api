using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using YogaApi.Core.Interfaces;
using YogaApi.Core.Models;
using YogaApi.Models;
using YogaApi.Services.LevelOne;

namespace YogaApi.Tests.Services
{
    [TestFixture]
    public class SequencesServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ISequenceRepository> _sequencesRepositoryMock;
        private Fixture _fixture;
        private SequenceService _sut;

        [SetUp]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();
            _sequencesRepositoryMock = new Mock<ISequenceRepository>();
            _fixture = new Fixture();
            _sut = new SequenceService(_sequencesRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task SaveSequence_calls_mapper()
        {
            var model = _fixture.Create<SequencePostModel>();
            var coreModel = _fixture.Create<Sequence>();
            coreModel.Poses[0].OrderInSequence = 1;
            coreModel.Poses[1].OrderInSequence = 2;
            coreModel.Poses[2].OrderInSequence = 3;
            coreModel.Poses[0].IsMiniSequence = false;
            coreModel.Poses[1].IsMiniSequence = true;
            coreModel.Poses[2].IsMiniSequence = false;
            var sequencePoses1 = _fixture.Create<SequencePose>();
            sequencePoses1.OrderInSequence = 1;
            sequencePoses1.IsMiniSequence = false;
            var sequencePoses2 = _fixture.Create<SequencePose>();
            sequencePoses2.OrderInSequence = 2;
            sequencePoses2.IsMiniSequence = true;
            var sequencePoses3 = _fixture.Create<SequencePose>();
            sequencePoses3.OrderInSequence = 3;
            sequencePoses3.IsMiniSequence = false;

            _mapperMock.Setup(r => r.Map<Sequence>(model)).Returns(coreModel);
            _sequencesRepositoryMock.SetupSequence(r => r.SavePoseData(It.IsAny<long>(), It.IsAny<PoseOrder>()))
                .ReturnsAsync(sequencePoses1)
                .ReturnsAsync(sequencePoses3)
                .ReturnsAsync(sequencePoses2);

            await _sut.SaveSequence(model);

            _mapperMock.Verify(r => r.Map<Sequence>(model), Times.Once);
        }

        [Test]
        public async Task SaveSequence_calls_SaveSequenceData()
        {
            var model = _fixture.Create<SequencePostModel>();
            var coreModel = _fixture.Create<Sequence>();
            coreModel.Poses[0].OrderInSequence = 1;
            coreModel.Poses[1].OrderInSequence = 2;
            coreModel.Poses[2].OrderInSequence = 3;
            coreModel.Poses[0].IsMiniSequence = false;
            coreModel.Poses[1].IsMiniSequence = true;
            coreModel.Poses[2].IsMiniSequence = false;
            var sequencePoses1 = _fixture.Create<SequencePose>();
            sequencePoses1.OrderInSequence = 1;
            sequencePoses1.IsMiniSequence = false;
            var sequencePoses2 = _fixture.Create<SequencePose>();
            sequencePoses2.OrderInSequence = 2;
            sequencePoses2.IsMiniSequence = true;
            var sequencePoses3 = _fixture.Create<SequencePose>();
            sequencePoses3.OrderInSequence = 3;
            sequencePoses3.IsMiniSequence = false;

            _mapperMock.Setup(r => r.Map<Sequence>(model)).Returns(coreModel);
            _sequencesRepositoryMock.SetupSequence(r => r.SavePoseData(It.IsAny<long>(), It.IsAny<PoseOrder>()))
                .ReturnsAsync(sequencePoses1)
                .ReturnsAsync(sequencePoses3)
                .ReturnsAsync(sequencePoses2);

            await _sut.SaveSequence(model);

            _sequencesRepositoryMock.Verify(r => r.SaveSequenceData(coreModel), Times.Once);
        }

        [Test]
        public async Task SaveSequence_calls_SavePoseData_for_each_pose()
        {
            var model = _fixture.Create<SequencePostModel>();
            var coreModel = _fixture.Create<Sequence>();
            coreModel.Poses[0].OrderInSequence = 1;
            coreModel.Poses[1].OrderInSequence = 2;
            coreModel.Poses[2].OrderInSequence = 3;
            coreModel.Poses[0].IsMiniSequence = false;
            coreModel.Poses[1].IsMiniSequence = true;
            coreModel.Poses[2].IsMiniSequence = false;
            var sequencePoses1 = _fixture.Create<SequencePose>();
            sequencePoses1.OrderInSequence = 1;
            sequencePoses1.IsMiniSequence = false;
            var sequencePoses2 = _fixture.Create<SequencePose>();
            sequencePoses2.OrderInSequence = 2;
            sequencePoses2.IsMiniSequence = true;
            var sequencePoses3 = _fixture.Create<SequencePose>();
            sequencePoses3.OrderInSequence = 3;
            sequencePoses3.IsMiniSequence = false;

            _mapperMock.Setup(r => r.Map<Sequence>(model)).Returns(coreModel);
            _sequencesRepositoryMock.SetupSequence(r => r.SavePoseData(It.IsAny<long>(), It.IsAny<PoseOrder>()))
                .ReturnsAsync(sequencePoses1)
                .ReturnsAsync(sequencePoses3)
                .ReturnsAsync(sequencePoses2);

            await _sut.SaveSequence(model);

            _sequencesRepositoryMock.Verify(r => r.SavePoseData(It.IsAny<long>(), It.IsAny<PoseOrder>()), Times.Exactly(3));
        }

        [Test]
        public async Task SaveSequence_calls_SaveMiniSequencePose_for_each_mini_sequence_pose()
        {
            var model = _fixture.Create<SequencePostModel>();
            var coreModel = _fixture.Create<Sequence>();
            coreModel.Poses[0].OrderInSequence = 1;
            coreModel.Poses[1].OrderInSequence = 2;
            coreModel.Poses[2].OrderInSequence = 3;
            coreModel.Poses[0].IsMiniSequence = true;
            coreModel.Poses[1].IsMiniSequence = true;
            coreModel.Poses[2].IsMiniSequence = false;
            var sequencePoses1 = _fixture.Create<SequencePose>();
            sequencePoses1.OrderInSequence = 1;
            sequencePoses1.IsMiniSequence = true;
            var sequencePoses2 = _fixture.Create<SequencePose>();
            sequencePoses2.OrderInSequence = 2;
            sequencePoses2.IsMiniSequence = true;
            var sequencePoses3 = _fixture.Create<SequencePose>();
            sequencePoses3.OrderInSequence = 3;
            sequencePoses3.IsMiniSequence = false;

            _mapperMock.Setup(r => r.Map<Sequence>(model)).Returns(coreModel);
            _sequencesRepositoryMock.SetupSequence(r => r.SavePoseData(It.IsAny<long>(), It.IsAny<PoseOrder>()))
                .ReturnsAsync(sequencePoses1)
                .ReturnsAsync(sequencePoses3)
                .ReturnsAsync(sequencePoses2);

            await _sut.SaveSequence(model);

            _sequencesRepositoryMock.Verify(r => r.SaveMiniSequencePose(It.IsAny<long>(), It.IsAny<MiniPose>()), Times.Exactly(6));
        }

        [Test]
        public async Task SaveSequence_returns_model_appropriately()
        {
            var model = _fixture.Create<SequencePostModel>();
            var coreModel = _fixture.Create<Sequence>();
            coreModel.Poses[0].OrderInSequence = 1;
            coreModel.Poses[1].OrderInSequence = 2;
            coreModel.Poses[2].OrderInSequence = 3;
            coreModel.Poses[0].IsMiniSequence = false;
            coreModel.Poses[1].IsMiniSequence = true;
            coreModel.Poses[2].IsMiniSequence = false;
            var sequencePoses1 = _fixture.Create<SequencePose>();
            sequencePoses1.OrderInSequence = 1;
            sequencePoses1.IsMiniSequence = false;
            var sequencePoses2 = _fixture.Create<SequencePose>();
            sequencePoses2.OrderInSequence = 2;
            sequencePoses2.IsMiniSequence = true;
            var sequencePoses3 = _fixture.Create<SequencePose>();
            sequencePoses3.OrderInSequence = 3;
            sequencePoses3.IsMiniSequence = false;

            _mapperMock.Setup(r => r.Map<Sequence>(model)).Returns(coreModel);
            _sequencesRepositoryMock.Setup(r => r.SaveSequenceData(It.IsAny<Sequence>())).ReturnsAsync(10);
            _sequencesRepositoryMock.SetupSequence(r => r.SavePoseData(It.IsAny<long>(), It.IsAny<PoseOrder>()))
                .ReturnsAsync(sequencePoses1)
                .ReturnsAsync(sequencePoses3)
                .ReturnsAsync(sequencePoses2);

            var result = await _sut.SaveSequence(model);

            Assert.IsInstanceOf<ApiResponse<long>>(result);
            Assert.AreEqual(10L, result.data);
            Assert.AreEqual(true, result.isOk);
            Assert.AreEqual(200, result.httpStatusCode);
        }
    }
}
