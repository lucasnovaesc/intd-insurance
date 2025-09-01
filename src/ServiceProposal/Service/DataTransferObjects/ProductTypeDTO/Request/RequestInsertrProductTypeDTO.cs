

namespace Service.DataTransferObjects.ProductTypeDTO.Request
{
    public class RequestInsertrProductTypeDTO
    {
        public RequestInsertrProductTypeDTO()
        {
            
        }

        public RequestInsertrProductTypeDTO(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
