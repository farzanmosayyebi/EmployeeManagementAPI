﻿namespace EmployeeManagement.Common.DTOs.Employee;

public record EmployeeUpdate(int Id, string FirstName, string LastName, int AddressId, int JobId);
