using System.ComponentModel.DataAnnotations;

namespace RevenueRecognitionSystem.Modules.Licence.DTOs.Requests;

public class CreateLicenceDto
{
    [Required]
    public int ClientId { get; set; }

    [Required]
    public int SoftwareId { get; set; }

    [Range(1, 3, ErrorMessage = "SupportYears must be between 1 and 3.")]
    public int SupportYears { get; set; } = 1;

    [Required]
    [DataType(DataType.Date)] 
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
}
