using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTest
{
	public class User
	{
		private string _identifier;
		private string _firstName;
		private string _lastName;
		private double _payment = 0;

		public User(string _tmpIdentifier, string _tmpFirstName, string _tmpLastName)
		{
			identifier = _tmpIdentifier;
			firstName = _tmpFirstName;
			lastName = _tmpLastName;
		}

		public string identifier { get { return _identifier; } set { _identifier = value; } }
		public string firstName { get { return _firstName; } set { _firstName = value; } }
		public string lastName { get { return _lastName; } set { _lastName = value; } }
		public double payment { get { return _payment; } set { _payment = value; } }

	}
}
