using System.Data;
using System.Data.Common;

namespace _1983.Models
{
    public class Registration
    {
        private IDbConnection Connect;

        public Registration(IDbConnection connect)
        {
            Connect = connect;

            using (IDbConnection database = Connect)
            {

            }

        }
    }
}
