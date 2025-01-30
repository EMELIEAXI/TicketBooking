
namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessor
    {
        public TicketBookingRequestProcessor()
        {
        }

        public TicketBookingResonse Book(TicketBookingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return new TicketBookingResonse
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };
        }
    }
}