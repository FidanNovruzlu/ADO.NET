using EmployeeManagementDB.Models;
using Microsoft.Data.SqlClient;

internal class Program
{
    const string CONNECTION_STRING = "Server=DESKTOP-U2MOD8D\\SQLEXPRESS;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;";

    private static void Main(string[] args)
    {
        //CreateCompanyTable();
        // CreateEmployeeTable();

        // InsertCompanyTable("Fidan");
        //InsertCompanyTable("ABB");

        //InsertEmployeeTable("Fidan","Novruzlu",1);
        //InsertEmployeeTable("Gunay", "Babayeva", 2);
        // InsertEmployeeTable("Namiq", "Kerimli", 1);

        //UpdateCompanyTable("APPLE");
        //UpdateCompanyTable("Samsung");

        //UpdateEmployeesTable("Ferid", "Novruzlu");

        // DeleteCompaniesById(3);
        //DeleteEmployeesById(4);

        //Console.WriteLine(GetCompaniesNameById(1));
        //Console.WriteLine(GetEmployeesNameById(1));
        GetAllEmployee();
        foreach (var employees in GetAllEmployee())
        {
            Console.WriteLine(employees.ToString());
        }
    }
    public static void CreateCompanyTable()
    {
        using(SqlConnection connection=new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "CREATE TABLE Companies(Id INT PRIMARY KEY IDENTITY(1,1),Name NVARCHAR(50) NOT NULL)";
            using(SqlCommand sqlCommand=new SqlCommand(query,connection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Commands completed successfully.");
            }
        };
    }
    public static void CreateEmployeeTable()
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "CREATE TABLE Employees(Id INT PRIMARY KEY IDENTITY(1,1),Name NVARCHAR(50) NOT NULL,Surname NVARCHAR(50),CompanyId INT  FOREIGN KEY REFERENCES Companies(Id))";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Commands completed successfully.");
            }
        };
    }
    public static void InsertCompanyTable(string Name)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "INSERT INTO Companies(Name) VALUES(@Name)";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{result} row effected!");
            }
        };
    }
    public static void InsertEmployeeTable(string Name,string Surname,int CompanyId)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "INSERT INTO Employees(Name,Surname,CompanyId) VALUES(@Name,@Surname,@CompanyId)";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                sqlCommand.Parameters.AddWithValue("@Surname", Surname);
                sqlCommand.Parameters.AddWithValue("@CompanyId", CompanyId);

                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{result} row effected!");
            }
        };
    }
    public static void UpdateCompanyTable(string Name)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "UPDATE Companies SET Name=@Name WHERE Id=2";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{result} row effected!");
            }
        };
    }
    public static void UpdateEmployeesTable(string Name,string Surname)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "UPDATE Employees SET Name=@Name, Surname=@Surname WHERE Id=2";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                sqlCommand.Parameters.AddWithValue("@Surname", Surname);

                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{result} row effected!");
            }
        };
    }
    public static void DeleteCompaniesById(int Id)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "DELETE FROM Companies WHERE Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", Id);
                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{result} row effected!");
            }
        };
    }
    public static void DeleteEmployeesById(int Id)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "DELETE FROM Employees WHERE Id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", Id);
                int result = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{result} row effected!");
            }
        };
    }
    public static string? GetCompaniesNameById(int id)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "SELECT Name from Companies where Id = @Id";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", id);
                string? name = sqlCommand.ExecuteScalar()?.ToString();
                return name;
            }
        };
    }
    public static string? GetEmployeesNameById(int id)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "SELECT Name from Employees where Id = @Id";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", id);
                string? name = sqlCommand.ExecuteScalar()?.ToString();
                return name;
            }
        };
    }
    public static List<Employee> GetAllEmployee()
    {
        using (SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING))
        {
            sqlConnection.Open();
            string query = "SELECT C.Name 'Company',E.Name 'Employee Name' FROM Companies AS C JOIN Employees AS E ON E.CompanyId=C.Id";
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Employee> employees = new List<Employee>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee()
                            {
                                Name = reader["Company"].ToString(),
                                Surname = reader["Employee Name"].ToString()
                            });
                        }
                    }
                    return employees;
                }
            }
        }
    }
}