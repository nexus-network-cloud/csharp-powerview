using NexusNetworkCloud.CsharpPowerview;
using NexusNetworkCloud.CsharpPowerview.Models;
using System.Net;

namespace CsharpPowerViewTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            IPowerViewAPIClient client = new PowerViewAPIClient(IPAddress.Parse("192.168.255.37"));

            //var result = await client.ActivatePowerViewSceneAsync(46143);

            var result = await client.SetShadePositionAsync(1, ShadePosition.OpenTop);
        }
    }
}