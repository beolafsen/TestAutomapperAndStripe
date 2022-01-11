using System;
using System.Collections.Generic;
using Stripe;
using AutoMapper;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<VMStripeInvoiceItem, Stripe.InvoiceItem>();
                c.CreateMap<Stripe.InvoiceItem, VMStripeInvoiceItem>()
                .ForMember(pts => pts.LineAmount, opt => opt.MapFrom(ps => ps.Quantity * (ps.UnitAmountDecimal ?? (decimal)0) * (decimal)0.01))
                .ForMember(pts => pts.UnitAmountDecimal, opt => opt.MapFrom(ps => (ps.UnitAmountDecimal ?? (decimal)0) * (decimal)0.01));
                c.CreateMap<Test1, Test2>().ReverseMap();
            });
            
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            Console.WriteLine("test");
        }
    }

    public class VMStripeInvoiceItem : InvoiceItem
    {
        public decimal LineAmount { get; set; }
    }
    public class Test1
    {
        public decimal LineAmount { get; set; }
    }
    public class Test2
    {
        public decimal LineAmount { get; set; }
    }
}