namespace WebNoiThat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LocationID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Parent { get; set; }

        public int? Levels { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        [StringLength(100)]
        public string NameWithType { get; set; }

        [StringLength(10)]
        public string Type { get; set; }
    }
}
