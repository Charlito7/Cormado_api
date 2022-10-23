using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WebContract;

public class UpdateCormadoRequest
{
    [Required]
    public string? LegalName { get; set; }
    [Required]
    public string? CommercialName { get; set; }

    [Required]
    public string? Phone { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    public string? City { get; set; }
    public string? ZipCode { get; set; }

    public DateTime? CreatedDate { get; set; }
}
