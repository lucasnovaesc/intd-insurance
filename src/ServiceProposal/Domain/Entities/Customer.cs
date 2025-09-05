

using Domain.Exceptions;

namespace Domain.Entities
{
    public class Customer
    {
        private Customer()
        {
            
        }

        public Customer(Guid customerId, string name, string email, string phoneNumber, string address, DateTime dateCreation, DateTime dateModification)
        {
            this.CustomerId = customerId;
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.DateCreation = dateCreation;
            this.DateModification = dateModification;
        }

        private Guid _customerId;
        private string _name;
        private string _email;
        private string _phoneNumber;
        private string _address;
        private const int PHONE_MIN_LENGTH = 10;
        private const int PHONE_MAX_LENGTH = 11;

        public Guid CustomerId
        {
            get => _customerId; set
            {
                if (value.Equals(Guid.Empty))
                {
                    throw new EntityPropertyIncorrect($"The Customer identifier is incorrect: {value}");
                }
                else
                {
                    this._customerId = value;
                }
            }
        }
        public string Name
        {
            get => _name; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EntityPropertyIncorrect($"The Customer name is incorrect: {value}");
                }
                else
                {
                    this._name = value;
                }
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (IsValidEmail(value))
                {
                    this._email = value;
                }
                else
                {
                    throw new EntityPropertyIncorrect($"The Customer Email is incorrect: {value}");
                }
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (IsValidPhoneNumber(value))
                {
                    this._phoneNumber = value;
                }
                else
                {
                    throw new EntityPropertyIncorrect($"The Customer Phone Number is incorrect: {value}");
                }
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EntityPropertyIncorrect($"The Customer Address is incorrect: {value}");
                }
                else
                {
                    this._address = value;
                }
            }
        }

        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }





        private static bool IsValidEmail(string email)
        {
            try
            {
                if (email == null)
                    throw new ArgumentNullException("The Email is invalid");
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                throw new EntityPropertyIncorrect($"The Email is invalid");
            }
        }

        private static bool IsValidPhoneNumber(string phone)
        {
            if (phone == null)
                throw new EntityPropertyIncorrect($"The Phone Number is invalid");

            phone = RemoveNonNumericCharacters(phone);

            if (phone.Length < PHONE_MIN_LENGTH || phone.Length > PHONE_MAX_LENGTH)
                throw new EntityPropertyIncorrect($"The Phone Number is invalid");

            return true;
        }
        private static string RemoveNonNumericCharacters(string input)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            return regex.Replace(input, "");
        }
    }
}
