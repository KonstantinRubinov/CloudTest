using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTest
{
	class Program
	{
		//Users Dictionary for saving user data by identifier
		private static Dictionary<string, User> users = new Dictionary<string, User>();

		//Payments Dictionary for checking paymnets doubles by identifier
		private static Dictionary<string, Payment> payments;

		//Variable for clearing Payment Dictionary if new two-letters data has come
		private static string filename="";

		//counting all payments
		private static ulong numberOfPayments = 0;

		//counting only double payments
		private static ulong numberOfPaymentsDouble = 0;

		//numberOfPaymentsDouble / numberOfPayments
		private static double paymentsRatio;

		static void ReadUsers()
		{
			//read all files from folder
			foreach (string file in Directory.EnumerateFiles(@"C:\CloudShareCodeChallenge\Challenge", "*.log"))
			{
				//read all lines from file
				foreach (string line in File.ReadLines(file))
				{
					//only if it user
					if (line.Trim().StartsWith("UR"))
					{
						//split row data
						string[] words = line.Split(',');

						//add data to user
						User user = new User(words[1], words[2], words[3]);

						//add user to dictionary by identifier
						users.Add(words[1], user);

						//log on console the user
						Console.WriteLine(words[1] + " " + words[2] + " " + words[3]);
					}
				}
			}

			//check Most/Least common firstname
			FindCommonName();
		}

		
		static void ReadPayments()
		{
			//read all files from folder
			foreach (string file in Directory.EnumerateFiles(@"C:\CloudShareCodeChallenge\Challenge", "*.log"))
			{
				//split for two leetter in filename
				string[] files = file.Split('_');

				//if it new two letterss, refresh payment dictionary
				if (!files[1].Equals(filename))
				{
					payments = new Dictionary<string, Payment>();
					filename = files[1];
				}

				//read all lines from file
				foreach (string line in File.ReadLines(file))
				{
					//only if it payment
					if (line.Trim().StartsWith("PR"))
					{
						//split row data
						string[] words = line.Split(',');

						//if double payment
						if (payments.ContainsKey(words[1]))
						{
							//count double payments
							numberOfPaymentsDouble++;
						}
						else
						{
							//add data to user
							Payment payment = new Payment(words[1], words[2], double.Parse(words[3]));

							//add payment to dictionary by identifier
							payments.Add(words[1], payment);

							//log on console the payment
							Console.WriteLine(words[1] + " " + words[2] + " " + words[3]);

							//add paymeny ammout to user by identifier
							users[words[2]].payment = users[words[2]].payment + double.Parse(words[3]);
						}

						//count payments
						numberOfPayments++;
					}
					

				}

			}
		}


		static void FindPaymentsRatio()
		{
			//PaymentsRatio=numberOfPaymentsDouble * 1000/numberOfPayments (show as double)
			paymentsRatio = (double)(numberOfPaymentsDouble * 1000) / numberOfPayments;
		}


		private static Dictionary<string, int> result;
		static void FindCommonName()
		{
			//order usernames by their counts
			result = (from u in users // define the data source
					  group u by u.Value.firstName into g  // group all items by status
					  orderby g.Count() descending // order by count descending
					  select new { g.Key, Total = g.Count() }) // cast the output
						 .ToDictionary(x => x.Key, x => x.Total); // cast to dictionary
		}


		static void PrintData()
		{
			//sort users by most paying, and get top 10
			var sortedDict = users.OrderByDescending(entry => entry.Value.payment).Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);

			Dictionary<string, User>.ValueCollection values = sortedDict.Values;

			string path = @"C:\CloudShareCodeChallenge\Challenge\output.log";
			if (!File.Exists(path))
			{
				using (StreamWriter sw = File.CreateText(path))
				{
					//write to output Payments Ratio
					sw.WriteLine("Payments Ratio = " + (float)System.Math.Round(paymentsRatio, 3) + "(" + numberOfPaymentsDouble + " / " + numberOfPayments + ")");

					//get max username count
					int max = result.First().Value;

					//get min username count
					int min = result.Last().Value;

					//write to output all users with max name count and exit loop
					for (int index = 0; index < result.Count; index++)
					{
						var item = result.ElementAt(index);

						if (item.Value == max)
						{
							sw.WriteLine("Most common name(s): " + item.Key + ": " + item.Value);
						}else
						{
							break;
						}
					}

					//write to output all users with min name count and exit loop
					for (int index = result.Count-1; index >=0; index--)
					{
						var item = result.ElementAt(index);

						if (item.Value == min)
						{
							
							sw.WriteLine("Least common name(s): " + item.Key + ": " + item.Value);
						}
						else
						{
							break;
						}
					}

					//write to output top paying users
					foreach (User user in values)
					{
						sw.WriteLine(user.firstName + " " + user.lastName + " : " + user.payment);
					}
				}
			}
		}

		static void Main(string[] args)
		{
			ReadUsers();
			ReadPayments();
			FindPaymentsRatio();
			PrintData();
		}
	}
}
