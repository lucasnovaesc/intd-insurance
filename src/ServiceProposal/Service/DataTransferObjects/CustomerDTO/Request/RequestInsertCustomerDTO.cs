namespace Service.DataTransferObjects.CustomerDTO.Request
{
    public class RequestInsertCustomerDTO
    {
        public RequestInsertCustomerDTO()
        {

        }

        public RequestInsertCustomerDTO(string name, string email, string phoneNumber, string address)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
