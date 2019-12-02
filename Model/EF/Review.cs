

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]

    public partial class Review
    {
        public long ID { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public float Rating { get; set; }

        public DateTime? DatePost { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public long OrderId { get; set; }

    }
}
