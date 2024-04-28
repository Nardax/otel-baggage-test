namespace OtelBaggageTest.UnitTests
{
    [TestClass]
    public class BaggageTests
    {
        [TestMethod]
        public async Task GetBaggage_NoHeaders_CallIsSuccess()
        {
            var baggageTestClient = new BaggageTestClient();

            string id = Guid.NewGuid().ToString();
            bool? isTest = null;
            int? waitTime = null;

            var baggageDetails = await baggageTestClient.GetBaggageDetails(id, isTest, waitTime);

            Assert.AreEqual(id, baggageDetails.Id);
            Assert.IsNull(baggageDetails.IsTest);
            Assert.IsNull(baggageDetails.WaitTime);
        }

        [TestMethod]
        public async Task GetBaggage_IsTestHeader_CallIsSuccess()
        {
            var baggageTestClient = new BaggageTestClient();

            string id = Guid.NewGuid().ToString();
            bool? isTest = true;
            int? waitTime = null;

            var baggageDetails = await baggageTestClient.GetBaggageDetails(id, isTest, waitTime);

            Assert.AreEqual(id, baggageDetails.Id);
            Assert.AreEqual(isTest.ToString(), baggageDetails.IsTest);
            Assert.IsNull(baggageDetails.WaitTime);
        }

        [TestMethod]
        public async Task GetBaggage_IsTestAndWaitTimeHeader_CallIsSuccess()
        {
            var baggageTestClient = new BaggageTestClient();

            string id = Guid.NewGuid().ToString();
            bool? isTest = true;
            int? waitTime = 1000;

            var baggageDetails = await baggageTestClient.GetBaggageDetails(id, isTest, waitTime);

            Assert.AreEqual(id, baggageDetails.Id);
            Assert.AreEqual(isTest.ToString(), baggageDetails.IsTest);
            Assert.AreEqual(waitTime.ToString(), baggageDetails.WaitTime);
        }

        [TestMethod]
        public async Task GetBaggage_IsTestAndWaitTimeHeaderOutOfOrder_CallIsSuccess()
        {
            var baggageTestClient = new BaggageTestClient();

            string id1 = Guid.NewGuid().ToString();
            bool? isTest1 = true;
            int? waitTime1 = 5000;

            string id2 = Guid.NewGuid().ToString();
            bool? isTest2 = false;
            int? waitTime2 = 1000;

            var baggageDetailsTask1 = baggageTestClient.GetBaggageDetails(id1, isTest1, waitTime1);

            var baggageDetailsTask2 = baggageTestClient.GetBaggageDetails(id2, isTest2, waitTime2);

            var baggageDetails = await Task.WhenAll(baggageDetailsTask1, baggageDetailsTask2);

            Assert.AreEqual(id1, baggageDetails[0].Id);
            Assert.AreEqual(isTest1.ToString(), baggageDetails[0].IsTest);
            Assert.AreEqual(waitTime1.ToString(), baggageDetails[0].WaitTime);

            Assert.AreEqual(id2, baggageDetails[1].Id);
            Assert.AreEqual(isTest2.ToString(), baggageDetails[1].IsTest);
            Assert.AreEqual(waitTime2.ToString(), baggageDetails[1].WaitTime);

            Assert.IsTrue(baggageDetails[0].RecievedOrder < baggageDetails[1].RecievedOrder);
            Assert.IsTrue(baggageDetails[0].ReturnedOrder > baggageDetails[1].ReturnedOrder);
        }
    }
}