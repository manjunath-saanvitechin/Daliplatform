
namespace AuthenticationService.Models
{
   public class Tenant
{
    public Guid TenantId { get; set; } = Guid.NewGuid();
    public string TenantName { get; set; }
    public string Domain { get; set; }
    public DateTime CreatedAt { get; set; }
}
}
