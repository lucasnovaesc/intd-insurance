namespace Service.DataTransferObjects.CustomerDTO.Response
{
    public class ResponseReadCustomerDTO
    {
        public ResponseReadCustomerDTO()
        {

        }

        public ResponseReadCustomerDTO(Guid customerId, string name, string email, string phoneNumber, string address, DateTime dateCreation)
        {
            this.CustomerId = customerId;
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.DateCreation = dateCreation;
        }

        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateCreation { get; set; }

    }
}
