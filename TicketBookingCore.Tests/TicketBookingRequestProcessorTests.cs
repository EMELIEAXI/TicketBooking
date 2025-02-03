using TicketBookingCore.Tests;
using Moq;

namespace TicketBookingCore
{
    public class TicketBookingRequestProcessorTests
    { 
        private readonly TicketBookingRequestProcessor _processor;

        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;

        private readonly TicketBookingRequest _ticketBookingRequest;
        
        public TicketBookingRequestProcessorTests()
        {
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
            _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
            _ticketBookingRequest = new TicketBookingRequest
            {
                FirstName = "Emelie",
                LastName = "Axi",
                Email = "Emelieaxi@hotmail.com"
            };
        }


        [Fact]
       
        public void ShouldReturnTicketBookingResultWithRequestsValues()
        { 

            //Act
            TicketBookingResponse response = _processor.Book(_ticketBookingRequest);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(_ticketBookingRequest.FirstName, response.FirstName);
            Assert.Equal(_ticketBookingRequest.LastName, response.LastName);
            Assert.Equal(_ticketBookingRequest.Email, response.Email);

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

            // Act
            TicketBookingResponse response = _processor.Book(_ticketBookingRequest);


            // Assert
            Assert.NotNull(savedTicketBooking);
            Assert.Equal(_ticketBookingRequest.FirstName, savedTicketBooking.FirstName);
            Assert.Equal(_ticketBookingRequest.LastName, savedTicketBooking.LastName);
            Assert.Equal(_ticketBookingRequest.Email, savedTicketBooking.Email);
        }
    }
}