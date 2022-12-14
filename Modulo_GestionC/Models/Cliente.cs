//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Modulo_GestionC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.Impuesto = new HashSet<Impuesto>();
        }
    
        public int IdCliente { get; set; }
        [Required]
        public string NombreContribuyente { get; set; }
        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string CUIT { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string Telefono { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string TipoCliente { get; set; }
        [Required]
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        [Required]
        public Nullable<int> RegimenImpositivoPerteneciente { get; set; }
    
        public virtual RegimenImpositivo RegimenImpositivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Impuesto> Impuesto { get; set; }
    }
}
