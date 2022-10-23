using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WebContract;

public class ReadCormadoResponse
{


    public string? LegalName { get; set; }

    public string? CommercialName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }
    public string? ZipCode { get; set; }

    public DateTime? CreatedDate { get; set; }
}
