﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Exceptions
{
	public class InternalServerErrorException : Exception
	{
		public InternalServerErrorException(string message) : 
			base(message) { }
	}
}
