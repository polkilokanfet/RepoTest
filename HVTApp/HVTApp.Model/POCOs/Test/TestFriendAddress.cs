using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs.Test
{
  public partial class TestFriendAddress : BaseEntity
    {
    public string City { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
  }
}