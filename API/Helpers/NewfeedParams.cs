using System.Security;

namespace API.Helpers
{
    public class NewfeedParams :PaginationParams
    {
        public string Username { get; set; }
        public string News { get; set; }
    }
}
