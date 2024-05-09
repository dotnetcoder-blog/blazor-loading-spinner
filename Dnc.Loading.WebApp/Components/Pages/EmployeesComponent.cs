using Dnc.Common.Razor.Wrapper;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Dnc.Loading.WebApp.Components.Pages
{
    public class EmployeesComponent : ComponentBase
    {
        [CascadingParameter] protected 
        DncWrapper DncWrapper { get; set; }


        protected List<Employee> Employees = null;
        protected override async Task OnInitializedAsync()
        {
            Employees = await DncWrapper.AwaitTask(GetEmployees());
        }

        private async Task<List<Employee>> GetEmployees()
        {
            await Task.Delay(7000);
            var json = File.ReadAllText(@"./Data/employees.json");
            return JsonSerializer.Deserialize<List<Employee>>(json, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
    }
}
