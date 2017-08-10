namespace TablePerType
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public abstract class BillingDetail
    {
        public int BillingDetailId { get; set; }

        public string Number { get; set; }

        public string Owner { get; set; }
    }

    [Table("BankAccounts")]
    public class BankAccount : BillingDetail
    {
        public string BankName { get; set; }

        public string Swift { get; set; }
    }

    [Table("CreditCards")]
    public class CreditCard : BillingDetail
    {
        public int CardType { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }
    }

    public class InheritanceMappingContext : DbContext
    {
        public DbSet<BillingDetail> BillingDetails { get; set; }
    }

    /// <summary>
    /// Table per Type is about representing inheritance relationships as relational foreign key associations. 
    /// Every class/subclass that declares persistent properties—including abstract classes—has its own table. 
    /// </summary>
    class Program
    {
        private static void GenerateTestData(InheritanceMappingContext context)
        {
            var ba = new BankAccount
                         {
                             Owner = "BankAccount",
                             BankName = "BankAccount",
                             Number = "1",
                             Swift = "BankAccount"
                         };

            context.BillingDetails.Add(ba);

            var cc = new CreditCard
                         {
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

                // {
                // SELECT
                // CASE WHEN([UnionAll1].[C3] = 1) THEN '0X0X' ELSE '0X1X' END AS[C1],

                // [UnionAll1].[BillingDetailId]
                // AS[C2], 
                // [Extent3].[Owner]
                // AS[Owner], 
                // [Extent3].[Number]
                // AS[Number], 
                // CASE WHEN([UnionAll1].[C3] = 1) THEN[UnionAll1].[C1]
                // END AS[C3],
                // CASE WHEN([UnionAll1].[C3] = 1) THEN[UnionAll1].[C2]
                // END AS[C4],
                // CASE WHEN([UnionAll1].[C3] = 1) THEN CAST(NULL AS int) ELSE[UnionAll1].[CardType]
                // END AS[C5],
                // CASE WHEN([UnionAll1].[C3] = 1) THEN CAST(NULL AS varchar(1)) ELSE[UnionAll1].[ExpiryMonth]
                // END AS[C6],
                // CASE WHEN([UnionAll1].[C3] = 1) THEN CAST(NULL AS varchar(1)) ELSE[UnionAll1].[ExpiryYear]
                // END AS[C7]
                // FROM(SELECT
                // [Extent1].[BillingDetailId] AS[BillingDetailId],
                // CAST(NULL AS varchar(1)) AS[C1], 
                // CAST(NULL AS varchar(1)) AS[C2], 
                // [Extent1].[CardType]
                // AS[CardType], 
                // [Extent1].[ExpiryMonth]
                // AS[ExpiryMonth], 
                // [Extent1].[ExpiryYear]
                // AS[ExpiryYear], 
                // cast(0 as bit) AS[C3]
                // FROM[dbo].[CreditCards]
                // AS[Extent1]
                // UNION ALL
                // SELECT
                // [Extent2].[BillingDetailId]
                // AS[BillingDetailId], 
                // [Extent2].[BankName]
                // AS[BankName], 
                // [Extent2].[Swift]
                // AS[Swift], 
                // CAST(NULL AS int) AS[C1], 
                // CAST(NULL AS varchar(1)) AS[C2], 
                // CAST(NULL AS varchar(1)) AS[C3], 
                // cast(1 as bit) AS[C4]
                // FROM[dbo].[BankAccounts]
                // AS[Extent2]) AS[UnionAll1]
                // INNER JOIN[dbo].[BillingDetails]
                // AS[Extent3] ON[UnionAll1].[BillingDetailId] = [Extent3].[BillingDetailId]
                // }

                // {
                // SELECT
                // '0X0X' AS[C1], 
                // [Extent1].[BillingDetailId]
                // AS[BillingDetailId], 
                // [Extent2].[Owner]
                // AS[Owner], 
                // [Extent2].[Number]
                // AS[Number], 
                // [Extent1].[BankName]
                // AS[BankName], 
                // [Extent1].[Swift]
                // AS[Swift]
                // FROM[dbo].[BankAccounts]
                // AS[Extent1]
                // INNER JOIN[dbo].[BillingDetails]
                // AS[Extent2] ON[Extent1].[BillingDetailId] = [Extent2].[BillingDetailId]
                // }
                #endregion

                #region TPT Advantages

                // The primary advantage of this strategy is that the SQL schema is normalized.
                // note how CardType in CreditCards table is now a non-nullable column
                #endregion

                #region TPT Problem

                // Even though this mapping strategy is deceptively simple, 
                // the experience shows that performance can be unacceptable for 
                // complex class hierarchies because queries always require a join across many tables.
                #endregion
            }

            Console.ReadKey();
        }
    }
}