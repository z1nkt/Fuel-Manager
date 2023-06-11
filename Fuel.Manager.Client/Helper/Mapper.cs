using Fuel.Manager.Client.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fuel.Manager.Client.Helper
{
    public static class Mapper
    {
        public static Employee JsonToEmployee(string json)
        {
            return JsonConvert.DeserializeObject<Employee>(json);
        }

        public static List<Refuel> JsonToRefuelList(string json)
        {
            return JsonConvert.DeserializeObject<List<Refuel>>(json);
        }

        public static List<Car> JsonToCarList(string json)
        {
            return JsonConvert.DeserializeObject<List<Car>>(json);
        }

        public static List<Employee> JsonToEmployeeList(string json)
        {
            return JsonConvert.DeserializeObject<List<Employee>>(json);
        }

        public static string CarToJson(Car car)
        {
            return JsonConvert.SerializeObject(car);
        }

        public static string EmployeeToJson(Employee employee)
        {
            return JsonConvert.SerializeObject(employee);
        }
    }
}
