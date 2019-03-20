using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Data.Models.Data
{
    public class TokenConfiguration : EntityTypeConfiguration<Token>
    {
        public TokenConfiguration()
        {
            HasRequired(n => n.Custodian)
           .WithMany()
           .HasForeignKey(n => n.CustodianId)
           .WillCascadeOnDelete(false);
        }
    }
}
