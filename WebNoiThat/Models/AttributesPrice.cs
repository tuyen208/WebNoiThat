namespace WebNoiThat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AttributesPrice
    {
        public int AttributesPriceID { get; set; }

        public int? AttributeID { get; set; }

        public int? ProductID { get; set; }

        public int? Price { get; set; }

        public bool Active { get; set; }

        public virtual Attribute Attribute { get; set; }

        public virtual Product Product { get; set; }
    }
}
