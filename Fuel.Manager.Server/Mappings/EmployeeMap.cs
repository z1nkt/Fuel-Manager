using System.ComponentModel.DataAnnotations.Schema;
using FluentNHibernate.Mapping;
using Fuel.Manager.Server.Models;

namespace Fuel.Manager.Server.Mappings
{
    public class EmployeeMap: ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employees");

            Id(x => x.Id).GeneratedBy.Native();

            Map(x => x.Firstname).Column("Firstname").Length(50);
            Map(x => x.Lastname).Column("Lastname").Length(50).Not.Nullable();
            Map(x => x.Username).Column("Username").Length(50).Not.Nullable();
            Map(x => x.Password).Column("Password").Length(60).Not.Not.Nullable();
            Map(x=> x.EmployeeNo).Column("EmployeeNo").Length(10).Not.Nullable();
            Map(x => x.IsAdmin).Column("IsAdmin").Not.Nullable();
            Map(x => x.Version).Column ("Version").Not.Nullable();

            HasManyToMany(x => x.Cars).Table("EmployeeToCarRelations")
                .ParentKeyColumn("EmployeeId")
                .ChildKeyColumn("CarId")
                .LazyLoad()
                .Cascade.SaveUpdate();

        }
        

    }
}
