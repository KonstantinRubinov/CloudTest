using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTest
{
	public class Payment
	{
		private string _identifier;
		private string _userIdentifier;
		private double _amount;

		public Payment(string _tmpIdentifier, string _tmpuserIdentifier, double _tmpAmount)
		{
			identifier = _tmpIdentifier;
			userIdentifier = _tmpuserIdentifier;
			amount = _tmpAmount;
		}

		public string identifier { get { return _identifier; } set { _identifier = value; } }
		public string userIdentifier { get { return _userIdentifier; } set { _userIdentifier = value; } }
		public double amount { get { return _amount; } set { _amount = value; } }

	}
}
