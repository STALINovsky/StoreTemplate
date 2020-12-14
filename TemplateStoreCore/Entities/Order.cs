using System;
using System.Collections.Generic;
using System.Text;
using StoreTemplateCore.Entities.Base;
using StoreTemplateCore.Identity;

namespace StoreTemplateCore.Entities
{
    public class Order : Entity
    {
        public User User { get; set; }

    }
}
