using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Entities
{
    public class OrderDetails : Entity
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Comments { get; set; }
    }
}
