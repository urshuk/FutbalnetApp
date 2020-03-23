using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FutbalnetApp.Services
{
	public interface IDatabaseStore
	{
		Task PostErrorLog(ErrorLog log);
	}
}
