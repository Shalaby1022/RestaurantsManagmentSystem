﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Exceptions
{
	public class KeyNotFoundEception : Exception

	{ 
		public KeyNotFoundEception(string resourceType, string resourceIdentifier)
			: base($"{resourceType} with id: {resourceIdentifier} isn't found") { }


	}
}
