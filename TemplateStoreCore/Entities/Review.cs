using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Entities
{
    public class Review : Entity
    {
        public string UserName { get; set; }
        public string Text { get; set; }
    }
}
