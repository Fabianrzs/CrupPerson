using System.ComponentModel.DataAnnotations;

namespace WebApp.Model;

public class Persona
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string Nombre1 { get; set; } = string.Empty ;
    public string Nombre2 { get; set; } = string.Empty;
    public string Apellido1 { get; set; } = string.Empty;
    public string Apellido2 { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}
