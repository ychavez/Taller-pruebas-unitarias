using Ordering.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities
{
    public class Order : EntityBase, IMultiTenant
    {
        public string UserName { get; set; } = null!;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;

        public int PaymentMethod { get; set; }
        public Guid TenantId { get; set; }
    }
}
