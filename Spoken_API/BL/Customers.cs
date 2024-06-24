namespace Spoken_API.BL
{
    public class Customers
    {
        string customerName;
        string customerEmail;
        string customerPhone;   
        string customerAddress;


        public Customers()
        { 
        }

        public Customers(string customerName, string customerEmail, string customerPhone, string customerAddress)
        {
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            CustomerAddress = customerAddress;
        }

        public string CustomerName { get => customerName; set => customerName = value; }
        public string CustomerEmail { get => customerEmail; set => customerEmail = value; }
        public string CustomerPhone { get => customerPhone; set => customerPhone = value; }
        public string CustomerAddress { get => customerAddress; set => customerAddress = value; }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertCustomer(this);

        }

    }

}
