//using Accounting.DataLayer.Repositories;
//using Accounting.ViewModels.Customers;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Accounting.DataLayer.Services
//{
//	public class CustomerRepository : ICustomerRepository
//	{
//		//Accounting_DBEntities db = new Accounting_DBEntities();
//		private Accounting_DBEntities db;

//		public CustomerRepository(Accounting_DBEntities context)
//		{
//			db = context;
//		}

//		public bool DeleteCustomer(Customers customer)
//		{
//			try
//			{
//				db.Entry(customer).State = EntityState.Deleted;

//				return true;
//			}
//			catch (Exception)
//			{
//				return false;
//			}
//		}

//		public bool DeleteCustomer(int customerID)
//		{
//			try
//			{
//				Customers customer = GetCustomerbyId(customerID);
//				DeleteCustomer(customer);
//				return true;
//			}
//			catch (Exception)
//			{
//				return false;
//			}
//		}

//		public List<Customers> GetAllCustomers()
//		{
//			return db.Customers.ToList();
//		}

//		public Customers GetCustomerbyId(int customerID)
//		{
//			return db.Customers.Find(customerID);
//		}

//		public int GetCustomerIdByName(string name)
//		{
//			return db.Customers.First(x=> x.FullName==name).CustomerID;
//		}

//		public string GetCustomerNameById(int customerId)
//		{
//			return db.Customers.Find(customerId).FullName;
//		}

//		public IEnumerable<Customers> GetCustomersByFilter(string parameter)
//		{
//		return db.Customers.Where(x=>x.FullName.Contains(parameter) || x.Email.Contains(parameter) || x.Mobile.Contains(parameter)   ).ToList()	;
//		}

//		public List<ListCustomerViewModel> GetNameCustomer(string filter = "")
//		{
//            if (filter == "")
//            {
//				return db.Customers.Select(x => new ListCustomerViewModel {
//				CustomerId=x.CustomerID,
//				FullName = x.FullName
				
//				} ).ToList();
//            }
//			return db.Customers.Where(x=> x.FullName.Contains(filter)).Select(x => new ListCustomerViewModel
//			{
//				CustomerId = x.CustomerID,
//				FullName = x.FullName

//			}).ToList();
//        }

//		public bool InsertCustomer(Customers customer)
//		{
//			try
//			{
//				db.Customers.Add(customer);
			
//				return true;
//			}
//			catch (Exception)
//			{
//				return false;
//			}
//		}

//		public bool UpdateCustomer(Customers customer)
//		{
//			//try
//			//{
//				//We can use of Using(){ }
//				//Or
//				var local=db.Set<Customers>()
//					.Local
//					.FirstOrDefault(x => x.CustomerID == customer.CustomerID);
//				if (local != null)
//				{
//					db.Entry(local).State = EntityState.Detached;
//				}

//				db.Entry(customer).State = EntityState.Modified;
		
//				return true;
//			//}
//			//catch (Exception)
//			//{
//			//	return false;
//			//}
//		}
//	}
//}
