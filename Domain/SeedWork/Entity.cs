namespace Domain.SeedWork
{
	public class Entity : object
	{
		public Entity() : base()
		{
			Id = System.Guid.NewGuid();

			InsertDateTime = Utility.Now;
		}

		// **********
		[System.ComponentModel.DataAnnotations.Key]

		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resourses.DataDictionary),
			Name = nameof(Resourses.DataDictionary.Id))]

		[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
			(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
		public System.Guid Id { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resourses.DataDictionary),
			Name = nameof(Resourses.DataDictionary.InsertDateTime))]
		public System.DateTime InsertDateTime { get; private set; }
		// **********
	}
}
