using Clusterization.Controllers.Youtube;
using Domain.Interfaces.Youtube;
using FakeItEasy;

namespace Clusterization.Tests
{
    public class ChannelsControllerTests
    {
        public ChannelsControllerTests()
        {
            var channelService = A.Fake<IYoutubeChannelService>();

            var controller = new ChannelsController()
        }
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void LoadMultipleChannels_Returns_Collection_Of_Loaded_Channels()
        {


            var controller = new ChannelsController();

            var actionResult = controller.GetLoadedChannels()
            Assert.Pass();
        }
    }
}