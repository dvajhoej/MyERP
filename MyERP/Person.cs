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
