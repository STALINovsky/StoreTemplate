using StoreTemplateCore.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreTemplateCore.Entities
{
    public class News : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public string ImagePath { get; set; }
    }
}
