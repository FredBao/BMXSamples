namespace TablePerConcreteType
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public abstract class BillingDetail
    {
        public int BillingDetailId { get; set; }

        public string Number { get; set; }

        public string Owner { get; set; }
    }

    public class BankAccount : BillingDetail
    {
        public string BankName { get; set; }

        public string Swift { get; set; }
    }

    public class CreditCard : BillingDetail
    {
        public int CardType { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }
    }

    public class InheritanceMappingContext : DbContext
    {
        public DbSet<BillingDetail> BillingDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().Map(
                m =>
                    {
                        m.MapInheritedProperties();
                        m.ToTable("BankAccounts");
                    });

            modelBuilder.Entity<CreditCard>().Map(
                m =>
                    {
                        m.MapInheritedProperties();
                        m.ToTable("CreditCards");
                    });
        }
    }

    /// <summary>
    /// In Table per Concrete type (aka Table per Concrete class) we use exactly one table for each (nonabstract) class. 
    /// All properties of a class, including inherited properties, can be mapped to columns of this table.
    /// </summary>
    class Program
    {
        private static void GenerateTestData(InheritanceMappingContext context)
        {
            var ba = new BankAccount
                         {
                             // BillingDetailId = 1,
                             Owner = "BankAccount",
                             BankName = "BankAccount",
                             Number = "1",
                             Swift = "BankAccount"
                         };

            context.BillingDetails.Add(ba);

            var cc = new CreditCard
                         {
                             // BillingDetailId = 2,
                             Owner = "BankAccount",
                             CardType = 1,
                             Number = "1",
                             ExpiryMonth = "6",
                             ExpiryYear = "2017"
                         };

            context.BillingDetails.Add(cc);
        }

        static void Main(string[] args)
        {
            using (InheritanceMappingContext context = new InheritanceMappingContext())
            {
                GenerateTestData(context);

                context.SaveChanges();

                // polymorphic queries
                // linqQuery return a list of objects of the type BillingDetail, 
                // which is an abstract class but the actual concrete objects 
                // in the list are of the subtypes of BillingDetail: CreditCard and BankAccount.
                IQueryable<BillingDetail> linqQuery = from b in context.BillingDetails select b;
                List<BillingDetail> billingDetails = linqQuery.ToList();

                Console.WriteLine("polymorphic queries count : " + billingDetails.Count);

                // non-polymorphic queries
                // the following query returns only instances of BankAccount:
                IQueryable<BankAccount> query = from b in context.BillingDetails.OfType<BankAccount>() select b;
                Console.WriteLine("non-polymorphic queries count : " + query.ToList().Count);

                #region GeneratedSQL

                // SELECT
                // CASE WHEN([UnionAll1].[C4] = 1) THEN '0X0X' ELSE '0X1X' END AS[C1],

                // [UnionAll1].[BillingDetailId]
                // AS[C2], 
                // [UnionAll1].[Owner]
                // AS[C3], 
                // [UnionAll1].[Number]
                // AS[C4], 
                // CASE WHEN([UnionAll1].[C4] = 1) THEN[UnionAll1].[BankName]
                // END AS[C5],
                // CASE WHEN([UnionAll1].[C4] = 1) THEN[UnionAll1].[Swift]
                // END AS[C6],
                // CASE WHEN([UnionAll1].[C4] = 1) THEN CAST(NULL AS int) ELSE[UnionAll1].[C1]
                // END AS[C7],
                // CASE WHEN([UnionAll1].[C4] = 1) THEN CAST(NULL AS varchar(1)) ELSE[UnionAll1].[C2]
                // END AS[C8],
                // CASE WHEN([UnionAll1].[C4] = 1) THEN CAST(NULL AS varchar(1)) ELSE[UnionAll1].[C3]
                // END AS[C9]
                // FROM(SELECT
                // [Extent1].[BillingDetailId] AS[BillingDetailId],
                // [Extent1].[Owner] AS[Owner],
                // [Extent1].[Number] AS[Number],
                // [Extent1].[BankName] AS[BankName],
                // [Extent1].[Swift] AS[Swift],
                // CAST(NULL AS int) AS[C1], 
                // CAST(NULL AS varchar(1)) AS[C2], 
                // CAST(NULL AS varchar(1)) AS[C3], 
                // cast(1 as bit) AS[C4]
                // FROM[dbo].[BankAccounts]
                // AS[Extent1]
                // UNION ALL
                // SELECT
                // [Extent2].[BillingDetailId]
                // AS[BillingDetailId], 
                // [Extent2].[Owner]
                // AS[Owner], 
                // [Extent2].[Number]
                // AS[Number], 
                // CAST(NULL AS varchar(1)) AS[C1], 
                // CAST(NULL AS varchar(1)) AS[C2], 
                // [Extent2].[CardType]
                // AS[CardType], 
                // [Extent2].[ExpiryMonth]
                // AS[ExpiryMonth], 
                // [Extent2].[ExpiryYear]
                // AS[ExpiryYear], 
                // cast(0 as bit) AS[C3]
                // FROM[dbo].[CreditCards]
                // AS[Extent2]) AS[UnionAll1]
                // }

                // {
                // SELECT
                // '0X0X' AS[C1], 
                // [Extent1].[BillingDetailId]
                // AS[BillingDetailId], 
                // [Extent1].[Owner]
                // AS[Owner], 
                // [Extent1].[Number]
                // AS[Number], 
                // [Extent1].[BankName]
                // AS[BankName], 
                // [Extent1].[Swift]
                // AS[Swift]
                // FROM[dbo].[BankAccounts]
                // AS[Extent1]
                // }
                #endregion

                #region TPT Problem

                // ObjectStateManager cannot track objects of the same type (i.e.BillingDetail)with 
                // the same EntityKey value hence it throws.
                #endregion
            }

            Console.ReadKey();
        }
    }
}