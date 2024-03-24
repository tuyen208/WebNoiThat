namespace WebNoiThat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page
    {
        public int PageID { get; set; }

        [StringLength(250)]
        public string PageName { get; set; }

        public string Contents { get; set; }

        [StringLength(250)]
        public string Thumb { get; set; }

        public bool Published { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string MetaDesc { get; set; }

        [StringLength(250)]
        public string MetaKey { get; set; }

        [StringLength(250)]
        public string Alias { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Ordering { get; set; }
    }
}
