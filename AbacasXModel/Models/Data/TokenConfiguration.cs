using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AbacasXModel.Models.Data
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
