using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQAssignment
{
	public class Program
	{
		IList<Employee> employeeList;
		IList<Salary> salaryList;

		public Program()
		{
			employeeList = new List<Employee>() {
			new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
			new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
			new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
			new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
			new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
			new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
			new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
		};

			salaryList = new List<Salary>() {
			new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
		};
		}

		public static void Main()
		{
			Program program = new Program();
            Console.WriteLine("Task 1 =>\n");
			program.Task1();
            Console.WriteLine();
			Console.WriteLine("Task 2 =>\n");
			program.Task2();
            Console.WriteLine();
			Console.WriteLine("Task 3 => \n");
			program.Task3();
			
		}

		public void Task1()
		{
			var sortedEmployeeBySalary = employeeList.GroupJoin(salaryList,
												e => e.EmployeeID, 
												s => s.EmployeeID,
												(e, salaries) => new
													   {
														   employeeKey = e,
														   salary = salaries.Sum(y => y.Amount)
													   }).OrderBy(z => z.salary);

			foreach(var group in sortedEmployeeBySalary)
            {
				Console.WriteLine($"Name = " +
					$"{group.employeeKey.EmployeeFirstName} " +
					$"{group.employeeKey.EmployeeLastName}, " +
					$"Salary = {group.salary}");
            }
		}

		public void Task2()
		{
			var secondOldestEmployee = employeeList.OrderByDescending(e => e.Age)
													.Skip(1)
													.FirstOrDefault();
			if (secondOldestEmployee != null)
			{
				var secondOldestEmployeeSalary = salaryList.Where(s => s.EmployeeID == secondOldestEmployee.EmployeeID && s.Type == 0)
															.FirstOrDefault();
				Console.WriteLine($"Name = {secondOldestEmployee.EmployeeFirstName} {secondOldestEmployee.EmployeeLastName}" +
					$"Monthly Salary = {secondOldestEmployeeSalary.Amount}");
			}
		
		}

		public void Task3()
		{
			var employeesGroup = employeeList.Where(s=>s.Age>30).GroupJoin(salaryList,
												e => e.EmployeeID, 
												s => s.EmployeeID,
												(e, salaryGroup) => new
													   {
														   employeeKey = e,
														   AvgSalary = salaryGroup.Average(y => y.Amount)
													   });
			foreach (var group in employeesGroup)
			{
				Console.WriteLine($"Name = " +
					$"{group.employeeKey.EmployeeFirstName} " +
					$"{group.employeeKey.EmployeeLastName}, Salary = {group.AvgSalary:N3}");
			}
		}
	}
}
