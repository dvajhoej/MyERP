using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string FullName
        {
            get => $"{FirstName} {LastName}";
            set
            {
                var nameParts = value?.Split(' ');
                if (nameParts != null && nameParts.Length > 1)
                {
                    FirstName = nameParts[0];
                    LastName = string.Join(" ", nameParts.Skip(1)); 
                }
                else if (nameParts?.Length == 1)
                {
        
                    FirstName = nameParts[0];
                    LastName = string.Empty;
                }
            }
        }

        public Person()
        {
            
        }
        public Person(string firstName, string lastName, Address address, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"{FullName}, {Address}, Email: {Email}, Phone: {Phone}";
        }
    }
}
