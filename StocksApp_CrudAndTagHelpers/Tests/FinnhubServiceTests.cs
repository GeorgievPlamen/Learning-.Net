using Services;
using ServicesContracts;

namespace Tests
{
    public class FinnhubServiceTests
    {
        private readonly IStocksService _stocksService;

        public FinnhubServiceTests()
        {
            _stocksService = new StocksService();
        }

        [Fact]
        public void CreateBuyOrder_RequestIsNull()
        {
            //Assert
            BuyOrderRequest? request = null;
            //Arrange
            Assert.Throws<ArgumentNullException>(() =>
            //Act
            _stocksService.CreateBuyOrder(request)
            );
        }
    }
}