using FluentNHibernate.Mapping;
using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Mappings
{
    public class EmployeeToCarRelationMap : ClassMap<EmployeeToCarRelation>
    {
        public EmployeeToCarRelationMap()
        {
            Table("EmployeeToCarRelations");

            Id(x => x.Id).GeneratedBy.Native();

            References(x => x.Employee).Column("EmployeeId").Not.Nullable();
            References(x => x.Car).Column("CarId").Not.Nullable();
        }
    }
}
