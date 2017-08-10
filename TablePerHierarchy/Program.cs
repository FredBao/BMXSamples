namespace TablePerHierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public abstract class BillingDetail
    {
        public int BillingDetailId { get; set; }

        public string Discriminator { get; set; }

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

        // protected override void OnModelCreating(DbModelBuilder modelBuilder)
        // {
        // modelBuilder.Entity<BillingDetail>()
        // .Map<BankAccount>(m => m.Requires("BillingDetailType").HasValue("BA"))
        // .Map<CreditCard>(m => m.Requires("BillingDetailType").HasValue("CC"));
        // }
    }

    /// <summary>
    /// A simple strategy for mapping classes to database tables 
    /// might be “one table for every entity persistent class.” 
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
                // [Extent1].[Discriminator] AS[Discriminator], 
                // [Extent1].[BillingDetailId]
                // AS[BillingDetailId], 
                // [Extent1].[Owner]
                // AS[Owner], 
                // [Extent1].[Number]
                // AS[Number], 
                // [Extent1].[BankName]
                // AS[BankName], 
                // [Extent1].[Swift]
                // AS[Swift], 
                // [Extent1].[CardType]
                // AS[CardType], 
                // [Extent1].[ExpiryMonth]
                // AS[ExpiryMonth], 
                // [Extent1].[ExpiryYear]
                // AS[ExpiryYear]
                // FROM[dbo].[BillingDetails]
                // AS[Extent1]
                // WHERE[Extent1].[Discriminator] IN(N'BankAccount', N'CreditCard')}

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
                // FROM[dbo].[BillingDetails]
                // AS[Extent1]
                // WHERE[Extent1].[Discriminator] = N'BankAccount'}
                #endregion

                #region TPH Problem

                // TPH Requires Properties in SubClasses to be Nullable in the Database
                // For exmaple, see 'CardType' property.
                #endregion
            }

            Console.ReadKey();
        }
    }
}