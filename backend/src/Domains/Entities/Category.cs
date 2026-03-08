using System;
using System.Collections.Generic;
using Domains.Base;

namespace Domains.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? IconPath { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<RestaurantCategory> RestaurantCategories { get; set; } = new List<RestaurantCategory>();
    }
}
