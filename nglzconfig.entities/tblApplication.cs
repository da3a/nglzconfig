namespace lubrizol.nglzconfig.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblApplication")]
    public partial class tblApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblApplication()
        {
            tblApplicationConnection = new HashSet<tblApplicationConnection>();
            tblApplicationVariable = new HashSet<tblApplicationVariable>();
        }

        public Guid ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(1024)]
        public string URL { get; set; }

        [StringLength(30)]
        public string AssemblyVersion { get; set; }

        [StringLength(30)]
        public string AssemblyFileVersion { get; set; }

        [StringLength(30)]
        public string SetupVersion { get; set; }

        public Guid? InstalledProductCode { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(20)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblApplicationConnection> tblApplicationConnection { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblApplicationVariable> tblApplicationVariable { get; set; }
    }
}
