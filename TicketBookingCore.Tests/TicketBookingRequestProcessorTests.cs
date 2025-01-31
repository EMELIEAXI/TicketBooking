using TicketBookingCore.Tests;
using Moq;

namespace TicketBookingCore
{
    public class TicketBookingRequestProcessorTests
    { 
        private readonly TicketBookingRequestProcessor _processor;

        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;
        
        public TicketBookingRequestProcessorTests()
        {
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
            _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
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

        public void ShouldSaveToDatabase()
        {
            // Arrange
            TicketBooking savedTicketBooking = null;

            // Setup the Save method to capture the saved ticket booking
            _ticketBookingRepositoryMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
            .Callback<TicketBooking>((ticketBooking) =>
            {
                savedTicketBooking = ticketBooking;
            });

            var request = new TicketBookingRequest
            {
                FirstName = "Emelie",
                LastName = "Axi",
                Email = "Emelieaxi@hotmail.com"
            };

            // Act
            TicketBookingResonse response = _processor.Book(request);

            // Assert
            Assert.NotNull(savedTicketBooking);
            Assert.Equal(request.FirstName, savedTicketBooking.FirstName);
            Assert.Equal(request.LastName, savedTicketBooking.LastName);
            Assert.Equal(request.Email, savedTicketBooking.Email);
        }
    }
}