using FluentNHibernate.Mapping;
using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Mappings
{
    public class CarMap : ClassMap<Car>
    {
        public CarMap() 
        {
            Table("Cars");
            Id(x => x.Id).GeneratedBy.Native();

            Map(x => x.LicensePlate)
                .Column("LicensePlate").
                Length(50)
                .Not.Nullable();
            Map(x => x.Vendor)
                .Column("Vendor")
                .Length(20)
                .Not.Nullable();
            Map(x => x.Model)
                .Column("Model").Length(20)
                .Not.Nullable();
            Map(x => x.Version)
                .Column("Version")
                .Not.Nullable();


        }
    }
}
