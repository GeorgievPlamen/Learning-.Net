using Services;
using ServicesContracts.DTO;
using System.Runtime.ConstrainedExecution;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tests
{
    public class StockServiceTest
    {
        private readonly IStocksService _stocksService = new StockService();

        public StockServiceTest()
        {
            _stocksService = new StockService();
        }

        
        #region CreateBuyOrder
        //1. When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public void CreateBuyOrder_RequestIsNull()
        {
            //Arrange
            BuyOrderRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        //2. When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_QuantityIsZero()
        {
            //Arrange

            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-10-11"),
                Quantity = 0,
                Price = 100
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }
        //3. When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_QuantityIsOverCap()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-10-11"),
                Quantity = 100001,
                Price = 100
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        //4. When you supply buyOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_PriceIsZero()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-10-11"),
                Quantity = 10,
                Price = 0
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        //5. When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_PriceIsoverCap()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-10-11"),
                Quantity = 10,
                Price = 10001
            };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        //6. When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public  void CreateBuyOrder_SymbolIsNull()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockSymbol = null,
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-10-11"),
                Quantity = 10,
                Price = 100
            };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        //7. When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_DateIsOld()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31"),
                Quantity = 10,
                Price = 100
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        //8. If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID(guid).
        [Fact]
        public void CreateBuyOrder_Valid()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-12-31"),
                Quantity = 10,
                Price = 100
            };
            //Act
            BuyOrderResponse response =  _stocksService.CreateBuyOrder(request);
            //Assert
            Assert.NotEqual(Guid.Empty, response.BuyOrderID);
        }
        #endregion
        #region SellOrderTests
        //1. When you supply SellOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public void CreateSellOrder_RequestIsNull()
        {
            //Assert
            SellOrderRequest? request = null;
            
            //Arrange
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _stocksService.CreateSellOrder(request);
            });
        }

        //2. When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_QuantityBelowZero()
        {
            //Arrange
            SellOrderRequest request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-04-10"),
                Quantity = 0,
                Price = 100
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                SellOrderResponse response = _stocksService.CreateSellOrder(request);
            });
        }

        //3. When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Fact]
        public void GetSellOrder_QuantityOverCap() 
        {
            //Arrange
            SellOrderRequest request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-04-10"),
                Quantity = 100001,
                Price = 100
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                SellOrderResponse response = _stocksService.CreateSellOrder(request);
            });
        }

        //4. When you supply sellOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public void GetSellOrder_PriceBelowZero()
        {
            //Arrange
            SellOrderRequest request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-04-10"),
                Quantity = 10,
                Price = 0
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                SellOrderResponse response = _stocksService.CreateSellOrder(request);
            });
        }

        //5. When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Fact]
        public void GetSellOrder_PriceOverCap()
        {
            //Arrange
            SellOrderRequest request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-04-10"),
                Quantity = 10,
                Price = 100001
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                SellOrderResponse response = _stocksService.CreateSellOrder(request);
            });
        }

        //6. When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public void GetSellOrder_SymbolIsNull()
        {
            //Arrange
            SellOrderRequest request = new SellOrderRequest()
            {
                StockSymbol = null,
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-04-10"),
                Quantity = 10,
                Price = 100
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                SellOrderResponse response = _stocksService.CreateSellOrder(request);
            });
        }

        //7. When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public void GetSellOrder_DateIsOld()
        {
            //Arrange
            SellOrderRequest request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("1998-04-10"),
                Quantity = 10,
                Price = 100
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                SellOrderResponse response = _stocksService.CreateSellOrder(request);
            });
        }

        //8. If you supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID(guid).
        [Fact]
        public void GetSellOrder_Valid()
        {
            //Arrange
            SellOrderRequest request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-04-10"),
                Quantity = 10,
                Price = 100
            };

            //Act
            SellOrderResponse response = _stocksService.CreateSellOrder(request);

            //Assert
            Assert.True(response.SellOrderID != Guid.Empty);
        }
        #endregion
        #region GetBuyOrders
        /*1. When you invoke this method, by default, the returned list should be empty.
        2. */
        [Fact]
        public void GetBuyOrders_Empty()
        {
            //Arrange

            //Act


            //Assert
            Assert.Empty(_stocksService.GetBuyOrders());
        }
        //When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders.
        [Fact]
        public void GetBuyOrders_Valid()
        {
            //Arrange
            List<BuyOrderResponse> dummyList = new List<BuyOrderResponse>()
            {
                new BuyOrderResponse()
                {
                    StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-12-31"),
                Quantity = 10,
                Price = 100
                },
                new BuyOrderResponse()
                {
                    StockSymbol = "App",
                StockName = "Apple",
                DateAndTimeOfOrder = DateTime.Parse("2008-12-31"),
                Quantity = 24,
                Price = 500
                },
                new BuyOrderResponse()
                {
                    StockSymbol = "Net",
                StockName = "Netflix",
                DateAndTimeOfOrder = DateTime.Parse("2001-12-31"),
                Quantity = 130,
                Price = 5000
                }
            };
            //Act
            BuyOrderRequest request1 = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "MicroSoft",
                DateAndTimeOfOrder = DateTime.Parse("2006-12-31"),
                Quantity = 10,
                Price = 100
            };
            BuyOrderResponse response1 = _stocksService.CreateBuyOrder(request1);

            BuyOrderRequest request2 = new BuyOrderRequest()
            {
                StockSymbol = "App",
                StockName = "Apple",
                DateAndTimeOfOrder = DateTime.Parse("2008-12-31"),
                Quantity = 24,
                Price = 500
            };
            BuyOrderResponse response2 = _stocksService.CreateBuyOrder(request2);

            BuyOrderRequest request3 = new BuyOrderRequest()
            {
                StockSymbol = "Net",
                StockName = "Netflix",
                DateAndTimeOfOrder = DateTime.Parse("2001-12-31"),
                Quantity = 130,
                Price = 5000
            };
            BuyOrderResponse response3 = _stocksService.CreateBuyOrder(request3);

            List<BuyOrderResponse> fromOrders = _stocksService.GetBuyOrders();
            //Assert
            foreach(BuyOrderResponse response in dummyList)
            {
                Assert.Contains(response, fromOrders);
            }
        }
        #endregion
        #region GetSellOrders
        [Fact]
        public void GetSellOrders_Empty()
        {
            //Arrange
            
            //Act


            //Assert
            Assert.Empty(_stocksService.GetSellOrders());
        }
        #endregion
    }
}