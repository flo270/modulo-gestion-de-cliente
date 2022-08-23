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

    public partial class Impuesto
    {
        public int IdImp { get; set; }
        [Required]
        public string NombreImpuesto { get; set; }
        [Required]
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        [Required]
        public Nullable<double> Monto { get; set; }
        [Required]
        public System.DateTime FechaPago { get; set; }
        [Required]
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        [Required]
        public string TipoImpuesto { get; set; }
        [Required]
        public Nullable<int> ClientePerteneciente { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}
