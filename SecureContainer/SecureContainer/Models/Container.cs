using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureContainer.Models
{
    public class Container
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContainerId { get; set; }

        [Required(ErrorMessage = "Container Number is required")]
        [Display(Name = "Container Number")]

        public string ContainerNumber { get; set; }

        [Required(ErrorMessage = "Shipment Date is required")]
        [Display(Name = "Shipment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ShipmentDate { get; set; }

        [Required(ErrorMessage = "Origin Port is required")]
        [Display(Name = "Origin Port")]
        
        public string OriginPort { get; set; }

        [Required(ErrorMessage = "Destination Port is required")]
        [Display(Name = "Destination Port")]
        
        public string DestinationPort { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "File Path")]
        public string FilePath { get; set; }
    }
}