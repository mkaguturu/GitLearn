using Nop.Core;
using SDK.IT.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

//information added to see the diff in action

namespace SDK.IT.Data.Domain.Business
{
    public class DistributorMap : EntityTypeConfiguration<Distributor>
    {
        public DistributorMap()
        {
            const string DESCRIMINATOR = "Changed the Descriminator";

            //ToTable("IT_Distributor");
            HasKey(e => e.Id);

            Map<Entrepreneur>(m => m.Requires(DESCRIMINATOR).HasValue((int)DistributorType.Entrepreneur));
            Map<ITCustomer>(m => m.Requires(DESCRIMINATOR).HasValue((int)DistributorType.Customer));

            HasOptional(d => d.PersonalDetails).WithRequired();
            HasOptional(d => d.WebAdministration).WithRequired();
            HasOptional(d => d.ActivityLog).WithRequired();
            HasOptional(d => d.BankDetails).WithRequired();
            HasOptional(d => d.D_TaxDetails).WithRequired();

            HasRequired(d => d.SourceType).WithMany().HasForeignKey(d => d.SourceTypeId);
            HasRequired(d => d.BusinessType).WithMany().HasForeignKey(d => d.BusinessTypeId);
            HasRequired(d => d.PreferredLanguageType).WithMany().HasForeignKey(d => d.PreferredLanguageTypeId);

            HasOptional(d => d.Sponsor).WithMany(p => p.SponsoredList).HasForeignKey(d => d.SponsorId).WillCascadeOnDelete(false);
            HasOptional(d => d.Upline).WithMany(p => p.DownlineList).HasForeignKey(d => d.UplineId).WillCascadeOnDelete(false);

            HasMany(d => d.LogDetails).WithRequired(log => log.DistributorEntity).HasForeignKey(l => l.DistributorId).WillCascadeOnDelete(false);
        }
    }
}