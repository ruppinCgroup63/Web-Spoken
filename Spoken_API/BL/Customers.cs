namespace Spoken_API.BL
{
    public class Customers
    {
        int id;
        string customerName;
        string customerEmail;
        string customerPhone;   
        string customerAddress;


        public Customers()
        { 
        }

        public Customers(int id,string customerName, string customerEmail, string customerPhone, string customerAddress)
        {
            Id = id;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            CustomerAddress = customerAddress;
        }

        public int Id { get => id; set => id = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string CustomerEmail { get => customerEmail; set => customerEmail = value; }
        public string CustomerPhone { get => customerPhone; set => customerPhone = value; }
        public string CustomerAddress { get => customerAddress; set => customerAddress = value; }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertCustomer(this);

        }

        public List<Customers> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadCustomer();
        }


    }

}
