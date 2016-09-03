namespace lubrizol.nglzconfig.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblApplicationVariable")]
    public partial class tblApplicationVariable
    {
        [Key]
        [Column(Order = 0)]
        public Guid ApplicationID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(750)]
        public string Value { get; set; }

        public bool? Secure { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual tblApplication tblApplication { get; set; }
    }
}
