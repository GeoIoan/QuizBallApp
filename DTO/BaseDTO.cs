

namespace StudentsDBApp.DTO
{

    ///<summary>
    ///A simple class tha just contains an id property.
    ///It is extended by all the other dto classes the need
    ///to have an id property.
    ///</summary>

    public class BaseDTO
	{
        public int Id { get; set; }
    }
}
