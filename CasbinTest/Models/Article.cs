using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CasbinTest.Models
{
	public class Article
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }

		public int OwnerId { get; set; }
		public User Owner { get; set; }
	}
}
