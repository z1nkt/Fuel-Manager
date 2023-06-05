using FluentNHibernate.Mapping;
using Fuel.Manager.Server.Models;
using NHibernate.Mapping;

namespace Fuel.Manager.Server.Mappings
{
    public class RefuelMap : ClassMap<Refuel>
    {
        public RefuelMap()
        {
            Table("Refuels");
            Id(x => x.Id).GeneratedBy.Native();

            Map(x => x.Date).Column("Date").Not.Nullable();
            Map(x => x.Mileage).Column("Mileage").Not.Nullable();
            Map(x => x.Amount).Column("Amount").Precision(16).Scale(2).Not.Nullable();
            Map(x => x.Price).Column("Price").Precision(16).Scale(2).Not.Nullable();
            Map(x => x.Version).Column("Version").Not.Nullable();

            References(x => x.Car).Column("CarId").Not.Nullable().Cascade.All();
        }
        
    }
}
