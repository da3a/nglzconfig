namespace lubrizol.nglzconfig.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblApplicationConnection")]
    public partial class tblApplicationConnection
    {
        [Key]
        [Column(Order = 0)]
        public Guid ApplicationID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(512)]
        public string ConnectionString { get; set; }

        [Required]
        [StringLength(512)]
        public string VirtualConnectionString { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public short? CommandTimeout { get; set; }

        [StringLength(255)]
        public string ProviderName { get; set; }

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
