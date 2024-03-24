namespace WebNoiThat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TinDang
    {
        [Key]
        public int PostID { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string SContents { get; set; }

        public string Contents { get; set; }

        [StringLength(255)]
        public string Thumb { get; set; }

        public bool Published { get; set; }

        [StringLength(255)]
        public string Alias { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        public int? AccountID { get; set; }

        public string Tags { get; set; }

        public int? CatID { get; set; }

        public bool isHot { get; set; }

        public bool isNewfeed { get; set; }

        [StringLength(255)]
        public string MetaKey { get; set; }

        [StringLength(255)]
        public string MetaDesc { get; set; }

        public int? Views { get; set; }
    }
}
