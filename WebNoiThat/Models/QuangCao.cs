namespace WebNoiThat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuangCao
    {
        public int QuangCaoID { get; set; }

        [StringLength(150)]
        public string SubTitle { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(250)]
        public string ImageBG { get; set; }

        [StringLength(250)]
        public string ImageProduct { get; set; }

        [StringLength(250)]
        public string UrlLink { get; set; }

        public bool Active { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
