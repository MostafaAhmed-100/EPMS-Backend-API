using System;
using System.Collections.Generic;
using System.Text;

namespace EPMS.Domain.Entitys
{
    public class User : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsActive { get; private set; }
        public User(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("first Name is required.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("last Name is required.", nameof(lastName));
            FirstName = firstName;
            LastName = lastName;
            IsActive = true;
        }
        public void DeactivateAccount ()
        {
            if (!IsActive) throw new InvalidOperationException("the Account is not active");
            IsActive = false;
        }
        public void ActivateAccount(User user)
        {
            if (IsActive) throw new InvalidOperationException("the Account is active");
            IsActive = true;
        }

    }
}