namespace Service.DataTransferObjects.CustomerDTO.Request
{
    public class RequestUpdateCustomerDTO
    {
        public RequestUpdateCustomerDTO()
        {

        }

        public RequestUpdateCustomerDTO(Guid customerId, string name, string email, string phoneNumber, string address, DateTime dateCreation)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            DateCreation = dateCreation;
        }

        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
