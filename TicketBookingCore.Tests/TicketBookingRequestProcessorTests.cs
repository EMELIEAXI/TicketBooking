using TicketBookingCore.Tests;

namespace TicketBookingCore
{
    public class TicketBookingRequestProcessorTests
    { 
        private readonly TicketBookingRequestProcessor _processor;
        
        public TicketBookingRequestProcessorTests()
        {
            _processor = new TicketBookingRequestProcessor();
        }

        [Fact]
       
        public void ShouldReturnTicketBookingResultWithRequestsValues()
        { 
            //Arrange

            var request = new TicketBookingRequest
            {
                FirstName = "Emelie",
                LastName = "Axi",
                Email = "Emelieaxi@hotmail.com"
            };

            //Act
            TicketBookingResonse response = _processor.Book(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);

        }
        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));

            //Assert
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]

        public void ShouldSaveToDataBase() 
        { 

        }
    }
}