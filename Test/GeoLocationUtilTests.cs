using NUnit.Framework;

namespace GeoLocationUtilityTests
{
    [TestFixture]
    public class GeoLocationUtilityTests
    {
        [Test]
        [Description("Test valid city and state input 'Madison, WI'. Should return latitude, longitude, place name, and state.")]
        public async Task TestCityStateLocation()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                string[] args = { "Madison, WI" };
                await GeoLocationUtil.Main(args);

                string result = sw.ToString();
                Assert.That(result, Does.Contain("Madison"));
                Assert.That(result, Does.Contain("Latitude: 43.074761"));
                Assert.That(result, Does.Contain("Longitude: -89.3837613"));
            }
        }

        [Test]
        [Description("Test valid ZIP code input '12345'. Should return latitude, longitude, place name, and state.")]
        public async Task TestZipCodeLocation()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                string[] args = { "12345" };
                await GeoLocationUtil.Main(args);

                string result = sw.ToString();
                Assert.That(result, Does.Contain("Latitude: 42.8142"));
                Assert.That(result, Does.Contain("Longitude: -73.9396"));
            }
        }

        [Test]
        [Description("Test invalid location input. Should return 'No data found' message.")]
        public async Task TestInvalidLocation()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                string[] args = { "InvalidLocation" };
                await GeoLocationUtil.Main(args);

                string result = sw.ToString();
                Assert.That(result, Does.Contain("No data found"));
            }
        }

        [TestCase("Madison, WI", "Madison", 43.074761, -89.3837613)]
        [TestCase("10001", "New York", 40.7484, -73.9967)]
        [TestCase("Los Angeles, CA", "Los Angeles", 34.0536909, -118.242766)]
        [TestCase("Chicago, IL", "Chicago", 41.8755616, -87.6244212)]
        [Description("Test valid inputs with multiple locations. Should return data for each location.")]
        public async Task TestMultipleLocations(string location, string expectedLocation, double expectedLatitude, double expectedLongitude)
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                string[] args = { location };
                await GeoLocationUtil.Main(args);

                string result = sw.ToString();

                Assert.That(result, Does.Contain(expectedLocation));
                Assert.That(result, Does.Contain($"Latitude: {expectedLatitude}"));
                Assert.That(result, Does.Contain($"Longitude: {expectedLongitude}"));
            }
        }

        [Test]
        [Description("Test no input. Should return a prompt asking for input.")]
        public async Task TestNoInput()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                string[] args = { };
                await GeoLocationUtil.Main(args);

                string result = sw.ToString();

                Assert.That(result, Does.Contain("Please provide location inputs."));
            }
        }
    }
}
