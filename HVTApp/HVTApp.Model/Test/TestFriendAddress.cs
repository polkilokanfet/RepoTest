using HVTApp.Infrastructure;

namespace HVTApp.Model
{
  public class TestFriendAddress : BaseEntity
    {
    public string City { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
  }
}