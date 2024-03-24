namespace WebNoiThat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderID { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public int TransactStatusID { get; set; }

        public bool Deleted { get; set; }

        public bool Paid { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int TotalMoney { get; set; }

        public int? PaymentID { get; set; }

        public string Note { get; set; }

        public string Address { get; set; }

        public int? LocationID { get; set; }

        public int? District { get; set; }

        public int? Ward { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual TransactStatu TransactStatu { get; set; }
    }
}
