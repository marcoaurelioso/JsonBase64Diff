using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonBase64Diff.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [Fact]
        public async Task PassingTest()
        {
            var webHostBuilder = CreateWebHostBuilder();
            var server = new TestServer(webHostBuilder);

            using (var client = server.CreateClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/api/values/");
                var responseMessage = await client.SendAsync(requestMessage);

                var content = await responseMessage.Content.ReadAsStringAsync();

                Assert.Equal(content, "Hello World!");
            }
        }
    }
}
