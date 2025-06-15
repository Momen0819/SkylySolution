using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Domain.Entities
{
    public class Subscriber : BaseEntity
    {
        public string Email { get; set; }
        public DateTime SubscribedAt { get; set; }
    }
}
