using AutoMapper;
using NUnit.Framework;

namespace YogaApi.Tests
{
    [TestFixture]
    public class AutoMapperTests
    {
        private readonly IMapper mapper;

        public AutoMapperTests()
        {
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles("YogaApi");
            }));
        }

        [Test]
        public void AutoMapper_Configuration_IsValid()
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
